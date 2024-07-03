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

namespace calculadora_de_semanas.views
{
    /// <summary>
    /// Interaction logic for UpdateChecker.xaml
    /// </summary>
    public partial class UpdateChecker : Window
    {
        public UpdateChecker()
        {
            InitializeComponent();
            //check if the serial is valid along with if the hw is the same if not error not usable
            //if 3 days with no coms have passed block the app ussage.
            //---if unable to communicate to the server, then compare the embebed resource file with the current hw
            //if eerything ok, get the updates for the version assigned to the serialkey
            //apply self update and restart app
        }
    }
}
