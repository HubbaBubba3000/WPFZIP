using System;
using System.IO;
using System.IO.Compression;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;

namespace WPFZIP
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            DateTime dt = DateTime.Now;
            InitializeComponent();
            Console.WriteLine("initUI - "+Math.Round((DateTime.Now - dt).TotalSeconds,3).ToString() + " s");
        }
        public void Exit(Object sender, RoutedEventArgs e) 
        {
            foreach (string file in Directory.GetFiles("temp"))
                File.Delete(file);
            this.Close();
        }
        public void WindowMove(Object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed) 
                this.DragMove();
        }

        public void AddFiles(Object sender, RoutedEventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.ShowDialog();
            opf.Multiselect = true;
            foreach (string file in opf.FileNames)
            {
                FileInfo f = new FileInfo(file);
                f.CopyTo("temp/"+f.Name);
            }
            af.Content = $"Add ({Directory.GetFiles("temp").Length})";
        }

        public void Extract(Object sender, RoutedEventArgs e) 
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.ShowDialog();
            string file = new FileInfo(opf.FileName).Name;

            ZipFile.ExtractToDirectory(opf.FileName,file.Remove(file.Length-3));
        }

        public void Compair(Object sender, RoutedEventArgs e)
        {
            Console.WriteLine(Name.Text);
            if (Directory.GetFiles("temp").Length == 0) {
                MessageBox.Show("error: Add the files");  
                return;
            }  

            ZipFile.CreateFromDirectory("temp", Name.Text+".WZ");
            MessageBox.Show("Files compression complite");
        
        }
        
    }

}
