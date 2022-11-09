from typing import NamedTuple
from typing import List, Dict
import json

import configuration

# processes a transactions.json file listing all distinct stocks mentioned

class Transaction(NamedTuple):
    date: str
    transaction: str
    description: str
    quantity: int    
    amount_gbp: float
    reference: str

def read_transactions(file_name):
    with open(file_name, 'r') as json_file:
        transactions = json.load(json_file)
    return transactions

def get_stocks(transactions: List):

    stocks = []

    for transaction in transactions:
        stocks.append(transaction["description"])

    unique_stocks = list(set(stocks))
    unique_stocks.sort()

    return unique_stocks


def main():
    if __name__ == "__main__":
        transactions = read_transactions(configuration.dataDirectory + "/transactions.json")
        stocks = get_stocks(transactions)
        
        for stock in stocks:
            print(stock)
        
if __name__ == "__main__":
    main()
