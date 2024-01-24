using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ExtendedNumerics;

namespace calculadora_de_semanas
{
    public class Job
    {
        public string patron { get; set; }
        public string registroPatronal { get; set; }
        public string entidadFederativa { get; set; }
        public BigDecimal salario { get; set; }
        public BigDecimal semanas { get; set; }
        public DateTime alta { get; set; }
        public DateTime baja { get; set; }
        public string semanasDisplay { get; set;}


        public Job(string patron, string registroPatronal, string entidadFederativa, BigDecimal salario, string alta, string baja)
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
        public string getSemanasDisplay()
        {
            return this.semanasDisplay;
        }

         public void setSemanasDisplay(string semanasDisplay)
        {
            this.semanasDisplay = semanasDisplay;
        }
        
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

        public BigDecimal getSalario()
        {
            return this.salario;
        }

        public void setSalario(BigDecimal salario)
        {
            this.salario = salario;
        }

        public BigDecimal getSemanas()
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

        public void calcularSemanas()
        {
            this.semanas = (BigDecimal)(baja - alta).TotalDays / 7;
            this.semanasDisplay = Regex.Match(semanas.ToString(), @"\d*\.?\d{0,2}").ToString();
        }
    }
}