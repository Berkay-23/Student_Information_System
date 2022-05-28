using SISAPI.Domain.Entities;
using System.Collections.Generic;

namespace SISAPI.API.Models.StudentModels
{
    public class ReceivedLessonsModel
    {
        public List<Lesson> Lessons { get; set; }
        public List<string> Periods { get; set; }
        public int SelectedPeriod { get; set; }
    }
}
