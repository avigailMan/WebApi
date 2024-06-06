using AutoMapper;
using DTO;
using Entities;

namespace MyShop
{
    public class AutoMapper:Profile
    {
        public AutoMapper()
        {
            
                CreateMap<Product, ProductDtoGet>().ReverseMap();
                CreateMap<Product, ProductDtoPost>().ReverseMap();
                CreateMap<Category, CategotyDtoPost>().ReverseMap();
                CreateMap<Category, CategotyDtoGet>().ReverseMap();
                CreateMap<User, UserDtoGet>().ReverseMap();
                CreateMap<User, UserDtoRegisterAndUpdate>().ReverseMap();
                CreateMap<User, UserDtoLogin>().ReverseMap();
                CreateMap<Order, OrderDtoPost>().ReverseMap();
                CreateMap<OrederItem, OrderItemDtoPost>().ReverseMap();


           

        }
    }
}
