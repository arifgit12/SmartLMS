using SmartLMS.Models;
using SmartLMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartLMS.Infrastructure.Mapping
{
    public class AutoMapperConfiguration
    {
        public static void RegisterMaps()
        {
            AutoMapper.Mapper.Initialize(config => 
            {
                config.CreateMap<Category, CategoryViewModel>();
                config.CreateMap<CategoryViewModel, Category>();
            });
        }
    }
}