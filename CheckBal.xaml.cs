using System.Windows;


namespace DBS_Bank
{
    public partial class CheckBal : Window
    {
        public CheckBal()
        {
            InitializeComponent();
        }

        private void Close_btn_Click(object sender, RoutedEventArgs e)
        {
            TransactionsForm transactionsForm = new TransactionsForm();
            transactionsForm.Show();
            this.Close();
        }

        private void Check_Bal_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int accountNumber = int.Parse(AccNo_txt.Text);
                int accountBalance = Database.GetBalanceByAccountNumber(accountNumber);

                Bal_lbl.Content = accountBalance.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}

