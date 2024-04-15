using System;
using System.Data.SqlClient;
using System.Windows;

namespace DBS_Bank
{
    public partial class WithdrawFunds : Window
    {
        private int accountNumber;
        private int amountWithDraw;
        private string transactionType = "Withdraw";

        public WithdrawFunds()
        {
            InitializeComponent();
        }

        private void Back_btn_Click(object sender, RoutedEventArgs e)
        {
            TransactionsForm transactionsForm = new TransactionsForm();
            transactionsForm.Show();
            this.Close();
        }

        private void Withdraw_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.TryParse(AccNo_txt.Text, out accountNumber) && int.TryParse(Amount_WithDraw_txt.Text, out amountWithDraw))
                {
                    DateTime transferDateTime = DateTime.Now;
                    int initialBalance = Database.InitialBalanceByAccountNumber(accountNumber);
                    if (amountWithDraw <= initialBalance)
                    {
                        bool success = WithDrawBalance(accountNumber, amountWithDraw);
                        if (success)
                        {
                            Database.InsertTransaction(accountNumber, transactionType, amountWithDraw, transferDateTime);
                            MessageBox.Show($"Funds successfully withdrawn from account {accountNumber}.");
                        }
                        else
                        {
                            MessageBox.Show("Invalid amount. Please enter a valid numeric value.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Insufficient Balance");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid input. Please enter valid numeric values.");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           
        }

        private bool WithDrawBalance(int accountNumber, int amountWithDraw)
        {
            string query = "UPDATE Accounts SET InitialBalance = InitialBalance - @AmountWithDraw WHERE AccountNumber = @accountNumber";
            using (SqlConnection connection = Database.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@accountNumber", accountNumber);
                    command.Parameters.AddWithValue("@AmountWithDraw", amountWithDraw);
                    //connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        private void ViewWithDraw_btn_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(AccNo_txt.Text, out accountNumber))
            {
                WithdrawView_Grid.ItemsSource = Database.AccountByAccountNumber(accountNumber).DefaultView;
                AccNo_txt.Clear();
                Amount_WithDraw_txt.Clear();
            }
            else
            {
                MessageBox.Show("Invalid account number. Please enter a valid numeric value.");
            }
        }
    }
}
