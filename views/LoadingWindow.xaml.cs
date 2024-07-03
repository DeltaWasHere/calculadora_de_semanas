using ExtendedNumerics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
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
using System.Windows.Shapes;

namespace calculadora_de_semanas.views
{
    /// <summary>
    /// Lógica de interacción para LoadingWindow.xaml
    /// </summary>
    public partial class LoadingWindow : Window
    {
        public Person person;
        string fileContent;

        public LoadingWindow(string fileContent)
        {
            InitializeComponent();
            this.fileContent = fileContent;
            this.DataContext = person;
        }
        
        public void loadingProcess(object sender, DoWorkEventArgs e) {
            (sender as BackgroundWorker).ReportProgress(0, "Enocontrano semanas");
            int semanas = PdfParser.getWeeks(fileContent);
            Thread.Sleep(500);

            (sender as BackgroundWorker).ReportProgress(0,"Encontrando CURP");
            string curp = PdfParser.getCurp(fileContent);
            (sender as BackgroundWorker).ReportProgress(0, "Encontrando CURP: "+ curp);
            Thread.Sleep(500);

            (sender as BackgroundWorker).ReportProgress(0, "Encontrando NSS");
            string nss = PdfParser.getNss(fileContent);
            (sender as BackgroundWorker).ReportProgress(0, "Encontrando NSS: "+nss);
            Thread.Sleep(500);

            (sender as BackgroundWorker).ReportProgress(0,"Encontrando nombre");
            string nombre = PdfParser.getNombre(fileContent);
            (sender as BackgroundWorker).ReportProgress(0, "Encontrando nombre: "+nombre);
            Thread.Sleep(500);

            (sender as BackgroundWorker).ReportProgress(0, "Encontrando trabajos");
            ArrayList rawJobs = PdfParser.getJobs(fileContent);
            (sender as BackgroundWorker).ReportProgress(0, "Encontrando trabajos: "+ rawJobs.Count);

            ArrayList jobs = new ArrayList();
            int count = rawJobs.Count;
            foreach (string job in rawJobs)
            {
                string baja = job.Contains("Vigente") ? "Vigente" : Regex.Match(job, @"(\d{1,2}\/\d{2}\/\d{4})").Value;
                string alta = baja.Equals("Vigente") ? Regex.Match(job, @"(\d{1,2}\/\d{2}\/\d{4})").Value : Regex.Matches(job, @"(\d{1,2}\/\d{2}\/\d{4})")[1].Value;
                string entidad = Regex.Match(job, @"[A-Z]{2,}").Value;
                string patron = Regex.Match(job, @"(?<=Nombre del patrón)(.*)(?=Registro Patronal)").Value;
                string registro = Regex.Match(job, @"\w?\d{5,}").Value;
                BigDecimal finalSalary = BigDecimal.Parse(Regex.Match(job, @"\d*\.\d*").Value);

                if (job.Contains("MODIFICACION DE SALARIO"))
                {
                    string auxBaja = baja;
                    string auxAlta;
                    BigDecimal auxSalary;
                    Regex regexConditional = new Regex(@"MODIFICACION DE SALARIO");
                    string nonIterableJob = job;
                    do
                    {
                        auxAlta = Regex.Match(nonIterableJob, @"(\d{1,2}\/\d{2}\/\d{4})(?=MODIFICACION DE SALARIO)").Value;
                        string auxmatch = Regex.Match(nonIterableJob, @"(?<=MODIFICACION DE SALARIO\$ )(\d*\.\d*)").Value;
                        auxSalary = BigDecimal.Parse(auxmatch.Remove(auxmatch.Length-2)); //remover los ultimos dos digitos que son el reingreso overlapeadp
                        jobs.Add(new Job(patron, registro, entidad, auxSalary, auxAlta, auxBaja));
                        auxBaja = auxAlta;
                        nonIterableJob = regexConditional.Replace(nonIterableJob, "", 1);
                        (sender as BackgroundWorker).ReportProgress(100 * jobs.Count / count, "Recuperando informacion de trabajos : "+ jobs.Count.ToString()+"/"+count.ToString());
                        count++;
                    } while (nonIterableJob.Contains("MODIFICACION DE SALARIO"));
                    auxAlta = Regex.Match(nonIterableJob, @"(\d{1,2}\/\d{2}\/\d{4})(?=REINGRESO)").Value;
                    auxSalary = BigDecimal.Parse(Regex.Match(nonIterableJob, @"(?<=REINGRESO\$ )(\d*\.\d*)").Value);
                    jobs.Add(new Job(patron, registro, entidad, auxSalary, auxAlta, auxBaja));


                    
                }
                else
                {
                    jobs.Add(new Job(patron, registro, entidad, finalSalary, alta, baja));
                    (sender as BackgroundWorker).ReportProgress(100 * jobs.Count / count, "Recuperando informacion de trabajos : " + jobs.Count.ToString() + "/" + count.ToString());
                }
            }
            Thread.Sleep(2000);
            person = new Person(semanas, nombre, curp, nss, jobs);
            
                }

        public Person answer { 
            get { return person; }
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += loadingProcess;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        private void Worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                this.DialogResult = true;
            }
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Debug.WriteLine("Progress: " + e.ProgressPercentage);

            if (e.ProgressPercentage > 0)
            {
                progressBar.Value = e.ProgressPercentage;
                progressBarProgress.Text = e.UserState as string;
            }
            else
            {
                loadingStatus.Text = e.UserState as String;
            }
        }
    }
}
