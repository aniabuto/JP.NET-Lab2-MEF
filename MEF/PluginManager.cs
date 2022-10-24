using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using Calculator;
using MEF;


public class PluginManager
{
    [ImportMany(typeof(ICalculator))]
    private List<ICalculator> calculators = new List<ICalculator>();
        
    public List<ICalculator> Calculators
    {
        get { return calculators; }
    }
    
    public void SetUpPluginManager()
    {
        var aggregateCatalog = new AggregateCatalog();
        var compositionContainer = new CompositionContainer(aggregateCatalog);
        var compositionBatch = new CompositionBatch();
            
        compositionBatch.AddPart(this);

        string path = new DirectoryInfo(".").FullName + "\\Plugins";
        aggregateCatalog.Catalogs.Add(new DirectoryCatalog(path));
            
        compositionContainer.Compose(compositionBatch);
    }
}