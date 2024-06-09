using Gara_Management.DAO;
using Gara_Management.DTO;
using Gara_Management.GUI.Item;
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
    /// Interaction logic for crdMoney.xaml
    /// </summary>
    public partial class crdMoney : Window
    {
        string gara;
        Customer customer;
        public crdMoney(string gara, Customer customer)
        {
            InitializeComponent();
            this.gara = gara;
            this.customer = customer;
            
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
        private void LoadListMoney()
        {
            List<Fluctuation> fluctuations = FluctuationDAO.Instance.LoadListFluctuationOfCus(customer.IDCus, gara);
            foreach (Fluctuation fluctuation in fluctuations) 
            { 
                itMoney it = new itMoney(fluctuation, customer);
            }
        }
    }
}
