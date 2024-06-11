using Gara_Management.DAO;
using Gara_Management.GUI.Card;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;
using System.Globalization;

namespace Gara_Management.GUI
{
    /// <summary>
    /// Interaction logic for scrRevenue.xaml
    /// </summary>
    public partial class scrRevenue : UserControl
    {
        private decimal revenue = 0;
        private decimal spend = 0;
        private string gara;
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
        public scrRevenue(string gara)
        {
            InitializeComponent();
            this.gara = gara;
            InitializeInfo();
            LoadChart();
        }

        private void LoadChart()
        {
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Doanh thu",
                    Values = new ChartValues<double> { Convert.ToDouble(revenue) }
                }
            };

            //adding series will update and animate the chart automatically
            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Chi phí",
                Values = new ChartValues<double> { Convert.ToDouble(spend) }
            });

            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Lợi nhuận",
                Values = new ChartValues<double> { Convert.ToDouble(revenue - spend) }
            });

            //also adding values updates and animates the chart automatically
            Labels = new[] { "Tháng " + dpk_startDate.SelectedDate.Value.Month };
            Formatter = value => ToKMB(value);
            //Formatter = value => value.ToString("N");

            DataContext = this;
        }

        private void UpdateChart()
        {
            if(dpk_startDate.SelectedDate.Value.Month != dpk_endDate.SelectedDate.Value.Month)
            {
                Labels[0] = "Tháng " + dpk_startDate.SelectedDate.Value.Month + " - " + dpk_endDate.SelectedDate.Value.Month;
            }
            else
            {
                Labels[0] = "Tháng " + dpk_startDate.SelectedDate.Value.Month;
            }
            SeriesCollection[0].Values = new ChartValues<double> { Convert.ToDouble(revenue) };
            SeriesCollection[1].Values = new ChartValues<double> { Convert.ToDouble(spend) };
            SeriesCollection[2].Values = new ChartValues<double> { Convert.ToDouble(revenue - spend) };
            chart.Update(true, true);
            DataContext = this;
        }

        private void bt_revenue_MouseDown(object sender, MouseButtonEventArgs e)
        {
            cardRevenue revenue = new cardRevenue(gara, dpk_startDate.SelectedDate.Value, dpk_endDate.SelectedDate.Value);
            revenue.Show();
        }

        private void bt_inventory_MouseDown(object sender, MouseButtonEventArgs e)
        {
            cardInventoryReport inventory = new cardInventoryReport(gara, dpk_startDate.SelectedDate.Value, dpk_endDate.SelectedDate.Value);
            inventory.Show();
        }

        private void bt_exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có muốn thoát ứng dụng?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // Kiểm tra xem người dùng đã chọn Yes hay không
            if (result == MessageBoxResult.Yes)
            {
                App.Current.Shutdown();
            }
        }

        private void InitializeInfo()
        {
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            dpk_startDate.Text = startDate.ToString();
            dpk_endDate.Text = endDate.ToString();
            DataTable revenueList = RevenueDetailDAO.Instance.LoadRevenueDetailListByPeriod(gara, dpk_startDate.SelectedDate.Value.ToString("MM/dd/yyyy"), dpk_endDate.SelectedDate.Value.ToString("MM/dd/yyyy"));
            DataTable spendList = RevenueDetailDAO.Instance.LoadSpendDetailListByPeriod(gara, dpk_startDate.SelectedDate.Value.ToString("MM/dd/yyyy"), dpk_endDate.SelectedDate.Value.ToString("MM/dd/yyyy"));
            foreach (DataRow row in revenueList.Rows)
            {
                revenue = revenue + Convert.ToDecimal(row[4].ToString());
            }
            foreach (DataRow row in spendList.Rows)
            {
                spend = spend + Convert.ToDecimal(row[5].ToString());
            }
            tbl_revenue.Text =((int) revenue).ToString();
            tbl_spend.Text =((int) spend).ToString();
        }

        private void dpk_CalendarClosed(object sender, RoutedEventArgs e)
        {
            revenue = 0;
            spend = 0;
            DataTable revenueList = RevenueDetailDAO.Instance.LoadRevenueDetailListByPeriod(gara, dpk_startDate.SelectedDate.Value.ToString("MM/dd/yyyy"), dpk_endDate.SelectedDate.Value.ToString("MM/dd/yyyy"));
            DataTable spendList = RevenueDetailDAO.Instance.LoadSpendDetailListByPeriod(gara, dpk_startDate.SelectedDate.Value.ToString("MM/dd/yyyy"), dpk_endDate.SelectedDate.Value.ToString("MM/dd/yyyy"));
            foreach (DataRow row in revenueList.Rows)
            {
                revenue = revenue + Convert.ToDecimal(row[4].ToString());
            }
            foreach (DataRow row in spendList.Rows)
            {
                spend = spend + Convert.ToDecimal(row[5].ToString());
            }
            tbl_revenue.Text = revenue.ToString();
            tbl_spend.Text = spend.ToString();
            UpdateChart();
        }

        private string ToKMB(double num)
        {
            if (num > 999999999 || num < -999999999)
            {
                return num.ToString("0,,,.###B", CultureInfo.InvariantCulture);
            }
            else
            if (num > 999999 || num < -999999)
            {
                return num.ToString("0,,.##M", CultureInfo.InvariantCulture);
            }
            else
            if (num > 999 || num < -999)
            {
                return num.ToString("0,.#K", CultureInfo.InvariantCulture);
            }
            else
            if (num < 0.001 && num > -0.001)
            {
                return num.ToString("0.", CultureInfo.InvariantCulture);
            }
            else
            {
                return num.ToString(CultureInfo.InvariantCulture);
            }
        }
    }
}
