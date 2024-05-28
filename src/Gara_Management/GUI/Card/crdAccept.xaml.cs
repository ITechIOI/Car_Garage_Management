using Gara_Management.DAO;
using Gara_Management.DTO;
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
using System.Windows.Shapes;

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

        // khi khởi tạo tự động có mã phiếu ( với phiếu mới) và ngày tiếp nhận
        public crdAccept()
        {
            InitializeComponent();
            InitializeIDAndDate();
            LoadCarBrand();
        }

        // hiển thị lên phiếu đã có
        public crdAccept(string a)
        {
            InitializeComponent();
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
            if (GetIDCusByNameAndPhone(tbx_NameCus.Text, tbx_PhoneCus.Text) == null)
            {
                MessageBox.Show("Người dùng chưa đăng ký!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                //chuyển qua form đăng ký
                return false;
            }

            //nếu hãng xe ngoài danh sách tiếp nhận thì không tiếp nhận
            if (GetIDBrandByName(cbx_CarBrand.Text) == null)
            {
                MessageBox.Show("Hãng xe không được tiếp nhận!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            //xử lý thông tin trùng lặp ở phiếu mới
            if (tbx_save.Text == "Lưu")
            {
                int i = IsExist(receptionForm);
                switch (i)
                {
                    case 1:
                        MessageBoxResult result1 = MessageBox.Show("Bạn có muốn tạo mã phiếu mới?", "Mã phiếu trùng lặp!", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result1 == MessageBoxResult.Yes)
                        {
                            string query = "SELECT dbo.GET_MAX_IDRECEPTION()";
                            tbx_IDRec.Text = "REC" + (int.Parse(DataProvider.Instance.ExecuteScalar(query).ToString()) + 1).ToString();
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
                            isChanged = false;
                            tbx_save.Text = "Sửa";
                        }
                        return false;
                }
            }

            //xử lý thông tin trùng lặp ở phiếu cũ
            if (tbx_save.Text == "Sửa")
            {
                int i = IsExist(receptionForm);
                if (i == 0)
                {
                    MessageBoxResult result = MessageBox.Show("Bạn có muốn tải lại phiếu đã có?", "Thông tin trùng lặp!", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        LoadReceptionFormByInfo();
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
                        int i = UpdateReceptionForm();
                        if (i == 1)
                        {
                            MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            check = receptionForm;
                            isChanged = false;
                        }
                    }
                    else if (tbx_save.Text == "Lưu")
                    {
                        int i = InsertReceptionForm();
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
            tbx_IDRec.Text = IDRec;

            receptionForm = ReceptionForm.LoadReceptionFormByID(IDRec);

            string loadCustomer = "SELECT * FROM CUSTOMERS WHERE ID_CUS = '" + receptionForm.IDCus + "'";
            Customer customer = new Customer(DataProvider.Instance.ExecuteQuery(loadCustomer).Rows[0]);

            string loadCarBrand = "SELECT * FROM CAR_BRANDS WHERE ID_BRAND = '" + receptionForm.IDBrand + "'";
            CarBrand carBrand = new CarBrand(DataProvider.Instance.ExecuteQuery(loadCarBrand).Rows[0]);

            tbx_NameCus.Text = customer.NameCus;
            tbx_PhoneCus.Text = customer.PhoneNumberCus;
            tbx_NumberPlate.Text = receptionForm.NumberPlate;
            cbx_CarBrand.Text = carBrand.NameBrand;
            tbx_RecDate.Text = receptionForm.ReceptionDate.ToString("dd/MM/yyyy");
        }

        //hiển thị id Rec trong database lên phiếu sửa chữa theo thông tin có sẵn
        private string LoadReceptionFormByInfo()
        {
            string query = "EXEC USP_GET_IDRECEPTION @ID_CUS , @ID_GARA , @NUMBER_PLATES , @RECEPTION_DATE";
            string id = DataProvider.Instance.ExecuteScalar(query, new object[] { receptionForm.IDCus, receptionForm.IDGara, receptionForm.NumberPlate, receptionForm.ReceptionDate }).ToString().Trim();
            LoadReceptionFormById(id, ref receptionForm);
            return id;
        }

        //lưu trữ dữ liệu từ phiếu sửa chữa vào receptionForm
        private void CreateRecptionFormFromCard(ref ReceptionForm receptionForm)
        {
            string iDRec = tbx_IDRec.Text;
            string iDCus = GetIDCusByNameAndPhone(tbx_NameCus.Text, tbx_PhoneCus.Text);
            string iDBrand = GetIDBrandByName(cbx_CarBrand.Text);
            string iDGara = "GR1";
            string numberPlate = tbx_NumberPlate.Text;
            DateTime receptionDate = Convert.ToDateTime(tbx_RecDate.Text);
            bool statusRec = false;
            receptionForm = new ReceptionForm(iDRec, iDCus, iDBrand, iDGara, numberPlate, receptionDate, statusRec);
        }

        //thêm dữ liệu từ phiếu sửa chữa vào RECEPTION_FORMS
        private int InsertReceptionForm()
        {
            int result = 0;
            string query = "EXEC USP_INSERT_RECEPTIONFORM @ID_CUS , @ID_BRAND , @ID_GARA , @NUMBER_PLATES , @RECEPTION_DATE";
            result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { receptionForm.IDCus, receptionForm.IDBrand, receptionForm.IDGara, receptionForm.NumberPlate, receptionForm.ReceptionDate });
            return result;
        }

        private int UpdateReceptionForm()
        {
            int result = 0;
            string query = "EXEC USP_UPDATE_RECEPTIONFORM @ID_REC , @ID_CUS , @ID_BRAND , @ID_GARA , @NUMBER_PLATES , @RECEPTION_DATE";
            result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { receptionForm.IDRec, receptionForm.IDCus, receptionForm.IDBrand, receptionForm.IDGara, receptionForm.NumberPlate, receptionForm.ReceptionDate });
            return result;

        }

        //kiểm tra xem thông tin có trùng lặp không 
        private int IsExist(ReceptionForm receptionForm)
        {
            string IDCheck = "SELECT * FROM RECEPTION_FORMS WHERE ID_REC = '" + receptionForm.IDRec + "'";
            object i = DataProvider.Instance.ExecuteScalar(IDCheck);
            string InfoCheck = "EXEC USP_GET_IDRECEPTION @ID_CUS , @ID_GARA , @NUMBER_PLATES , @ID_BRAND , @RECEPTION_DATE";
            object j = DataProvider.Instance.ExecuteScalar(InfoCheck, new object[] { receptionForm.IDCus, receptionForm.IDGara, receptionForm.NumberPlate, receptionForm.IDBrand, receptionForm.ReceptionDate });
            if (j != null)
            {
                //thông tin phiếu trùng lặp
                return 0;
            }
            else if (i != null)
            {
                //id Rec trùng lặp
                return 1;
            }
            //không trùng lặp
            return -1;
        }

        //lấy id khách hàng theo tên khách hàng và số điện thoại
        private string GetIDCusByNameAndPhone(string name, string phone)
        {
            string id;
            string query = "EXEC USP_GET_IDCUSTOMER @NAME_CUS , @PHONE_NUMBER_CUS";
            if (DataProvider.Instance.ExecuteScalar(query, new object[] { name, phone }) != null)
            {
                id = DataProvider.Instance.ExecuteScalar(query, new object[] { name, phone }).ToString().Trim();
            }
            else
            {
                id = null;
            }
            return id;
        }

        //lấy id hãng theo tên hãng
        private string GetIDBrandByName(string name)
        {
            string id;
            string query = "SELECT ID_BRAND FROM CAR_BRANDS WHERE NAME_BRAND = '" + name + "'";
            if (DataProvider.Instance.ExecuteScalar(query) != null)
            {
                id = DataProvider.Instance.ExecuteScalar(query).ToString().Trim();
            }
            else
            {
                id = null;
            }
            return id;
        }

        //khởi tạo id Rec và ngày tiếp nhận
        private void InitializeIDAndDate()
        {
            string query = "SELECT dbo.GET_MAX_IDRECEPTION()";
            tbx_IDRec.Text = "REC" + (int.Parse(DataProvider.Instance.ExecuteScalar(query).ToString()) + 0).ToString();
            tbx_RecDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        //khởi tạo CarBrand combobox
        private void LoadCarBrand()
        {
            string query = "SELECT * FROM CAR_BRANDS";
            List<CarBrand> carBrands = CarBrandDAO.Instance.LoadCarBrandList();
            cbx_CarBrand.Items.Clear();
            foreach (CarBrand item in carBrands)
            {
                cbx_CarBrand.Items.Add(item.NameBrand);
            }
        }
    }
}
