﻿using Gara_Management.GUI.Item;
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

namespace Gara_Management.GUI.Card
{
    /// <summary>
    /// Interaction logic for scrMoney.xaml
    /// </summary>
    public partial class scrMoney : UserControl
    {
        public scrMoney()
        {
           
        }

        private void bd_exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}

