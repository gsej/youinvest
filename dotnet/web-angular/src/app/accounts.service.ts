import { Injectable } from '@angular/core';
import { Account } from './account';
import { AccountSummary } from "./AccountSummary";
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class AccountsService {

  constructor(private http: HttpClient) { }

  getAccounts(): Observable<Account[]> {
    return this.http.get<any>('http://localhost:5100/accounts').pipe(map((result: any) => result.accounts));
  }

  getAccountSummary(accountCodes: string[]): Observable<AccountSummary> {
    return this.http.post<AccountSummary>('http://localhost:5100/account/summary', { accountCodes: accountCodes, date: '2024-01-01' })
  }
}
