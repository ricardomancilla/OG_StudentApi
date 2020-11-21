using AutoMapper;
using Student.API.DTO;

namespace Student.API.Mapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<DataAccess.Model.Student, StudentDto>();
            CreateMap<StudentDto, DataAccess.Model.Student>();
        }
    }
}
