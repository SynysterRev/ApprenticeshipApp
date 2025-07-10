using ApprenticeshipApp.Domain.Entities;
using ApprenticeshipApp.Domain.Repositories;
using ApprenticeshipApp.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace ApprenticeshipApp.Infrastructure.Repositories
{
    public class CoursesRepository : ICoursesRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CoursesRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Course> AddCourseAsync(Course newCourse)
        {
            _dbContext.Courses.Add(newCourse);
            await _dbContext.SaveChangesAsync();

            return newCourse;
        }

        public async Task<bool> DeleteCourseAsync(Guid courseId)
        {
            Course? foundCourse = await _dbContext.Courses.FindAsync(courseId);
            if (foundCourse == null)
            {
                return false;
            }

            _dbContext.Courses.Remove(foundCourse);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Course>> GetAllCoursesAsync()
        {
            return await _dbContext.Courses.ToListAsync();
        }

        public async Task<List<Course>> GetAllCoursesByInstructorAsync(Guid instructorGuid)
        {
            return await _dbContext.Courses
                .Where(c => c.InstructorId == instructorGuid)
                .ToListAsync();
        }

        public async Task<Course?> GetCourseByGuidAsync(Guid courseId)
        {
            return await _dbContext.Courses.FindAsync(courseId);
        }

        public async Task<Course?> UpdateCourseAsync(Course updatedCourse)
        {
            Course? foundCourse = await _dbContext.Courses.FindAsync(updatedCourse.Id);
            if (foundCourse == null)
            {
                return null;
            }

            _dbContext.Courses.Entry(foundCourse).CurrentValues.SetValues(updatedCourse);
            await _dbContext.SaveChangesAsync();
            return foundCourse;
        }
    }
}
