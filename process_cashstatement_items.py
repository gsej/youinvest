from typing import List
import json

from decimal import Decimal

# processes a cashstatement_items.json file (at the momement handling all of the events in the file, but later will
# be able to stop at a particular date). 
# creates an object showing the accumulated state of the account

class AccountState():

    balance = Decimal(0)

    def add_cashstatement_item(self, item):
        receipt_amount = Decimal(item["receipt_amount_gbp"])
        payment_amount = Decimal(item["payment_amount_gbp"])

        if (receipt_amount != 0 and payment_amount != 0):
            raise Exception("ERROR: receipt_amount is " + str(receipt_amount) + "  and payment_amount is " + str(payment_amount)) 

        self.balance = self.balance + receipt_amount + payment_amount

    def __str__(self):
        return "balance" + "\t" + str(self.balance)

def read_cashstatement_items(file_name):
    with open(file_name, 'r') as json_file:
        items = json.load(json_file)
    return items

def calculate_balance(items: List):
    account_state = AccountState()

    for item in items:
        if (item["description"] != "* BALANCE B/F *"):
                account_state.add_cashstatement_item(item)
    
    return account_state

def main():
    if __name__ == "__main__":
        items = read_cashstatement_items("../youinvest-csv-files/gsej-sipp/cashstatement_items.json")
        account_state = calculate_balance(items)
        print(account_state)
        
if __name__ == "__main__":
    main()