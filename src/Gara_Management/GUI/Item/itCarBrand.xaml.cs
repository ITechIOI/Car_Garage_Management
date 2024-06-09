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
    /// Interaction logic for itCarBrand.xaml
    /// </summary>
    public partial class itCarBrand : UserControl
    {
        public itCarBrand(CarBrand carbrand)
        {
            InitializeComponent();
            txtb_idCarBrand.Text = carbrand.IDBrand;
            txtb_nameBrand.Text = carbrand.NameBrand;
        }
    }
}
