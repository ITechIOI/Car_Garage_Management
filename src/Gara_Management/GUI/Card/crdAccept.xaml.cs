using Gara_Management.DAO;
using Gara_Management.DTO;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Gara_Management.GUI.Card
{
    /// <summary>
    /// Interaction logic for crdAccept.xaml
    /// </summary>
    public partial class crdAccept : Window
    {
        private ReceptionForm receptionForm; /*biến lưu trữ thông tin của phiếu tiếp nhận*/
        private ReceptionForm check = null; /*biến kiểm tra các thông tin đã thay đổi hay chưa*/
        private bool isChanged = false;  /*biến kiểm tra các thông tin đã thay đổi hay chưa*/
        string gara;

        // khi khởi tạo tự động có mã phiếu ( với phiếu mới) và ngày tiếp nhận
        public crdAccept(string gara)
        {
            InitializeComponent();
            this.Opacity = 0;
            InitializeIDAndDate();
            this.gara = gara;
            LoadCarBrand();
            dpk_RecDate.SelectedDate = DateTime.Now;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Lấy storyboard từ resource
            Storyboard fadeInStoryboard = (Storyboard)this.Resources["FadeInStoryboard"];
            // Bắt đầu storyboard
            fadeInStoryboard.Begin(this);
        }

        // hiển thị lên phiếu đã có
        public crdAccept(string a, string gara)
        {
            InitializeComponent();
            this.gara = gara;
            LoadCarBrand();
            tbx_save.Text = "Sửa";
            // cho các textBox unenable

            // các hàm lấy thông tin và gán vào UI
            LoadReceptionFormById(a, ref receptionForm);
        }

        // đã có thay đổi ở các text box
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            isChanged = true;
        }

        // move màn hình
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        // thoát
        private void bd_exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // kiểm tra xem cần làm gì trước khi đóng
            if (check != null)
            {
                if (check.IsEqual(receptionForm))
                    isChanged = false;
                else
                    isChanged = true;
            }

            if (isChanged)
            {
                MessageBoxResult result = MessageBox.Show("Thoát và không lưu?", "Thông tin chưa được lưu!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                    this.Close();
                return;
            }
            this.Close();
        }

        // kiểm tra các thông tin đã hợp lệ chưa
        private bool isValid()
        {
            //nếu khách hàng chưa đăng ký thì không tiếp nhận
            if (CustomerDAO.Instance.GetIDCusByNameAndPhone(tbx_NameCus.Text, tbx_PhoneCus.Text) == null)
            {
                MessageBoxResult result = MessageBox.Show("Người dùng chưa đăng ký! Bạn có muốn tạo khách hàng mới không?", "Lỗi!", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                //chuyển qua form đăng ký
                if (result == MessageBoxResult.OK)
                {
                    crdCustomer crdCustomer = new crdCustomer(gara, tbx_NameCus.Text, tbx_PhoneCus.Text);
                    crdCustomer.ShowDialog();
                }
                return false;
            }

            //nếu hãng xe ngoài danh sách tiếp nhận thì không tiếp nhận
            if (CarBrandDAO.Instance.GetIDBrandByName(cbx_CarBrand.Text, gara) == null)
            {
                MessageBox.Show("Hãng xe không được tiếp nhận!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            //xử lý thông tin trùng lặp ở phiếu mới
            if (tbx_save.Text == "Lưu")
            {
                int i = ReceptionFormDAO.IsExist(receptionForm);
                switch (i)
                {
                    case 1:
                        MessageBoxResult result1 = MessageBox.Show("Bạn có muốn tạo mã phiếu mới?", "Mã phiếu trùng lặp!", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result1 == MessageBoxResult.Yes)
                        {
                            tbx_IDRec.Text = "REC" + (ReceptionFormDAO.GetMaxID() + 1).ToString();
                            tbx_save.Text = "Lưu";
                        }
                        else if (result1 == MessageBoxResult.No)
                        {
                            MessageBoxResult result2 = MessageBox.Show("Bạn có muốn tải lại thông tin của mã phiếu tương ứng?", "Mã phiếu trùng lặp!", MessageBoxButton.YesNo, MessageBoxImage.Question);
                            if (result2 == MessageBoxResult.Yes)
                            {
                                LoadReceptionFormById(tbx_IDRec.Text, ref check);
                                isChanged = false;
                                tbx_save.Text = "Sửa";
                            }
                        }
                        return false;
                    case 0:
                        MessageBoxResult result3 = MessageBox.Show("Bạn có muốn tải lại phiếu đã có?", "Thông tin trùng lặp!", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result3 == MessageBoxResult.Yes)
                        {
                            LoadReceptionFormByInfo();
                            check = receptionForm;
                            isChanged = false;
                            tbx_save.Text = "Sửa";
                        }
                        return false;
                }
            }

            //xử lý thông tin trùng lặp ở phiếu cũ
            if (tbx_save.Text == "Sửa")
            {
                int i = ReceptionFormDAO.IsExist(receptionForm);
                if (i == 0)
                {
                    MessageBoxResult result = MessageBox.Show("Bạn có muốn tải lại phiếu đã có?", "Thông tin trùng lặp!", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        LoadReceptionFormByInfo();
                        check = receptionForm;
                        isChanged = false;
                        tbx_save.Text = "Sửa";
                    }
                    return false;
                }
            }
            return true;
        }

        // lưu phiếu, sau khi lưu nút sẽ trở thành nút sửa, nếu mở phiếu đã có sẵn thì ban đầu sẽ là nút sửa ( 1 nút kiêm 2 chức năng)
        private void bd_save_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CreateRecptionFormFromCard(ref receptionForm);
            if (check != null)
            {
                if (check.IsEqual(receptionForm))
                    isChanged = false;
                else
                    isChanged = true;
            }

            if (isChanged)
            {
                if (isValid())
                {
                    if (tbx_save.Text == "Sửa")
                    {
                        int i = ReceptionFormDAO.UpdateReceptionForm(receptionForm);
                        if (i == 1)
                        {
                            MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            check = receptionForm;
                            isChanged = false;
                        }
                    }
                    else if (tbx_save.Text == "Lưu")
                    {
                        int i = ReceptionFormDAO.InsertReceptionForm(receptionForm);
                        if (i == 1)
                        {
                            MessageBox.Show("Thêm phiếu sửa chữa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            tbx_save.Text = "Sửa";
                            check = receptionForm;
                            isChanged = false;
                        }
                    }
                }
            }
            else
            {
                return;
            }
        }

        // in phiếu
        private void bd_print_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // lưu phiếu mới hoặc lưu các thông tin đã sửa trước khi in 
            if (!isChanged && isValid())
            {

            }
        }

        //hiển thị dữ liệu từ RECEPTION_FORMS trong database lên phiếu sửa chữa theo id
        private void LoadReceptionFormById(string IDRec, ref ReceptionForm receptionForm)
        {
            receptionForm = ReceptionFormDAO.Instance.LoadReceptionFormByID(IDRec);
            Customer customer = CustomerDAO.Instance.LoadCustomerByID(receptionForm.IDCus, gara);
            CarBrand carBrand = CarBrandDAO.Instance.LoadCarBrandByID(receptionForm.IDBrand, gara);

            tbx_IDRec.Text = IDRec;
            tbx_NameCus.Text = customer.NameCus;
            tbx_PhoneCus.Text = customer.PhoneNumberCus;
            tbx_NumberPlate.Text = receptionForm.NumberPlate;
            cbx_CarBrand.Text = carBrand.NameBrand;
            dpk_RecDate.Text = receptionForm.ReceptionDate.ToString();
        }

        //hiển thị id Rec trong database lên phiếu sửa chữa theo thông tin có sẵn
        private string LoadReceptionFormByInfo()
        {
            string id = ReceptionFormDAO.GetReceptionFormIDByInfo(receptionForm);
            LoadReceptionFormById(id, ref receptionForm);
            return id;
        }

        //lưu trữ dữ liệu từ phiếu sửa chữa vào receptionForm
        private void CreateRecptionFormFromCard(ref ReceptionForm receptionForm)
        {
            string iDRec = tbx_IDRec.Text;
            string iDCus = CustomerDAO.Instance.GetIDCusByNameAndPhone(tbx_NameCus.Text, tbx_PhoneCus.Text);
            string iDBrand = CarBrandDAO.Instance.GetIDBrandByName(cbx_CarBrand.Text, gara);
            string iDGara = this.gara;
            string numberPlate = tbx_NumberPlate.Text;
            DateTime receptionDate = Convert.ToDateTime(dpk_RecDate.ToString());
            bool statusRec = false;
            receptionForm = new ReceptionForm(iDRec, iDCus, iDBrand, iDGara, numberPlate, receptionDate, statusRec);
        }

        //khởi tạo id Rec và ngày tiếp nhận
        private void InitializeIDAndDate()
        {
            tbx_IDRec.Text = "REC" + (ReceptionFormDAO.GetMaxID() + 1).ToString();
            dpk_RecDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        //khởi tạo CarBrand combobox
        private void LoadCarBrand()
        {
            List<CarBrand> carBrands = CarBrandDAO.Instance.LoadCarBrandList(gara);
            cbx_CarBrand.Items.Clear();
            foreach (CarBrand item in carBrands)
            {
                cbx_CarBrand.Items.Add(item.NameBrand);
            }
        }

        //hiển thị họ tên khách hàng khi nhập sdt
        private void tbx_PhoneCus_TextChanged(object sender, TextChangedEventArgs e)
        {
            isChanged = true;
            Customer customer = CustomerDAO.Instance.GetCustomerByPhone(tbx_PhoneCus.Text, gara)[0];
            tbx_NameCus.Text = customer.NameCus;
        }
    }
}
