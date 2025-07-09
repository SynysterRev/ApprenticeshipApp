using ApprenticeshipApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprenticeshipApp.Domain.Repositories
{
    public interface ICoursesRepository
    {
        /// <summary>
        /// Get all courses stocked in the database
        /// </summary>
        /// <returns>A list of all courses</returns>
        public Task<List<Course>> GetAllCourses();

        /// <summary>
        /// Get all courses related to an instructor
        /// </summary>
        /// <param name="instructorGuid">The ID of the instructor</param>
        /// <returns>A list of all the courses of an instructor</returns>
        public Task<List<Course>> GetAllCoursesByInstructor(Guid instructorGuid);

        /// <summary>
        /// Get course matching with ID if one
        /// </summary>
        /// <param name="courseId">The guid of the wanted course</param>
        /// <returns>The course if any matching</returns>
        public Task<Course?> GetCourseByGuid(Guid courseId);

        /// <summary>
        /// Add a new course in the database
        /// </summary>
        /// <param name="newCourse">The new course to add</param>
        /// <returns>The added course</returns>
        public Task<Course> AddCourse(Course newCourse);

        /// <summary>
        /// Update the course with the same ID
        /// </summary>
        /// <param name="updatedCourse">The updated course</param>
        /// <returns>The updated course</returns>
        public Task<Course> UpdateCourse(Course updatedCourse);

        /// <summary>
        /// Delete the course matching with the ID
        /// </summary>
        /// <param name="courseId">The ID of the course to delete</param>
        /// <returns>True if delete, false otherwise</returns>
        public Task<bool> DeleteCourse(Guid courseId);
    }
}
