﻿using Microsoft.Win32;
using System.Collections;
using System.Windows;





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
        for (int i = 0; i < this.person.LastJobs; i++)
        {
            jobsToShow.Items.Add(this.person.jobs[i]);
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

    private void CreateProyections_Click(object sender, RoutedEventArgs e)
    {
        if (this.person == null)
        {
            MessageBox.Show("Porfavor cargue el archivo de las semanas antes de continuar");
        }
        else
        {
            (new proyectionForm(person)).Show();
        }
    }
}
