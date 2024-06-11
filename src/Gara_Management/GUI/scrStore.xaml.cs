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

namespace Gara_Management.GUI
{
    /// <summary>
    /// Interaction logic for scrStore.xaml
    /// </summary>
    public partial class scrStore : UserControl
    {
        Color color3 = (Color)ColorConverter.ConvertFromString("#5790AB");
        Color color4 = (Color)ColorConverter.ConvertFromString("#064469");

        string gara;
        Account acc;
        int minQuan = -1, maxQuan = -1;
        int minPrice = -1, maxPrice = -1;
        public EventHandler changeToStockInScr;
        public scrStore(string gara, Account account)
        {
            InitializeComponent();
            this.gara = gara;
            this.acc = account;
            LoadListComponent();
            SetSliderMaxValue();
            txtb_total.Text = "Tổng loại vật tư: " + CarComponentDAO.Instance.LoadCarComponentList(gara).Count;
            txtb_zero.Text = "Số vật tư đã hết: " + CarComponentDAO.Instance.LoadCarComponentListZero(gara).Count;
            if (acc.AccAuthorization)
            {
                bd_summarize.Visibility = Visibility.Collapsed;
            }    
            else
            {
                bd_summarize.Visibility = Visibility.Visible;
            }    
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

        // chuyển sang màn hình phiếu nhập kho
        private void bd_stockIn_scr_MouseDown(object sender, MouseButtonEventArgs e)
        {
            changeToStockInScr?.Invoke(this, EventArgs.Empty);
        }

        // tạo phiếu nhập kho
        private void bd_stockIn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            crdStockIn crdStockIn = new crdStockIn(gara, acc);
            crdStockIn.ShowDialog();
            LoadListComponent();
        }
        // lọc
        private void bd_filter_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (filter.Visibility == Visibility.Hidden)
            {
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
                if (minQuan == -1 && maxQuan == -1)
                {
                    rangeSlider2.LowerValue = rangeSlider2.Minimum;
                    rangeSlider2.HigherValue = rangeSlider2.Maximum;
                    ckb_quantity.IsChecked = false;
                }
                else
                {
                    rangeSlider2.LowerValue = minQuan;
                    rangeSlider2.HigherValue = maxQuan;
                    ckb_quantity.IsChecked = true;
                }
                filter.Visibility = Visibility.Visible;
            }
            else
            {
                filter.Visibility = Visibility.Hidden;
            }
        }
        private void apply_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtb_findComponent.Text = "";
            if (ckb_quantity.IsChecked == true && ckb_price.IsChecked == false)
            {
                minPrice = -1;
                maxPrice = -1;
                minQuan = (int)rangeSlider2.LowerValue;
                maxQuan = (int)rangeSlider2.HigherValue;
                LoadCarComponentListByQuantity();
            }
            else
            {
                if (ckb_quantity.IsChecked == false && ckb_price.IsChecked == true)
                {
                    minQuan = -1;
                    maxQuan = -1;
                    minPrice = (int)rangeSlider.LowerValue;
                    maxPrice = (int)rangeSlider.HigherValue;
                    LoadCarComponentListByPrice();
                }
                else
                {
                    if (ckb_quantity.IsChecked == true && ckb_price.IsChecked == true)
                    {
                        minQuan = (int)rangeSlider2.LowerValue;
                        maxQuan = (int)rangeSlider2.HigherValue;
                        minPrice = (int)rangeSlider.LowerValue;
                        maxPrice = (int)rangeSlider.HigherValue;
                        LoadCarComponentListByQuantityAndPrice();
                    }
                    else
                    {
                        minQuan = -1;
                        maxQuan = -1;
                        minPrice = -1;
                        maxPrice = -1;
                        LoadListComponent();
                    }
                }
            }
            filter.Visibility = Visibility.Hidden;
            //


        }

        private void SetSliderMaxValue()
        {
            int quantity = 0;
            decimal price = 0;
            List<CarComponent> list = CarComponentDAO.Instance.LoadCarComponentList(gara);
            foreach (CarComponent car in list)
            {
                if (car.CurPrice > price)
                    price = car.CurPrice;
                if (car.ComQuantity > quantity)
                    quantity = car.ComQuantity;
                rangeSlider.Maximum = Convert.ToDouble(price);
                rangeSlider2.Maximum = quantity;
            }
        }
        private void rangeSlider_HigherValueChanged(object sender, RoutedEventArgs e)
        {
            ckb_price.IsChecked = true;
        }

        private void rangeSlider_LowerValueChanged(object sender, RoutedEventArgs e)
        {
            ckb_price.IsChecked = true;
        }

        private void rangeSlider2_HigherValueChanged(object sender, RoutedEventArgs e)
        {
            ckb_quantity.IsChecked = true;
        }

        private void rangeSlider2_LowerValueChanged(object sender, RoutedEventArgs e)
        {
            ckb_quantity.IsChecked = true;
        }

        private void LoadListComponent()
        {
            List<CarComponent> list = CarComponentDAO.Instance.LoadCarComponentList(gara);
            ds_phutung.Children.Clear();
            foreach (CarComponent car in list)
            {
                itSupplies it = new itSupplies(car, gara, acc);
                ds_phutung.Children.Add(it);
            }
        }

        private void LoadListComponentByName()
        {
            List<CarComponent> list = CarComponentDAO.Instance.LoadCarComponentListByName(gara, txtb_findComponent.Text);
            ds_phutung.Children.Clear();
            foreach (CarComponent car in list)
            {
                itSupplies it = new itSupplies(car, gara, acc);
                ds_phutung.Children.Add(it);
            }
        }

        private void LoadCarComponentListByQuantity()
        {
            List<CarComponent> list = CarComponentDAO.Instance.LoadCarComponentListByQuantity(gara, minQuan, maxQuan);
            ds_phutung.Children.Clear();
            foreach (CarComponent car in list)
            {
                itSupplies it = new itSupplies(car, gara, acc);
                ds_phutung.Children.Add(it);
            }
        }

        private void LoadCarComponentListByPrice()
        {
            List<CarComponent> list = CarComponentDAO.Instance.LoadCarComponentListByPrice(gara, minPrice, maxPrice);
            ds_phutung.Children.Clear();
            foreach (CarComponent car in list)
            {
                itSupplies it = new itSupplies(car, gara, acc);
                ds_phutung.Children.Add(it);
            }
        }

        private void LoadCarComponentListByQuantityAndPrice()
        {
            List<CarComponent> list = CarComponentDAO.Instance.LoadCarComponentListByQuantityAndPrice(gara, minQuan, maxQuan, minPrice, maxPrice);
            ds_phutung.Children.Clear();
            foreach (CarComponent car in list)
            {
                itSupplies it = new itSupplies(car, gara, acc);
                ds_phutung.Children.Add(it);
            }
        }

        private void LoadCarComponentListByNameAndQuantity()
        {
            List<CarComponent> list = CarComponentDAO.Instance.LoadCarComponentListByNameAndQuantity(gara, txtb_findComponent.Text, minQuan, maxQuan);
            ds_phutung.Children.Clear();
            foreach (CarComponent car in list)
            {
                itSupplies it = new itSupplies(car, gara, acc);
                ds_phutung.Children.Add(it);
            }
        }

        private void LoadCarComponentListByNameAndPrice()
        {
            List<CarComponent> list = CarComponentDAO.Instance.LoadCarComponentListByNameAndPrice(gara, txtb_findComponent.Text, minPrice, maxPrice);
            ds_phutung.Children.Clear();
            foreach (CarComponent car in list)
            {
                itSupplies it = new itSupplies(car, gara, acc);
                ds_phutung.Children.Add(it);
            }
        }

        private void bd_summarize_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DateTime now = DateTime.Today;
            DateTime startMonth = new DateTime(now.Year, now.Month, 1);
            DateTime endMonth = startMonth.AddMonths(1).AddDays(-1);
            if (now != startMonth && now !=endMonth)
            {
                MessageBox.Show("Hôm nay không thể tổng kết.", "Thông báo");
            }    
            else
            {
                if (now == startMonth)
                {
                    if (InventoryReportDAO.Instance.CheckExistBeginingInventory(gara))
                    {
                        MessageBox.Show("Đã tổng kết tồn đầu tháng này.", "Thông báo");
                    }    
                    else
                    {
                        List<CarComponent> list = CarComponentDAO.Instance.LoadCarComponentList(gara);
                        foreach (CarComponent com in list)
                        {
                            InventoryReportDAO.Instance.SummarizeBeginingInventory(gara, com.IDCom, com.ComQuantity);
                        }
                        MessageBox.Show("Tổng kết tồn đầu của tháng này.", "Thông báo");
                    }    
                }   
                else
                {
                    if (InventoryReportDAO.Instance.CheckExistEndingInventory(gara))
                    {
                        MessageBox.Show("Đã tổng kết tồn đầu tháng này.", "Thông báo");
                    }
                    else
                    {
                        List<CarComponent> list = CarComponentDAO.Instance.LoadCarComponentList(gara);
                        foreach (CarComponent com in list)
                        {
                            InventoryReportDAO.Instance.SummarizeEndingInventory(gara, com.IDCom, com.ComQuantity);
                        }
                        MessageBox.Show("Tổng kết tồn đầu của tháng này.", "Thông báo");
                    }
                }    
            }    
        }

        private void LoadCarComponentListByNamePriceAndQuantity()
        {
            List<CarComponent> list = CarComponentDAO.Instance.LoadCarComponentListByNamePriceAndQuantity(gara, txtb_findComponent.Text, minPrice, maxPrice, minQuan, maxQuan);
            ds_phutung.Children.Clear();
            foreach (CarComponent car in list)
            {
                itSupplies it = new itSupplies(car, gara, acc);
                ds_phutung.Children.Add(it);
            }
        }

        private void txtb_findComponent_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtb_findComponent.Text == "")
            {
                if (ckb_quantity.IsChecked == true && ckb_price.IsChecked == false)
                {
                    LoadCarComponentListByQuantity();
                }
                else
                {
                    if (ckb_quantity.IsChecked == false && ckb_price.IsChecked == true)
                    {
                        LoadCarComponentListByPrice();
                    }
                    else
                    {
                        if (ckb_quantity.IsChecked == true && ckb_price.IsChecked == true)
                        {
                            LoadCarComponentListByQuantityAndPrice();
                        }
                        else
                        {
                            LoadListComponent();
                        }
                    }
                }
            }
            else
            {
                if (ckb_quantity.IsChecked == true && ckb_price.IsChecked == false)
                {
                    LoadCarComponentListByNameAndQuantity();
                }
                else
                {
                    if (ckb_quantity.IsChecked == false && ckb_price.IsChecked == true)
                    {
                        LoadCarComponentListByNameAndPrice();
                    }
                    else
                    {
                        if (ckb_quantity.IsChecked == true && ckb_price.IsChecked == true)
                        {
                            LoadCarComponentListByNamePriceAndQuantity();
                        }
                        else
                        {
                            LoadListComponentByName();
                        }
                    }
                }
            }
        }
    }
}
