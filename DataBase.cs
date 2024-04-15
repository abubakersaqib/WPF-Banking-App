using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace DBS_Bank
{
    internal class Database
    {
        public static SqlConnection GetConnection()
        {
            string strConnection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Alpha\\Desktop\\c#_proj_res\\Project\\DBS_Bank\\DBS_Bank\\DBS_Banking_System.mdf;Integrated Security=True;Integrated Security=True;Encrypt=False";

            SqlConnection connection = new SqlConnection(strConnection);
            try
            {
                connection.Open();
                //MessageBox.Show("Connected With DataBase");
            }
            catch (SqlException)
            {
                MessageBox.Show("Can't Connect!!!");
            }
            return connection;
        }

        // Fetching Username From Database From Login Table
        public static string FetchUsername()
        {
            string qry = "SELECT Username FROM LOG_IN;";
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand(qry, connection);
                return (string)command.ExecuteScalar();
            }
        }

        // Fetching Password From Database From Login Table
        public static string FetchPassword(string username)
        {
            string qry = "SELECT Pass_word FROM LOG_IN WHERE Username=@username";
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand(qry, connection);
                command.Parameters.AddWithValue("@username", username);
                return (string)command.ExecuteScalar();
            }
        }

        // Fetching all accounts from the database
        public static DataSet FetchAccountsData()
        {
            string query = "SELECT * FROM Accounts";
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "Accounts");
                return dataSet;
            }
        }

        // Fetching transactions for a specific account number
        public static DataTable FetchTransactions(int accountNumber)
        {
            using (SqlConnection connection = GetConnection())
            {
                string query = "SELECT * FROM Transactions WHERE AccountNumber = @accountNumber";
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                sqlCommand.Parameters.AddWithValue("@accountNumber", accountNumber);
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }

        // Fetching account details for a specific account number
        public static DataTable AccountByAccountNumber(int accountNumber)
        {
            using (SqlConnection connection = GetConnection())
            {
                string query = "SELECT * FROM Accounts WHERE AccountNumber = @accountNumber";
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                sqlCommand.Parameters.AddWithValue("@accountNumber", accountNumber);
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }

        // Getting initial balance for a specific account number
        public static int InitialBalanceByAccountNumber(int accountNumber)
        {
            using (SqlConnection connection = GetConnection())
            {
                string query = "SELECT InitialBalance FROM Accounts WHERE AccountNumber = @accountNumber";
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                sqlCommand.Parameters.AddWithValue("@accountNumber", accountNumber);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                int balance = 0;
                if (reader.Read())
                {
                    balance = Convert.ToInt32(reader["InitialBalance"]);
                }
                else
                {
                    MessageBox.Show("Sender's Account Not Found", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                return balance;
            }
        }

        // Getting account type for a specific account number
        public static string AccountTypeByAccountNumber(int accountNumber)
        {
            using (SqlConnection connection = GetConnection())
            {
                string query = "SELECT AccountType FROM Accounts WHERE AccountNumber = @accountNumber";
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                sqlCommand.Parameters.AddWithValue("@accountNumber", accountNumber);
                return (string)sqlCommand.ExecuteScalar();
            }
        }

        // Adding a new account to the database
        public static void AddNewAccount(Account account)
        {
            using (SqlConnection connection = GetConnection())
            {
                string insertQuery = "INSERT INTO Accounts(AccountNumber, FirstName, Surname, Email, Phone, PostalAddress, PermanentAddress, City, Country, AccountType, SortCode, InitialBalance, OverdraftLimit) " +
                    "VALUES(@AccountNumber, @FirstName, @Surname, @Email, @Phone, @PostalAddress, @PermanentAddress, @City, @Country, @AccountType, @SortCode, @InitialBalance, @OverdraftLimit)";
                SqlCommand command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@AccountNumber", account.AccountNumber);
                command.Parameters.AddWithValue("@FirstName", account.FirstName);
                command.Parameters.AddWithValue("@Surname", account.Surname);
                command.Parameters.AddWithValue("@Email", account.Email);
                command.Parameters.AddWithValue("@Phone", account.Phone);
                command.Parameters.AddWithValue("@PostalAddress", account.PostalAddress);
                command.Parameters.AddWithValue("@PermanentAddress", account.PermanentAddress);
                command.Parameters.AddWithValue("@City", account.City);
                command.Parameters.AddWithValue("@Country", account.Country);
                command.Parameters.AddWithValue("@AccountType", account.AccountType);
                command.Parameters.AddWithValue("@SortCode", account.SortCode);
                command.Parameters.AddWithValue("@InitialBalance", account.InitialBalance);
                command.Parameters.AddWithValue("@OverdraftLimit", account.OverdraftLimit);
                command.ExecuteNonQuery();
            }
        }

        // Getting balance for a specific account number
        public static int GetBalanceByAccountNumber(int accountNumber)
        {
            using (SqlConnection connection = GetConnection())
            {
                string query = "SELECT InitialBalance FROM Accounts WHERE AccountNumber = @accountNumber";
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                sqlCommand.Parameters.AddWithValue("@AccountNumber", accountNumber);
                object result = sqlCommand.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0;
            }
        }

        // Inserting a transaction record into the database
        public static void InsertTransaction(int accountNumber, string transactionType, int amount, DateTime transactionDateTime)
        {
            using (SqlConnection connection = GetConnection())
            {
                string insertQuery = "INSERT INTO Transactions(AccountNumber, TransactionType, Amount, TransactionDateTime) " +
                    "VALUES(@AccountNumber, @TransactionType, @Amount, @TransactionDateTime)";
                SqlCommand command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);
                command.Parameters.AddWithValue("@TransactionType", transactionType);
                command.Parameters.AddWithValue("@Amount", amount);
                command.Parameters.AddWithValue("@TransactionDateTime", transactionDateTime);
                command.ExecuteNonQuery();
            }
        }

        // Updating account details in the database
        public static void UpdateAccountDetails(Account account)
        {
            using (SqlConnection connection = GetConnection())
            {
                string updateQuery = "UPDATE Accounts SET Email = @Email, Phone = @Phone, PostalAddress = @PostalAddress, " +
                    "PermanentAddress = @PermanentAddress, City = @City, Country = @Country, OverdraftLimit = @OverdraftLimit " +
                    "WHERE AccountNumber = @AccountNumber";
                SqlCommand command = new SqlCommand(updateQuery, connection);
                command.Parameters.AddWithValue("@Email", account.Email);
                command.Parameters.AddWithValue("@Phone", account.Phone);
                command.Parameters.AddWithValue("@PostalAddress", account.PostalAddress);
                command.Parameters.AddWithValue("@PermanentAddress", account.PermanentAddress);
                command.Parameters.AddWithValue("@City", account.City);
                command.Parameters.AddWithValue("@Country", account.Country);
                command.Parameters.AddWithValue("@OverdraftLimit", account.OverdraftLimit);
                command.Parameters.AddWithValue("@AccountNumber", account.AccountNumber);
                command.ExecuteNonQuery();
            }
        }
    }
}