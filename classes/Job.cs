using ExtendedNumerics;
using System.Text.RegularExpressions;

namespace calculadora_de_semanas
{
    public class Job
    {
        public string patron { get; set; }
        public string registroPatronal { get; set; }
        public string entidadFederativa { get; set; }
        public BigDecimal salario { get; set; }
        public BigDecimal semanas { get; set; }
        public string alta { get; set; }
        public string baja { get; set; }
        public string semanasDisplay { get; set; }


        public Job(string patron, string registroPatronal, string entidadFederativa, BigDecimal salario, string alta, string baja)
        {
            this.patron = patron;
            this.registroPatronal = registroPatronal;
            this.entidadFederativa = entidadFederativa;
            this.salario = salario;
            this.alta = alta;
            this.baja = baja;
            calcularSemanas();
        }
        //constructor for proyections
        public Job(BigDecimal salario, int weeks  )
        {
            this.salario = salario;
            this.semanas = weeks;
            this.semanasDisplay = weeks.ToString();
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
        #endregion

        public void calcularSemanas()
        {
            this.semanas = (BigDecimal)((baja.Equals("Vigente") ? DateTime.Today : DateTime.Parse(baja)) - DateTime.Parse(alta)).TotalDays / 7;
            if (Regex.Match(semanas.ToString(), @"\d+\.[5-9]").ToString().Length > 0)
            {
                this.semanasDisplay = (this.semanas + 1).WholeValue.ToString();
            }
            else
            {
                this.semanasDisplay = this.semanas.WholeValue.ToString();
            }
        }
    }
}