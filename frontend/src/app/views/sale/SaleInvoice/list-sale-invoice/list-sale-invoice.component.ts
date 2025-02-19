import { ChangeDetectorRef, Component, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import Swal from 'sweetalert2';
import { MatSort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { MatCheckboxChange } from '@angular/material/checkbox';
import{SaleInvoiceVM , SaleInvoiceDetailVM} from '../../Models/SaleInvoiceVM'
//import { CatalogService } from 'src/app/views/catalog/CatalogService';
//import { SettingsVM } from 'src/app/views/catalog/Models/SettingsVM';
//import { AppConstants } from 'src/app/app.constants/AppConstants';
import { SaleService } from '../../sale.service';
import { CatalogService } from '../../../catalog/catalog.service';

@Component({
  selector: 'app-list-sale-invoice',
  templateUrl: './list-sale-invoice.component.html',
  styleUrls: ['./list-sale-invoice.component.css'],


  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class ListSaleInvoiceComponent {
  Service: SaleInvoiceVM[] = [];
  isLoading: boolean;
  selectedRowIndex = -1;
  selectedRow: SaleInvoiceVM
  Edit: boolean = true;
  isPosted: boolean = false
  innerDisplayedColumns = ['barcode','itemCode',  'quantity', 'retailPrice','discount','discAmount', 'lineTotal',];
  dataSource: any;
 columnsToDisplay = ['expand','invoiceNo', 'customerName', 'cell', 'date','Action'];
  columnsToDisplayWithExpand = [...this.columnsToDisplay];
  expandedElement: any;
  @ViewChild('outerSort', { static: true }) sort: MatSort | undefined;
  @ViewChildren('innerSort') innerSort: QueryList<MatSort> | undefined;
  @ViewChildren('innerTables') innerTables: QueryList<MatTable<SaleInvoiceDetailVM>> | undefined;
  selectedSaleInvoice = new SaleInvoiceVM();
  constructor(
    private route: Router,
    public dialog: MatDialog,
   public serSvc: SaleService,
   // private storeSvc: StorageService,
    public catSvc: CatalogService,
    private cdr: ChangeDetectorRef,
  ) {
    this.selectedRow = new SaleInvoiceVM;
  }
  ngOnInit(): void {
    this.GetInvoice();
  }
  highlight(row: SaleInvoiceVM) {
    debugger
    this.selectedRow = new SaleInvoiceVM
    this.selectedRowIndex = row.id;
    this.selectedRow = row
  }
  GetInvoice() {
    const ser = new SaleInvoiceVM();
    this.serSvc.SearchSaleinvoice(ser).subscribe({
      next: (res: SaleInvoiceVM[]) => {
        this.Service = res;
        this.dataSource = new MatTableDataSource(this.Service);  // Assign modified array to dataSource
      },
      error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred!", 4000);
        console.warn(e);
      }
    });
  }
  openPdf(si: SaleInvoiceVM) {
    this.isLoading = true
    this.serSvc.GetInvPdf(si).subscribe({
      next: (response) => {
        console.warn(response)
        const blob = new Blob([response.body], { type: 'application/pdf' });
        const url = URL.createObjectURL(blob);
        window.open(url);
        this.isLoading = false
      }, error: (err) => {
        this.isLoading = false
        this.catSvc.ErrorMsgBar("Error Occurred", 4000)
        console.warn(err)
      }
    }
    )
  }
  EditInvoice(ser: SaleInvoiceVM): void {
    this.route.navigate(['/sale/SaleInvoice'], {
      queryParams: {
        id: ser.id,
        mode: 'edit' // Pass the mode as 'edit'
      }
    });
  }
  DeleteInvoice(id: number) {
    debugger
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
      if (result.value) {
        this.serSvc.DeleteSaleinvoice(id).subscribe({
          next: (data: any) => {
            Swal.fire(
              'Deleted!',
              'Successfully Deleted.',
              'success'
            )
            this.GetInvoice();
          }, error: (e) => {
            this.catSvc.ErrorMsgBar("Error Occurred!", 4000)
            console.warn(e);
          }
        })
      }
    })
  }
  Refresh() {
    this.selectedRowIndex = -1
    this.selectedRow = new SaleInvoiceVM

  }



// --- NEW FUNCTIONS FOR PRINTING ---

  /**
   * Opens a popup window with the hidden print content.
   */
  printReceipt(): void {
    setTimeout(() => {
      const printContents = document.getElementById('receipt-content')?.innerHTML;
      if (printContents) {
        const popupWin = window.open('', '_blank', 'width=800,height=600');
        if (popupWin) {
          popupWin.document.open();
          popupWin.document.write(`
            <html>
              <head>
                <title>Print Invoice</title>
                <style>
                  /* Add print styles here if needed */
                </style>
              </head>
              <body onload="window.print(); window.close();">
                ${printContents}
              </body>
            </html>
          `);
          popupWin.document.close();
        } else {
          console.error("Popup blocked or failed to open.");
        }
      } else {
        console.error("Receipt content not found.");
      }
    }, 500);
  }

  /**
   * Fetches full invoice data by its ID and then calls printReceipt() to print.
   * @param row The invoice row from the table.
   */
  printInvoice(row: SaleInvoiceVM): void {
    this.serSvc.getSaleInvoiceById(row.id).subscribe({
      next: (data) => {
        if (data) {
          this.selectedSaleInvoice = data;
          // Give time for the selectedSaleInvoice to render in the hidden print container
          setTimeout(() => {
            this.printReceipt();
          }, 500);
        } else {
          console.error("Invoice not found");
        }
      },
      error: (error) => {
        console.error("Error fetching invoice:", error);
      }
    });
  }
}
