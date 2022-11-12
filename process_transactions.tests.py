import json
import unittest

from process_transactions import calculate_account_states

transactions = [
    {
        "date": "2012-11-19",
        "transaction": "Purchase",
        "description": "Vanguard FTSE All-World UCITS ETF GBP",
        "quantity": 73,
        "amount_gbp": 2442.22,
        "reference": "44612P43356"
    }
]


class ProcessTransactionsTests(unittest.TestCase):

    def test_empty_account(self):
        transactions = []
        account_states = calculate_account_states(transactions)
        self.assertEqual(0, len(account_states))

    def test_single_transaction(self):
        transactions = [
            {
                "date": "2012-11-19",
                "transaction": "Purchase",
                "description": "Vanguard FTSE All-World UCITS ETF GBP",
                "quantity": 73,
                "amount_gbp": 2442.22,
                "reference": "44612P43356"
            }
        ]

        account_states = calculate_account_states(transactions)
        self.assertEqual(1, len(account_states))

        account_state = account_states[0]
        self.assertEqual(account_state.date, "2012-11-19")
        self.assertEqual(len(account_state.holdings), 1, "should be one holding")

    def test_two_transaction_same_date(self):
        transactions = [
            {
                "date": "2012-11-19",
                "transaction": "Purchase",
                "description": "Vanguard FTSE All-World UCITS ETF GBP",
                "quantity": 10,
                "amount_gbp": 300.00,
                "reference": "44612P43356"
            },
              {
                "date": "2012-11-19",
                "transaction": "Purchase",
                "description": "Vanguard FTSE All-World UCITS ETF GBP",
                "quantity": 15,
                "amount_gbp": 450.00,
                "reference": "44612P43356"
            }
        ]

        account_states = calculate_account_states(transactions)
        self.assertEqual(1, len(account_states))

    def test_two_transaction_different_dates(self):
        transactions = [
            {
                "date": "2012-11-19",
                "transaction": "Purchase",
                "description": "Vanguard FTSE All-World UCITS ETF GBP",
                "quantity": 10,
                "amount_gbp": 300.00,
                "reference": "44612P43356"
            },
              {
                "date": "2013-11-19",
                "transaction": "Purchase",
                "description": "Vanguard FTSE All-World UCITS ETF GBP",
                "quantity": 15,
                "amount_gbp": 450.00,
                "reference": "44612P43356"
            }
        ]

        account_states = calculate_account_states(transactions)
        self.assertEqual(2, len(account_states))

        account_state = account_states[0]
        self.assertEqual(account_state.date, "2012-11-19")
        self.assertEqual(len(account_state.holdings), 1, "should be one holding")

        holding = list(account_state.holdings.values())[0]
        self.assertEqual(holding.quantity, 10)

        account_state = account_states[1]
        self.assertEqual(account_state.date, "2013-11-19")
        self.assertEqual(len(account_state.holdings), 1, "should be one holding")
        holding = list(account_state.holdings.values())[0]
        self.assertEqual(holding.quantity, 25)

    def test_two_transaction_purchase_then_sale(self):
        transactions = [
            {
                "date": "2012-11-19",
                "transaction": "Purchase",
                "description": "Vanguard FTSE All-World UCITS ETF GBP",
                "quantity": 10,
                "amount_gbp": 300.00,
                "reference": "44612P43356"
            },
              {
                "date": "2013-11-19",
                "transaction": "Sale",
                "description": "Vanguard FTSE All-World UCITS ETF GBP",
                "quantity": 10,
                "amount_gbp": 450.00,
                "reference": "44612P43356"
            }
        ]

        account_states = calculate_account_states(transactions)
        self.assertEqual(2, len(account_states))

        account_state = account_states[0]
        self.assertEqual(account_state.date, "2012-11-19")
        self.assertEqual(len(account_state.holdings), 1, "should be one holding")
        holding = list(account_state.holdings.values())[0]
        self.assertEqual(holding.quantity, 10)

        account_state = account_states[1]
        self.assertEqual(account_state.date, "2013-11-19")
        self.assertEqual(len(account_state.holdings), 0, "should be no holdings")


if __name__ == '__main__':
    unittest.main()
