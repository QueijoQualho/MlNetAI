using AutoMapper;
using Checkpoint_5_6.App.Dtos;
using Checkpoint_5_6.Domain.Models;
using Checkpoint_5_6.Infra.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Checkpoint_5_6.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PredictionController : ControllerBase
    {
        private readonly IModelPredictor _predictor;
        private readonly IMapper _mapper;

        public PredictionController(IModelPredictor predictor, IMapper mapper)
        {
            _predictor = predictor;
            _mapper = mapper;
        }

        [HttpPost("predict")]
        public ActionResult<OutputPrediction> Predict([FromBody] PredictionInput input)
        {
            var inputData = _mapper.Map<InputData>(input);
            var prediction = _predictor.Predict(inputData);
            return Ok("Probalidade de Sinistro: "+ prediction.Score + "%");
        }
    }

}
