using zzu_university.domain.DTOS.ProgramDto;

namespace zzu_university.data.DTOs
{
    public class FacultyDto
    {
        public int FacultyId { get; set; }

        public string Name { get; set; }
      public List<ProgramCreateDto> Programs { get; set; }
    }
}
