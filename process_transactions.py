from typing import NamedTuple
from typing import List, Dict
import json

import configuration

# processes a transactions.json file 
# outputs a list of objects showing the holdings in the accounts
# at each date on which a transaction falls

class Transaction(NamedTuple):
    date: str
    transaction: str
    description: str
    quantity: int
    amount_gbp: float
    reference: str


class Holding:
    security: str
    quantity: int

    def __init__(self, security, quantity):
        self.security = security
        self.quantity = quantity

    def __str__(self):
        justified = self.security.ljust(configuration.width)
        return justified + str(self.quantity)

    def __iter__(self):
        yield ('security', self.security)
        yield ('quantity', self.quantity)


class AccountState():

    def __init__(self, date, previous_account_state=None):
        self.holdings: Dict[str, Holding] = {}
        self.date = date

        if (previous_account_state != None):
            for holding in previous_account_state.holdings.values():
                self.holdings[holding.security] = Holding(
                    holding.security, holding.quantity)

    def add_transaction(self, transaction):
        holding = self.holdings.get(transaction["description"])

        if (holding is None):
            self.holdings[transaction["description"]] = Holding(
                transaction["description"], 0)
            holding = self.holdings.get(transaction["description"])

        if transaction["transaction"] == "Transfer In":
            holding.quantity += transaction["quantity"]
        elif transaction["transaction"] == "Purchase":
            holding.quantity += transaction["quantity"]
        elif transaction["transaction"] == "Sale":
            holding.quantity -= transaction["quantity"]
        elif transaction["transaction"] == "Receipt of stock following a corporate action":
            holding.quantity += transaction["quantity"]
        elif transaction["transaction"] == "Removal of stock following a corporate action":
            holding.quantity -= transaction["quantity"]
        elif transaction["transaction"] == "Receipt of stock following a consolidation":
            holding.quantity += transaction["quantity"]
        elif transaction["transaction"] == "Removal of stock following a consolidation":
            holding.quantity -= transaction["quantity"]
        else:
            raise Exception("transaction type " +
                            transaction["transaction"] + " is not handled")

        if (holding.quantity == 0):
            del self.holdings[holding.security]

    def __str__(self):
        state = ""

        state += self.date + "\n"
        state += "----------" + "\n"

        sorted_securities = sorted(self.holdings.keys())

        for security in sorted_securities:
            if self.holdings[security].quantity != 0:
                state = state + str(self.holdings[security]) + "\n"
        state += "\n"
        return state

class AccountStatesJSONEncoder(json.JSONEncoder):
    def default(self, o: List[AccountState]):
        if isinstance(o, AccountState):
           holdings = list(o.holdings.values())
           holdings.sort(key=lambda h: h.security)

           hhh = list(map(lambda h: dict(h), holdings))

           #sorted_holdings = hh.sort(key=lambda h: h["security"])
           return {"date": o.date, "holdings": list(map(lambda h: dict(h), hhh))}
        return json.JSONEncoder.default(self, o)

def read_transactions(file_name):
    with open(file_name, 'r') as json_file:
        transactions = json.load(json_file)
    return transactions

def calculate_account_states(transactions: List):
    date = None
    account_states = []

    account_state = None

    for transaction in transactions:
        if (transaction["date"] != date):
            date = transaction["date"]
            if (account_state == None):
                account_state = AccountState(date)
            else:
                account_state = AccountState(date, account_state)
            account_states.append(account_state)
        account_state.add_transaction(transaction)

    return account_states

def main():
    if __name__ == "__main__":
        transactions = read_transactions(
            configuration.dataDirectory + "/transactions.json")

        account_states = calculate_account_states(transactions)

        if (configuration.output == 'text'):
            for account_state in account_states:
                print(account_state)
        else:
            jsonText = json.dumps( account_states, indent=2, cls=AccountStatesJSONEncoder)
            print(jsonText)


if __name__ == "__main__":
    main()