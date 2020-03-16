using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Test_Project.BLL.Models;
using Test_Project.DAL.Database.Entities;
using Test_Project.DAL.Models;

namespace Test_Project.BLL.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EntityBase, BaseModel>();
            CreateMap<City, CityModel>();
            CreateMap<MobileNumber, MobileNumberModel>();
            CreateMap<Person, PersonModel>();
            CreateMap<Person, PersonViewModel>();
            CreateMap<RelatedPerson, RelatedPersonModel>();
            CreateMap<RelatedPersonCount, RelatedPersonCountModel>();
            CreateMap<GroupRelatedPersonByRelationType, GroupRelatedPersonByRelationTypeModel>();
            CreateMap<PersonFilter, PersonFilterModel>();

            CreateMap<BaseModel, EntityBase>();
            CreateMap<CityModel, City>();
            CreateMap<MobileNumberModel, MobileNumber>();
            CreateMap<PersonModel, Person>();
            CreateMap<RelatedPersonModel, RelatedPerson>();
            CreateMap<PersonFilterModel, PersonFilter>();
        }
    }
}
