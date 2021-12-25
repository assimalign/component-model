using System;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Assimalign.ComponentModel.Mapping.Types
{
    /// <summary>
    /// 
    /// </summary>
    public record class MapperPath
    {
        private bool isMapped;


        public MapperPath(string path)
        {
            this.Path = path;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// 
        /// </summary>
        public Type PathType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public MapperPath? NestedPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsMapped => isMapped;



        internal void SetMapped(bool? isMapped)
        {
            if (isMapped is not null)
            {
                this.isMapped = isMapped ?? default;
            }
        }


        //public MapperPath 


        public static MapperPath Create<T>(string path)
            where T : class
        {
            return Create(path, typeof(T));
        }


        internal static IEnumerable<MapperPath> Create(Type type)
        {
            var paths = new List<MapperPath>();
            var members = type.GetMembers();





            return paths;
        }



        internal static MapperPath Create(string path, Type type)
        {
            var paths = path.Split('.');
            var member = type.GetMember("Member")
                .FirstOrDefault(x =>
                {
                    if (x.DeclaringType == type)
                    {
                        return x.MemberType == MemberTypes.Property || 
                               x.MemberType == MemberTypes.Field;
                    }
                    else
                    {
                        return false;
                    }
                });

            if (member is PropertyInfo property)
            {
                var nestedPath = paths.Length > 1 ? MapperPath.Create(string.Join('.', paths.Skip(1)), property.PropertyType) : null;

                return new MapperPath(paths[0])
                {
                    PathType = property.PropertyType,
                    NestedPath = nestedPath
                };
            }
            else if (member is FieldInfo field)
            {
                var nestedPath = paths.Length > 1 ? MapperPath.Create(string.Join('.', paths.Skip(1)), field.FieldType) : null;

                return new MapperPath(paths[0])
                {
                    PathType = field.FieldType,
                    NestedPath = nestedPath
                };
            }
            else
            {
                throw new Exception("");
            }
        }
    }
}
