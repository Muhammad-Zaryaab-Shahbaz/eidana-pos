import {
  ChangeDetectorRef,
  Component,
  ElementRef,
  ViewChild,
} from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { SaleInvoiceDetailVM, SaleInvoiceVM } from '../../Models/SaleInvoiceVM';
import Swal from 'sweetalert2';
import { ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { SaleService } from '../../sale.service';
import { NgForm } from '@angular/forms';
import { CatalogService } from '../../../catalog/catalog.service';
import { ItemsVM } from '../../Models/ItemsVM';
import { ManageItemsComponent } from '../../Items/manage-items/manage-items.component';
import { Location } from '@angular/common';
import { Router, Params } from '@angular/router';

@Component({
  selector: 'app-manage-sale-invoice',
  templateUrl: './manage-sale-invoice.component.html',
  styleUrls: ['./manage-sale-invoice.component.css'],
})
export class ManageSaleInvoiceComponent {
  lineAddMode = false;
  lineUpdateMode = false;
  proccessing = false;
  Edit = false;
  Add = true;

  lastEditedField: string = '';
  public date = new Date();
  Items: ItemsVM[] = [];
  SIList: SaleInvoiceDetailVM[] = [];
  selectedSaleInvoice = new SaleInvoiceVM();
  SI?: SaleInvoiceVM[];
  @ViewChild('userForm', { static: true }) userForm!: NgForm;
  @ViewChild('SaleInvoiceDetailForm') SaleInvoiceDetailForm!: NgForm;
  SaleInvoiceDetail: SaleInvoiceDetailVM = new SaleInvoiceDetailVM();
  displayedColumns: string[] = [
    'barcode',
    'itemCode',
    'quantity',
    'retailPrice',
    'discount',
    'discAmount',
    'lineTotal',
    'actions',
  ];
  searchText: string = '';
  exemptFromTax: boolean = true;
  dataSource = new MatTableDataSource<SaleInvoiceDetailVM>([]);
  SIId: number;
  LineTotals: number;
  isLoading: boolean;
  isReadonly: boolean = true;
  minDate: Date = new Date();
  isPrint: boolean;
  @ViewChild('qtyInput', { static: false }) qtyInput: ElementRef;
  barCode: any;
  SearchValue?: any;
  totalAmount: number;
  isSaving: boolean = false; // Flag to prevent multiple saves
  isFormValid: boolean = true;
  //selectedSaleInvoice: any;

  constructor(
    public serSvc: SaleService,
    public catSvc: CatalogService,
    private location: Location,
    private route: ActivatedRoute,
    public dialog: MatDialog,
    private cdr: ChangeDetectorRef,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.Refresh();
    this.route.queryParams.subscribe((params) => {
      this.SIId = params['id'];
    });

    if (this.SIId > 0) {
      this.Edit = true;
      this.Add = false;
      this.calculateTotalAmount();
      this.GetInvoiceForEdit();
    } else {
      this.Add = true;
      this.Edit = false;
      this.selectedSaleInvoice.isActive = true;
      this.SaleInvoiceDetail.isActive = true;
      this.GetNextInvoNo(this.selectedSaleInvoice);
    }
    this.GetItems();
    this.selectedSaleInvoice.isActive = true;
    this.SaleInvoiceDetail.isActive = true;
  }

  setQueryParams() {
    const qParams: Params = {};
    this.router.navigate([], {
      relativeTo: this.activatedRoute,
      queryParams: qParams,
      queryParamsHandling: '',
    });
  }

  GetNextInvoNo(val: SaleInvoiceVM) {
    this.serSvc.GetNextInvoNo(val).subscribe({
      next: (res: SaleInvoiceVM) => {
        this.selectedSaleInvoice.invoiceNo = res.invoiceNo;
      },
      error: (e) => {
        console.error('Error getting next Invoice number:', e);
        this.catSvc.ErrorMsgBar(
          'Error occurred while getting Invoice number',
          5000
        );
      },
    });
  }

  AddLinetoList(): void {
    if (this.isSaving) return; // Prevent multiple saves
    this.isSaving = true;

    //console.log("📌 Current SaleInvoiceDetail:", this.SaleInvoiceDetail);

    // ✅ Validate required fields
    if (
      !this.SaleInvoiceDetail.itemCodeId ||
      this.SaleInvoiceDetail.itemCodeId === 0 ||
      !this.SaleInvoiceDetail.barcodeId ||
      this.SaleInvoiceDetail.barcodeId === 0 ||
      !this.SaleInvoiceDetail.quantity ||
      this.SaleInvoiceDetail.quantity <= 0 ||
      !this.SaleInvoiceDetail.retailPrice ||
      this.SaleInvoiceDetail.retailPrice <= 0
    ) {
      this.catSvc.ErrorMsgBar('❌ Please fill out all fields properly', 5000);
      this.isSaving = false; // Re-enable the button
      return;
    }

    if (!this.selectedSaleInvoice.siLines) {
      this.selectedSaleInvoice.siLines = [];
    }

    if (this.lineUpdateMode) {
      const index = this.selectedSaleInvoice.siLines.findIndex(
        (x) => x.id === this.SaleInvoiceDetail.id
      );
      if (index !== -1) {
        this.selectedSaleInvoice.siLines[index] = { ...this.SaleInvoiceDetail };
      }
    } else {
      this.selectedSaleInvoice.siLines.push({ ...this.SaleInvoiceDetail });
    }

    this.SIList = [...this.selectedSaleInvoice.siLines];
    this.dataSource = new MatTableDataSource(this.SIList);

    // console.log("✅ Updated SIList:", this.SIList);

    this.RefreshLine();
    this.calculateTotalAmount();
    this.lineUpdateMode = false;

    // ✅ Clear the SaleInvoiceDetail object to avoid duplicate entries

    this.SaleInvoiceDetail = new SaleInvoiceDetailVM();
    this.SaleInvoiceDetail.quantity = 1; // Default quantity
    this.SaleInvoiceDetail.retailPrice = 1; // Default cost price
    this.SaleInvoiceDetail.discount = 0; // Default discount
    this.SaleInvoiceDetail.lineTotal = 0; // Default line total
    this.isSaving = false; // Re-enable the button
  }

  RefreshLine(): void {
    console.log('🔄 Refreshing Line...');
    this.SaleInvoiceDetail = new SaleInvoiceDetailVM();
    this.SaleInvoiceDetail.quantity = 1;
    this.SaleInvoiceDetail.retailPrice = 1;
    this.SaleInvoiceDetail.discount = 0;
    this.SaleInvoiceDetail.lineTotal = 0;
    this.lineUpdateMode = false;
    this.cdr.detectChanges();
  }

  GetItems(): void {
    const item = new ItemsVM();
    this.serSvc.SearchItems(item).subscribe({
      next: (res: ItemsVM[]) => {
        this.Items = res;
      },
      error: (e) => {
        this.catSvc.ErrorMsgBar('Error Occurred', 5000);
        console.warn(e);
      },
    });
  }

  GetInvoiceForEdit() {
    const Si = new SaleInvoiceVM();
    Si.id = this.SIId;
    this.serSvc.SearchSaleinvoice(Si).subscribe({
      next: (res: SaleInvoiceVM[]) => {
        if (!res || res.length === 0) {
          this.catSvc.ErrorMsgBar('No invoice found ', 5000);
          return;
        }
        this.SI = res;
        this.selectedSaleInvoice = this.SI[0];
        this.SIList = Array.isArray(this.selectedSaleInvoice.siLines)
          ? [...this.selectedSaleInvoice.siLines]
          : [];
        this.calculateTotalAmount();
        this.dataSource = new MatTableDataSource(this.SIList);
      },
      error: (e) => {
        this.catSvc.ErrorMsgBar('Error Occurred', 5000);
        console.error('Error in GetInvoiceForEdit:', e);
      },
    });
  }

  Refresh(): void {
    this.SaleInvoiceDetail = new SaleInvoiceDetailVM();
    this.selectedSaleInvoice = new SaleInvoiceVM();
    this.selectedSaleInvoice.isActive = true;
    this.SaleInvoiceDetail.isActive = true;
    this.Add = true;
    this.Edit = false;
    this.SIList = [];
    this.lineUpdateMode = false;
    this.dataSource = new MatTableDataSource(this.SIList);
    this.calculateTotalAmount();
    this.GetNextInvoNo(this.selectedSaleInvoice);
    this.totalAmount = 0;
  }

  delete(val: SaleInvoiceDetailVM): void {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#d',
      cancelButtonColor: '#d',
      confirmButtonText: 'Yes, delete it!',
    }).then((result) => {
      if (result.value) {
        if (val.id > 0) {
          const grn = this.selectedSaleInvoice;
          grn.siLines = [];
          val.dBoperation = 3;
          grn.siLines.push(val);

          this.serSvc.UpdateSaleinvoice(grn).subscribe({
            next: () => {
              this.DeleteLine(val);
            },
            error: () => {
              this.catSvc.ErrorMsgBar('Error Occurred', 5000);
            },
          });
        } else {
          this.DeleteLine(val);
        }
      }
    });
  }

  DeleteLine(val: SaleInvoiceDetailVM): void {
    Swal.fire('Deleted!', 'Successfully Deleted.', 'success');
    this.SIList = this.SIList.filter((x) => x !== val);
    this.selectedSaleInvoice.siLines = this.selectedSaleInvoice.siLines.filter(
      (x) => x !== val
    );
    this.dataSource = new MatTableDataSource(this.SIList);
    this.calculateTotalAmount();
  }

  edit(val: SaleInvoiceDetailVM): void {
    console.log('Editing row:', val);
    this.lineUpdateMode = true;
    this.SaleInvoiceDetail = { ...val };
    this.calculateTotalAmount();
    setTimeout(() => {
      if (this.qtyInput) {
        this.qtyInput.nativeElement.focus();
      }
    }, 0);
  }

  Submit(isPrint: boolean = false) {
    this.calculateAmount();
    if (!this.userForm.invalid) {
      if (this.SIList.length > 0) {
        this.proccessing = true;
        this.selectedSaleInvoice.siLines = this.SIList;

        this.selectedSaleInvoice.siLines.forEach((element) => {
          element.dBoperation = element.id > 0 ? 2 : 1;
          element.isActive = true;
        });
        console.log('Mapped Invoice Before Save:', this.selectedSaleInvoice);

        const saveOrUpdate = this.Edit
          ? this.serSvc.UpdateSaleinvoice(this.selectedSaleInvoice)
          : this.serSvc.SaveSaleinvoice(this.selectedSaleInvoice);
        this.Refresh();

        saveOrUpdate.subscribe({
          next: (res: SaleInvoiceVM) => {
            this.selectedSaleInvoice = res;
            this.SIList = res.siLines;
            this.dataSource = new MatTableDataSource(this.SIList);
            //this.Refresh();
            if (this.isPrint) {
              this.printReceipt();
              this.location.replaceState('/sale/SaleInvoice');

              setTimeout(() => {
                this.Refresh();
              }, 1000);
            } else {
              this.Refresh();
            }

            this.calculateTotalAmount();
            this.GetNextInvoNo(this.selectedSaleInvoice);
            this.proccessing = false;

            if (!res.hasErrors) {
              //if (this.isPrint) this.openPdf(res);
            }
          },
          error: (e) => {
            console.warn('Save/Update Error:', e);
            this.catSvc.ErrorMsgBar('Error Occurred', 5000);
            this.proccessing = false;
          },
        });
      } else {
        this.catSvc.ErrorMsgBar('Please add some details to continue...', 5000);
      }
    } else {
      //alert('i m here');
      this.catSvc.ErrorMsgBar('Please fill all required fields!', 5000);
      this.isFormValid = false;
    }
  }

  printReceipt() {
    setTimeout(() => {
      const printContents =
        document.getElementById('receipt-content')?.innerHTML;
      const originalContents = document.body.innerHTML;

      if (printContents) {
        document.body.innerHTML = printContents;
        window.print();
        document.body.innerHTML = originalContents;
        location.reload(); // Reload to restore the original page
      } else {
        console.error('Receipt content not found.');
      }
    }, 500);
  }

  onSaveAndPrint() {
    this.isPrint = true;
    this.Submit(); // Save invoice first
    if (this.isFormValid) {
      setTimeout(() => {
        if (this.selectedSaleInvoice) {
          this.printReceipt();
          this.location.replaceState('/sale/SaleInvoice');
        } else {
          console.error('❌ Error: Invoice not loaded before print.');
        }
      }, 2000); // Wait longer to ensure data is available before printing
    }
  }

  onPrintInvoice(invoiceId: number) {
    this.serSvc.getSaleInvoiceById(invoiceId).subscribe((data) => {
      if (data) {
        this.selectedSaleInvoice = data;
        setTimeout(() => {
          window.print(); // ✅ Print after data is loaded
        }, 500);
      } else {
        console.error('Invoice not found');
      }
    });
  }

  calculateAmount() {
    const quantity = this.SaleInvoiceDetail.quantity || 0;
    const retailPrice = this.SaleInvoiceDetail.retailPrice || 0;
    let discount = this.SaleInvoiceDetail.discount || 0;
    let discAmount = this.SaleInvoiceDetail.discAmount || 0;
    let grossAmount = quantity * retailPrice; // Full price before discount

    if (this.lastEditedField === 'discount') {
      // User changed Discount %, so update Discount Amount
      discAmount = (discount / 100) * grossAmount;
      this.SaleInvoiceDetail.discAmount = Number(discAmount.toFixed(2));
    } else if (this.lastEditedField === 'discAmount') {
      // User changed Discount Amount, so update Discount %
      discount = (discAmount / grossAmount) * 100;
      this.SaleInvoiceDetail.discount = Number(discount.toFixed(2));
    }

    // Calculate Line Total after discount
    this.SaleInvoiceDetail.lineTotal = grossAmount - discAmount;

    // Prevent negative total
    if (this.SaleInvoiceDetail.lineTotal < 0) {
      this.SaleInvoiceDetail.lineTotal = 0;
    }
  }

  calculateTotalAmount() {
    this.totalAmount = this.dataSource.data.reduce(
      (acc, line) => acc + (line.lineTotal || 0),
      0
    );
    this.calculateAmount();
    this.totalAmount = this.SIList.reduce(
      (acc, line) => acc + (line.lineTotal || 0),
      0
    );
    this.selectedSaleInvoice.totalAmount = this.totalAmount;
  }

  onBarcodeScanned(scannedBarcode: string): void {
    // If there's a valid barcode, try to find it in the itemsTable.
    if (scannedBarcode) {
      const selectedItem = this.Items.find(
        (item) => item.barcode.toString() === scannedBarcode
      );

      if (selectedItem) {
        // Update SaleInvoiceDetail properties with the found item's details.
        this.SaleInvoiceDetail.barcodeId = selectedItem.id;
        this.SaleInvoiceDetail.barcode = selectedItem.barcode.toString(); // string, for display

        this.SaleInvoiceDetail.itemCodeId = selectedItem.id;
        this.SaleInvoiceDetail.itemCode = selectedItem.itemCode.toString(); // string, for display

        this.SaleInvoiceDetail.retailPriceId = selectedItem.id;
        this.SaleInvoiceDetail.retailPrice = selectedItem.retailPrice ?? 0;

        // Recalculate total amount if needed
        this.calculateTotalAmount();
      } else {
        console.error(`Item not found for barcode: ${scannedBarcode}`);
        // Optionally reset the item fields or show a message to the user.
        this.SaleInvoiceDetail.itemCode = '';
        this.SaleInvoiceDetail.retailPrice = 0;
      }
    }
  }

  OpenItemsDialog() {
    let dialogRef = this.dialog.open(ManageItemsComponent, {
      disableClose: true,
      panelClass: 'calendar-form-dialog',
      width: '75%',
      height: '80%',
      data: {
        isDialog: true,
      },
    });
    dialogRef.afterClosed().subscribe({
      next: (res) => {
        this.GetItems();
      },
    });
  }

  Back() {
    this.location.back();
  }
}
