using Gara_Management.DAO;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
            ExportToCSVAndSaveDialog();
        }

        private void LoadSpendDetails()
        {
            if (startDate.Date.Month != endDate.Date.Month)
            {
                tbl_month.Text = startDate.Date.Month.ToString() + "-" + endDate.Date.Month.ToString();
            }
            else
            {
                tbl_month.Text = startDate.Date.Month.ToString();
            }
            dgr_SpendDetails.ItemsSource = RevenueDetailDAO.Instance.LoadSpendDetailListByPeriod(gara, startDate.Date.ToString("MM/dd/yyyy"), endDate.Date.ToString("MM/dd/yyyy")).DefaultView;
        }
        private void ExportToCSVAndSaveDialog()
        {
            var columnNames = new StringBuilder();
            foreach (var column in dgr_SpendDetails.Columns)
            {
                columnNames.Append(column.Header.ToString()).Append(",");
            }
            columnNames.Remove(columnNames.Length - 1, 1); // Xóa dấu phẩy cuối cùng
            columnNames.AppendLine();

            // Tạo chuỗi dữ liệu cho CSV
            var dataRows = new StringBuilder();
            foreach (var row in dgr_SpendDetails.Items)
            {
                var rowData = new StringBuilder();
                foreach (var column in dgr_SpendDetails.Columns)
                {
                    var cellContent = column.GetCellContent(row);
                    rowData.Append(((TextBlock)cellContent).Text).Append(",");
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
