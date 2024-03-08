using ExtendedNumerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
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
        private BigDecimal remainingWeeksTillSixties;
        public Person person { get; }
        private BigDecimal BASIC_CUANTIC_PERCENT= 0.13;
        private BigDecimal ANUAL_INCREMENT_FIRST_PERCENT = 0.0245;
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
            if (this.person.age < 60) {
                DateTime aux = this.person.birthday;
                aux =aux.AddYears(60);
                this.remainingWeeksTillSixties = (DateTime.Today-aux).TotalDays/7*(-1);
            } else{
                remainingWeeksTillSixties = 0;
            }
        }

        public void calcTotalPension() {
            if (this.person.age < 60)
            {
                bool substract = false;
                BigDecimal anualSalary = BigDecimal.Parse(this.person.getSalarioPromedioDisplay()) * YEAR_DAYS;

                BigDecimal basicCuanticPercent = (this.person.semanasTotales - 500);
                basicCuanticPercent = ((basicCuanticPercent) / 52) / 100;
                basicCuanticPercent = BigDecimal.Round(basicCuanticPercent, 9);

                if (basicCuanticPercent.IsNegative())
                {
                    substract = true;
                    basicCuanticPercent = (500 - this.person.getSemanasTotales()) / 52 / 100;
                }
                BigDecimal basicCuantia;
                if (substract)
                {
                    basicCuantia = (anualSalary * BASIC_CUANTIC_PERCENT) -
                        (anualSalary * BASIC_CUANTIC_PERCENT * basicCuanticPercent);
                }
                else
                {
                    BigDecimal aux = (anualSalary * BASIC_CUANTIC_PERCENT * basicCuanticPercent);
                    basicCuantia = (anualSalary * BASIC_CUANTIC_PERCENT) + aux;
                }

                BigDecimal anualIncrement = (anualSalary * ANUAL_INCREMENT_FIRST_PERCENT) *
                    ((this.person.semanasTotales + remainingWeeksTillSixties - 500) / 52);
                anualIncrement = anualIncrement + (anualIncrement * ANUAL_INCREMENT_SECOND_PERCENT);

                this.pension = BigDecimal.Round(((basicCuantia + anualIncrement) +
                ((basicCuantia + anualIncrement) * FAMILIAL_ASSIGNATIONS_PERCENT)) / 12, 4);
                
            }
            else {
                this.pension = 0;
            }
        }

    }
}
