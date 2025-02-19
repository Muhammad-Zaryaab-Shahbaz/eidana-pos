import { BaseDomainVM } from "../../catalog/Models/BaseDomainVM"

export class ItemsVM  extends BaseDomainVM{
    itemCode: number;
    description: string;
    barcode: number;
    brandName: string;
    size: string;
    quantity: number;
    costPrice: number;
    wholeSalePrice: number;
    retailPrice?: number;
 
  }
