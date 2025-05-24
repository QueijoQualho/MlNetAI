using Checkpoint_5_6.Domain.Models;
using Checkpoint_5_6.Infra.Interfaces;
using Microsoft.ML;

namespace Checkpoint_5_6.App.Services
{
    public class CsvDataLoader : IDataLoader
    {
        private readonly MLContext _mlContext;

        public CsvDataLoader(MLContext mlContext)
        {
            _mlContext = mlContext;
        }

        public IDataView LoadData(string dataPath)
        {
            return _mlContext.Data.LoadFromTextFile<InputData>(
                path: dataPath, hasHeader: true, separatorChar: ',');
        }
    }

}
