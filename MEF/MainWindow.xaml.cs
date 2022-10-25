using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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

        private PluginManager pluginManager;
        
        public List<ICalculator> Calculators
        {
            get { return calculators; }
        }

        public MainWindow()
        {
            InitializeComponent();
            
            pluginManager = new PluginManager();
            pluginManager.FilesChanged += OnFilesChanged;

            Console.WriteLine("Number of Calculators: {0}", pluginManager.Calculators.Count);
            pluginManager.SetUpPluginManager();
            Console.WriteLine("Number of loaded Calculators: {0}", pluginManager.Calculators.Count);

            foreach (var plugin in pluginManager.Calculators)
            {
                TabItem pluginItem = new TabItem();
                pluginItem.Header = plugin.GetName();
                pluginItem.Content = new Calc(plugin);

                Tabs.Items.Add(pluginItem);
            }
            
        }

        private void OnFilesChanged(object? sender, EventArgs e)
        {
            RefeshTabs();
        }

        private void RefeshTabs()
        {
            // List<string> toDo = pluginManager.Calculators.Select(plugin => plugin.GetName()).ToList();
            List<string> toDo = Tabs.Items.Cast<TabItem>().Select(plugin => plugin.Header.ToString()).ToList();

            foreach (var plugin in pluginManager.Calculators )
            {
                if (!toDo.Remove(plugin.GetName()))
                {
                    TabItem pluginItem = new TabItem();
                    pluginItem.Header = plugin.GetName();
                    pluginItem.Content = new Calc();
                    var calculator = ((Calc) (pluginItem.Content));
                    calculator.CalculateButton.Click += CalculateSolution;
                    Tabs.Items.Add(pluginItem);
                }
            }

            foreach (var name in toDo)
            {
                // var leftoverProcess = Tabs.Items.First<TabItem>(process => process.Header.ToString().Equals(name));
                var leftoverProcess = Tabs.Items.OfType<TabItem>()
                    .SingleOrDefault(ti => ti.Header.ToString().Equals(name));
                Tabs.Items.Remove(leftoverProcess);
            }
        }
    }
}