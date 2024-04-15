using System.Windows;
using System.Windows.Threading;

namespace DBS_Bank
{
    public partial class SplashScreen : Window
    {
        private DispatcherTimer timer;

        public SplashScreen()
        {
            InitializeComponent();

            Loaded += SplashScreen_Loaded;
        }

        private void SplashScreen_Loaded(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100); // Adjust the interval as needed
            timer.Tick += SplashTimer_Tick;
            timer.Start();
        }

        private void SplashTimer_Tick(object sender, EventArgs e)
        {
     
            LoadingpanelSlide.Width += 5;
            if (LoadingpanelSlide.Width > 300)
            {
              timer.Stop();
              LoginForm login = new LoginForm();
              login.Show();
              this.Close();
            }

        }
    }
}
