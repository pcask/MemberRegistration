using DevFramework.Core.CrossCuttingConcerns.Validation.FluentValidation;
using FluentValidation;
using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.Aspects.PostSharp.ValidationAspects
{
    [Serializable]
    public class FluentValidationAspect : OnMethodBoundaryAspect
    {
        private readonly Type _validatorType;
        public FluentValidationAspect(Type validatorType)
        {
            _validatorType = validatorType;
        }


        // Attribute'u uyguladığımız method'ın hangi aşamasında işlem yapacaksak o aşamayı temsil eden methodları override etmemiz yeterli olacaktır.
        public override void OnEntry(MethodExecutionArgs args)
        {
            IValidator validator = (IValidator)Activator.CreateInstance(_validatorType);
            Type entityType = _validatorType.BaseType.GetGenericArguments()[0];

            var entities = args.Arguments.Where(a => a.GetType() == entityType);

            foreach (var entity in entities)
            {
                ValidatorTool.FluentValidate(validator, entity);
            }
        }
    }
}
