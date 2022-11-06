from typing import NamedTuple
from typing import List, Dict
import json
from unittest.util import sorted_list_difference
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

class Holding():
    security: str
    quantity: int
    
    def __init__(self, security, quantity):
        self.security = security
        self.quantity = quantity

    def __str__(self):
        justified = self.security.ljust(50)
        return justified + str(self.quantity)

class AccountState():
    holdings: Dict[str, Holding]

    def __init__(self):
        self.holdings = {} # Is this needed?
    
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

def calculate_account_state(transactions: List):
    account_state = AccountState()
    for transaction in transactions:
        account_state.add_transaction(transaction)
    
    return account_state

def main():
    if __name__ == "__main__":
        transactions = read_transactions("../youinvest-csv-files/gsej-sipp/transactions.json")
        account_state = calculate_account_state(transactions)
        print(account_state)
        
if __name__ == "__main__":
    main()
