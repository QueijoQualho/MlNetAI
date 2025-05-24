using Microsoft.ML.Data;
using Microsoft.ML;
using Checkpoint_5_6.Infra.Interfaces;
using Checkpoint_5_6.Domain.Models;

namespace Checkpoint_5_6.App.Services
{
    public class ModelTrainer : IModelTrainer
    {
        private readonly MLContext _mlContext;

        public ModelTrainer(MLContext mlContext)
        {
            _mlContext = mlContext;
        }

        public ITransformer TrainModel(IDataView data)
        {
            var pipeline = _mlContext.Transforms.Categorical.OneHotEncoding(new[]
            {
                new InputOutputColumnPair("GeneroEncoded", "Genero"),
                new InputOutputColumnPair("DoencasSistemicasEncoded", "DoencasSistemicas"),
                new InputOutputColumnPair("TipoPlanoEncoded", "TipoPlano")
            })
            .Append(_mlContext.Transforms.Conversion.ConvertType(new[]
            {
                new InputOutputColumnPair("HistoricoCaries_f", nameof(InputData.HistoricoCaries)),
                new InputOutputColumnPair("DoencaPeriodontal_f", nameof(InputData.DoencaPeriodontal)),
                new InputOutputColumnPair("Fumante_f", nameof(InputData.Fumante)),
                new InputOutputColumnPair("Alcoolismo_f", nameof(InputData.Alcoolismo)),
                new InputOutputColumnPair("EscovacaoDiaria_f", nameof(InputData.EscovacaoDiaria)),
                new InputOutputColumnPair("UsoFioDental_f", nameof(InputData.UsoFioDental)),
                new InputOutputColumnPair("MedicamentosUsoContinuo_f", nameof(InputData.MedicamentosUsoContinuo)),
                new InputOutputColumnPair("TratamentosComplexosPrevios_f", nameof(InputData.TratamentosComplexosPrevios))
            }, outputKind: DataKind.Single))
            .Append(_mlContext.Transforms.Concatenate("Features",
                nameof(InputData.Idade),
                "GeneroEncoded",
                nameof(InputData.FrequenciaConsultas),
                nameof(InputData.AderenciaTratamento),
                "HistoricoCaries_f",
                "DoencaPeriodontal_f",
                nameof(InputData.NumeroImplantes),
                "TratamentosComplexosPrevios_f",
                "Fumante_f",
                "Alcoolismo_f",
                "EscovacaoDiaria_f",
                "UsoFioDental_f",
                "DoencasSistemicasEncoded",
                "MedicamentosUsoContinuo_f",
                nameof(InputData.NumeroSinistrosPrevios),
                "TipoPlanoEncoded"
            ))
            .Append(_mlContext.Regression.Trainers.FastTree(labelColumnName: nameof(InputData.ProbabilidadeSinistro)));

            var model = pipeline.Fit(data);
            return model;
        }

        public void SaveModel(ITransformer model, string modelPath, DataViewSchema schema)
        {
            _mlContext.Model.Save(model, schema, modelPath);
        }
    }

}
