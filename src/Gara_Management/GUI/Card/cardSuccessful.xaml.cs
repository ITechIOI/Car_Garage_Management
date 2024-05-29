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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfAnimatedGif;

namespace Gara_Management.GUI.Card
{
    /// <summary>
    /// Interaction logic for cardSuccessful.xaml
    /// </summary>
    public partial class cardSuccessful : Window
    {
        public cardSuccessful()
        {
            InitializeComponent();
            //var image = new BitmapImage();
            //image.BeginInit();
            //image.UriSource = new Uri("/Images/preview.gif", UriKind.RelativeOrAbsolute);
            //image.EndInit();

            //ImageBehavior.SetAnimatedSource(img_gif, image);
            SetGifInImage();
            StartCloseTimer();
        }
        private void SetGifInImage()
        {
            var gifUri = new Uri("pack://application:,,,/Images/check.gif", UriKind.Absolute);
            var imageSource = new BitmapImage(gifUri);
            ImageBehavior.SetAnimatedSource(img_gif, imageSource);
        }

        private void StartCloseTimer()
        {
            //Chỉnh thời gian hiển thị 3s
            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(3)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Stop the timer and close the window
            var timer = sender as DispatcherTimer;
            timer.Stop();
            this.Close();
        }
    }
}
