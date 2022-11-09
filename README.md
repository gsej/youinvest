Python programs to parse the cash statements and transaction history files downloaded from youinvest.


Cash statements are csv files containing all changes to the cash balance
Transaction history files are csv files containing all purchases or sales of securities


## Instructions

### Parsing CSV

`parse_transaction_files.py` and `parse_cashstatement_files.py` take the CSV files from youinvest and emit JSON files (`transactions.json` and `cashstatement_items.json`) containing the relevant data in order.

### Create account state for a particular time

`process_transactions.py` reads the `transactions.json` file and outputs the securities held in an account at a particular time (currently it reads them all so produces the latest state).

`process_cashstatement_items.py` does the same, producing the cash balance.  
