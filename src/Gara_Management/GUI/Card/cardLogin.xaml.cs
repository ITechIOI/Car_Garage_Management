using Gara_Management.DAO;
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
using System.Windows.Shapes;

namespace Gara_Management.GUI.Card
{
    /// <summary>
    /// Interaction logic for cardLogin.xaml
    /// </summary>
    public partial class cardLogin : Window
    {
        public cardLogin()
        {
            InitializeComponent();
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

        private void bt_login_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string username = usernameTextbox.Text;
            string password = passwordTextBox.Password;
            if (AccountDAO.Instance.CheckForLogin(username, password) != null)
            {
                MainWindow mainWindow = new MainWindow(AccountDAO.Instance.CheckForLogin(username, password));
                cardSuccessful success = new cardSuccessful();
                success.Show();
                success.Closed += (s, args) =>
                {
                    this.Hide();
                    // Khi cửa sổ success đóng, hiển thị mainWindow
                    mainWindow.ShowDialog();
                };
            }
            else
            {
                cardWrong wrong = new cardWrong();
                wrong.Show();
            }    
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Giả lập sự kiện MouseDown cho Border
                bt_login_MouseDown(bt_login, null);
            }
        }
    }
}
