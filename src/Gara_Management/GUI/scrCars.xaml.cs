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
    /// Interaction logic for scrCars.xaml
    /// </summary>
    public partial class scrCars : UserControl
    {
        Color color3 = (Color)ColorConverter.ConvertFromString("#5790AB");
        Color color4 = (Color)ColorConverter.ConvertFromString("#064469");
        Color color5 = (Color)ColorConverter.ConvertFromString("#072D44");



        public event EventHandler changeToRepairCardScr;
        string gara;
        Account account;
        string brand = "";
        int maxDebt = -1;
        int minDebt = -1;
        public scrCars(string gara, Account account)
        {
            InitializeComponent();
            rangeSlider.Maximum = ReceptionFormDAO.Instance.GetMaxDebt(gara);
            ckb_debt.IsChecked = false;
            this.gara = gara;
            this.account = account;
            LoadListReceipt();
            LoadCarBrand();
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
            //MessageBoxResult result = MessageBox.Show("Bạn có muốn thoát ứng dụng?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);

            //// Kiểm tra xem người dùng đã chọn Yes hay không
            //if (result == MessageBoxResult.Yes)
            //{
                App.Current.Shutdown();
            //}
        }

        // nút chuyển màn hình sang danh sách phiếu sửa chữa
        private void bt_repair_card_scr_MouseDown(object sender, MouseButtonEventArgs e)
        {
            changeToRepairCardScr?.Invoke(this, EventArgs.Empty);
          
        }

        private void bd_acceptCar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // tiếp nhận sửa chửa (giống bên home)
            crdAccept newCar = new crdAccept(gara);
            newCar.ShowDialog();
        }

        private void bd_filter_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (minDebt == -1 && maxDebt == -1)
            {
                rangeSlider.LowerValue = rangeSlider.Minimum;
                rangeSlider.HigherValue = rangeSlider.Maximum;
                ckb_debt.IsChecked = false;                
            }
            else
            {
                rangeSlider.LowerValue = minDebt;
                rangeSlider.HigherValue = maxDebt;
                ckb_debt.IsChecked = true;               
            }
            if (brand == "")
            {
                cbx_carBrand.SelectedItem = null;
                ckb_carBrand.IsChecked = false;
            }
            else
            {
                cbx_carBrand.SelectedItem = brand;
                ckb_carBrand.IsChecked = true;
            }
            filter.Visibility = Visibility.Visible;
        }
        private void LoadListReceipt()
        {
            List<ReceptionForm> list = ReceptionFormDAO.Instance.LoadReceptionFormtList(gara);
            ds_acc.Children.Clear();
            int i = 1;
            foreach (ReceptionForm item in list)
            {
                Customer cus = CustomerDAO.Instance.LoadCustomerByID(item.IDCus, gara);
                itCar itCar1 = new itCar(gara, account, cus, item.IDRec, i++);
                ds_acc.Children.Add(itCar1);

            }
        }
        private void LoadListReceiptByName()
        {
            List<ReceptionForm> list = ReceptionFormDAO.Instance.LoadReceptionFormtListByNameCus(gara, txtb_findCar.Text);
            ds_acc.Children.Clear();
            int i = 1;
            foreach (ReceptionForm item in list)
            {
                Customer cus = CustomerDAO.Instance.LoadCustomerByID(item.IDCus, gara);
                itCar itCar1 = new itCar(gara, account, cus, item.IDRec, i++);
                ds_acc.Children.Add(itCar1);

            }
        }

        private void LoadListReceiptByCarBrand()
        {
            string brand = CarBrandDAO.Instance.GetIDBrandByName(cbx_carBrand.SelectedItem.ToString(), gara);
            List<ReceptionForm> list = ReceptionFormDAO.Instance.LoadReceptionFormtListByCarBrand(gara, brand);
            ds_acc.Children.Clear();
            int i = 1;
            foreach (ReceptionForm item in list)
            {
                Customer cus = CustomerDAO.Instance.LoadCustomerByID(item.IDCus, gara);
                itCar itCar1 = new itCar(gara, account, cus, item.IDRec, i++);
                ds_acc.Children.Add(itCar1);

            }
        }
        private void LoadListReceiptByDebt()
        {
            List<ReceptionForm> list = ReceptionFormDAO.Instance.LoadReceptionFormtListByDebt(gara, minDebt, maxDebt);
            ds_acc.Children.Clear();
            int i = 1;
            foreach (ReceptionForm item in list)
            {
                Customer cus = CustomerDAO.Instance.LoadCustomerByID(item.IDCus, gara);
                itCar itCar1 = new itCar(gara, account, cus, item.IDRec, i++);
                ds_acc.Children.Add(itCar1);

            }
        }
        private void LoadListReceiptByBrandAndDebt()
        {
            string brand = CarBrandDAO.Instance.GetIDBrandByName(cbx_carBrand.SelectedItem.ToString(), gara);
            List<ReceptionForm> list = ReceptionFormDAO.Instance.LoadReceptionFormtListByCarBrandAndDebt(gara,brand, minDebt, maxDebt);
            ds_acc.Children.Clear();
            int i = 1;
            foreach (ReceptionForm item in list)
            {
                Customer cus = CustomerDAO.Instance.LoadCustomerByID(item.IDCus, gara);
                itCar itCar1 = new itCar(gara, account, cus, item.IDRec, i++);
                ds_acc.Children.Add(itCar1);

            }
        }
        private void LoadListReceiptByNameBrandAndDebt()
        {
            string brand = CarBrandDAO.Instance.GetIDBrandByName(cbx_carBrand.SelectedItem.ToString(), gara);
            List<ReceptionForm> list = ReceptionFormDAO.Instance.LoadReceptionFormtListByCusCarBrandAndDebt(gara,
                txtb_findCar.Text,brand, minDebt, maxDebt);
            ds_acc.Children.Clear();
            int i = 1;
            foreach (ReceptionForm item in list)
            {
                Customer cus = CustomerDAO.Instance.LoadCustomerByID(item.IDCus, gara);
                itCar itCar1 = new itCar(gara, account, cus, item.IDRec, i++);
                ds_acc.Children.Add(itCar1);

            }
        }
        private void LoadListReceiptByNameAndDebt()
        {
            List<ReceptionForm> list = ReceptionFormDAO.Instance.LoadReceptionFormtListByCusAndDebt(gara,
                txtb_findCar.Text, minDebt, maxDebt);
            ds_acc.Children.Clear();
            int i = 1;
            foreach (ReceptionForm item in list)
            {
                Customer cus = CustomerDAO.Instance.LoadCustomerByID(item.IDCus, gara);
                itCar itCar1 = new itCar(gara, account, cus, item.IDRec, i++);
                ds_acc.Children.Add(itCar1);

            }
        }
        private void LoadListReceiptByNameAndBrand()
        {
            string brand = CarBrandDAO.Instance.GetIDBrandByName(cbx_carBrand.SelectedItem.ToString(), gara);
            List<ReceptionForm> list = ReceptionFormDAO.Instance.LoadReceptionFormtListByCusAndCarBrand(gara,
                txtb_findCar.Text, brand);
            ds_acc.Children.Clear();
            int i = 1;
            foreach (ReceptionForm item in list)
            {
                Customer cus = CustomerDAO.Instance.LoadCustomerByID(item.IDCus, gara);
                itCar itCar1 = new itCar(gara, account, cus, item.IDRec, i++);
                ds_acc.Children.Add(itCar1);

            }
        }
        private void filter_LostFocus(object sender, RoutedEventArgs e)
        {
            
        }

        // áp dụng lọc
        private void apply_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtb_findCar.Text = "";
            if (ckb_carBrand.IsChecked == true && ckb_debt.IsChecked == false)
            {
                if (cbx_carBrand.SelectedItem == null)
                {
                    MessageBox.Show("Hãy chọn một hiệu xe cần lọc.");
                }
                else
                {
                    brand = cbx_carBrand.SelectedItem.ToString();
                    maxDebt = -1;
                    minDebt = -1;
                    LoadListReceiptByCarBrand();
                }
            }    
            else
            {
                if (ckb_carBrand.IsChecked == false && ckb_debt.IsChecked == true)
                {
                    brand = "";
                    maxDebt = (int)rangeSlider.HigherValue;
                    minDebt = (int) rangeSlider.LowerValue;
                    LoadListReceiptByDebt();
                }   
                else
                {
                    if (ckb_carBrand.IsChecked == true && ckb_debt.IsChecked == true)
                    {
                        if (cbx_carBrand.SelectedItem == null)
                        {
                            MessageBox.Show("Hãy chọn một hiệu xe cần lọc.");
                        }
                        else
                        {
                            brand = cbx_carBrand.SelectedItem.ToString();
                            maxDebt = (int)rangeSlider.HigherValue;
                            minDebt = (int)rangeSlider.LowerValue;
                            LoadListReceiptByBrandAndDebt();
                        }
                    }   
                    else
                    {
                        brand = "";
                        maxDebt = -1;
                        minDebt = -1;
                        LoadListReceipt();
                    }    
                }    
            }    
            filter.Visibility = Visibility.Hidden;
            //


        }
        private void LoadCarBrand()
        {
            List<CarBrand> carBrands = CarBrandDAO.Instance.LoadCarBrandList(gara);
            cbx_carBrand.Items.Clear();
            foreach (CarBrand item in carBrands)
            {
                cbx_carBrand.Items.Add(item.NameBrand);
            }
        }


        private void txtb_findCar_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtb_findCar_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if (txtb_findCar.Text == "")
            {
                if (ckb_carBrand.IsChecked == true && ckb_debt.IsChecked == false)
                {
                    LoadListReceiptByCarBrand();
                }
                else
                {
                    if (ckb_carBrand.IsChecked == false && ckb_debt.IsChecked == true)
                    {
                        LoadListReceiptByDebt();
                    }
                    else
                    {
                        if (ckb_carBrand.IsChecked == true && ckb_debt.IsChecked == true)
                        {
                            LoadListReceiptByBrandAndDebt();
                        }
                        else
                        {
                            LoadListReceipt();
                        }
                    }
                }
            }
            else
            {
                if (ckb_carBrand.IsChecked == true && ckb_debt.IsChecked == false)
                {
                    LoadListReceiptByNameAndBrand();
                }
                else
                {
                    if (ckb_carBrand.IsChecked == false && ckb_debt.IsChecked == true)
                    {
                        LoadListReceiptByNameAndDebt();
                    }
                    else
                    {
                        if (ckb_carBrand.IsChecked == true && ckb_debt.IsChecked == true)
                        {
                            LoadListReceiptByNameBrandAndDebt();
                        }
                        else
                        {
                            LoadListReceiptByName();
                        }
                    }
                }
            }
        }

        private void cbx_carBrand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ckb_carBrand.IsChecked = true;
        }

        private void rangeSlider_HigherValueChanged(object sender, RoutedEventArgs e)
        {
            ckb_debt.IsChecked = true;
        }

        private void rangeSlider_LowerValueChanged(object sender, RoutedEventArgs e)
        {
            ckb_debt.IsChecked = true;
        }

        private void btn_carbrand_MouseDown(object sender, MouseButtonEventArgs e)
        {
            cardCarBrand cardCarBrand = new cardCarBrand();
            cardCarBrand.ShowDialog();
        }
    }
}
