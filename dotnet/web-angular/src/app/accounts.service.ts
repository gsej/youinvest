import { Injectable } from '@angular/core';
import { Account } from './models/account';
import { AccountSummary } from "./models/accountSummary";
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { AccountHistory } from './models/accountHistory';


@Injectable({
  providedIn: 'root'
})
export class AccountsService {

  _today: string;
  constructor(private http: HttpClient) {
    this._today = new Date().toISOString().substring(0, 10);
  }

  getAccounts(): Observable<Account[]> {
    return this.http.get<any>('http://localhost:5100/accounts').pipe(map((result: any) => result.accounts));
  }

  getAccountSummary(accountCodes: string[]): Observable<AccountSummary> {
    return this.http.post<AccountSummary>('http://localhost:5100/account/summary', { accountCodes: accountCodes, date: this._today })
  }

  getAccountHistory(accountCode: string):  Observable<AccountHistory> {
    return this.http.post<AccountHistory>('http://localhost:5100/account/history', { accountCode: accountCode })
  }
}
