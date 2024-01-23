using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace template
{
    public class person
    {
        private double salarioPromedio;
        private int semanasTotales;
        private ArrayList rawJobs;
        private ArrayList jobs;

        public person(int semanasTotales, ArrayList rawJobs)
        {
            this.semanasTotales = semanasTotales;
            this.rawJobs = rawJobs;
            parseJobs();
        }

        public void parseJobs()
        {
            foreach (var job in rawJobs)
            {

                //TODO: separar los datos e isntanciar lso objetos, also dont forget to parse string into dateTime


            }
        }
    }
}