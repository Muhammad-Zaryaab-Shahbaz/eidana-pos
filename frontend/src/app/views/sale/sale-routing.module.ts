import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ManageSaleInvoiceComponent } from './SaleInvoice/manage-sale-invoice/manage-sale-invoice.component';
import { ListSaleInvoiceComponent } from './SaleInvoice/list-sale-invoice/list-sale-invoice.component';
import { ManageItemsComponent } from './Items/manage-items/manage-items.component';
// import { ManageLoginComponent } from './Login/manage-login/manage-login.component';
import { BarcodeGeneratorComponent } from './barcode-generator/barcode-generator.component';
import { SaleInvoicePrintComponent } from './sale-invoice-print/sale-invoice-print.component';
//import { PosSaleInvoiceComponent } from './pos-sale-invoice/pos-sale-invoice.component';

const routes: Routes = [
  
  {
    path: "Invoice",
    component: ManageSaleInvoiceComponent,
    pathMatch: "full"
  },
  {
    path: "ListSaleInvoice",
    component: ListSaleInvoiceComponent,
    pathMatch: "full"
  },
  {
    path: "Items",
    component: ManageItemsComponent,
    pathMatch: "full"
  },
  /* {
    path: "Login",
    component: ManageLoginComponent,
    pathMatch: "full"
  }, */

  {
    path: "BarcodeGenerator",
    component: BarcodeGeneratorComponent,
    pathMatch: "full"
  },
  {
    path: "PrintReceipt",
    component: SaleInvoicePrintComponent,
    pathMatch: "full"
  },
  // {
  //   path: "possale",
  //   component: PosSaleInvoiceComponent,
  //   pathMatch: "full"
  // }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SaleRoutingModule { }