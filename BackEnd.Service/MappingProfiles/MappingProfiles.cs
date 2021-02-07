using AutoMapper;
using BackEnd.DAL.Entities;
using BackEnd.Service.DTO.CategoriesDto;
using BackEnd.Service.DTO.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackEnd.Service.MappingProfiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
           
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Nationality, NationalityDto>().ReverseMap();
            CreateMap<City, CityDto>().ReverseMap();

            #region Category
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<SubCategory, ShowSubCategoryDto>().
           ForMember(x => x.CategoryName, x => x.MapFrom(x => x.Category.CategoryName)).ReverseMap();
            CreateMap<SubCategory, SubCategoryDto>().ReverseMap();
            #endregion

        }

        string getPath()
        {
            return "/wwwroot/UploadFiles/";
        }


    }
}