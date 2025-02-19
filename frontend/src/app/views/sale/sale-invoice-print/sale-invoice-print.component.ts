import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-sale-invoice-print',
  templateUrl: './sale-invoice-print.component.html',
  styleUrls: ['./sale-invoice-print.component.css']
})
export class SaleInvoicePrintComponent {
  @Input() saleInvoice: any;

  printReceipt() {
    setTimeout(() => {
      window.print();
    }, 500);
  }
}
