using Gara_Management.DAO;
using Gara_Management.DTO;
using Gara_Management.GUI.Card;
using Gara_Management.GUI.Item;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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

namespace Gara_Management.GUI
{
    /// <summary>
    /// Interaction logic for scrRepairCard.xaml
    /// </summary>
    public partial class scrRepairCard : UserControl
    {
         Color color3 = (Color)ColorConverter.ConvertFromString("#5790AB");
        Color color4 = (Color)ColorConverter.ConvertFromString("#064469");
        Color color5 = (Color)ColorConverter.ConvertFromString("#072D44");
        public EventHandler changeToCarsScr;
        string gara;
        int minMoney = -1;
        int maxMoney = -1;
        string date = "";
        string endDate = "";
        bool t = false;
        public scrRepairCard(string gara)
        {
            InitializeComponent();
            this.gara = gara;
            dpk_date.SelectedDate = DateTime.Now;
            dpk_endDate.SelectedDate = DateTime.Now;
            rangeSlider.Maximum = RepairPaymentBillDAO.Instance.GetMaxTotalPayment(gara);
            ckb_date.IsChecked = false;
            ckb_endDate.IsChecked = false;
            ckb_money.IsChecked = false;
            t = true;
            LoadListRepair();
            txtb_isFinish.Text = "Chưa thanh toán: " + RepairPaymentBillDAO.Instance.LoadNotFinishedRepairPaymentBillList(gara);
        }

        private void bt_car_scr_MouseDown(object sender, MouseButtonEventArgs e)
        {
            changeToCarsScr?.Invoke(this, EventArgs.Empty);
        }
        private void bd_exit_MouseEnter(object sender, MouseEventArgs e)
        {
            bd_exit.Background = new SolidColorBrush(color4);
        }

        private void bd_exit_MouseLeave(object sender, MouseEventArgs e)
        {
            bd_exit.Background = new SolidColorBrush(color3);
        }

        private void bd_exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có muốn thoát ứng dụng?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // Kiểm tra xem người dùng đã chọn Yes hay không
            if (result == MessageBoxResult.Yes)
            {
                App.Current.Shutdown();
            }
        }

        private void bd_repairInvoice_MouseDown(object sender, MouseButtonEventArgs e)
        {
            crdRepair crdRepair = new crdRepair(gara);
            crdRepair.ShowDialog();
            LoadListRepair();
        }

