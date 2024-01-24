using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace calculadora_de_semanas
{
    public class Person
    {
        public decimal salarioPromedio{get;set;}
        public int semanasTotales{get;set;}
        public ArrayList jobs{get;set;}
        public ArrayList rawJobs{get;set;}
        public int LastJobs{get;set;}

        public Person(int semanasTotales, ArrayList rawJobs)
        {
            jobs = new ArrayList();
            LastJobs=0;
            this.semanasTotales = semanasTotales;
            this.rawJobs = rawJobs;
            parseJobs();
            calcularSalarioPromedio();
        }
     #region "Getter&Setter"
        public decimal getSalarioPromedio()
        {
            return this.salarioPromedio;
        }

        public void setSalarioPromedio(decimal salarioPromedio)
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


     

        public void parseJobs()
        {
            foreach (string job in rawJobs)
            {

                //TODO: separar los datos e isntanciar lso objetos, also dont forget to parse string into dateTime
                string[] split = job.Split('\n');
                //ejemplo de array spliteado:
                //0 4/05/1993
                //1 JALISCO
                //2 Salario Base de Cotización */ Fecha de alta Fecha de baja 16/07/1992
                //3 Entidad federativa
                //4 $ 39.99
                //5 Nombre del patrón NOMBRE DEL PATRON.
                //6 Registro Patronal fdfbfwe7184 (18)
                jobs.Add(new Job(split[5].Substring(18), split[6].Substring(18), split[1], decimal.Parse(split[4].Substring(1)), split[2].Substring(57), split[0]));

            }
        }
        public void showJobs()
        {
            foreach (Job job in jobs)
            {
                Console.WriteLine(job.getAlta());
                Console.WriteLine(job.getBaja());
                Console.WriteLine(job.getPatron());
                Console.WriteLine(job.getRegistroPatronal());
                Console.WriteLine(job.getEntidadFederativa());
                Console.WriteLine(job.getSalario());
                Console.WriteLine(job.getSemanas());
            }
        }
        public void showWeeksPerJob()
        {
            decimal weeksSumary = 0M;
            foreach (Job job in jobs)
            {
                weeksSumary = weeksSumary + job.getSemanas();
                Console.WriteLine(job.getPatron());
                Console.WriteLine(job.getSemanas());
                Console.WriteLine(job.getAlta());
                Console.WriteLine(job.getBaja());
            }
            Console.WriteLine("Total weeks:" + weeksSumary);
        }

        private void calcularSalarioPromedio()
        {
            jobs.RemoveAt(0);
            decimal cumulativeWeeks = 0;
            decimal cumulativeSalary = 0;


            foreach (Job job in this.jobs)
            {
                cumulativeWeeks += job.getSemanas();
                LastJobs++;
                if (cumulativeWeeks >= 250)
                {
                    decimal semanasGap = cumulativeWeeks - 250;
                    cumulativeSalary += (job.getSemanas() - semanasGap) * job.getSalario();
                    break;
                }
                else
                {
                    cumulativeSalary += job.getSemanas() * job.getSalario();
                }
            }

            salarioPromedio = cumulativeSalary / 250;
            Console.WriteLine(salarioPromedio);

        }
    }
}