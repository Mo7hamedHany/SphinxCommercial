using AutoMapper;
using SphinxCommercial.Core.DataTransferObjects;
using SphinxCommercial.Core.Models;

namespace SphinxCommercial.Presentation.Helpers

{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ClientProduct, ClientProductToReturnDto>()
                .ForMember(d => d.ProductName , o=> o.MapFrom(s => s.Product.Name))
                .ForMember(d => d.ClientName, o => o.MapFrom(s => s.Client.Name)).ReverseMap();

            CreateMap<ClientProductToCreateDto, ClientProduct>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id)).ReverseMap();    

            CreateMap<Product, ProductDto>().ReverseMap();

            CreateMap<Client, ClientDto>()
                .ReverseMap();
        }
    }
}
