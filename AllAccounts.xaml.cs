using System;
using System.Data;
using System.Windows;


namespace DBS_Bank
{
    public partial class AllAccounts : Window
    {
        public AllAccounts()
        {
            InitializeComponent();

            RefreshGrid();
        }

        private void RefreshGrid()
        {
            try
            {
                // Fetch account data from the database
                DataSet ds = Database.FetchAccountsData();

                // Bind the account data to the DataGrid
                dgAccounts.ItemsSource = ds.Tables["Accounts"].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading accounts: " + ex.Message);
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshGrid();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            AccountsForm accountsForm = new AccountsForm();
            accountsForm.Show();
            this.Close();
        }
    }
}
