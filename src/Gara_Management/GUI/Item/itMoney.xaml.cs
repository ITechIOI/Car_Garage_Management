using Gara_Management.DTO;
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
    /// Interaction logic for itMoney.xaml
    /// </summary>
    public partial class itMoney : UserControl
    {
       
        public itMoney (Fluctuation fluc)
        {
            InitializeComponent();
            txbl_id.Text = fluc.ID;
            txbl_content.Text = fluc.Content;
            txbl_date.Text = fluc.Date.ToString("dd/MM/yyyy");
            txbl_money.Text = fluc.Money.ToString();
        }
    }
}
