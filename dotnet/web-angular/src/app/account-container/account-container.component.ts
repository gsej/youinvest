import { Component, OnInit } from '@angular/core';
import { Account } from '../models/account';
import { AccountsService } from '../accounts.service';
import { AccountSummary } from '../models/accountSummary';
import { AccountHistory } from '../models/accountHistory';

@Component({
  selector: 'app-account-container',
  templateUrl: './account-container.component.html',
  styleUrls: ['./account-container.component.scss']
})
export class AccountContainerComponent implements OnInit {


  public accounts: Account[] = []
  public accountSummary: AccountSummary | null = null;
  public accountHistory: AccountHistory | null = null;

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
        this.accountSummary.holdings.push({ stockSymbol: "£", stockDescription: "Cash Balance", quantity: this.accountSummary.cashBalance })
      });

    if (accountCodes.length === 1) {
      this.accountsService.getAccountHistory(accountCodes[0])
        .subscribe(history => {
          this.accountHistory = history;
        });
    }
    else {
      this.accountHistory = null;
    }

  }

}
