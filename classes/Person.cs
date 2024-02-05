using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ExtendedNumerics;

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

        private void parseJobs()
        {
            foreach (string job in rawJobs)
            {
                //TODO usar reguex en lugar de numeros magicos
                string[] split = job.Split('\n');
                //ejemplo de array spliteado:
                //0 4/05/1993
                //1 JALISCO
                //2 Salario Base de Cotización */ Fecha de alta Fecha de baja 16/07/1992
                //3 Entidad federativa
                //4 $ 39.99
                //5 Nombre del patrón NOMBRE DEL PATRON.
                //6 Registro Patronal fdfbfwe7184 (18)
                jobs.Add(new Job(split[5].Substring(18), split[6].Substring(18), split[1], BigDecimal.Parse(split[4].Substring(1)), split[2].Substring(57), split[0]));

            }
        }

        private void calcularSalarioPromedio()
        {
            int cumulativeWeeks = 0;
            BigDecimal cumulativeSalary = 0;

            int counter;
            foreach (Job job in jobs)
            {
                for (counter = cumulativeWeeks; counter < cumulativeWeeks + job.getSemanas() && counter < 250; counter++)
                {
                    cumulativeSalary += job.getSalario();
                }
                this.LastJobs++;
                if (cumulativeWeeks+job.getSemanas() >= 250)
                {
                    break;
                }
                cumulativeWeeks += int.Parse(Regex.Match(job.getSemanas().ToString(), @"^(\d+)").ToString());
            }
    
            salarioPromedio = cumulativeSalary / Person.LAST_WEEKS;
            this.salarioPromedioDisplay = Regex.Match(salarioPromedio.ToString(), @"\d*\.?\d{0,2}").ToString();
        }
    }
}