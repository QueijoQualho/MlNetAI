namespace Checkpoint_5_6.App.Dtos
{
    public class PredictionInput
    {
        public float Idade { get; set; }
        public string Genero { get; set; }
        public float FrequenciaConsultas { get; set; }
        public float AderenciaTratamento { get; set; }
        public bool HistoricoCaries { get; set; }
        public bool DoencaPeriodontal { get; set; }
        public float NumeroImplantes { get; set; }
        public bool TratamentosComplexosPrevios { get; set; }
        public bool Fumante { get; set; }
        public bool Alcoolismo { get; set; }
        public float EscovacaoDiaria { get; set; }
        public bool UsoFioDental { get; set; }
        public string DoencasSistemicas { get; set; }
        public float MedicamentosUsoContinuo { get; set; }
        public float NumeroSinistrosPrevios { get; set; }
        public string TipoPlano { get; set; }
    }
}
