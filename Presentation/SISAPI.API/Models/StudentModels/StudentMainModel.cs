using SISAPI.Domain.Entities;

namespace SISAPI.API.Models.StudentModels
{
    public class StudentMainModel
    {
        public Student student { get; set; }
        public Academic advisor { get; set; }
        public GradeInformation gradeInformation { get; set; }
    }
}
