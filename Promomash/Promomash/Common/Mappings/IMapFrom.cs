using AutoMapper;

namespace Promomash.Demo.App.Common.Mappings
{
    /// <summary>
    /// Base interface of mapped objects
    /// </summary>
    /// <typeparam name="T">Type of source mapping object</typeparam>
    public interface IMapFrom<T>
    {
        /// <summary>
        /// Creates mapping profile between source and destination objects
        /// </summary>
        /// <param name="profile">AutoMapper profile</param>
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}
