using System.Windows;

namespace DBS_Bank
{
    public partial class MenuForm : Window
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void Accounts_pic_Click(object sender, RoutedEventArgs e)
        {
            // Handle accounts button click event
            AccountsForm accountsForm = new AccountsForm();
            accountsForm.Show();
            this.Close();
        }

        private void Transactions_pic_Click(object sender, RoutedEventArgs e)
        {
            // Handle transactions button click event
            TransactionsForm transactionsForm = new TransactionsForm();
            transactionsForm.Show();
            this.Close();
        }

        private void Logout_pic_Click(object sender, RoutedEventArgs e)
        {
            // Handle logout button click event
            if (MessageBox.Show("Are you sure you want to log out?", "Log Out", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                LoginForm login = new LoginForm();
                login.Show();
                this.Close();
            }
        }
    }
}
