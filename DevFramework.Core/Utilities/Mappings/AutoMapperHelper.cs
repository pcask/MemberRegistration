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

                var result = mapper.Map<List<T>>(list);
                return result;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
