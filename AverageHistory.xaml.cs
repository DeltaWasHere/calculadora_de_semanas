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
            int counter;
            foreach (Job job in jobs)
            {
                for (counter = cumulativeWeeks; counter < cumulativeWeeks + job.getSemanas() && counter < 250; counter++)
                {
                    entries.Add(new AverageEntry(job.getAlta(), job.getBaja(), job.getPatron(), job.getSalario(), counter+1));
                }
                if (cumulativeWeeks >= 250)
                {
                    break;
                }
                cumulativeWeeks += int.Parse(Regex.Match(job.getSemanas().ToString(), @"^(\d+)").ToString());
            }
            entriesToShow.DataContext = entries;
        }
    }
}