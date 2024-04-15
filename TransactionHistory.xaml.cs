using System;
using System.Data;
using System.Windows;

namespace DBS_Bank
{
    public partial class TransactionHistory : Window
    {
        public TransactionHistory()
        {
            InitializeComponent();
        }

        private void Back_btn_Click(object sender, RoutedEventArgs e)
        {
            TransactionsForm transactionsForm = new TransactionsForm();
            transactionsForm.Show();
            this.Close();
        }

        private void TransactionHistory_Load(object sender, RoutedEventArgs e)
        {
            // Initialize the form when it loads
            AccountDetails_grid.AutoGenerateColumns = true;
            AccountDetails_grid.ItemsSource = null;
        }

        private void View_His__btn_Click(object sender, RoutedEventArgs e)
        {
            // View transaction history when the button is clicked
            try
            {
                int accountNumber = int.Parse(AccNo_txt.Text);
                DataTable transactions = Database.FetchTransactions(accountNumber);
                if (transactions != null)
                {
                    AccountDetails_grid.ItemsSource = transactions.DefaultView;
                }
                else
                {
                    MessageBox.Show("No transactions found for the specified account number.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

    }
}
