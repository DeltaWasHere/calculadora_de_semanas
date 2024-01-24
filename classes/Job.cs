using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace calculadora_de_semanas
{
    public class Job
    {
        public string patron {get; set;}
        public string registroPatronal {get; set;}
        public string entidadFederativa {get; set;}
        public decimal salario {get; set;}
        public decimal semanas {get; set;}
        public DateTime alta {get; set;}
        public DateTime baja {get; set;}

       

        public Job(string patron, string registroPatronal, string entidadFederativa, decimal salario, string alta, string baja)
        {
            this.patron = patron;
            this.registroPatronal = registroPatronal;
            this.entidadFederativa = entidadFederativa;
            this.salario = salario;
            this.alta = DateTime.Parse(alta);
            this.baja = DateTime.Parse(baja);
            calcularSemanas();
        }
        #region  "Getter&Setter"
         public string getPatron()
        {
            return this.patron;
        }

        public void setPatron(string patron)
        {
            this.patron = patron;
        }

        public string getRegistroPatronal()
        {
            return this.registroPatronal;
        }

        public void setRegistroPatronal(string registroPatronal)
        {
            this.registroPatronal = registroPatronal;
        }

        public string getEntidadFederativa()
        {
            return this.entidadFederativa;
        }

        public void setEntidadFederativa(string entidadFederativa)
        {
            this.entidadFederativa = entidadFederativa;
        }

        public decimal getSalario()
        {
            return this.salario;
        }

        public void setSalario(decimal salario)
        {
            this.salario = salario;
        }

        public decimal getSemanas()
        {
            return this.semanas;
        }

        public void setSemanas(int semanas)
        {
            this.semanas = semanas;
        }

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

        #endregion
        
        public void calcularSemanas(){
            this.semanas = (decimal)(baja-alta).TotalDays/7;
        }

    }
}