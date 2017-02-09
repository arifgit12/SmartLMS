using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;

namespace SmartLMS.Models
{
    public enum UserRoles
    {
        Administrator = 1,
        Manager = 2,
        Lecturer = 4,
        User = 8,
    }
    public class ApplicationUser : IdentityUser
    {
        public bool ConfirmedEmail { get; set; }
        public ICollection<StudentAssignment> Assignments { get; set; }
        public virtual ICollection<StudentCourse> Courses { get; set; }
        public ICollection<StudentQuiz> Quiz { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<AnswerChoice> Answerchoices { get; set; }
        public DbSet<Quiz> Quiz { get; set; }
        public DbSet<Assignment> Assignment { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Subscribe> Subscribe { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<StudentQuiz> StudentQuiz { get; set; }
        public DbSet<ADLog> ADLogs { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<StudentCourse>()
                    .HasKey(t => new { t.StudentId, t.CourseId });


                modelBuilder.Entity<StudentCourse>()
                    .HasRequired(pt => pt.Student)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(pt => pt.StudentId);

                modelBuilder.Entity<StudentCourse>()
                    .HasRequired(pt => pt.Course)
                    .WithMany(t => t.Enrollments)
                    .HasForeignKey(pt => pt.CourseId);            

                modelBuilder.Entity<StudentQuiz>()
                    .HasKey(t => new { t.StudentId, t.QuizId });


                modelBuilder.Entity<StudentQuiz>()
                    .HasRequired(pt => pt.Student)
                    .WithMany(p => p.Quiz)
                    .HasForeignKey(pt => pt.StudentId);

                modelBuilder.Entity<StudentQuiz>()
                    .HasRequired(pt => pt.Quiz)
                    .WithMany(t => t.Students)
                    .HasForeignKey(pt => pt.QuizId);


                modelBuilder.Entity<StudentAssignment>()
                    .HasKey(t => new { t.StudentId, t.AssignmentId });


                modelBuilder.Entity<StudentAssignment>()
                    .HasRequired(pt => pt.Student)
                    .WithMany(p => p.Assignments)
                    .HasForeignKey(pt => pt.StudentId);

                modelBuilder.Entity<StudentAssignment>()
                    .HasRequired(pt => pt.Assignment)
                    .WithMany(t => t.Students)
                    .HasForeignKey(pt => pt.AssignmentId);

        }
    }
}