using AutoMapper;

namespace TaskManagementSystem.Application.Common.Mappings
{
    public interface IMapTo<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(GetType(), typeof(T));
    }
}