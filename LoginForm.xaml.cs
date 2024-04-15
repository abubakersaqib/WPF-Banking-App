using System;
using System.Windows;




namespace DBS_Bank
{
    public partial class LoginForm : Window
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        private void Clear_btn_Click(object sender, EventArgs e)
        {
            Username_txt.Clear();
            Password_txt.Clear();
        }
        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            string username = Username_txt.Text;
            string password = Password_txt.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter username and password.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                string fetchedUsername = Database.FetchUsername();
                string fetchedPassword = Database.FetchPassword(fetchedUsername);

                if (username == fetchedUsername && password == fetchedPassword)
                {
                    MenuForm menuForm = new MenuForm();
                    menuForm.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Incorrect username or password. Please try again.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

       
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the application
            Application.Current.Shutdown();
        }

    }
}
