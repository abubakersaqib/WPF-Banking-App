using System.Windows;
using System.Windows.Controls;

namespace DBS_Bank
{
    public partial class TransactionsForm : Window
    {
        public TransactionsForm()
        {
            InitializeComponent();
        }

        private void Check_Bal_btn_Click(object sender, RoutedEventArgs e)
        {
            // Handle Check Balance button click event
            CheckBal checkBal = new CheckBal();
            checkBal.Show();
            this.Hide();
        }

        private void Deposit_btn_Click(object sender, RoutedEventArgs e)
        {
            // Handle Deposit Funds button click event
            DepositFunds depositFunds = new DepositFunds();
            depositFunds.Show();
            this.Hide();
        }

        private void Withdraw_btn_Click(object sender, RoutedEventArgs e)
        {
            // Handle Withdraw Funds button click event
            WithdrawFunds withdrawFunds = new WithdrawFunds();
            withdrawFunds.Show();
            this.Hide();
        }

        private void Transfer_btn_Click(object sender, RoutedEventArgs e)
        {
            // Handle Transfer Funds button click event
            TransferFunds transferFunds = new TransferFunds();
            transferFunds.Show();
            this.Hide();
        }

        private void History_btn_Click(object sender, RoutedEventArgs e)
        {
            // Handle View Transaction History button click event
            TransactionHistory history = new TransactionHistory();
            history.Show();
            this.Hide();
        }

        private void Back_btn_Click(object sender, RoutedEventArgs e)
        {
            // Handle Back button click event
            MenuForm menuForm = new MenuForm();
            menuForm.Show();
            this.Close();
        }
    }
}
