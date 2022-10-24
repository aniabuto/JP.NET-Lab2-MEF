using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace ConsoleApp1;

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

        aggregateCatalog.Catalogs.Add(new DirectoryCatalog(System.Environment.CurrentDirectory));
            
        compositionContainer.Compose(compositionBatch);
    }
}