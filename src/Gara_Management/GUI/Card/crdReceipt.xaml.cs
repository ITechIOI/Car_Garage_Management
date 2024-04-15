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
    /// Interaction logic for crdReceipt.xaml
    /// </summary>
    public partial class crdReceipt : Window
    {
        /* biếm kiểm tra các thông tin đã thay đổi hay chưa*/
        bool isChanged = false;
        public crdReceipt()
        {
            InitializeComponent();
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }


        private void bd_exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        // kiểm tra các thông tin có hợp lệ
        private bool isValid()
        {

            return true;
        }
        private void bd_save_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!isChanged)
            {


                isChanged = true;
            }
            else
            {


            }
        }

        private void bd_print_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
