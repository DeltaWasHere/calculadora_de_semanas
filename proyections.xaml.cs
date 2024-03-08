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
            DataContext = itemsToShow;
            foreach (var item in proyectionList)
            {
                if (item.week!=0) {
                    item.person.jobs.Insert(0, new Job(item.uma, item.week));
                    item.person.calcularSalarioPromedio();
                    item.calcTotalPension();
                }
                itemsToShow.Items.Add(item);

            }
        }
    }
}
