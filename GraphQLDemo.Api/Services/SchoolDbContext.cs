using GraphQLDemo.Api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.Api.Services;

public class SchoolDbContext : DbContext
{
    public SchoolDbContext(DbContextOptions<SchoolDbContext> options) 
        : base(options) {   }

    public DbSet<CourseDTO> Courses { get; set; } = null!;
    public DbSet<StudentDTO> Students { get; set; } = null!;
    public DbSet<InstructorDTO> Instructors { get; set; } = null!;
}