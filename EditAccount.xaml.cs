using System;
using System.Data;
using System.Windows;

namespace DBS_Bank
{
    public partial class EditAccount : Window
    {
        public EditAccount()
        {
            InitializeComponent();

            // Load account details into the grid
            RefreshGrid();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            AccountsForm accountsForm = new AccountsForm();
            accountsForm.Show();
            this.Close();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
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

        private void dgAccounts_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Check if the selected item is a DataRowView
            if (dgAccounts.SelectedItem is DataRowView selectedRow)
            {
                // Fill input fields with selected account details
                txtFirstName.Text = selectedRow["FirstName"].ToString();
                txtSurname.Text = selectedRow["Surname"].ToString();
                txtEmail.Text = selectedRow["Email"].ToString();
                txtPhone.Text = selectedRow["Phone"].ToString();
                txtAddressLine1.Text = selectedRow["PostalAddress"].ToString();
                txtAddressLine2.Text = selectedRow["PermanentAddress"].ToString();
                txtCity.Text = selectedRow["City"].ToString();
                txtCountry.Text = selectedRow["Country"].ToString();

                string accountType = selectedRow["AccountType"].ToString(); // Fixed typo in column name
                rbCurrent.IsChecked = (accountType == "Current");
                rbSavings.IsChecked = (accountType == "Savings");

                txtAccountNumber.Text = selectedRow["AccountNumber"].ToString();
                txtSortCode.Text = selectedRow["SortCode"].ToString();
                txtInitialBalance.Text = selectedRow["InitialBalance"].ToString();
                txtOverdraftLimit.Text = selectedRow["OverdraftLimit"].ToString();

                // Enable Update Button
                UpdateButton.IsEnabled = true;
            }
        }


        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get updated values from input fields
                int accountNumber = int.Parse(txtAccountNumber.Text);
                string email = txtEmail.Text;
                string phone = txtPhone.Text;
                string postalAddress = txtAddressLine1.Text;
                string permanentAddress = txtAddressLine2.Text;
                string city = txtCity.Text;
                string country = txtCountry.Text;
                int overDraft = int.Parse(txtOverdraftLimit.Text);

                // Update account details in the database
                Account updatedAccount = new Account(accountNumber, email, phone, postalAddress, permanentAddress, city, country, overDraft);
                Database.UpdateAccountDetails(updatedAccount);
                MessageBox.Show("Account Updated Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating account: " + ex.Message);
            }
        }
    }
}
