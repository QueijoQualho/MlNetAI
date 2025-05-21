using Microsoft.ML;
using Microsoft.ML.Data;

namespace ConsoleApp1
{
    public class InputData
    {
        [LoadColumn(0)] public float Idade;
        [LoadColumn(1)] public string Genero;
        [LoadColumn(2)] public float FrequenciaConsultas;
        [LoadColumn(3)] public float AderenciaTratamento;
        [LoadColumn(4)] public bool HistoricoCaries;
        [LoadColumn(5)] public bool DoencaPeriodontal;
        [LoadColumn(6)] public float NumeroImplantes;
        [LoadColumn(7)] public bool TratamentosComplexosPrevios;
        [LoadColumn(8)] public bool Fumante;
        [LoadColumn(9)] public bool Alcoolismo;
        [LoadColumn(10)] public float EscovacaoDiaria;
        [LoadColumn(11)] public bool UsoFioDental;
        [LoadColumn(12)] public string DoencasSistemicas;
        [LoadColumn(13)] public float MedicamentosUsoContinuo;
        [LoadColumn(14)] public float NumeroSinistrosPrevios;
        [LoadColumn(15)] public string TipoPlano;
        [LoadColumn(16)] public float ProbabilidadeSinistro;
    }

    public class OutputPrediction
    {
        [ColumnName("Score")]
        public float PredictedProbabilidadeSinistro;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var mlContext = new MLContext();

            string dataPath = Path.Combine(AppContext.BaseDirectory, "dados_sinistro.csv");

            IDataView data = mlContext.Data.LoadFromTextFile<InputData>(
                path: dataPath,
                hasHeader: true,
                separatorChar: ',');

            var pipeline = mlContext.Transforms.Categorical.OneHotEncoding(new[]
            {
                new InputOutputColumnPair("GeneroEncoded", "Genero"),
                new InputOutputColumnPair("DoencasSistemicasEncoded", "DoencasSistemicas"),
                new InputOutputColumnPair("TipoPlanoEncoded", "TipoPlano")
            })
            .Append(mlContext.Transforms.Conversion.ConvertType(new[]
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
            .Append(mlContext.Transforms.Concatenate("Features",
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
            .Append(mlContext.Regression.Trainers.FastTree(labelColumnName: nameof(InputData.ProbabilidadeSinistro)));

            // Treinando o modelo e expotando 

            var model = pipeline.Fit(data);

            string modelPath = "ModeloSinistro.zip";
            mlContext.Model.Save(model, data.Schema, modelPath);
            Console.WriteLine("✅ Modelo salvo em: " + modelPath);

            using var stream = new FileStream(modelPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            var loadedModel = mlContext.Model.Load(stream, out var modelInputSchema);
            var predictionEngine = mlContext.Model.CreatePredictionEngine<InputData, OutputPrediction>(loadedModel);

            // Teste

            var exemplo = new InputData
            {
                Idade = 50,
                Genero = "M",
                FrequenciaConsultas = 2,
                AderenciaTratamento = 0.90f,
                HistoricoCaries = true,
                DoencaPeriodontal = true,
                NumeroImplantes = 1,
                TratamentosComplexosPrevios = false,
                Fumante = false,
                Alcoolismo = false,
                EscovacaoDiaria = 2,
                UsoFioDental = true,
                DoencasSistemicas = "Diabetes",
                MedicamentosUsoContinuo = 2,
                NumeroSinistrosPrevios = 1,
                TipoPlano = "premium",
                ProbabilidadeSinistro = 0 // nao usa
            };

            var prediction = predictionEngine.Predict(exemplo);
            Console.WriteLine($"🔍 Previsão de sinistro: {prediction.PredictedProbabilidadeSinistro:F4}");
        }
    }
}