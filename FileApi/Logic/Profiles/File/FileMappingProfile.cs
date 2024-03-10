using AutoMapper;
using Dal.Models;
using Logic.Models;
using Logic.Records.File.Params;
using Logic.Records.File.Results;

namespace Logic.Profiles.File;

/// <summary>
/// Маппинг классов, связанных с файлом
/// </summary>
public class FileMappingProfile : Profile
{
    public FileMappingProfile()
    {
        CreateMap<FileDal, GetFileResult>()
            .ForMember(x => x.OriginalFileName, y => y.MapFrom(z => z.OriginalFileName))
            .ForMember(x => x.HandledFileName, y => y.MapFrom(z => z.HandledFileName))
            .ForMember(x => x.FileExtension, y => y.MapFrom(z => z.FileExtension))
            .ForMember(x => x.CreatedTime, y => y.MapFrom(z => z.CreatedTime))
            .ForMember(x => x.FileSize, y => y.MapFrom(z => z.FileSize));

        CreateMap<FileDal, FileModel>()
            .ForMember(x => x.FileId, y => y.MapFrom(z => z.Id))
            .ForMember(x => x.OriginalFileName, y => y.MapFrom(z => z.OriginalFileName))
            .ForMember(x => x.HandledFileName, y => y.MapFrom(z => z.HandledFileName))
            .ForMember(x => x.FileExtension, y => y.MapFrom(z => z.FileExtension))
            .ForMember(x => x.CreatedTime, y => y.MapFrom(z => z.CreatedTime))
            .ForMember(x => x.FileSize, y => y.MapFrom(z => z.FileSize))
            .ForMember(x => x.AccessModifier, y => y.MapFrom(z => z.AccessModifier));
        
        CreateMap<UpdateFileParam, FileDal>()
            .ForMember(x => x.Id, y => y.MapFrom(z => z.FileId))
            .ForMember(x => x.AccessModifier, y => y.MapFrom(z => z.AccessModifier))
            .ForMember(x => x.CreatedTime, y => y.NullSubstitute(null));
    }
}