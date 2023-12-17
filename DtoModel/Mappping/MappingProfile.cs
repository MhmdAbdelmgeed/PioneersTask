using AutoMapper;
using DataModel;

namespace DtoModel.Mappping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TransactionDTO, TransactionEntity>().ReverseMap();
            CreateMap<GoodDTO, GoodEnitty>().ReverseMap();

        }
    }
}
