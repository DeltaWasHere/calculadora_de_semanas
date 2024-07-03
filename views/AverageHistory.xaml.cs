using System.Collections;
using System.Collections.ObjectModel;
using System.Windows;


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
            ArrayList jobs = person.jobs;
            int lastJobs = person.getLastJobs();

            int cumulativeWeeks = 0;
            int counter, semanasHolder;
            //usando semanasDisplay porque viene redondeado
            foreach (Job job in jobs)
            {
                for (counter = cumulativeWeeks; counter < cumulativeWeeks + int.Parse(job.getSemanasDisplay()) && counter < 250; counter++)
                {
                    entries.Add(new AverageEntry(job.getAlta(), job.getBaja(), job.getPatron(), job.getSalario(), counter + 1));
                }
                if (cumulativeWeeks + job.getSemanas() >= 250)
                {
                    break;
                }

                cumulativeWeeks += int.Parse(job.getSemanasDisplay());
            }
            entriesToShow.DataContext = entries;
        }
    }
}