import { Component, Input } from '@angular/core';
import { AccountSummary } from '../AccountSummary';

@Component({
  selector: 'app-account-summary',
  templateUrl: './account-summary.component.html',
  styleUrls: ['./account-summary.component.scss']
})
export class AccountSummaryComponent {


  @Input()
  public accountSummary: AccountSummary | null = null;
}
