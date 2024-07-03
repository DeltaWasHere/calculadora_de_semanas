using System.Collections;
using System.Windows;

namespace calculadora_de_semanas
{
    public partial class proyections : Window
    {
        public proyections(List<Proyection> proyectionList)
        {
            
            InitializeComponent(); //diccionario de datos no encontrado
            this.DataContext = itemsToShow;
            foreach (var item in proyectionList)
            {
                if (item.week != 0)
                {
                    item.calcTotalPension();
                    itemsToShow.Items.Add(item);
                }
                else
                {
                    itemsToShow.Items.Add(item);
                }
            }
        }
    }
}
