using System.Collections;
using System.Windows;

namespace calculadora_de_semanas
{
    public partial class proyections : Window
    {
        public proyections(List<Proyection> proyectionList)
        {
            
            InitializeComponent();
            this.DataContext = itemsToShow;
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
