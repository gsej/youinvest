import { Component, Input } from '@angular/core';
import { AccountHistory } from '../models/accountHistory';

@Component({
  selector: 'app-account-history',
  templateUrl: './account-history.component.html',
  styleUrls: ['./account-history.component.scss']
})
export class AccountHistoryComponent {

  public displayedColumns = ['date', 'accountCode', 'totalValue'];

  @Input()
  public accountHistory : AccountHistory | null = null;
}
