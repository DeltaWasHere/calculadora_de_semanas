using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace calculadora_de_semanas
{
    public partial class proyectionForm : Window
    {
        public proyectionForm(Person person)
        {
            InitializeComponent();
        }

        private void weeks_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text.Text.Equals("Semanas"))
            {

                text.Text = "";
                text.Foreground = new SolidColorBrush(Colors.Black);
            }
            
        }

        private void weeks_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text.Text.Length==0) {
                text.Text = "Semanas";
                text.Foreground = new SolidColorBrush(Colors.LightGray);
            }

        }

        private void weeks_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);

        }
    }
}
