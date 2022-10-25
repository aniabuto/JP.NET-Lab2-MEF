using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using Calculator;
using MEF;


public class PluginManager
{
    [ImportMany(typeof(ICalculator))]
    private List<ICalculator> calculators = new List<ICalculator>();
        
    string path = new DirectoryInfo(".").FullName + "\\Plugins";
    private AggregateCatalog aggregateCatalog;
    
    public List<ICalculator> Calculators
    {
        get { return calculators; }
    }

    public PluginManager()
    {
        aggregateCatalog = new AggregateCatalog();
    }

    public void SetUpPluginManager()
    {
        var compositionContainer = new CompositionContainer(aggregateCatalog);
        var compositionBatch = new CompositionBatch();
            
        compositionBatch.AddPart(this);
        
        using var watcher = new FileSystemWatcher(path);
        
        watcher.NotifyFilter = NotifyFilters.Attributes
                               | NotifyFilters.CreationTime
                               | NotifyFilters.DirectoryName
                               | NotifyFilters.FileName
                               | NotifyFilters.LastAccess
                               | NotifyFilters.LastWrite
                               | NotifyFilters.Security
                               | NotifyFilters.Size;
        
        watcher.Changed += OnFilesUpdate;
        watcher.Created += OnFilesUpdate;
        watcher.Deleted += OnFilesUpdate;
        watcher.Renamed += OnFilesUpdate;
        watcher.EnableRaisingEvents = true;
        
        
        aggregateCatalog.Catalogs.Add(new DirectoryCatalog(path));
            
        compositionContainer.Compose(compositionBatch);
    }

    public event EventHandler FilesChanged;
    private void OnFilesUpdate(object sender, FileSystemEventArgs e)
    {
        aggregateCatalog.Catalogs.Clear();
        aggregateCatalog.Catalogs.Add(new DirectoryCatalog(path));
        
        var handler = FilesChanged;
        if (handler != null)
            handler(this, e);
    }
}