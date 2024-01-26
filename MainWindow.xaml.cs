using System.Collections;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks.Dataflow;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using calculadora_de_semanas;
using Microsoft.Win32;





namespace calculadora_de_semanas;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public Person person;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void OpenPdf_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.DefaultExt = ".pdf";
        openFileDialog.Filter = "Pdf files (*.pdf)|*.pdf";

        if (openFileDialog.ShowDialog() == true)
        {
            loadFile(openFileDialog.FileName);
        }
    }

    private void loadFile(string filename)
    {
        string fileContent = PdfParser.getFileContent(filename);
        int semanas = PdfParser.getWeeks(fileContent);
        string curp = PdfParser.getCurp(fileContent);
        string nss = PdfParser.getNss(fileContent);
        string nombre = PdfParser.getNombre(fileContent);
        ArrayList rawJobs = PdfParser.getJobs(fileContent);

        this.person = new Person(semanas, rawJobs, nombre, curp, nss);

        jobsToShow.Items.Clear();
        foreach (Job job in person.getJobs())
        {
            jobsToShow.Items.Add(job);
        }
        semanasTotales.Text = "Promedio de salario en las ultimas 250 semanas: " + person.getSalarioPromedioDisplay();
        curpShow.Text = "Curp: " + person.getCurp();
        nssShow.Text = "Nss: " + person.getNss();
        nombreShow.Text = "Nombre: " + person.getNombre();
    }

    private void OpenHistory(object sender, RoutedEventArgs e)
    {
        if (this.person == null)
        {
            MessageBox.Show("Porfavor cargue el archivo de las semanas antes de continuar");
        }
        else
        {
            (new AverageHistory(person)).Show();
        }
    }
}
