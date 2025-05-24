using Microsoft.ML.Data;

namespace Checkpoint_5_6.Domain.Models
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
}