        private void bd_filetr_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (filter.Visibility == Visibility.Hidden)
            {
                if (date == "")
                {
                    ckb_date.IsChecked = false;
                    ckb_endDate.IsChecked = false;
                    ckb_endDate.IsEnabled = false;
                    dpk_endDate.IsEnabled = false;
                }
                else
                {
                    ckb_date.IsChecked = true;
                    DateTime d = DateTime.Parse(date);
                    dpk_date.SelectedDate = d;
                    ckb_endDate.IsEnabled = true;
                    dpk_endDate.IsEnabled = true;
                    if (endDate == "")
                    {
                        ckb_endDate.IsChecked = false;
                    }
                    else
                    {
                        ckb_endDate.IsChecked = true;
                        dpk_endDate.SelectedDate = DateTime.Parse(endDate);
                    }
                }
                if (minMoney == -1 && maxMoney == -1)
                {
                    rangeSlider.LowerValue = rangeSlider.Minimum;
                    rangeSlider.HigherValue = rangeSlider.Maximum;
                    ckb_money.IsChecked = false;
                }
                else
                {
                    rangeSlider.LowerValue = minMoney;
                    rangeSlider.HigherValue = maxMoney;
                    ckb_money.IsChecked = true;
                }
                filter.Visibility = Visibility.Visible;
            }
            else
            {
                filter.Visibility = Visibility.Hidden;
            }
        }
        private void LoadListRepair()
        {
            List<RepairPaymentBill> list = RepairPaymentBillDAO.Instance.LoadRepairPaymentBillList(gara);
            ds_phieuTN.Children.Clear();
            foreach (RepairPaymentBill bill in list)
            {
                ReceptionForm recept = ReceptionFormDAO.Instance.LoadReceptionFormByID(bill.IDRec, gara);
                itRepairCard it = new itRepairCard(recept, bill);
                ds_phieuTN.Children.Add(it);
            }
        }
        // áp dụng lọc
        private void apply_MouseDown(object sender, MouseButtonEventArgs e)
        {
            filter.Visibility = Visibility.Hidden;
            txtb_findBill.Text = "";
            //
            if (ckb_date.IsChecked == true && ckb_endDate.IsChecked == false && ckb_money.IsChecked == false)
            {
                date = dpk_date.SelectedDate.ToString();
                endDate = "";
                minMoney = -1;
                maxMoney = -1;
                LoadListRepairByDate();
            }   
            else
            {
                if (ckb_date.IsChecked == false && ckb_endDate.IsChecked == false && ckb_money.IsChecked == true)
                {
                    minMoney = (int) rangeSlider.LowerValue;
                    maxMoney = (int)rangeSlider.HigherValue;
                    date = "";
                    endDate = "";
                    LoadListRepairByMoney();
                }   
                else
                {
                    if (ckb_date.IsChecked == true && ckb_endDate.IsChecked == true && ckb_money.IsChecked == false)
                    {
                        date = dpk_date.SelectedDate.ToString();
                        endDate = dpk_endDate.SelectedDate.ToString();
                        minMoney = -1;
                        maxMoney = -1;
                        LoadListRepairByDate2();
                    }   
                    else
                    {
                        if (ckb_date.IsChecked == true && ckb_endDate.IsChecked == false && ckb_money.IsChecked == true)
                        {
                            date = dpk_date.SelectedDate.ToString();
                            endDate = "";
                            minMoney = (int)rangeSlider.LowerValue;
                            maxMoney = (int)rangeSlider.HigherValue;
                            LoadListRepairByDateAndMoney();
                        }   
                        else
                        {
                            if (ckb_date.IsChecked == true && ckb_endDate.IsChecked == true && ckb_money.IsChecked == true)
                            {
                                date = dpk_date.SelectedDate.ToString();
                                endDate = dpk_endDate.SelectedDate.ToString();
                                minMoney = (int)rangeSlider.LowerValue;
                                maxMoney = (int)rangeSlider.HigherValue;
                                LoadListRepairByDate2AndMoney();
                            }   
                            else
                            {
                                if (ckb_date.IsChecked == false && ckb_endDate.IsChecked == false && ckb_money.IsChecked == false)
                                {
                                    date = "";
                                    endDate = "";
                                    minMoney = -1;
                                    maxMoney = -1;
                                    LoadListRepair();
                                }    
                            }    
                        }    
                    }    
                }    
            }    


        }
        private void LoadListRepairByNumberPlate()
        {
            List<RepairPaymentBill> list = RepairPaymentBillDAO.Instance.LoadRepairPaymentBillListByNumberPlate(gara, txtb_findBill.Text);
            ds_phieuTN.Children.Clear();
            foreach (RepairPaymentBill bill in list)
            {
                ReceptionForm recept = ReceptionFormDAO.Instance.LoadReceptionFormByID(bill.IDRec, gara);
                itRepairCard it = new itRepairCard(recept, bill);
                ds_phieuTN.Children.Add(it);
            }
        }
        private void LoadListRepairByDate()
        {
            DateTime date1 = DateTime.Parse(date);
            List<RepairPaymentBill> list = RepairPaymentBillDAO.Instance.LoadRepairPaymentBillListByDate(gara, date1.ToString("dd/MM/yyyy"));
            ds_phieuTN.Children.Clear();
            foreach (RepairPaymentBill bill in list)
            {
                ReceptionForm recept = ReceptionFormDAO.Instance.LoadReceptionFormByID(bill.IDRec, gara);
                itRepairCard it = new itRepairCard(recept, bill);
                ds_phieuTN.Children.Add(it);
            }
        }
        private void LoadListRepairByDateAndNumberPlate()
        {
            DateTime date1 = DateTime.Parse(date);
            List<RepairPaymentBill> list = RepairPaymentBillDAO.Instance.LoadRepairPaymentBillListByDateAndNumberPlate(gara, 
                date1.ToString("dd/MM/yyyy"), txtb_findBill.Text);
            ds_phieuTN.Children.Clear();
            foreach (RepairPaymentBill bill in list)
            {
                ReceptionForm recept = ReceptionFormDAO.Instance.LoadReceptionFormByID(bill.IDRec, gara);
                itRepairCard it = new itRepairCard(recept, bill);
                ds_phieuTN.Children.Add(it);
            }
        }

        private void LoadListRepairByMoney()
        {
            List<RepairPaymentBill> list = RepairPaymentBillDAO.Instance.LoadRepairPaymentBillListByMoney(gara, minMoney, maxMoney);
            ds_phieuTN.Children.Clear();
            foreach (RepairPaymentBill bill in list)
            {
                ReceptionForm recept = ReceptionFormDAO.Instance.LoadReceptionFormByID(bill.IDRec, gara);
                itRepairCard it = new itRepairCard(recept, bill);
                ds_phieuTN.Children.Add(it);
            }
        }

        private void LoadListRepairByMoneyAndNumberPlate()
        {
            List<RepairPaymentBill> list = RepairPaymentBillDAO.Instance.LoadRepairPaymentBillListByMoneyAndNumberPlate(gara,
                minMoney, maxMoney, txtb_findBill.Text);
            ds_phieuTN.Children.Clear();
            foreach (RepairPaymentBill bill in list)
            {
                ReceptionForm recept = ReceptionFormDAO.Instance.LoadReceptionFormByID(bill.IDRec, gara);
                itRepairCard it = new itRepairCard(recept, bill);
                ds_phieuTN.Children.Add(it);
            }
        }
        private void LoadListRepairByDate2()
        {
            DateTime date1 = DateTime.Parse(date);
            DateTime date2 = DateTime.Parse(endDate);
            List<RepairPaymentBill> list = RepairPaymentBillDAO.Instance.LoadRepairPaymentBillListByDate2(gara, 
                date1.ToString("dd/MM/yyyy"), date2.ToString("dd/MM/yyyy"));
            ds_phieuTN.Children.Clear();
            foreach (RepairPaymentBill bill in list)
            {
                ReceptionForm recept = ReceptionFormDAO.Instance.LoadReceptionFormByID(bill.IDRec, gara);
                itRepairCard it = new itRepairCard(recept, bill);
                ds_phieuTN.Children.Add(it);
            }
        }
        private void LoadListRepairByDate2AndNumberPlate()
        {
            DateTime date1 = DateTime.Parse(date);
            DateTime date2 = DateTime.Parse(endDate);
            List<RepairPaymentBill> list = RepairPaymentBillDAO.Instance.LoadRepairPaymentBillListByDate2AndNumberPlate(gara,
                date1.ToString("dd/MM/yyyy"), date2.ToString("dd/MM/yyyy"), endDate);
            ds_phieuTN.Children.Clear();
            foreach (RepairPaymentBill bill in list)
            {
                ReceptionForm recept = ReceptionFormDAO.Instance.LoadReceptionFormByID(bill.IDRec, gara);
                itRepairCard it = new itRepairCard(recept, bill);
                ds_phieuTN.Children.Add(it);
            }
        }
        private void LoadListRepairByDateAndMoney()
        {
            DateTime date1 = DateTime.Parse(date);
            List<RepairPaymentBill> list = RepairPaymentBillDAO.Instance.LoadRepairPaymentBillListByDateAndMoney(gara, date1.ToString("dd/MM/yyyy")
                , minMoney, maxMoney);
            ds_phieuTN.Children.Clear();
            foreach (RepairPaymentBill bill in list)
            {
                ReceptionForm recept = ReceptionFormDAO.Instance.LoadReceptionFormByID(bill.IDRec, gara);
                itRepairCard it = new itRepairCard(recept, bill);
                ds_phieuTN.Children.Add(it);
            }
        }

        private void LoadListRepairByDateMoneyAndNumberPlate()
        {
            DateTime date1 = DateTime.Parse(date);
            List<RepairPaymentBill> list = RepairPaymentBillDAO.Instance.LoadRepairPaymentBillListByDateMoneyAndNumberPlate(gara,
                date1.ToString("dd/MM/yyyy"), minMoney, maxMoney, txtb_findBill.Text);
            ds_phieuTN.Children.Clear();
            foreach (RepairPaymentBill bill in list)
            {
                ReceptionForm recept = ReceptionFormDAO.Instance.LoadReceptionFormByID(bill.IDRec, gara);
                itRepairCard it = new itRepairCard(recept, bill);
                ds_phieuTN.Children.Add(it);
            }
        }
        private void LoadListRepairByDate2AndMoney()
        {
            DateTime date1 = DateTime.Parse(date);
            DateTime date2 = DateTime.Parse(endDate);
            List<RepairPaymentBill> list = RepairPaymentBillDAO.Instance.LoadRepairPaymentBillListByDate2AndMoney(gara, 
                date1.ToString("dd/MM/yyyy"), date2.ToString("dd/MM/yyyy"), minMoney, maxMoney);
            ds_phieuTN.Children.Clear();
            foreach (RepairPaymentBill bill in list)
            {
                ReceptionForm recept = ReceptionFormDAO.Instance.LoadReceptionFormByID(bill.IDRec, gara);
                itRepairCard it = new itRepairCard(recept, bill);
                ds_phieuTN.Children.Add(it);
            }
        }
        private void LoadListRepairByDate2MoneyAndNumberPlate()
        {
            DateTime date1 = DateTime.Parse(date);
            DateTime date2 = DateTime.Parse(endDate);
            List<RepairPaymentBill> list = RepairPaymentBillDAO.Instance.LoadRepairPaymentBillListByDate2MoneyAndNumberPlate(gara,
                date1.ToString("dd/MM/yyyy"), date2.ToString("dd/MM/yyyy"), minMoney, maxMoney, txtb_findBill.Text);
            ds_phieuTN.Children.Clear();
            foreach (RepairPaymentBill bill in list)
            {
                ReceptionForm recept = ReceptionFormDAO.Instance.LoadReceptionFormByID(bill.IDRec, gara);
                itRepairCard it = new itRepairCard(recept, bill);
                ds_phieuTN.Children.Add(it);
            }
        }

        private void txtb_findBill_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtb_findBill.Text == "")
            {
                if (ckb_date.IsChecked == true && ckb_endDate.IsChecked == false && ckb_money.IsChecked == false)
                {
                    LoadListRepairByDate();
                }
                else
                {
                    if (ckb_date.IsChecked == false && ckb_endDate.IsChecked == false && ckb_money.IsChecked == true)
                    {
                        LoadListRepairByMoney();
                    }
                    else
                    {
                        if (ckb_date.IsChecked == true && ckb_endDate.IsChecked == true && ckb_money.IsChecked == false)
                        {
                            LoadListRepairByDate2();
                        }
                        else
                        {
                            if (ckb_date.IsChecked == true && ckb_endDate.IsChecked == false && ckb_money.IsChecked == true)
                            {
                                LoadListRepairByDateAndMoney();
                            }
                            else
                            {
                                if (ckb_date.IsChecked == true && ckb_endDate.IsChecked == true && ckb_money.IsChecked == true)
                                {
                                    LoadListRepairByDate2AndMoney();
                                }
                                else
                                {
                                    if (ckb_date.IsChecked == false && ckb_endDate.IsChecked == false && ckb_money.IsChecked == false)
                                    {
                                        LoadListRepair();
                                    }
                                }
                            }
                        }
                    }
                }
            }    
            else
            {
                if (ckb_date.IsChecked == true && ckb_endDate.IsChecked == false && ckb_money.IsChecked == false)
                {
                    LoadListRepairByDateAndNumberPlate();
                }
                else
                {
                    if (ckb_date.IsChecked == false && ckb_endDate.IsChecked == false && ckb_money.IsChecked == true)
                    {
                        LoadListRepairByMoneyAndNumberPlate();
                    }
                    else
                    {
                        if (ckb_date.IsChecked == true && ckb_endDate.IsChecked == true && ckb_money.IsChecked == false)
                        {
                            LoadListRepairByDate2AndNumberPlate();
                        }
                        else
                        {
                            if (ckb_date.IsChecked == true && ckb_endDate.IsChecked == false && ckb_money.IsChecked == true)
                            {
                                LoadListRepairByDateMoneyAndNumberPlate();
                            }
                            else
                            {
                                if (ckb_date.IsChecked == true && ckb_endDate.IsChecked == true && ckb_money.IsChecked == true)
                                {
                                    LoadListRepairByDate2MoneyAndNumberPlate();
                                }
                                else
                                {
                                    if (ckb_date.IsChecked == false && ckb_endDate.IsChecked == false && ckb_money.IsChecked == false)
                                    {
                                        LoadListRepairByNumberPlate();
                                    }
                                }
                            }
                        }
                    }
                }
            }    
        }

        private void rangeSlider_LowerValueChanged(object sender, RoutedEventArgs e)
        {
            ckb_money.IsChecked = true;
        }

        private void dpk_date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ckb_date.IsChecked = true;
            ckb_endDate.IsEnabled = true;
            dpk_endDate.IsEnabled = true;

        }

        private void ckb_date_Click(object sender, RoutedEventArgs e)
        {
            if (ckb_date.IsChecked == true)
            {
                ckb_endDate.IsEnabled = true;
                dpk_endDate.IsEnabled = true;
            }  
            else
            {
                ckb_endDate.IsEnabled = false;
                ckb_endDate.IsChecked = false;
                dpk_endDate.IsEnabled = false;
            }    
        }

        private void dpk_date_SelectedDateChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (DateTime.Parse(dpk_endDate.SelectedDate.ToString()) <= DateTime.Parse(dpk_date.SelectedDate.ToString()) && t)
            {
                MessageBox.Show("Ngày kết thúc phải lớn hơn ngày bắt đầu", "Thông báo");
            }
            else
            {
                ckb_endDate.IsChecked = true;
            }
        }
    }
}
