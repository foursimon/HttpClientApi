using HttpClientApi.Models.Entity;
using HttpClientApi.Models.DTOs;
using AutoMapper;

namespace HttpClientApi.Mapper
{
	public class MapperProfile : Profile
	{
		public MapperProfile()
		{
			CreateMap<PostDto, Post>().ReverseMap();
		}
	}
}
