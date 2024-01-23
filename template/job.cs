using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace template
{
    public class job
    {
        private string patron;
        private double salario;
        private int semanas;
        private DateTime alta;
        private DateTime baja;
        
        public job(string patron, double salario, int semanas, string alta, string baja)
        {
            this.patron = patron;
            this.salario = salario;
            this.alta = DateTime.Parse(alta);
            this.baja = DateTime.Parse(baja);
            calcularSemanas();
        }
        
        public void calcularSemanas(){
            this.semanas = (int)(baja-alta).TotalDays/7;
        }
    }
}