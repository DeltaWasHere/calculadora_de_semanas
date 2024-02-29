using ExtendedNumerics;
using System.Collections;
using System.Text.RegularExpressions;

namespace calculadora_de_semanas
{
    public class Person
    {
        public BigDecimal salarioPromedio { get; set; }
        public int semanasTotales { get; set; }
        public ArrayList jobs { get; set; }
        public ArrayList rawJobs { get; set; }
        public int LastJobs { get; set; }
        public string salarioPromedioDisplay { get; set; }
        public string curp;
        public BigDecimal age;
        public DateTime birthday;
        public string nss;
        public string nombre;
        private const int LAST_WEEKS = 250;
        public Person(int semanasTotales, ArrayList rawJobs, string nombre, string curp, string nss)
        {
            jobs = new ArrayList();
            LastJobs = 0;
            this.semanasTotales = semanasTotales;
            this.rawJobs = rawJobs;
            this.nombre = nombre;
            this.curp = curp;
            this.nss = nss;
            parseJobs();
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

        public ArrayList getJobs()
        {
            return this.jobs;
        }

        public void setJobs(ArrayList jobs)
        {
            this.jobs = jobs;
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


        private void calcAge() {
            int aux = DateTime.Today.Year;
            string birthYear="19"+curp.Substring(4, 2);
            string birthMonth = curp.Substring(6, 2);
            string birthDayDay =  curp.Substring(8, 2);

            this.birthday = DateTime.Parse($"{birthDayDay}/{birthMonth}/{birthYear}");
            this.age = (DateTime.Today - this.birthday).TotalDays/365;

        }
        private void parseJobs()
        {
            foreach (string job in rawJobs)
            {
                //TODO usar reguex en lugar de numeros magicos
                string[] split = job.Split('\n');
                //define index of baja
                 string baja = job.Contains("Vigene")?"Vigente":Regex.Match(job, @"/(\d{1,2}\/\d{2}\/\d{4})/").Value;
                string alta = Regex.Matches(job, @"/(\d{ 1,2}\/\d{ 2}\/\d{ 4})/g")[1].Value;
                string entidad = Regex.Match(job, @"/[A-Z]{2,}/").Value;
                string patron = Regex.Match(job, @"/\b[A-Z]+(?:\s[A-Z]+)+\b/").Value;
                string registro = Regex.Match(job, @"/\w?\d{5,}/").Value;
                BigDecimal finalSalary = BigDecimal.Parse(Regex.Match(job, @"/\d*\.\d*/").Value);

                if (job.Contains("MODIFICACION DE SALARIO")){
                    string auxAlta= alta;
                    string auxBaja;
                    do
                    {
                        auxBaja = Regex.Match(job, @"").Value;
                        jobs.Add(new Job(patron, registro, entidad, finalSalary, alta, baja));
                    } while (!job.Equals(job.Replace("MODIFICACION DE SALARIO", "")));
                }
                else {
                    jobs.Add(new Job(patron, registro, entidad, finalSalary, alta, baja));
                }
            }
        }

        private void calcularSalarioPromedio()
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