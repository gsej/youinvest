Python programs to parse the cash statements and transaction history files downloaded from youinvest.


Cash statements are csv files containing all changes to the cash balance
Transaction history files are csv files containing all purchases or sales of securities


## Instructions

### Transaction History

Run `parse_transaction_files.py` to parse the transaction history csv files from youinvest and produce a json file containing the transactions - `transactions.json`.

Run `process_transactions.py` to read the json file containing the transactions and print a list of holdings at a particular point in time (currently processes all transactions).

### Cash Statements

Run `parse_cashstatement_files.py` to parse the csv cash statement files from youinvest and produce a json file containing
