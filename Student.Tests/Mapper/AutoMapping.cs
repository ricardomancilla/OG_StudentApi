using AutoMapper;
using Student.API.DTO;

namespace Student.Tests.Mapper
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
