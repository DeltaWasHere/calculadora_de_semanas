using System.Collections;
using System.Windows;

namespace calculadora_de_semanas
{
    /// <summary>
    /// Lógica de interacción para proyections.xaml
    /// </summary>
    public partial class proyections : Window
    {
        public proyections(ArrayList proyectionList)
        {
            InitializeComponent();
            //todo for each proyection genera un frame de proyectionData
            foreach (var item in proyectionList)
            {
                
            }
        }
    }
}
