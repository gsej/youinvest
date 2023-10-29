import { Component, OnInit } from '@angular/core';
import { Account } from '../account';
import { AccountsService } from '../accounts.service';
import { AccountSummary } from '../AccountSummary';

@Component({
  selector: 'app-account-container',
  templateUrl: './account-container.component.html',
  styleUrls: ['./account-container.component.scss']
})
export class AccountContainerComponent implements OnInit {


  public accounts: Account[] = []
  public accountSummary: AccountSummary | null = null;

  constructor(private accountsService: AccountsService) {
  }


  ngOnInit(): void {
    this.accountsService.getAccounts().subscribe(accounts => {
      this.accounts = accounts;
    })
  }

  accountsSelected(accountCodes: string[]) {
    this.accountsService.getAccountSummary(accountCodes)
      .subscribe(summary => {
        this.accountSummary = summary

        this.accountSummary.holdings.push( { stockSymbol: "£", stockDescription: "Cash Balance", quantity: this.accountSummary.cashBalance})
      });
  }

}
