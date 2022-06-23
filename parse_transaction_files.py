import csv
import os
import datetime
import json
from typing import NamedTuple

# processes a set of transaction csv files, and outputs a transactions.json file which contains a json array
# of the transactions. 

class Transaction(NamedTuple):
    date: str
    transaction: str
    description: str
    quantity: int    
    amount_gbp: float
    reference: str

def get_transaction_rows(path):
    transaction_rows = []

    files = os.listdir(path)

    transaction_files = list(
        filter(lambda filename: "transactionhistory" in filename, files))
    transaction_files.sort()

    print(transaction_files)

    for file in transaction_files:
        with open(path + "/" + file) as csvDataFile:
            csvReader = csv.DictReader(csvDataFile)

            for row in csvReader:
                transaction_rows.append(row)
    return transaction_rows


def get_transactions(transaction_rows):

    def get_date(element):
        return element.date

    transactions = []
    
    for row in transaction_rows:

           dateString = row['\ufeff"Date"']
           date = datetime.date(int(dateString[6:10]), int(dateString[3: 5]), int(dateString[0: 2])).isoformat()
           transaction = row["Transaction"]
           description = row["Description"]
           quantity = int(row["Quantity"])
           amount_gbp = float(row["Amount (GBP)"])
           reference = row["Reference"]
           transaction = Transaction(date, transaction, description, quantity, amount_gbp, reference)
           transactions.append(transaction)
           transactions.sort(key=get_date)
    
    for transaction in transactions:
        print(transaction)
    
    return transactions


class Event:
     def __init__(self, eventId, transaction):
         self.eventId = eventId
         self.transaction = transaction

def write_events(transactions, output_file):
    
    def asdict(element):
        return element._asdict()
    
    transactionDicts = list(map(asdict, transactions))

    json_string = json.dumps(transactionDicts, indent=4)

    with open(output_file, 'w') as json_file:
        json_file.write(json_string)


def main():
    csv_file_path = "../youinvest-csv-files/gsej-sipp"
    transaction_rows = get_transaction_rows(csv_file_path)
    transactions = get_transactions(transaction_rows)

    output_file_name = "../youinvest-csv-files/gsej-sipp/transactions.json"
    write_events(transactions, output_file_name)



if __name__ == "__main__":
    main()
