using Gara_Management.DAO;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for cardRevenue.xaml
    /// </summary>
    public partial class cardRevenue : Window
    {
        private string gara;
        private decimal totalRevenue = 0;
        private int numberOfRepairs = 0;
        private DateTime startDate;
        private DateTime endDate;
        public cardRevenue(string gara, DateTime startDate, DateTime dateTime)
        {
            InitializeComponent();
            this.gara = gara;
            this.startDate = startDate;
            this.endDate = dateTime;
            LoadRevenueList();
            this.Opacity = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Lấy storyboard từ resource
            Storyboard fadeInStoryboard = (Storyboard)this.Resources["FadeInStoryboard"];
            // Bắt đầu storyboard
            fadeInStoryboard.Begin(this);
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

        private void bt_exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void LoadRevenueList()
        {
            if (startDate.Date.Month != endDate.Date.Month)
            {
                tbl_month.Text = startDate.Date.Month.ToString() + "-" + endDate.Date.Month.ToString();
            }
            else
            {
                tbl_month.Text = startDate.Date.Month.ToString();
            }
            dgr_revenue.ItemsSource = RevenueDetailDAO.Instance.LoadRevenueDetailListByPeriod(gara, startDate.Date.ToString("MM/dd/yyyy"), endDate.Date.ToString("MM/dd/yyyy")).DefaultView;
            for (int i = 0; i < dgr_revenue.Items.Count - 1; i++)
            {
                DataRowView row = (DataRowView)dgr_revenue.Items[i];

                totalRevenue = totalRevenue + Convert.ToDecimal(row.Row[4].ToString());
                numberOfRepairs = numberOfRepairs + int.Parse(row.Row[2].ToString());
            }
            tbl_totalRevenue.Text = totalRevenue.ToString("N");
            tbl_numberOfRepairs.Text = numberOfRepairs.ToString();
        }
    }
}
