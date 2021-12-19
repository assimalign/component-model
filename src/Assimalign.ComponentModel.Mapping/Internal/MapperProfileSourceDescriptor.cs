using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Mapping.Internal
{
    using Assimalign.ComponentModel.Mapping.Abstractions;

    internal sealed class MapperProfileSourceDescriptor<TTarget> : 
        IMapperProfileSourceDescriptor<TTarget>
    {


        public void Ingore()
        {
            
        }

        public void Reverse()
        {
            
        }


        public IMapperProfileSourceDescriptor<TTarget> MapSource<TMember>(Expression<Func<TTarget, TMember>> expression)
        {



            return this;
        }

        public IMapperProfileSourceDescriptor<TTarget> MapSource(string member)
        {


            return this;
        }
    }
}
