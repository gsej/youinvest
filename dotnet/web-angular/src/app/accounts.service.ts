import { Injectable } from '@angular/core';
import { Account } from './account';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class AccountsService {

  constructor(private http: HttpClient) { }

  getAccounts(): Observable<Account[]> {
    return this.http.get<any>('https://localhost:7048/accounts').pipe(map( (result:any) => result.accounts));
  }
}
