using SISAPI.Domain.Entities;
using System.Collections.Generic;

namespace SISAPI.API.Models.StudentModels
{
    public class TranscriptInformationModel
    {
        public List<List<Note>> Notes { get; set; }
        public List<List<Lesson>> Lessons { get; set; }
        public StudentMainModel StudentInfos { get; set; }
    }
}
