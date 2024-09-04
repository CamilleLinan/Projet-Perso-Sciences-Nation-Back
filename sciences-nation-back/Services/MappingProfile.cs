using AutoMapper;
using sciences_nation_back.Models;
using sciences_nation_back.Models.Dto;

namespace sciences_nation_back.Services
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<User, UserDto>();
			CreateMap<UserDto, User>();
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
        }
	}
}