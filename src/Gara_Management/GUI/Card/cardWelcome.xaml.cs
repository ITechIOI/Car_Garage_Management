using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Gara_Management.GUI.Card
{
    /// <summary>
    /// Interaction logic for cardWelcome.xaml
    /// </summary>
    public partial class cardWelcome : Window
    {
        public cardWelcome()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Lấy storyboard từ resource
            Storyboard fadeInStoryboard = (Storyboard)this.Resources["FadeInStoryboard"];
            fadeInStoryboard.Completed += FadeInStoryboard_Completed;
            // Bắt đầu storyboard
            fadeInStoryboard.Begin();
        }

        private void FadeInStoryboard_Completed(object sender, EventArgs e)
        {
            // Tạo dispatcher timer để chờ 3 giây trước khi bắt đầu fade out
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Dừng timer
            DispatcherTimer timer = sender as DispatcherTimer;
            timer.Stop();
            timer.Tick -= Timer_Tick;

            // Lấy storyboard từ resource
            Storyboard fadeOutStoryboard = (Storyboard)this.Resources["FadeOutStoryboard"];
            fadeOutStoryboard.Completed += FadeOutStoryboard_Completed;
            // Bắt đầu storyboard
            fadeOutStoryboard.Begin(this);
        }

        private void FadeOutStoryboard_Completed(object sender, EventArgs e)
        {
            cardLogin cardLogin = new cardLogin();
            cardLogin.Show();
            this.Close();
        }

    }
}
