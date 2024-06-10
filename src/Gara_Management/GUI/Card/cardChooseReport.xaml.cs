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

namespace Gara_Management.GUI.Card
{
    /// <summary>
    /// Interaction logic for cardChooseReport.xaml
    /// </summary>
    public partial class cardChooseReport : Window
    {
        string gara;
        public cardChooseReport(string gara)
        {
            InitializeComponent();
            this.Opacity = 0;
            this.gara = gara;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Lấy storyboard từ resource
            Storyboard fadeInStoryboard = (Storyboard)this.Resources["FadeInStoryboard"];
            // Bắt đầu storyboard
            fadeInStoryboard.Begin(this);
        }

        private void Reveune_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // tạo báo cáo
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            cardRevenue revenue = new cardRevenue("GR1", startDate, endDate);
            revenue.Show();
            this.Close();

        }

        private void Invetory_MouseDown(object sender, MouseButtonEventArgs e)
        {           
            cardStockInfo card = new cardStockInfo(gara, DateTime.Now.AddMonths(-1));
            card.Show();
            this.Close();
        }
    }
}
