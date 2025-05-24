using Checkpoint_5_6.Infra.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Checkpoint_5_6.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModelTrainingController : ControllerBase
    {
        private readonly IDataLoader _dataLoader;
        private readonly IModelTrainer _trainer;

        public ModelTrainingController(IDataLoader dataLoader, IModelTrainer trainer)
        {
            _dataLoader = dataLoader;
            _trainer = trainer;
        }

        [HttpGet("train")]
        public IActionResult Train()
        {
            var data = _dataLoader.LoadData("Infra/Data/dados_sinistro.csv");
            var model = _trainer.TrainModel(data);
            _trainer.SaveModel(model, "Infra/IA/ModeloSinistro.zip", data.Schema);

            return Ok("Modelo treinado e salvo.");
        }
    }

}
