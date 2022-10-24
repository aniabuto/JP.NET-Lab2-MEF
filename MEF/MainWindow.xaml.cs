using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;
using Calculator;

namespace MEF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [ImportMany(typeof(ICalculator))]
        private List<ICalculator> calculators = new List<ICalculator>();
        
        public List<ICalculator> Calculators
        {
            get { return calculators; }
        }

        public MainWindow()
        {
            InitializeComponent();
            
            var pluginManager = new PluginManager();

            Console.WriteLine("Number of Calculators: {0}", pluginManager.Calculators.Count);
            pluginManager.SetUpPluginManager();
            Console.WriteLine("Number of loaded Calculators: {0}", pluginManager.Calculators.Count);
            
        }
        private void Calculate(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}