using Checkpoint_5_6.Domain.Models;
using Checkpoint_5_6.Infra.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.ML;

namespace Checkpoint_5_6.App.Services
{
    public class ModelPredictor : IModelPredictor
    {
        private readonly MLContext _mlContext;
        private readonly ITransformer _model;
        private readonly PredictionEngine<InputData, OutputPrediction> _predictionEngine;

        public ModelPredictor(IConfiguration configuration)
        {
            _mlContext = new MLContext();
            string modelPath = configuration["ModelSettings:ModelPath"];

            using var stream = new FileStream(modelPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            _model = _mlContext.Model.Load(stream, out var modelInputSchema);

            _predictionEngine = _mlContext.Model.CreatePredictionEngine<InputData, OutputPrediction>(_model);
        }

        public OutputPrediction Predict(InputData inputData)
        {
            return _predictionEngine.Predict(inputData);
        }
    }
}
