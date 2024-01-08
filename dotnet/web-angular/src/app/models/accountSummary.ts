import { Holding } from "./holding";


// TODO: why do these all have to be labelled nullable
export class StockPriceResult {
  price!: number;
  currency!: string;
  ageInDays!: number;
}

export class AccountSummary {
  holdings!: Holding[];
  cashBalance!: number;
  stockPrice!: StockPriceResult;
}


