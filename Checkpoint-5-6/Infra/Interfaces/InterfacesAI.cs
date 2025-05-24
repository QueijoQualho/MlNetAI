using Microsoft.ML.Data;
using Microsoft.ML;
using Checkpoint_5_6.Domain.Models;

namespace Checkpoint_5_6.Infra.Interfaces
{
    public interface IDataLoader
    {
        IDataView LoadData(string dataPath);
    }

    public interface IModelTrainer
    {
        ITransformer TrainModel(IDataView data);
        void SaveModel(ITransformer model, string modelPath, DataViewSchema schema);
    }

    public interface IModelPredictor
    {
        OutputPrediction Predict(InputData input);
    }

    public interface IModelEvaluator
    {
        RegressionMetrics Evaluate(IDataView testData);
    }

}
