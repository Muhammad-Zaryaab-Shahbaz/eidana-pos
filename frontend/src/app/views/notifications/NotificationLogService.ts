import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from 'rxjs';
import { NotificationLogVM } from "./NotificationLogVM";
import { Globals } from "src/app/globals";

@Injectable({
  providedIn: 'root'
})
export class NotificationLogService {
  constructor(private http: HttpClient) { }
  notification: NotificationLogVM

  getNextNotificationLog() {
    return this.http.get<NotificationLogVM[]>(Globals.BASE_API_URL + 'NotificationLog/').pipe();
  }
  updateNotificationLog(nLog: NotificationLogVM) {
    return this.http.put(Globals.BASE_API_URL + 'NotificationLog', nLog);
  }
 
  SearchNotificationLog(nLog): Observable<NotificationLogVM[]> {
    return this.http.post<NotificationLogVM[]>(Globals.BASE_API_URL + 'NotificationLog/Search', nLog).pipe();
  }
 
  SaveNotificationLog(nLog): Observable<NotificationLogVM> {
    return this.http.post<NotificationLogVM>(Globals.BASE_API_URL + 'NotificationLog', nLog).pipe();
  }
 
  deleteNotificationLog(id) {
    return this.http.delete(Globals.BASE_API_URL + 'NotificationLog/' + id);
  }
 
}
