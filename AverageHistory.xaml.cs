using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using calculadora_de_semanas;
using Microsoft.Win32;


namespace calculadora_de_semanas
{
    /// <summary>
    /// Interaction logic for AverageHistory.xaml
    /// </summary>
    public partial class AverageHistory : Window
    {
        ObservableCollection<AverageEntry> entries = new ObservableCollection<AverageEntry>();
        public AverageHistory(Person person)
        {
            InitializeComponent();
            ArrayList jobs = person.getJobs();
            int lastJobs = person.getLastJobs();

            decimal cumulativeWeeks = 0;
            //decimal cumulativeSalary = 0;
            int counter = 0;
            foreach (Job job in jobs)
            {
                cumulativeWeeks += job.getSemanas();
                if (cumulativeWeeks >= 250)
                {
                    decimal semanasGap = cumulativeWeeks - 250;
                    for (int i = 0; i < job.getSemanas() - semanasGap; i++)
                    {
                        counter++;
                        entries.Add(new AverageEntry(job.getAlta(), job.getBaja(), job.getPatron(), job.getSalario(), counter));
                    }
                    //cumulativeSalary += (job.getSemanas() - semanasGap) * job.getSalario();
                    break;
                }
                else
                {
                    for (int i = 0; i < job.getSemanas(); i++)
                    {
                        counter++;
                        entries.Add(new AverageEntry(job.getAlta(), job.getBaja(), job.getPatron(), job.getSalario(), counter));
                    }
                    //cumulativeSalary += job.getSemanas() * job.getSalario();
                }
            }
            entriesToShow.DataContext = entries;
            //averageEntries = entries;
        }
    }
}