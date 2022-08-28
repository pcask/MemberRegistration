using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.Utilities.Mappings
{
    public class AutoMapperHelper
    {
        public static List<T> MapToSameTypeList<T>(List<T> list)
        {
            try
            {
                MapperConfiguration config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<T, T>().IgnoreAllOfIEntity();
                });

                IMapper mapper = config.CreateMapper();

                return mapper.Map<List<T>>(list);
            }
            catch (Exception)
            {
                throw new Exception("Mapping operation has failed!");
            }

        }


        public static TDestination Map<TSource, TDestination>(TSource source)
        {
            try
            {
                MapperConfiguration config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<TSource, TDestination>().IgnoreAllOfIEntity();
                });

                IMapper mapper = config.CreateMapper();

                return mapper.Map<TDestination>(source);
            }
            catch (Exception)
            {
                throw new Exception("Mapping operation has failed!");
            }
        }
    }
}
