using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace DBS_Bank
{
    public partial class TransferFunds : Window
    {
        private int amountTransfer;
        private int senderAccountNumber;
        private int receiverAccountNumber;
        private string transactionType = "Transfer";

        public TransferFunds()
        {
            InitializeComponent();
        }

        private void Back_btn_Click(object sender, RoutedEventArgs e)
        {
            TransactionsForm transactionsForm = new TransactionsForm();
            transactionsForm.Show();
            this.Close();
        }

        private void TransferFunds_Load(object sender, RoutedEventArgs e)
        {

        }

        private bool SubtractBalance(int senderAccountNumber, int amountTransfer)
        {
            string query = "UPDATE Accounts SET InitialBalance = InitialBalance - @AmountTransfer WHERE AccountNumber = @senderAccountNumber";
            using (SqlConnection connection = Database.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@senderAccountNumber", senderAccountNumber);
                    command.Parameters.AddWithValue("@AmountTransfer", amountTransfer);
                    //connection.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
            }
        }

        private bool AddBalance(int receiverAccountNumber, int amountTransfer)
        {
            string query = "UPDATE Accounts SET InitialBalance = InitialBalance + @AmountTransfer WHERE AccountNumber = @receiverAccountNumber";
            using (SqlConnection connection = Database.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@receiverAccountNumber", receiverAccountNumber);
                    command.Parameters.AddWithValue("@AmountTransfer", amountTransfer);
                    //connection.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
            }
        }

        private void Transfer_Funds_btn_Click(object sender, RoutedEventArgs e)
        {
            senderAccountNumber = int.Parse(SenderAccNo_txt.Text);
            receiverAccountNumber = int.Parse(ReceiverAccNo_txt.Text);
            amountTransfer = int.Parse(Amount_Trns_txt.Text);
            DateTime transferDateTime = DateTime.Now;

            if (amountTransfer == 0)
            {
                MessageBox.Show("Invalid Amount!!!");
                return;
            }

            int sourceAccountBalance = Database.InitialBalanceByAccountNumber(senderAccountNumber);
            if (amountTransfer > sourceAccountBalance)
            {
                MessageBox.Show("Insufficient balance in the source account.");
                return;
            }

            bool transferSuccess = FundsTransfer(senderAccountNumber, receiverAccountNumber, amountTransfer);
            if (transferSuccess)
            {
                Database.InsertTransaction(senderAccountNumber, transactionType, amountTransfer, transferDateTime);
                Database.InsertTransaction(receiverAccountNumber, transactionType, amountTransfer, transferDateTime);
                MessageBox.Show("Funds transferred successfully.");

                // Refresh the receiver's account details
                receiverAccountNumber = int.Parse(ReceiverAccNo_txt.Text);
                Transaction_grid.ItemsSource = Database.AccountByAccountNumber(receiverAccountNumber).DefaultView;
            }
            else
            {
                MessageBox.Show("Failed to transfer funds. Please try again.");
            }
        }

        private bool FundsTransfer(int senderAccountNumber, int receiverAccountNumber, int amountTransfer)
        {
            try
            {
                using (SqlConnection connection = Database.GetConnection())
                {
                    //connection.Open();

                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        string deductQuery = "UPDATE Accounts SET InitialBalance = InitialBalance - @AmountTransfer WHERE AccountNumber = @senderAccountNumber";
                        using (SqlCommand deductCommand = new SqlCommand(deductQuery, connection, transaction))
                        {
                            deductCommand.Parameters.AddWithValue("@AmountTransfer", amountTransfer);
                            deductCommand.Parameters.AddWithValue("@SenderAccountNumber", senderAccountNumber);

                            int rowsAffected = deductCommand.ExecuteNonQuery();
                            if (rowsAffected != 1)
                            {
                                transaction.Rollback();
                                return false;
                            }
                        }

                        string addQuery = "UPDATE Accounts SET InitialBalance = InitialBalance + @AmountTransfer WHERE AccountNumber = @receiverAccountNumber";
                        using (SqlCommand addCommand = new SqlCommand(addQuery, connection, transaction))
                        {
                            addCommand.Parameters.AddWithValue("@AmountTransfer", amountTransfer);
                            addCommand.Parameters.AddWithValue("@ReceiverAccountNumber", receiverAccountNumber);
                            int rowsAffected = addCommand.ExecuteNonQuery();
                            if (rowsAffected != 1)
                            {
                                transaction.Rollback();
                                return false;
                            }
                        }
                        transaction.Commit();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
                return false;
            }
        }
    }
}
