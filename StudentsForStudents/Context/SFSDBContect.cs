using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;
using MySqlX.XDevAPI;
using SmartLineSystem.Models;
using StudentsForStudents.Models;
using StudentsForStudents.Models.EntityTypeConfig;

namespace StudentsForStudents.Context
{
    public class SFSDBContect : IdentityDbContext<ApplicationUser>
    {
        public SFSDBContect(DbContextOptions<SFSDBContect> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration<Student>(new StudentEntityTypeConfig());
            modelBuilder.ApplyConfiguration<Teacher>(new TeacherEntityTypeConfig());
            modelBuilder.ApplyConfiguration<Lecture>(new LectureEntityTypeConfig());
            modelBuilder.ApplyConfiguration<Chat>(new ChatEntityTypeConfig());
            //  modelBuilder.ApplyConfiguration<ChatViewModel>(new ChatViewModelEntityTypeConfig());
            modelBuilder.ApplyConfiguration<CourseRequest>(new CourseRequestEntityTypeConfig());

            modelBuilder.ApplyConfiguration<Courses>(new CoursesEntityTypeConfig());
            modelBuilder.ApplyConfiguration<Payment>(new PaymentEntityTypeConfig());
            modelBuilder.ApplyConfiguration<Event>(new EventEntityTypeConfig());
            modelBuilder.ApplyConfiguration<Rating>(new RatingEntityTypeConfig());



        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<Lecture> Lecture { get; set; }

        public DbSet<Courses> Courses { get; set; }
        public DbSet<CourseRequest> CourseRequests { get; set; }

        public DbSet<Chat> Chats { get; set; }
        public DbSet<Payment> Payment { get; set; }

        public DbSet<Event> Events { get; set; }
        public DbSet<Rating> Rating { get; set; }

        // public DbSet<ChatViewModel> ChatViewModel { get; set; }

    }
}

