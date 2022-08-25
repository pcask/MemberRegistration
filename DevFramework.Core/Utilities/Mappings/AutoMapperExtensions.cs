using AutoMapper;
using DevFramework.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.Utilities.Mappings
{
    // Olurda Automapper'da direk entity'ler üzerinde çalışmak gerekirse navigation property'lerin mapping işlemine tabi tutulMAMAsını isteriz özellikte 
    // EntityFramework ile çalışırken navigation propertylerin serialize edilmesi sırasında hata ile karşılaşıyoruz.
    // Bu durumun önüne geçmek için aşağıdaki gibi bir extension method yazmamız yeterli olacaktır.
    // Alt yapımızın hem EntityFramework hem de NHbirnate desteklemesi gerektiğinde entitylerimizin tüm propertyleri virtual işaretlememiz gerekebilir,
    // Burada virtual olanları ignore et dersek bu sefer de projemizin NHibernate desteğini kesmiş oluruz.
    // Navigation propertylerin her birinin aslında bir Entity olduğunu ve her birini IEntity interface'i ile imzaladığımıza göre sadece IEntity'leri
    // ignore etmek yeterli olacaktır.
    public static class AutoMapperExtensions
    {
        public static IMappingExpression<TSource, TDestination> IgnoreAllOfIEntity<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
        {
            var desType = typeof(TDestination);
            var pInfo = desType.GetProperties();
            foreach (var property in desType.GetProperties().Where(p => typeof(IEntity).IsAssignableFrom(p.PropertyType)))
            {
                expression.ForMember(property.Name, opt => opt.Ignore());
            }

            return expression;
        }
    }
}
