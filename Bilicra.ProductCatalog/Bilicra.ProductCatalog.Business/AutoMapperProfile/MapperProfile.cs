using AutoMapper;
using Bilicra.ProductCatalog.Common.Entities;
using Bilicra.ProductCatalog.Common.Models.ProductModels;
using Bilicra.ProductCatalog.Common.Models.UserModels;

namespace Bilicra.ProductCatalog.Business.AutoMapperProfile
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserEntity, UserModel>().ReverseMap();
            CreateMap<UserEntity, UserPostModel>().ReverseMap();

            CreateMap<ProductEntity, ProductModel>().ReverseMap();
            CreateMap<ProductEntity, ProductPostModel>().ReverseMap();
            CreateMap<ProductEntity, ProductPutModel>().ReverseMap();
        }
    }
}
