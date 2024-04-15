using System.Windows;

namespace DBS_Bank
{
    public partial class AccountsForm : Window
    {
        public AccountsForm()
        {
            InitializeComponent();
        }

        private void Back_btn_Click(object sender, RoutedEventArgs e)
        {
            MenuForm menuForm = new MenuForm();
            menuForm.Show();
            this.Close();
        }

        private void Create_Acc_btn_Click(object sender, RoutedEventArgs e)
        {
            CreateAccount createAccount = new CreateAccount();
            createAccount.Show();
            this.Close();
        }

        private void Edit_Acc_btn_Click(object sender, RoutedEventArgs e)
        {
            EditAccount editAccount = new EditAccount();
            editAccount.Show();
            this.Close();
        }

        private void View_Acc_btn_Click(object sender, RoutedEventArgs e)
        {
            AllAccounts allAccounts = new AllAccounts();
            allAccounts.Show();
            this.Close();
        }
    }
}
