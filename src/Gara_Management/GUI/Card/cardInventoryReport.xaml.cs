using Gara_Management.DAO;
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
    /// Interaction logic for cardInventoryReport.xaml
    /// </summary>
    public partial class cardInventoryReport : Window
    {
        private string gara;
        private DateTime startDate;
        private DateTime endDate;
        public cardInventoryReport(string gara, DateTime startDate, DateTime endDate)
        {
            InitializeComponent();
            this.Opacity = 0;
            this.gara = gara;
            this.startDate = startDate;
            this.endDate = endDate;
            LoadSpendDetails();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Lấy storyboard từ resource
            Storyboard fadeInStoryboard = (Storyboard)this.Resources["FadeInStoryboard"];
            // Bắt đầu storyboard
            fadeInStoryboard.Begin(this);
        }

        private void bt_exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void bt_previous_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void bt_next_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void bt_report_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void LoadSpendDetails()
        {
            dgr_SpendDetails.ItemsSource = RevenueDetailDAO.Instance.LoadSpendDetailListByPeriod(gara, startDate.Date.ToString("MM/dd/yyyy"), endDate.Date.ToString("MM/dd/yyyy")).DefaultView;
        }

    }
}
