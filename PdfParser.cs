using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using ExtendedNumerics;

namespace calculadora_de_semanas
{
    public class PdfParser
    {
        private const string WEEKS_FILTER = "Constancia de Semanas Cotizadas en el IMSS";
        private static readonly string[] JOB_SEPARATORS = ["/* Valor del último salario base de cotización diario en pesos.", "Tu historia laboral", "deInstituto Mexicano del Seguro Social"];

        public static string getFileContent(string file)
        {
            PdfReader pdfReader = new PdfReader(file);
            PdfDocument pdfDocument = new PdfDocument(pdfReader);

            string data = "";
            for (int page = 1; page <= pdfDocument.GetNumberOfPages(); page++)
            {
                ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                data += PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(page), strategy);
            }
            return data;
        }
        public static int getWeeks(string data)
        {
            int weeksIndexStart = data.IndexOf(PdfParser.WEEKS_FILTER) + PdfParser.WEEKS_FILTER.Length + 1;
            int endIndex = weeksIndexStart;

            while (endIndex < data.Length && char.IsDigit(data[endIndex]))
            {
                endIndex++;
            }

            int weeks = int.Parse(data.Substring(weeksIndexStart, endIndex - weeksIndexStart));
            return weeks;
        }

        public static ArrayList getJobs(string data)
        {
            string[] jobs = data.Split(PdfParser.JOB_SEPARATORS, StringSplitOptions.RemoveEmptyEntries);
            ArrayList filteredJobs = new ArrayList();

            for (int i = 0; i < jobs.Length; i++)
            {
                char[] toTrim = { '\n', ' ' };
                jobs[i] = jobs[i].Trim(toTrim);
                if (jobs[i].Contains("Vigente\n") || char.IsDigit(jobs[i][0]))
                {
                    filteredJobs.Add(jobs[i]);
                }
            }
            return filteredJobs;
        }
    }
}