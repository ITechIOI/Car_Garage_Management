using Gara_Management.GUI.Item;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for crdRepair.xaml
    /// </summary>
    public partial class crdRepair : Window
    {
        // tạo phiếu mới
        public crdRepair()
        {
            InitializeComponent();

            bd_modify.Visibility = Visibility.Hidden;
        }
        // mở phiếu đã có
        public crdRepair(string maphieu)
        {
            InitializeComponent();
            for (int i = 0; i < 4; i++)
            {
                itRepairCardDetail it = new itRepairCardDetail();
                ds_suachua.Children.Add(it);
            }
            ds_suachua.IsEnabled = false;
            bd_add.Visibility = Visibility.Hidden;

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

        //
        private void bd_print_MouseDown(object sender, MouseButtonEventArgs e)
        {


        }

        private void bd_add_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            

                for (int i = 0; i < ds_suachua.Children.Count; i++)
                {
                    itRepairCardDetail child = (itRepairCardDetail)ds_suachua.Children[i];

                    if (child != null && !child.isValid())
                    {
                        MessageBox.Show("thông báo gì đấy");
                        return;
                    }
                }
                ds_suachua.Children.Add(new itRepairCardDetail());
            

        }

        private void bd_modify_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(tbx_modify.Text == "Sửa")// nghhĩa là phiếu đã có
            {
                bd_add.Visibility= Visibility.Visible;
                // code 

                tbx_modify.Text = "Lưu";
            }
            else // đang sửa phiếu
            {
                bd_add.Visibility = Visibility.Hidden;
                // code

                tbx_modify.Text = "Sửa";

            }
        }
    }
}
