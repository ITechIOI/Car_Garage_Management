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

namespace Gara_Management.GUI.Card
{
    /// <summary>
    /// Interaction logic for cardWrong.xaml
    /// </summary>
    public partial class cardWrong : Window
    {
        public cardWrong()
        {
            InitializeComponent();
            StartCloseTimer();
        }
        private void StartCloseTimer()
        {
            //Chỉnh thời gian hiển thị 2s
            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(2)
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
