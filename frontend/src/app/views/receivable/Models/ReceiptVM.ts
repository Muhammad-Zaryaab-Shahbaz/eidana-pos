
import { BaseDomainVM } from "../../catalog/Models/BaseDomainVM"

export class ReceiptVM extends BaseDomainVM {
    
    customerId?: number
    date:Date
    number?: number
    amount?: DoubleRange
    comments?:string
    nextPayDate?:Date
    customer?: string
    fromDate?: Date
   toDate?: Date
}

