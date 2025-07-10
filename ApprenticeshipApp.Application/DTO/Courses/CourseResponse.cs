using ApprenticeshipApp.Domain.Entities;
using ApprenticeshipApp.Domain.IdentityEntities;

namespace ApprenticeshipApp.Application.DTO.Courses
{
    public class CourseResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int DurationInHours { get; set; }
        public Guid InstructorId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public ApplicationUser Instructor { get; set; } = null!;

        /// <summary>
        /// Compares the current object data with the data of the parameter object
        /// </summary>
        /// <param name="obj">The object to compare</param>
        /// <returns>True if the objects are equals, false otherwise</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj.GetType() != typeof(CourseResponse)) return false;

            CourseResponse other = (CourseResponse)obj;

            return Id == other.Id && Title == other.Title && Description == other.Description && DurationInHours == other.DurationInHours 
                && InstructorId == other.InstructorId && Instructor == other.Instructor;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"Course ID: {Id}, Title: {Title}, Description: {Description}, Duration: {DurationInHours}h, Instructor: {Instructor.UserName}";
        }
    }

    public static class CourseExtensions
    {
        /// <summary>
        /// An extension method to convert an object of Course class into CourseResponse class
        /// </summary>
        /// <param name="course">The Course object to convert</param>
        /// <returns>The converted CourseResponse object</returns>
        public static CourseResponse ToCourseResponse(this Course course)
        {
            return new CourseResponse
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                DurationInHours = course.DurationInHours,
                InstructorId = course.InstructorId,
                CreatedAt = course.CreatedAt,
                UpdatedAt = course.UpdatedAt,
                Instructor = course.Instructor,
            };
        }
    }
}
