using AutoMapper;
using Sprout.Exam.DataAccess.Dtos;
using Sprout.Exam.Models;

namespace Sprout.Exam.DataAccess.MapperProfiles
{
    public class EmployeeMapper:Profile
    {
        public EmployeeMapper()
        {
            CreateMap<EmployeeAddDto, Employee>()
                .AfterMap((src, dest) =>
                {
                    dest.FullName = dest.FullName.Trim();
                    dest.Tin = dest.Tin.Trim();
                });

            CreateMap<EmployeeEditDto, Employee>()
                .ForMember(e => e.Id, opt => opt.Ignore());
        }
    }
}
