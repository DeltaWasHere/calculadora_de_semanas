using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExtendedNumerics;

namespace calculadora_de_semanas
{


    public class AverageEntry
    {
        public string alta{get;set;}
        public string baja{get;set;}
        public string patron{get;set;}
        public BigDecimal salario{get;set;}
        public int place{get;set;}
        #region "GEt&Set"
        public string getAlta()
        {
            return this.alta;
        }

        public void setAlta(string alta)
        {
            this.alta = alta;
        }

        public string getBaja()
        {
            return this.baja;
        }

        public void setBaja(string baja)
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

        public BigDecimal getSalario()
        {
            return this.salario;
        }

        public void setSalario(BigDecimal salario)
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
        public AverageEntry(string alta, string baja, string patron, BigDecimal salario, int place)
        {
            this.alta = alta;
            this.baja = baja;
            this.patron = patron;
            this.salario = salario;
            this.place = place;
        }
    }
}