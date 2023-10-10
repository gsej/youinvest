import { Component, OnInit } from '@angular/core';
import { Account } from '../account';
import { AccountsService } from '../accounts.service';

@Component({
  selector: 'app-account-selector',
  templateUrl: './account-selector.component.html',
  styleUrls: ['./account-selector.component.scss']
})
export class AccountSelectorComponent implements OnInit{


  public accounts: Account[] = [
    { accountCode: "ACC1" },
    { accountCode: "ACC2" }
  ]

  constructor(private accountsService: AccountsService) {

  }


  ngOnInit(): void {

    this.accountsService.getAccounts().subscribe( accounts => {
      this.accounts = accounts;
    })
  }

}
