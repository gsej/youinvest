import csv
import os
import datetime
import json
from typing import NamedTuple

# processes a set of cash statement csv files, and outputs a cash statement.json file which contains a json array
# of the events. 

class CashStatementItem(NamedTuple):
    date: str
    description: str
    receipt_amount_gbp: float
    payment_amount_gbp: float

    # def __init__(self, date, description, receipt_amount_gbp, payment_amount_gbp):
    #     self.date = date
    #     self.description = description
    #     self.receipt_amount_gdp = receipt_amount_gbp
    #     self.payment_amount_gdp = payment_amount_gbp

def get_cashstatement_rows(path):
    cashstatement_rows = []

    files = os.listdir(path)

    cashstatement_files = list(
        filter(lambda filename: "cashstatement" in filename, files))
    cashstatement_files.sort()

    print(cashstatement_files)

    for file in cashstatement_files:
        with open(path + "/" + file) as csvDataFile:
            csvReader = csv.DictReader(csvDataFile)

            for row in csvReader:
                cashstatement_rows.append(row)
    return cashstatement_rows


def get_cashstatement_items(cashstatement_rows):

    def get_date(element):
        return element.date

    cashstatement_items = []
    
    for row in cashstatement_rows:

        dateString = row['\ufeff"Date"']
        date = datetime.date(int(dateString[6:10]), int(dateString[3: 5]), int(dateString[0: 2])).isoformat()
        description = row["Description"]
        receipt_amount_gbp = float(row["Receipt (GBP)"])
        payment_amount_gbp = float(row["Payment (GBP)"])
        cashstatement_item = CashStatementItem(date, description, receipt_amount_gbp, payment_amount_gbp)
        cashstatement_items.append(cashstatement_item)
        cashstatement_items.sort(key=get_date)
    
    for item in cashstatement_items:
        print(item)
    
    return cashstatement_items


class Event:
     def __init__(self, eventId, cashstatement_item):
         self.eventId = eventId
         self.cashstatement_item = cashstatement_item

def write_events(cashstatement_items, output_file):
    
    def asdict(element):
        return element._asdict()
    
    cashstatement_items_dict = list(map(asdict, cashstatement_items))

    json_string = json.dumps(cashstatement_items_dict, indent=4)

    with open(output_file, 'w') as json_file:
        json_file.write(json_string)


def main():
    csv_file_path = "../youinvest-csv-files/gsej-sipp"
    cashstatement_rows = get_cashstatement_rows(csv_file_path)
    cashstatement_items = get_cashstatement_items(cashstatement_rows)

    output_file_name = "../youinvest-csv-files/gsej-sipp/cashstatement_items.json"
    write_events(cashstatement_items, output_file_name)

if __name__ == "__main__":
    main()
