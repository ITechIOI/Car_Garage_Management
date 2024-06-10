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
    /// Interaction logic for cardStockInfo.xaml
    /// </summary>
    public partial class cardStockInfo : Window
    {
        string gara;
        DateTime reportDate;
        public cardStockInfo(string gara, DateTime date)
        {
            InitializeComponent();
            this.Opacity = 0;
            this.gara = gara;
            this.reportDate = date;
            LoadInventoryReportDetail();
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
        private void LoadInventoryReportDetail()
        {
            dgr_SpendDetails.ItemsSource = InventoryReportDAO.Instance.LoadEndingInventoryListByDate(gara, reportDate);
        }

        private void dgr_SpendDetails_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Com" || e.Column.Header.ToString() == "BIComQuantity" ||
                e.Column.Header.ToString() == "EIComQuantity" || e.Column.Header.ToString() == "ICComQuantity")
            {
                e.Cancel = true;
            }    
        }
    }
}
