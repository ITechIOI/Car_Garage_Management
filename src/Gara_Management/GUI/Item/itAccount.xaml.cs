﻿using Gara_Management.GUI.Card;
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
        public itAccount()
        {
            InitializeComponent();
        }

        private void bt_view_info_MouseDown(object sender, MouseButtonEventArgs e)
        {
            cardViewInfo viewinfo = new cardViewInfo();
            viewinfo.Show();
        }
    }
}
