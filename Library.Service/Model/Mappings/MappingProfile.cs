using AutoMapper;
using Library.Db.Models;
using Library.Services.Model.Dto;


namespace Library.Services.Model.Mappings {


    public class MappingProfile : Profile {


        public MappingProfile() {

            CreateMap<BookModel, BookDto>();
            CreateMap<BookDto, BookModel>();

        }
    

    }


}