using System;
using System.Data.SqlClient;
using System.Windows;

namespace DBS_Bank
{
    public partial class DepositFunds : Window
    {
        private int accountNumber;
        private string transactionType = "Deposit";

        public DepositFunds()
        {
            InitializeComponent();
        }

        private void Back_btn_Click(object sender, RoutedEventArgs e)
        {
            TransactionsForm transactionsForm = new TransactionsForm();
            transactionsForm.Show();
            this.Close();
        }

        private void Deposit_btn_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(AccNo_txt.Text, out accountNumber) && int.TryParse(Amount_Dep_txt.Text, out int amountDeposit))
            {
                DateTime transferDateTime = DateTime.Now;
                bool success = AddBalance(accountNumber, amountDeposit);
                if (success)
                {
                    Database.InsertTransaction(accountNumber, transactionType, amountDeposit, transferDateTime);
                    MessageBox.Show($"Funds successfully added to account {accountNumber}.");
                }
                else
                {
                    MessageBox.Show("Invalid amount. Please enter a valid numeric value.");
                }
            }
            else
            {
                MessageBox.Show("Invalid input. Please enter valid numeric values.");
            }
        }

        private bool AddBalance(int accountNumber, int amountDeposit)
        {
            string query = "UPDATE Accounts SET InitialBalance = InitialBalance + @amountDeposit WHERE AccountNumber = @accountNumber";
            using (SqlConnection connection = Database.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@accountNumber", accountNumber);
                    command.Parameters.AddWithValue("@amountDeposit", amountDeposit);
                    //connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        private void ViewDeposit_btn_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(AccNo_txt.Text, out accountNumber))
            {
                DepositView_Grid.ItemsSource = Database.AccountByAccountNumber(accountNumber).DefaultView;
                AccNo_txt.Clear();
                Amount_Dep_txt.Clear();
            }
            else
            {
                MessageBox.Show("Invalid account number. Please enter a valid numeric value.");
            }
        }

        private void DepositFunds_Load(object sender, RoutedEventArgs e)
        {
            AccNo_txt.Focus();
        }
    }
}
