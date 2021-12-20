using Assimalign.ComponentModel.Mapping.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Mapping
{
    public sealed class MapperProfileContext
    {
        private readonly IList<IMapperProfile> profiles;

        public MapperProfileContext()
        {
            this.profiles = new List<IMapperProfile>();
        }



        /// <summary>
        /// 
        /// </summary>
        public Type SourceType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public MapperPaths SourcePaths { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public Type TargetType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public MapperPaths TargetPaths { get; set; }

        /// <summary>
        /// Sub Profiles are mapping profiles of nested child types. This is usually added
        /// when the 'AddProfile' API is called on the <see cref="IMapperProfileDescriptor{T,T}"/>
        /// </summary>
        public IEnumerable<IMapperProfile> SubProfiles => profiles;




        internal void AddSubProfile(IMapperProfile profile)
        {
            this.profiles.Add(profile);
        }
    }
}
