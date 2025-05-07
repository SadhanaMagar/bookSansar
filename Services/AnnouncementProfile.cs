using AutoMapper;
using bookSansar.DTO;
using bookSansar.Entities;

namespace bookSansar.Services
{
    public class AnnouncementProfile : Profile
    {
        public AnnouncementProfile()
        {
            CreateMap<AnnouncementCreateDto, Announcement>()
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true));

            CreateMap<Announcement, AnnouncementDto>();
        }
    }
}