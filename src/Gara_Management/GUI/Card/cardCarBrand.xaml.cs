﻿using System;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Gara_Management.GUI.Card
{
    /// <summary>
    /// Interaction logic for cardCarBrand.xaml
    /// </summary>
    public partial class cardCarBrand : Window
    {
        public cardCarBrand()
        {
            InitializeComponent();
            this.Opacity = 0;
        }

        private void CarBrand_Loaded(object sender, RoutedEventArgs e)
        {
            // Lấy storyboard từ resource
            Storyboard fadeInStoryboard = (Storyboard)this.Resources["FadeInStoryboard"];
            // Bắt đầu storyboard
            fadeInStoryboard.Begin(this);

        }

        private void bt_exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void btn_add_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btn_delete_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}