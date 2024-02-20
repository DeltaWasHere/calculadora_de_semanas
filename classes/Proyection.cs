using ExtendedNumerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculadora_de_semanas
{
    public class Proyection
    {//todo: calculos de la proyeccion
        public  int week { get; set; }
        public BigDecimal uma { get; set; }
        public Proyection(int week, BigDecimal uma) {
            this.week = week;
            this.uma=uma;
        }

    }
}
