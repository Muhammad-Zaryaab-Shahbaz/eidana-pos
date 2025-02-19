
import { BaseDomainVM } from "../../catalog/Models/BaseDomainVM"

export class InvoiceVM extends BaseDomainVM {
    
    customerId?: number
    invDate?: Date
    invNo? : string
    invAmount?: DoubleRange
    comments?: string
   customer?: string
   fromDate?: Date
   toDate?: Date
  filter: any
}