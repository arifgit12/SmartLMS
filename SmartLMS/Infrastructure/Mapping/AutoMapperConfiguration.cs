using SmartLMS.Models;
using SmartLMS.ViewModels;

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

                config.CreateMap<Course, CourseViewModel>();
                config.CreateMap<CourseViewModel, Course>();

                config.CreateMap<Course, SeeLectureViewModel>()
                    .ForMember(c => c.LecturerUserName,
                        opt => opt.MapFrom(x => x.User.UserName));
            });
        }
    }
}