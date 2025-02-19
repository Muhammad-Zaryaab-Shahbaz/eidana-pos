
import { BaseDomainVM } from "../../catalog/Models/BaseDomainVM"

export class CustomerVM extends BaseDomainVM {
    
    paymentTermId?: number
    name?: string
    email?: string
    phone?: string
    address?: string
    creditLimit?: number
    paymentTerm?: string
 

}