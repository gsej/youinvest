from typing import NamedTuple
from typing import List, Dict
import json
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
        return self.security + "\t" + str(self.quantity)

class AccountState():
    holdings: Dict[str, Holding]

    def __init__(self):
        self.holdings = {} # Is this needed?
    
    def add_transaction(self, transaction):
        holding = self.holdings.get(transaction["description"])

        if (holding is None):
            self.holdings[transaction["description"]]  = Holding(transaction["description"], transaction["quantity"])
        else:
            holding.quantity += transaction["quantity"]

    def __str__(self):
        state = ""
        for holding in self.holdings.values():
            state = state + str(holding) + "\n"
        return state 

def read_transactions(file_name):
    with open(file_name, 'r') as json_file:
        transactions = json.load(json_file)
    return transactions

def calculate_account_state(transactions: List):
    account_state = AccountState()
    for transaction in transactions:
        account_state.add_transaction(transaction)

    print(account_state)

def main():
    if __name__ == "__main__":
        transactions = read_transactions("../youinvest-csv-files/gsej-isa/transactions.json")
        account_state = calculate_account_state(transactions)
        print(account_state)
        
if __name__ == "__main__":
    main()