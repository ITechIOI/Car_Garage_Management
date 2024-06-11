using Gara_Management.DAO;
using Gara_Management.DTO;
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

namespace Gara_Management.GUI.Item
{
    /// <summary>
    /// Interaction logic for itAccount.xaml
    /// </summary>
    public partial class itAccount : UserControl
    {
        Staff staff;
        Account account;
        public itAccount(Staff staff, Account account)
        {
            InitializeComponent();
            this.staff = staff;
            this.account = account;
            staffID.Text = this.staff.IDStaff;
            staffName.Text = this.staff.NameStaff;
            staffPhonenumber.Text = this.staff.PhoneNumberStaff;
            if (this.account != null)
            {
                staffUsername.Text = this.account.UserName;
                if (this.account.AccAuthorization == false)
                {
                    staffRole.Text = "Quản lý";
                }
                else
                {
                    staffRole.Text = "Nhân viên";
                }
            }    

        }

        private void bt_view_info_MouseDown(object sender, MouseButtonEventArgs e)
        {
            cardViewInfo viewinfo = new cardViewInfo(staff, account);
            viewinfo.btn_delete.MouseDown += Btn_delete_MouseDown;
            viewinfo.Show();

            

        }

        private void Btn_delete_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string gara = staff.IDGara;
            Panel panel = (Panel)this.Parent;
            panel.Children.Clear();
            foreach (Staff staff in StaffDAO.Instance.LoadStaffList(gara))
            {
                Account acc = AccountDAO.Instance.GetAccountByIDStaff(staff.IDStaff);
                itAccount it = new itAccount(staff, acc);
                panel.Children.Add(it);
            }
        }
    }
}
