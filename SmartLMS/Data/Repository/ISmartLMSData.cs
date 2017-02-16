using SmartLMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLMS.Data.Repository
{
    public interface ISmartLMSData
    {
        IRepository<ApplicationUser> Users { get; }
        IRepository<Lecture> Lectures { get; }
        IRepository<Category> Categories { get; }
        IRepository<Course> Courses { get; }
        int SaveChanges();
    }
}
