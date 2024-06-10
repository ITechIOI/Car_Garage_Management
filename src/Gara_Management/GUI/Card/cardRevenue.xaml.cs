using Gara_Management.DAO;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
        private DateTime reportDate;
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
            ExportToCSVAndSaveDialog();
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
            tbl_totalRevenue.Text =((int) totalRevenue).ToString("N");
            tbl_numberOfRepairs.Text = numberOfRepairs.ToString();
        }
        
        private void ExportToCSVAndSaveDialog()
        {
            var columnNames = new StringBuilder();
            foreach (var column in dgr_revenue.Columns)
            {
                columnNames.Append(column.Header.ToString()).Append(",");
            }
            columnNames.Remove(columnNames.Length - 1, 1); // Xóa dấu phẩy cuối cùng
            columnNames.AppendLine();

            // Tạo chuỗi dữ liệu cho CSV
            var dataRows = new StringBuilder();
            foreach (var row in dgr_revenue.Items)
            {
                var rowData = new StringBuilder();
                foreach (var column in dgr_revenue.Columns)
                {
                    var cellContent = column.GetCellContent(row); 
                    rowData.Append(((TextBlock) cellContent).Text).Append(",");
                }
                rowData.Remove(rowData.Length - 1, 1); 
                dataRows.AppendLine(rowData.ToString());
            }
            // Kết hợp chuỗi tiêu đề và dữ liệu
            var csvContent = columnNames.ToString() + dataRows.ToString();

            // Sử dụng SaveFileDialog để lưu tệp CSV
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
            if (saveFileDialog.ShowDialog() == true)
            {
                // Sử dụng UTF-8 encoding để hỗ trợ các ký tự tiếng Việt
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8))
                {
                    writer.Write(csvContent);
                }
                MessageBox.Show("Dữ liệu đã được kết xuất thành công.");
            }
        }
    }
}
