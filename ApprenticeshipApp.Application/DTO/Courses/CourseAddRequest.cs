using ApprenticeshipApp.Domain.Entities;
using ApprenticeshipApp.Domain.IdentityEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprenticeshipApp.Application.DTO.Courses
{
    public class CourseAddRequest
    {
        [Required(ErrorMessage = "Title can't be blank")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description can't be blank")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Duration can't be blank")]
        [Range(1, int.MaxValue)]
        public int DurationInHours { get; set; }

        [Required(ErrorMessage = "Instructor ID can't be blank")]
        public Guid InstructorId { get; set; }

        public Course ToCourse()
        {
            return new Course
            {
                Title = Title,
                Description = Description,
                DurationInHours = DurationInHours,
                InstructorId = InstructorId
            };
        }
    }
}
