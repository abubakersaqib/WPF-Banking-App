
using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace DBS_Bank
{
    public partial class CreateAccount : Window
    {
        public CreateAccount()
        {
            InitializeComponent();

        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            AccountsForm accountsForm = new AccountsForm();
            accountsForm.Show();
            this.Close();
        }

        private void CreateAccount_Load(object sender, EventArgs e)
        {



        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            try
            {
                string firstName = txtFirstName.Text;
                string surname = txtSurname.Text;
                string email = txtEmail.Text;
                string phone = txtPhone.Text;
                string postalAddress = txtAddressLine1.Text;
                string permanentAddress = txtAddressLine2.Text;
                string city = txtCity.Text;
                string country = txtCounty.Text;
                string accountType = SelectedOption();


                int accountNumber = int.Parse(txtAccountNumber.Text);
                int sortCode = int.Parse(txtSortCode.Text);
                int initialBalance = int.Parse(txtInitialBalance.Text);
                int overDraft = int.Parse(txtOverdraftLimit.Text);
                Account account = new Account(accountNumber, firstName, surname, email, phone, postalAddress, permanentAddress, city, country, accountType, sortCode, initialBalance, overDraft);
                Database.AddNewAccount(account);
                MessageBox.Show("Account Created Successfully");
                txtFirstName.Clear();
                txtSurname.Clear();
                txtEmail.Clear();
                txtPhone.Clear();
                txtAddressLine1.Clear();
                txtAddressLine2.Clear();
                txtCity.Clear();
                txtCounty.Clear();
                rbCurrent.IsChecked = false;
                rbSavings.IsChecked = false;
                txtAccountNumber.Clear();
                txtSortCode.Clear();
                txtInitialBalance.Clear();
                txtOverdraftLimit.Clear();


            }
            catch (Exception)
            {
                MessageBox.Show("Account Creation Failed");
            }

        }
        public string SelectedOption()
        {
            if (rbCurrent.IsChecked == true)
            {
                return rbCurrent.Content?.ToString() ?? "Not Mentioned";
            }
            else if (rbSavings.IsChecked == true)
            {
                return rbSavings.Content?.ToString() ?? "Not Mentioned";
            }
            else
            {
                return "Not Mentioned";
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
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
    }
}
