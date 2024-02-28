using ExtendedNumerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace calculadora_de_semanas
{
    public class Proyection
    {//todo: calculos de la proyeccion
        public  int week { get; set; }
        public BigDecimal uma { get; set; }
        public BigDecimal pension { get; set; }
      
        private const int YEAR_DAYS = 365;
        private const int MONTHS = 12;
        private int remainingWeeksTillSixties;
        public Person person { get; }
        private BigDecimal BASIC_CUANTIC_PERCENT= 0.13;
        private BigDecimal ANUAL_INCREMENT_FIRST_PERCENT = 0.245;
        private BigDecimal ANUAL_INCREMENT_SECOND_PERCENT = 0.11;
        public  BigDecimal FAMILIAL_ASSIGNATIONS_PERCENT = 0.15;
        public Proyection(Person person,int week, BigDecimal uma) {
         
            this.week = week;
            this.uma=uma;
            this.person = person;
            calcRemainingWeeksTillSixties();
            calcTotalPension();
            
        }

        public void calcRemainingWeeksTillSixties() {
            if (person.age < 60)
            {
                BigDecimal aux = ((60 - person.age) * 365 / 7);
                if (Regex.Match(aux.ToString(), @"\d+\.[5-9]").ToString().Length > 0)
                {
                    this.remainingWeeksTillSixties = int.Parse(aux.WholeValue.ToString()) + 1;
                }
                else
                {
                    this.remainingWeeksTillSixties = int.Parse(aux.WholeValue.ToString());
                }
            }
            else
            {
                this.remainingWeeksTillSixties = 0;
            }
        }

        public void calcTotalPension() {
            bool substract = false;
            BigDecimal anualSalary = BigDecimal.Parse(this.person.getSalarioPromedioDisplay()) * YEAR_DAYS;

            BigDecimal basicCuanticPercent = (this.person.getSemanasTotales()-500) / 52 / 100;

            if (basicCuanticPercent.IsNegative()) { 
                substract= true;
                basicCuanticPercent = ( 500- this.person.getSemanasTotales()) / 52 / 100;
            }
            BigDecimal basicCuantia;
            if (substract)
            {
                basicCuantia=(anualSalary * BASIC_CUANTIC_PERCENT) -
                    (anualSalary * BASIC_CUANTIC_PERCENT * basicCuanticPercent);
            }
            else {
                basicCuantia=(anualSalary * BASIC_CUANTIC_PERCENT) +
                   (anualSalary * BASIC_CUANTIC_PERCENT * basicCuanticPercent);
            }

            BigDecimal anualIncrement = (anualSalary * ANUAL_INCREMENT_FIRST_PERCENT) * 
                ((this.person.getSemanasTotales() + remainingWeeksTillSixties - 500) / 52);
            anualIncrement = anualIncrement + (anualIncrement * ANUAL_INCREMENT_SECOND_PERCENT);


            this.pension = ((basicCuantia+anualIncrement)+
                ((basicCuantia + anualIncrement)*FAMILIAL_ASSIGNATIONS_PERCENT))/12;


            
        }

    }
}
