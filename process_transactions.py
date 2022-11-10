from typing import NamedTuple
from typing import List, Dict
import json

import configuration

# processes a transactions.json file (at the momement handling all of the events in the file, but later will
# be able to stop at a particular date). 
# creates an object showing the accumulated state of the account

# not sure if the named tuple is needed.....
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
  #  holdings: Dict[str, Holding] = {}

    def __init__(self, date, previous_account_state = None):
        self.holdings: Dict[str, Holding] = {}
        self.date = date

        if (previous_account_state != None):
            for holding in previous_account_state.holdings.values():
                self.holdings[holding.security] = Holding(holding.security, holding.quantity)

    def add_transaction(self, transaction):
        holding = self.holdings.get(transaction["description"])

        if (holding is None):
            self.holdings[transaction["description"]]  = Holding(transaction["description"], 0)        
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
            raise Exception("transaction type "+ transaction["transaction"] + " is not handled")

    def __str__(self):
        state = ""
        
        sorted_securities = sorted(self.holdings.keys())
        
        for security in sorted_securities:
            if self.holdings[security].quantity != 0:
                state = state + str(self.holdings[security]) + "\n"
        return state 

def read_transactions(file_name):
    with open(file_name, 'r') as json_file:
        transactions = json.load(json_file)
    return transactions

def calculate_current_account_state(transactions: List):
    # todo: remove this
    account_state = AccountState("date")
    for transaction in transactions:
        account_state.add_transaction(transaction)
    
    return account_state

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
        transactions = read_transactions(configuration.dataDirectory + "/transactions.json")

# todo: for each date in the list of transactions, calculate account state and add that state
# to the output.

        

#        first_date = transactions[0].date

        account_state = calculate_current_account_state(transactions)

        if (configuration.output == 'text'):
            print(account_state)
        else:
            non_zero_holdings = filter(lambda holding: holding.quantity > 70, account_state.holdings.values())
            jsonText = json.dumps([dict(holding) for holding in non_zero_holdings], indent=2)
            print (jsonText)
        
if __name__ == "__main__":
    main()
