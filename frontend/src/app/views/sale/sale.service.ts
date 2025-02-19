import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { Globals } from '../../globals';
import { SaleInvoiceVM } from './Models/SaleInvoiceVM';
import { ItemsVM } from './Models/ItemsVM';
import { UserAccountVM } from './Models/UserAccountVM';
import { LoginVM } from './Models/LoginVM';

@Injectable({
  providedIn: 'root'
})
export class SaleService {

  constructor(private http: HttpClient,) { }

  getSaleInvoiceById(invoiceId: number) {
    return this.http.get<SaleInvoiceVM>(`/api/saleinvoice/${invoiceId}`);
  }
  

  GetSaleinvoice(): Observable<SaleInvoiceVM[]> {
    debugger;
    return this.http.get<SaleInvoiceVM[]>(Globals.BASE_API_URL + 'saleinvoice').pipe();
  }
  GetSaleinvoiceId(id: number): Observable<SaleInvoiceVM[]> {
    debugger;
    return this.http.get<SaleInvoiceVM[]>(Globals.BASE_API_URL + 'saleinvoice' + id).pipe()
  }
  SaveSaleinvoice(value: SaleInvoiceVM): Observable<any> {
    debugger;
    return this.http.post(Globals.BASE_API_URL + 'saleinvoice', value);
  }
  UpdateSaleinvoice(value: SaleInvoiceVM): Observable<any> {
    debugger;
    return this.http.put(Globals.BASE_API_URL + 'saleinvoice', value);
  }
  DeleteSaleinvoice(id: number) {
    return this.http.delete(Globals.BASE_API_URL + 'saleinvoice/' + id);
  }
  SearchSaleinvoice(value: SaleInvoiceVM): Observable<SaleInvoiceVM[]> {
    return this.http.post<SaleInvoiceVM[]>(Globals.BASE_API_URL + 'saleinvoice/Search', value).pipe();
  }
  GetNextInvoNo(si: SaleInvoiceVM): Observable<any> {
    return this.http.post<any>(Globals.BASE_API_URL + "saleinvoice/next-InvoNo-number", si).pipe()
  }
  GetInvPdf(inv: SaleInvoiceVM): Observable<any> {
    console.warn(inv)
    return this.http.post<any>(Globals.BASE_API_URL + "ServiceRpt/SaleInvoice", inv, {
      responseType: 'blob' as 'json',
      observe: 'response'
    });
  }

  GetItems(): Observable<ItemsVM[]> {
    return this.http.get<ItemsVM[]>(Globals.BASE_API_URL + 'Items').pipe();
  }
  GetItemsId(id: number): Observable<ItemsVM[]> {
    return this.http.get<ItemsVM[]>(Globals.BASE_API_URL + 'Items/' + id).pipe();
  }
  SaveItems(value: ItemsVM) {
    return this.http.post(Globals.BASE_API_URL + 'Items', value);
  }
  UpdateItems(value: ItemsVM) {
    return this.http.put(Globals.BASE_API_URL + 'Items', value);
  }
  DeleteItems(id: number) {
    return this.http.delete(Globals.BASE_API_URL + 'Items/' + id);
  }
  SearchItems(value: ItemsVM): Observable<ItemsVM[]> {
    return this.http.post<ItemsVM[]>(Globals.BASE_API_URL + 'Items/Search', value).pipe();
  }

  GetUserAccount(): Observable<UserAccountVM[]> {
    return this.http.get<UserAccountVM[]>(Globals.BASE_API_URL + 'UserAccount').pipe();
  }
  GetUserAccountById(id: number): Observable<UserAccountVM> {
    return this.http.get<UserAccountVM>(Globals.BASE_API_URL + 'UserAccount/' + id).pipe()
  }
  SaveUserAccount(value: UserAccountVM): Observable<any> {
    return this.http.post(Globals.BASE_API_URL + 'UserAccount', value);
  }
  UpdateUserAccount(value: UserAccountVM) {
    return this.http.put(Globals.BASE_API_URL + 'UserAccount', value);
  }
  DeleteUserAccount(id: number) {
    return this.http.delete(Globals.BASE_API_URL + 'UserAccount/' + id);
  }
  SearchUserAccount(value: UserAccountVM): Observable<UserAccountVM[]> {
    return this.http.post<UserAccountVM[]>(Globals.BASE_API_URL + 'UserAccount/Search', value).pipe();
  }
  Login(User: LoginVM) {
      return this.http.post(Globals.BASE_API_URL + 'Account', User).pipe();
    }
}
