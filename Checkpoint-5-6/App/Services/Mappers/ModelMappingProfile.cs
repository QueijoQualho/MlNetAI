using AutoMapper;
using Checkpoint_5_6.App.Dtos;
using Checkpoint_5_6.Domain.Models;

namespace Checkpoint_5_6.App.Services.Mappers
{
    public class ModelMappingProfile : Profile
    {
        public ModelMappingProfile()
        {
            CreateMap<PredictionInput, InputData>()
                .ForMember(dest => dest.ProbabilidadeSinistro, opt => opt.Ignore());
        }
    }
}
