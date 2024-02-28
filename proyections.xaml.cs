using System.Collections;
using System.Windows;

namespace calculadora_de_semanas
{
    /// <summary>
    /// Lógica de interacción para proyections.xaml
    /// </summary>
    public partial class proyections : Window
    {
        public proyections(List<Proyection> proyectionList)
        {
            
            InitializeComponent();
            //todo for each proyection genera un frame de proyectionData
            DataContext = itemsToShot;
            foreach (var item in proyectionList)
            {
                itemsToShot.Items.Add(item);
            }
        }
    }
}
