using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using calculadora_de_semanas;
using ExtendedNumerics;
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

            int cumulativeWeeks = 0;
            int counter, semanasHolder;
            //usando semanasDisplay porque viene redondeado
            foreach (Job job in jobs)
            {
                for (counter = cumulativeWeeks; counter < cumulativeWeeks + int.Parse(job.getSemanasDisplay()) && counter < 250; counter++)
                {
                    entries.Add(new AverageEntry(job.getAlta(), job.getBaja(), job.getPatron(), job.getSalario(), counter+1));
                }
                if (cumulativeWeeks+job.getSemanas() >= 250)
                {
                    break;
                }
                
                cumulativeWeeks += int.Parse(job.getSemanasDisplay());
            }
            entriesToShow.DataContext = entries;
        }
    }
}