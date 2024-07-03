using ExtendedNumerics;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace calculadora_de_semanas
{
    public class Person: INotifyPropertyChanged
    {
        public BigDecimal salarioPromedio { get; set; }
        public int semanasTotales { get; set; }
        private ArrayList _jobs = new ArrayList();

        public ArrayList jobs
        {
            get { return _jobs; }
            set { _jobs = value; }
        }

        public ArrayList rawJobs { get; set; }
        public int LastJobs { get; set; }
        public string salarioPromedioDisplay { get; set; }
        public string curp;
        public BigDecimal age;
        public DateTime birthday;
        public string nss;
        public string nombre;
        private const int LAST_WEEKS = 250;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Person(int semanasTotales, ArrayList rawJobs, string nombre, string curp, string nss)
        {
            LastJobs = 0;
            this.semanasTotales = semanasTotales;
            this.rawJobs = rawJobs;
            this.nombre = nombre;
            this.curp = curp;
            this.nss = nss;
            parseJobs(rawJobs);
            calcularSalarioPromedio();
            calcAge();
        }

        public Person(int semanasTotales, string nombre, string curp, string nss, ArrayList jobs)
        {
            LastJobs = 0;
            this.semanasTotales = semanasTotales;
            this.nombre = nombre;
            this.curp = curp;
            this.nss = nss;
            this.jobs = jobs;
            calcularSalarioPromedio();
            calcAge();
        }
        #region "Getter&Setter"
        public string getCurp()
        {
            return this.curp;
        }

        public void setCurp(string curp)
        {
            this.curp = curp;
        }

        public string getNss()
        {
            return this.nss;
        }

        public void setNss(string nss)
        {
            this.nss = nss;
        }

        public string getNombre()
        {
            return this.nombre;
        }

        public void setNombre(string nombre)
        {
            this.nombre = nombre;
        }

        public string getSalarioPromedioDisplay()
        {
            return this.salarioPromedioDisplay;
        }

        public void setSalarioPromedioDisplay(string salarioPromedioDisplay)
        {
            this.salarioPromedioDisplay = salarioPromedioDisplay;
        }
        public BigDecimal getSalarioPromedio()
        {
            return this.salarioPromedio;
        }

        public void setSalarioPromedio(BigDecimal salarioPromedio)
        {
            this.salarioPromedio = salarioPromedio;
        }

        public int getSemanasTotales()
        {
            return this.semanasTotales;
        }

        public void setSemanasTotales(int semanasTotales)
        {
            this.semanasTotales = semanasTotales;
        }



        public ArrayList getRawJobs()
        {
            return this.rawJobs;
        }

        public void setRawJobs(ArrayList rawJobs)
        {
            this.rawJobs = rawJobs;
        }

        public int getLastJobs()
        {
            return this.LastJobs;
        }

        public void setLastJobs(int LastJobs)
        {
            this.LastJobs = LastJobs;
        }

        #endregion

        public void OnPropertyChanged([CallerMemberName] string propetyname = null) { 
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propetyname));
        }

        private void calcAge() {
            int aux = DateTime.Today.Year;
            string birthYear="19"+curp.Substring(4, 2);
            string birthMonth = curp.Substring(6, 2);
            string birthDayDay =  curp.Substring(8, 2);

            this.birthday = DateTime.Parse($"{birthDayDay}/{birthMonth}/{birthYear}");
            this.age = (DateTime.Today - this.birthday).TotalDays/365;

        }
        public void parseJobs( ArrayList rawJobs)
        {
            
        }

        public void calcularSalarioPromedio()
        {
            int cumulativeWeeks = 0;
            BigDecimal cumulativeSalary = 0;

            int counter;
            //usando semanasDisplay porque viene redondeado
            foreach (Job job in jobs)
            {
                for (counter = cumulativeWeeks; counter < cumulativeWeeks + int.Parse(job.getSemanasDisplay()) && counter < 250; counter++)
                {
                    cumulativeSalary += job.getSalario();
                }
                this.LastJobs++;
                if (cumulativeWeeks + job.getSemanas() >= 250)
                {
                    break;
                }
                cumulativeWeeks += int.Parse(job.getSemanasDisplay());
            }

            salarioPromedio = cumulativeSalary / Person.LAST_WEEKS;
            this.salarioPromedioDisplay = Regex.Match(salarioPromedio.ToString(), @"\d*\.?\d{0,2}").ToString();
        }
    }
}