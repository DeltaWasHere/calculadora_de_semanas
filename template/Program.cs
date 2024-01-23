namespace template;

using System.Collections;
using System.Diagnostics;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
class Program
{
    static void Main(string[] args)
    {
        string file = @"C:\Users\jegmo\OneDrive\Escritorio\despacho\template\SEMANAS_julio_perez_cazares.pdf";
        PdfReader pdfReader = new PdfReader(file);
        PdfDocument pdfDocument = new PdfDocument(pdfReader);

        string data = "";
        for (int page = 1; page <= pdfDocument.GetNumberOfPages(); page++)
        {
            ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
            data += PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(page), strategy);
        }

        const int expectedWeeks = 1116;
        int weeks = getWeeks(data);
        Console.WriteLine((weeks == expectedWeeks ? "true" : "false") + " expected: {0}, recieved: {1}", expectedWeeks, weeks);


        ArrayList jobs = getJobs(data);
        
    }
    //Returns the total weeks on the imms based on the file
    static int getWeeks(string data)
    {
        string condition = "Constancia de Semanas Cotizadas en el IMSS";

        int weeksIndexStart = data.IndexOf(condition) + condition.Length + 1;
        int endIndex = weeksIndexStart;

        while (endIndex < data.Length && char.IsDigit(data[endIndex]))
        {
            endIndex++;
        }

        int weeks = int.Parse(data.Substring(weeksIndexStart, endIndex - weeksIndexStart));
        return weeks;
    }

    static ArrayList getJobs(string data)
    {
        string[] conditions = new string[] { "/* Valor del último salario base de cotización diario en pesos.", "Tu historia laboral", "deInstituto Mexicano del Seguro Social" };
        string[]jobs = data.Split(conditions, StringSplitOptions.RemoveEmptyEntries);


        ArrayList filteredJobs = new ArrayList();
        for (int i = 0; i < jobs.Length; i++)
        {
            char[] toTrim = { '\n', ' ' };
            jobs[i] = jobs[i].Trim(toTrim);

            if (char.IsDigit(jobs[i][0]))
            {
                filteredJobs.Add(jobs[i]);
                Console.WriteLine(jobs[i]);
                //Console.WriteLine('\n');
            }
            else
            {
                // Console.WriteLine(chunks[i]);

            }
        }

        return filteredJobs;

    }
}
