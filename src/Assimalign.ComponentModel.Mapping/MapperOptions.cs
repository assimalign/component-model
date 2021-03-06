
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Mapping
{
    using Assimalign.ComponentModel.Mapping.Internal;
    using Assimalign.ComponentModel.Mapping.Abstractions;

    /// <summary>
    /// 
    /// </summary>
    public sealed partial class MapperOptions
    {
        private readonly IList<IMapperProfile> profiles;

        /// <summary>
        /// 
        /// </summary>
        public MapperOptions()
        {
            this.profiles = new List<IMapperProfile>();
        }


        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<IMapperProfile> Profiles => profiles;




        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="profile"></param>
        /// <returns></returns>
        public MapperOptions AddProfile<TSource, TTarget>(IMapperProfile<TSource, TTarget> profile)
        {   
            profile.Configure(new MapperProfileDescriptor<TSource, TTarget>(profile.Context));

            //if (this.ContainsKey(hashcode))
            //{
            //    throw new Exception("Profile already exists");
            //}

            AddProfile(profile as IMapperProfile);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="configure"></param>
        /// <returns></returns>
        public MapperOptions AddProfile<TSource, TTarget>(Func<IMapperProfile<TSource, TTarget>> configure)
        {
            var profile = configure.Invoke();
            var context = new MapperProfileContext();
            var descriptor = new MapperProfileDescriptor<TSource, TTarget>(context);

            if (this.profiles.Contains(profile))
            {
                throw new Exception("Profile already exists");
            }

            profile.Configure(descriptor);
            AddProfile(profile);
            return this;
        }


        private void AddProfile(IMapperProfile profile)
        {
            this.profiles.Add(profile);

            foreach (var child in profile.Context.Profiles)
            {
                if (child.Context.Profiles is not null && child.Context.Profiles.Any())
                {
                    AddProfile(child);
                }
                else
                {
                    this.profiles.Add(child);
                }
            }
        }
    }
}
