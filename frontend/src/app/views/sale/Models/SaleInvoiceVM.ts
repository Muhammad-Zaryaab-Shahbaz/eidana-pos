import { BaseDomainVM } from "../../catalog/Models/BaseDomainVM"

export class SaleInvoiceVM extends BaseDomainVM {

    cell?: string ='';
    customerName?: string ='';
    invoiceNo?: string ='';
    date:Date = new Date()
    siLines: SaleInvoiceDetailVM[] = []// [new SaleInvoiceDetailVM()]; // Array();
    totalAmount: number;
}
export class SaleInvoiceDetailVM extends BaseDomainVM {    
    saleInvoiceId: number =0;
    itemCodeId: number=0;
    barcodeId: number ;
    discount: number =0;
    discAmount: number;
    lineTotal: number=0;
    quantity: number=1 ;
    dBoperation: number;
    retailPriceId: number =0;
    barcode: string = '';
    itemCode:string = '';
    reduce: any
    retailPrice: number = 0;
    
}