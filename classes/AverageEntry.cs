using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace calculadora_de_semanas
{


    public class AverageEntry
    {
        public DateTime alta{get;set;}
        public DateTime baja{get;set;}
        public string patron{get;set;}
        public decimal salario{get;set;}
        public int place{get;set;}
        #region "GEt&Set"
        public DateTime getAlta()
        {
            return this.alta;
        }

        public void setAlta(DateTime alta)
        {
            this.alta = alta;
        }

        public DateTime getBaja()
        {
            return this.baja;
        }

        public void setBaja(DateTime baja)
        {
            this.baja = baja;
        }

        public string getPatron()
        {
            return this.patron;
        }

        public void setPatron(string patron)
        {
            this.patron = patron;
        }

        public decimal getSalario()
        {
            return this.salario;
        }

        public void setSalario(decimal salario)
        {
            this.salario = salario;
        }

        public int getPlace()
        {
            return this.place;
        }

        public void setPlace(int place)
        {
            this.place = place;
        }

        #endregion
        public AverageEntry(DateTime alta, DateTime baja, string patron, decimal salario, int place)
        {
            this.alta = alta;
            this.baja = baja;
            this.patron = patron;
            this.salario = salario;
            this.place = place;
        }
    }
}