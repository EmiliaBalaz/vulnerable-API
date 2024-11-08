using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using secure_online_bookstore.Models;


namespace secure_online_bookstore
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Book,GetBookDto>();
            CreateMap<Book,AddBookDto>(); 
            CreateMap<GetBookDto,Book>();
            CreateMap<AddBookDto,Book>();
            CreateMap<UpdateBookDto,Book>();
            CreateMap<Book,UpdateBookDto>();
            CreateMap<RegisterUserDto, RegisterUser>();
            CreateMap<RegisterUser, RegisterUserDto>();
        }
    }
}