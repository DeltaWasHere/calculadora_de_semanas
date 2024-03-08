using ExtendedNumerics;
using System.Collections;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace calculadora_de_semanas
{
    public partial class proyectionForm : Window

    {
        Person person;
        public proyectionForm(Person person)
        {
            InitializeComponent();
            this.person = person;
            BigDecimal UMA = 108.57;
            for (int i = 1; i < 14; i++)
            {
                uma1.Items.Add(BigDecimal.Round((UMA + 0.0001) * i, 2));
                uma2.Items.Add(BigDecimal.Round((UMA + 0.0001) * i, 2));
                uma3.Items.Add(BigDecimal.Round((UMA + 0.0001) * i, 2));
            }
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
            if (text.Text.Length == 0)
            {
                text.Text = "Semanas";
                text.Foreground = new SolidColorBrush(Colors.LightGray);
            }

        }

        private void weeks_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);

        }

        private void create_proyection_Click(object sender, RoutedEventArgs e)
        {
            if (weeks1.Text.Equals("") || uma1.SelectedIndex == 0)
            {
                MessageBox.Show("Porfavor rellene al menos una proyeccion");
                return;
            }
            List<Proyection> proyections = new List<Proyection>();
            proyections.Add(new Proyection(person, 0,0));
            proyections.Add(new Proyection(person,int.Parse(weeks1.Text), BigDecimal.Parse(uma1.SelectedValue.ToString())));
            if (!weeks2.Text.Equals("") && uma2.SelectedIndex != 0) {
                proyections.Add(new Proyection(person,int.Parse(weeks2.Text), BigDecimal.Parse(uma2.SelectedValue.ToString())));
            }
            if (!weeks3.Text.Equals("") && uma3.SelectedIndex != 0)
            {
                proyections.Add(new Proyection(person,int.Parse(weeks3.Text), BigDecimal.Parse(uma3.SelectedValue.ToString())));

            }
            (new proyections(proyections)).Show();
        }
    }
}
