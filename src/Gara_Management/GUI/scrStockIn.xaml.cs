using Gara_Management.GUI.Item;
using Gara_Management.GUI.Card;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Gara_Management.DTO;
using Gara_Management.DAO;
using Newtonsoft.Json.Linq;

namespace Gara_Management.GUI
{
    /// <summary>
    /// Interaction logic for scrStockIn.xaml
    /// </summary>
    public partial class scrStockIn : UserControl
    {
        Color color3 = (Color)ColorConverter.ConvertFromString("#5790AB");
        Color color4 = (Color)ColorConverter.ConvertFromString("#064469");

        string gara;
        Account acc;
        string startDate = "", endDate = "";
        int minPrice = -1, maxPrice = -1;
        string supplier = "";

        public EventHandler changeToStoreScr;
        public scrStockIn(string gara, Account acc)
        {
            InitializeComponent();
            this.gara = gara;
            this.acc = acc;
            LoadListGoodReceivedNote();
            SetSliderMaxValue();
        }
        private void bd_exit_MouseEnter(object sender, MouseEventArgs e)
        {
            bd_exit.Background = new SolidColorBrush(color4);
        }

        private void bd_exit_MouseLeave(object sender, MouseEventArgs e)
        {
            bd_exit.Background = new SolidColorBrush(color3);
        }
        // nút thoát ứng dụng
        private void bd_exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có muốn thoát ứng dụng?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // Kiểm tra xem người dùng đã chọn Yes hay không
            if (result == MessageBoxResult.Yes)
            {
                App.Current.Shutdown();
            }
        }
        // chuyển sang màn hình kho
        private void bd_store_scr_MouseDown(object sender, MouseButtonEventArgs e)
        {
            changeToStoreScr?.Invoke(this, EventArgs.Empty);
        }
        // tạo phiêu nhập kho
        private void bd_stockIn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            crdStockIn crdStockIn = new crdStockIn(gara, acc);
            crdStockIn.ShowDialog();
            LoadListGoodReceivedNote();
        }

        private void bd_filter_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (filter.Visibility == Visibility.Hidden)
            {
                if (startDate == "")
                {
                    dpk_startDate.Text = "";
                    ckb_startDate.IsChecked = false;
                }
                else
                {
                    DateTime start = DateTime.ParseExact(startDate, "MM/dd/yyyy", null);
                    dpk_startDate.Text = start.ToString("dd/MM/yyyy");
                    ckb_startDate.IsChecked = true;
                }

                if (endDate == "")
                {
                    dpk_endDate.Text = "";
                    ckb_endDate.IsChecked = false;
                }
                else
                {
                    DateTime end = DateTime.ParseExact(endDate, "MM/dd/yyyy", null);
                    dpk_endDate.Text = end.ToString("dd/MM/yyyy");
                    ckb_endDate.IsChecked = true;
                }

                if (minPrice == -1 && maxPrice == -1)
                {
                    rangeSlider.LowerValue = rangeSlider.Minimum;
                    rangeSlider.HigherValue = rangeSlider.Maximum;
                    ckb_price.IsChecked = false;
                }
                else
                {
                    rangeSlider.LowerValue = minPrice;
                    rangeSlider.HigherValue = maxPrice;
                    ckb_price.IsChecked = true;
                }
                filter.Visibility = Visibility.Visible;
            }
            else
            {
                filter.Visibility = Visibility.Hidden;
            }
        }

        private void SetSliderMaxValue()
        {
            List<GoodReceivedNote> list = GoodReceivedNoteDAO.Instance.LoadGoodReceivedNoteList(gara);
            decimal price = 0;
            foreach (GoodReceivedNote grn in list)
            {
                if (grn.TotalPaymentGRN > price)
                    price = grn.TotalPaymentGRN;
            }
            rangeSlider.Maximum = Convert.ToDouble(price);
        }

        private void LoadListGoodReceivedNote()
        {
            List<GoodReceivedNote> list = GoodReceivedNoteDAO.Instance.LoadGoodReceivedNoteList(gara);
            ds_phieunhap.Children.Clear();
            foreach (GoodReceivedNote good in list)
            {
                itStockIn it = new itStockIn(good);
                ds_phieunhap.Children.Add(it);
            }
        }

        #region fucntions for filter
        private void LoadListGoodReceivedNoteBySupplier()
        {
            List<GoodReceivedNote> list = GoodReceivedNoteDAO.Instance.LoadGoodReceivedNoteListBySupplier(gara, supplier);
            ds_phieunhap.Children.Clear();
            foreach (GoodReceivedNote good in list)
            {
                itStockIn it = new itStockIn(good);
                ds_phieunhap.Children.Add(it);
            }
        }

        private void LoadGoodReceivedNoteListByStartDate()
        {
            List<GoodReceivedNote> list = GoodReceivedNoteDAO.Instance.LoadGoodReceivedNoteListByStartDate(gara, startDate);
            ds_phieunhap.Children.Clear();
            foreach (GoodReceivedNote good in list)
            {
                itStockIn it = new itStockIn(good);
                ds_phieunhap.Children.Add(it);
            }
        }

        private void LoadGoodReceivedNoteListByEndDate()
        {
            List<GoodReceivedNote> list = GoodReceivedNoteDAO.Instance.LoadGoodReceivedNoteListByEndDate(gara, endDate);
            ds_phieunhap.Children.Clear();
            foreach (GoodReceivedNote good in list)
            {
                itStockIn it = new itStockIn(good);
                ds_phieunhap.Children.Add(it);
            }
        }

        private void LoadGoodReceivedNoteListByPrice()
        {
            List<GoodReceivedNote> list = GoodReceivedNoteDAO.Instance.LoadGoodReceivedNoteListByPrice(gara, minPrice, maxPrice);
            ds_phieunhap.Children.Clear();
            foreach (GoodReceivedNote good in list)
            {
                itStockIn it = new itStockIn(good);
                ds_phieunhap.Children.Add(it);
            }
        }
        private void LoadGoodReceivedNoteListBySupplierAndStartDate()
        {
            List<GoodReceivedNote> list = GoodReceivedNoteDAO.Instance.LoadGoodReceivedNoteListBySupplierAndStartDate(gara, supplier, startDate);
            ds_phieunhap.Children.Clear();
            foreach (GoodReceivedNote good in list)
            {
                itStockIn it = new itStockIn(good);
                ds_phieunhap.Children.Add(it);
            }
        }

        private void LoadGoodReceivedNoteListBySupplierAndEndDate()
        {
            List<GoodReceivedNote> list = GoodReceivedNoteDAO.Instance.LoadGoodReceivedNoteListBySupplierAndEndDate(gara, supplier, endDate);
            ds_phieunhap.Children.Clear();
            foreach (GoodReceivedNote good in list)
            {
                itStockIn it = new itStockIn(good);
                ds_phieunhap.Children.Add(it);
            }
        }

        private void LoadGoodReceivedNoteListBySupplierAndPrice()
        {
            List<GoodReceivedNote> list = GoodReceivedNoteDAO.Instance.LoadGoodReceivedNoteListBySupplierAndPrice(gara, supplier, minPrice, maxPrice);
            ds_phieunhap.Children.Clear();
            foreach (GoodReceivedNote good in list)
            {
                itStockIn it = new itStockIn(good);
                ds_phieunhap.Children.Add(it);
            }
        }
        private void LoadGoodReceivedNoteListByStartAndEndDate()
        {
            List<GoodReceivedNote> list = GoodReceivedNoteDAO.Instance.LoadGoodReceivedNoteListByStartAndEndDate(gara, startDate, endDate);
            ds_phieunhap.Children.Clear();
            foreach (GoodReceivedNote good in list)
            {
                itStockIn it = new itStockIn(good);
                ds_phieunhap.Children.Add(it);
            }
        }
        private void LoadGoodReceivedNoteListByStartDateAndPrice()
        {
            List<GoodReceivedNote> list = GoodReceivedNoteDAO.Instance.LoadGoodReceivedNoteListByStartDateAndPrice(gara, startDate, minPrice, maxPrice);
            ds_phieunhap.Children.Clear();
            foreach (GoodReceivedNote good in list)
            {
                itStockIn it = new itStockIn(good);
                ds_phieunhap.Children.Add(it);
            }
        }
        private void LoadGoodReceivedNoteListByEndDateAndPrice()
        {
            List<GoodReceivedNote> list = GoodReceivedNoteDAO.Instance.LoadGoodReceivedNoteListByEndDateAndPrice(gara, endDate, minPrice, maxPrice);
            ds_phieunhap.Children.Clear();
            foreach (GoodReceivedNote good in list)
            {
                itStockIn it = new itStockIn(good);
                ds_phieunhap.Children.Add(it);
            }
        }

        private void LoadGoodReceivedNoteListBySupplierStartAndEndDate()
        {
            List<GoodReceivedNote> list = GoodReceivedNoteDAO.Instance.LoadGoodReceivedNoteListBySupplierStartAndEndDate(gara, supplier, startDate, endDate);
            ds_phieunhap.Children.Clear();
            foreach (GoodReceivedNote good in list)
            {
                itStockIn it = new itStockIn(good);
                ds_phieunhap.Children.Add(it);
            }
        }
        private void LoadGoodReceivedNoteListBySupplierStartDateAndPrice()
        {
            List<GoodReceivedNote> list = GoodReceivedNoteDAO.Instance.LoadGoodReceivedNoteListBySupplierStartDateAndPrice(gara, supplier, startDate, minPrice, maxPrice);
            ds_phieunhap.Children.Clear();
            foreach (GoodReceivedNote good in list)
            {
                itStockIn it = new itStockIn(good);
                ds_phieunhap.Children.Add(it);
            }
        }
        private void LoadGoodReceivedNoteListBySupplierEndDateAndPrice()
        {
            List<GoodReceivedNote> list = GoodReceivedNoteDAO.Instance.LoadGoodReceivedNoteListBySupplierEndDateAndPrice(gara, supplier, endDate, minPrice, maxPrice);
            ds_phieunhap.Children.Clear();
            foreach (GoodReceivedNote good in list)
            {
                itStockIn it = new itStockIn(good);
                ds_phieunhap.Children.Add(it);
            }
        }
        private void LoadGoodReceivedNoteListByStartEndDateAndPrice()
        {
            List<GoodReceivedNote> list = GoodReceivedNoteDAO.Instance.LoadGoodReceivedNoteListByStartEndDateAndPrice(gara, startDate, endDate, minPrice, maxPrice);
            ds_phieunhap.Children.Clear();
            foreach (GoodReceivedNote good in list)
            {
                itStockIn it = new itStockIn(good);
                ds_phieunhap.Children.Add(it);
            }
        }
        private void LoadGoodReceivedNoteListBySupplierStartEndDateAndPrice()
        {
            List<GoodReceivedNote> list = GoodReceivedNoteDAO.Instance.LoadGoodReceivedNoteListBySupplierStartEndDateAndPrice(gara, supplier, startDate, endDate, minPrice, maxPrice);
            ds_phieunhap.Children.Clear();
            foreach (GoodReceivedNote good in list)
            {
                itStockIn it = new itStockIn(good);
                ds_phieunhap.Children.Add(it);
            }
        }
        #endregion

        private void apply_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtb_findGRN.Text = "";
            if (ckb_startDate.IsChecked == true && dpk_startDate.SelectedDate == null)
            {
                MessageBox.Show("Vui lòng chọn ngày bắt đầu!");
                return;
            }
            if (ckb_endDate.IsChecked == true && dpk_endDate.SelectedDate == null)
            {
                MessageBox.Show("Vui lòng chọn ngày kết thúc!");
                return;
            }
            filterList();
            filter.Visibility = Visibility.Hidden;
            //


        }

        private void txtb_findGRN_TextChanged(object sender, TextChangedEventArgs e)
        {
            supplier = txtb_findGRN.Text;
            filterList();
        }

        private void dpk_startDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ckb_startDate.IsChecked = true;
        }

        private void dpk_endDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ckb_endDate.IsChecked = true;
        }

        private void rangeSlider_LowerValueChanged(object sender, RoutedEventArgs e)
        {
            ckb_price.IsChecked = true;
        }

        private void rangeSlider_HigherValueChanged(object sender, RoutedEventArgs e)
        {
            ckb_price.IsChecked = true;
        }

        private void filterList()
        {
            switch (filter_state())
            {
                case 0:
                    {
                        supplier = "";
                        startDate = "";
                        endDate = "";
                        minPrice = -1;
                        maxPrice = -1;
                        LoadListGoodReceivedNote();
                        break;
                    }
                case 1:
                    {
                        supplier = "";
                        startDate = "";
                        endDate = "";
                        minPrice = (int)rangeSlider.LowerValue;
                        maxPrice = (int)rangeSlider.HigherValue;
                        LoadGoodReceivedNoteListByPrice();
                        break;
                    }
                case 2:
                    {
                        supplier = "";
                        startDate = "";
                        endDate = dpk_endDate.SelectedDate.Value.ToString("MM/dd/yyyy");
                        minPrice = -1;
                        maxPrice = -1;
                        LoadGoodReceivedNoteListByEndDate();
                        break;
                    }
                case 3:
                    {
                        supplier = "";
                        startDate = "";
                        endDate = dpk_endDate.SelectedDate.Value.ToString("MM/dd/yyyy");
                        minPrice = (int)rangeSlider.LowerValue;
                        maxPrice = (int)rangeSlider.HigherValue;
                        LoadGoodReceivedNoteListByEndDateAndPrice();
                        break;
                    }
                case 4:
                    {
                        supplier = "";
                        startDate = dpk_startDate.SelectedDate.Value.ToString("MM/dd/yyyy");
                        endDate = "";
                        minPrice = -1;
                        maxPrice = -1;
                        LoadGoodReceivedNoteListByStartDate();
                        break;
                    }
                case 5:
                    {
                        supplier = "";
                        startDate = dpk_startDate.SelectedDate.Value.ToString("MM/dd/yyyy");
                        endDate = "";
                        minPrice = (int)rangeSlider.LowerValue;
                        maxPrice = (int)rangeSlider.HigherValue;
                        LoadGoodReceivedNoteListByStartDateAndPrice();
                        break;
                    }
                case 6:
                    {
                        supplier = "";
                        startDate = dpk_startDate.SelectedDate.Value.ToString("MM/dd/yyyy");
                        endDate = dpk_endDate.SelectedDate.Value.ToString("MM/dd/yyyy");
                        minPrice = -1;
                        maxPrice = -1;
                        LoadGoodReceivedNoteListByStartAndEndDate();
                        break;
                    }
                case 7:
                    {
                        supplier = "";
                        startDate = dpk_startDate.SelectedDate.Value.ToString("MM/dd/yyyy");
                        endDate = dpk_endDate.SelectedDate.Value.ToString("MM/dd/yyyy");
                        minPrice = (int)rangeSlider.LowerValue;
                        maxPrice = (int)rangeSlider.HigherValue;
                        LoadGoodReceivedNoteListByStartEndDateAndPrice();
                        break;
                    }
                case 8:
                    {
                        supplier = txtb_findGRN.Text;
                        startDate = "";
                        endDate = "";
                        minPrice = -1;
                        maxPrice = -1;
                        LoadListGoodReceivedNoteBySupplier();
                        break;
                    }
                case 9:
                    {
                        supplier = txtb_findGRN.Text;
                        startDate = "";
                        endDate = "";
                        minPrice = (int)rangeSlider.LowerValue;
                        maxPrice = (int)rangeSlider.HigherValue;
                        LoadGoodReceivedNoteListBySupplierAndPrice();
                        break;
                    }
                case 10:
                    {
                        supplier = txtb_findGRN.Text;
                        startDate = "";
                        endDate = dpk_endDate.SelectedDate.Value.ToString("MM/dd/yyyy");
                        minPrice = -1;
                        maxPrice = -1;
                        LoadGoodReceivedNoteListBySupplierAndEndDate();
                        break;
                    }
                case 11:
                    {
                        supplier = txtb_findGRN.Text;
                        startDate = "";
                        endDate = dpk_endDate.SelectedDate.Value.ToString("MM/dd/yyyy");
                        minPrice = (int)rangeSlider.LowerValue;
                        maxPrice = (int)rangeSlider.HigherValue;
                        LoadGoodReceivedNoteListBySupplierEndDateAndPrice();
                        break;
                    }
                case 12:
                    {
                        supplier = txtb_findGRN.Text;
                        startDate = dpk_startDate.SelectedDate.Value.ToString("MM/dd/yyyy");
                        endDate = "";
                        minPrice = -1;
                        maxPrice = -1;
                        LoadGoodReceivedNoteListBySupplierAndStartDate();
                        break;
                    }
                case 13:
                    {
                        supplier = txtb_findGRN.Text;
                        startDate = dpk_startDate.SelectedDate.Value.ToString("MM/dd/yyyy");
                        endDate = "";
                        minPrice = (int)rangeSlider.LowerValue;
                        maxPrice = (int)rangeSlider.HigherValue;
                        LoadGoodReceivedNoteListBySupplierStartDateAndPrice();
                        break;
                    }
                case 14:
                    {
                        supplier = txtb_findGRN.Text;
                        startDate = dpk_startDate.SelectedDate.Value.ToString("MM/dd/yyyy");
                        endDate = dpk_endDate.SelectedDate.Value.ToString("MM/dd/yyyy");
                        minPrice = -1;
                        maxPrice = -1;
                        LoadGoodReceivedNoteListBySupplierStartAndEndDate();
                        break;
                    }
                case 15:
                    {
                        supplier = txtb_findGRN.Text;
                        startDate = dpk_startDate.SelectedDate.Value.ToString("MM/dd/yyyy");
                        endDate = dpk_endDate.SelectedDate.Value.ToString("MM/dd/yyyy");
                        minPrice = (int)rangeSlider.LowerValue;
                        maxPrice = (int)rangeSlider.HigherValue;
                        LoadGoodReceivedNoteListBySupplierStartEndDateAndPrice();
                        break;
                    }
            }
        }

        private int filter_state()
        {
            int state;
            if (supplier == "")
            {
                if (ckb_startDate.IsChecked == false)
                {
                    if (ckb_endDate.IsChecked == false)
                    {
                        if (ckb_price.IsChecked == false)
                        {
                            return 0;
                        }
                        else
                        {
                            return 1;
                        }
                    }
                    else
                    {
                        if (ckb_price.IsChecked == false)
                        {
                            return 2;
                        }
                        else
                        {
                            return 3;
                        }
                    }
                }
                else
                {
                    if (ckb_endDate.IsChecked == false)
                    {
                        if (ckb_price.IsChecked == false)
                        {
                            return 4;
                        }
                        else
                        {
                            return 5;
                        }
                    }
                    else
                    {
                        if (ckb_price.IsChecked == false)
                        {
                            return 6;
                        }
                        else
                        {
                            return 7;
                        }
                    }
                }
            }
            else
            {
                if (ckb_startDate.IsChecked == false)
                {
                    if (ckb_endDate.IsChecked == false)
                    {
                        if (ckb_price.IsChecked == false)
                        {
                            return 8;
                        }
                        else
                        {
                            return 9;
                        }
                    }
                    else
                    {
                        if (ckb_price.IsChecked == false)
                        {
                            return 10;
                        }
                        else
                        {
                            return 11;
                        }
                    }
                }
                else
                {
                    if (ckb_endDate.IsChecked == false)
                    {
                        if (ckb_price.IsChecked == false)
                        {
                            return 12;
                        }
                        else
                        {
                            return 13;
                        }
                    }
                    else
                    {
                        if (ckb_price.IsChecked == false)
                        {
                            return 14;
                        }
                        else
                        {
                            return 15;
                        }
                    }
                }
            }
        }
    }
}
