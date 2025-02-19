import { Component, ViewChild, OnInit, OnDestroy, Injector, HostListener } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { CatalogService } from '../../../catalog/catalog.service';
import Swal from 'sweetalert2';
import { Subscription } from 'rxjs';
import { SaleService } from '../../sale.service';
import { ItemsVM } from '../../Models/ItemsVM';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-manage-items',
  templateUrl: './manage-items.component.html',
  styleUrls: ['./manage-items.component.css']
})
export class ManageItemsComponent implements OnInit, OnDestroy {
  @ViewChild('POSForm', { static: true }) POSForm!: NgForm;

  displayedColumns: string[] = [
    'barcode','itemCode', 'quantity', 'brandName', 'size',  'costPrice', 
    'retailPrice', 'wholeSalePrice', 'description','isActive', 'actions'
  ];
  dataSource = new MatTableDataSource<ItemsVM>();

  selectedItem = new ItemsVM();
  isProcessing: boolean = false;
  Add: boolean = true;
  Edit: boolean = false;

  barcodeValue: string = '';

  private subscriptions: Subscription = new Subscription();
  dialogRefe: any;
  dialogData: any;

  constructor(
    private serSvc: SaleService,
    private catSvc: CatalogService,
    private injector: Injector,
  ) {  
    this.dialogRefe = this.injector.get(MatDialogRef, null);
    this.dialogData = this.injector.get(MAT_DIALOG_DATA, null);
  }

  ngOnInit(): void {
    this.refreshForm();
    this.GetItems();
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  @HostListener('window:keydown', ['$event'])
  onBarcodeScanned(event: KeyboardEvent) {
    if (event.key === 'Enter') {
      if (this.barcodeValue.length > 0) {
        this.selectedItem.barcode = parseInt(this.barcodeValue, 10) || 0; // ✅ `int` میں تبدیل کریں
        this.barcodeValue = ''; // ✅ اسکین کے بعد ری سیٹ کریں
      }
      event.preventDefault(); // ✅ فارم سبمٹ ہونے سے روکے
    } 
    else if (event.key >= '0' && event.key <= '9') { 
      this.barcodeValue += event.key; // ✅ صرف نمبر شامل کریں
    }
  }
  
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  GetItems(): void {
    this.serSvc.GetItems().subscribe({
      next: (services: ItemsVM[]) => {
        this.dataSource.data = services;
      },
      error: () => {
        this.catSvc.ErrorMsgBar('Error occurred while retrieving services', 5000);
      }
    });
  }

  saveItems(): void {
    if (!this.POSForm.valid) return;

    this.isProcessing = true;
    this.subscriptions.add(
      this.serSvc.SaveItems(this.selectedItem).subscribe({
        next: () => {
          this.catSvc.SuccessMsgBar('Item successfully added.', 5000);
          this.GetItems();
          this.refreshForm();
        },
        error: () => {
          this.catSvc.ErrorMsgBar('Error occurred while saving the item.', 5000);
        },
        complete: () => {
          this.isProcessing = false;
        }
      })
    );
  }

  GetItemsForEdit(id: number): void {
    this.subscriptions.add(
      this.serSvc.SearchItems({ id } as ItemsVM).subscribe({
        next: (items: ItemsVM[]) => {
          if (items && items.length) {
            this.selectedItem = items[0];
            this.Add = false;
            this.Edit = true;
          } else {
            this.catSvc.ErrorMsgBar('Item not found.', 5000);
          }
        },
        error: () => {
          this.catSvc.ErrorMsgBar('Error occurred while fetching the item details.', 5000);
        }
      })
    );
  }

  updateItems(): void {
    if (!this.POSForm.valid) return;

    this.isProcessing = true;
    this.subscriptions.add(
      this.serSvc.UpdateItems(this.selectedItem).subscribe({
        next: () => {
          this.catSvc.SuccessMsgBar('Item successfully updated.', 5000);
          this.GetItems();
          this.refreshForm();
        },
        error: () => {
          this.catSvc.ErrorMsgBar('Error occurred while updating the item.', 5000);
        },
        complete: () => {
          this.isProcessing = false;
        }
      })
    );
  }

  DeleteItems(id: number): void {
    Swal.fire({
      title: 'Are you sure?',
      text: 'You will not be able to recover this service!',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
      if (result.isConfirmed) {
        this.serSvc.DeleteItems(id).subscribe({
          next: () => {
            this.catSvc.SuccessMsgBar('Deleted Successfully', 5000);
            this.GetItems();
          },
          error: () => {
            this.catSvc.ErrorMsgBar('Error occurred while deleting the service', 5000);
          }
        });
      }
    });
  }

  refreshForm(): void {
    this.Add = true;
    this.Edit = false;
    this.isProcessing = false;
    this.selectedItem = new ItemsVM();
    if (this.POSForm) {
      this.POSForm.resetForm();
    }
  }
}
