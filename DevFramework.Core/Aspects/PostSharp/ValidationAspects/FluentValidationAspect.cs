using DevFramework.Core.CrossCuttingConcerns.Validation.FluentValidation;
using FluentValidation;
using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.Aspects.PostSharp.ValidationAspects
{
    [Serializable]
    public class FluentValidationAspect : OnMethodBoundaryAspect
    {
        private readonly Type _validatorType;
        private IValidator validator;
        public FluentValidationAspect(Type validatorType)
        {
            _validatorType = validatorType;
        }

        public override void RuntimeInitialize(MethodBase method)
        {
            if (typeof(IValidator).IsAssignableFrom(_validatorType) == false)
                throw new Exception("The validator type used is not an Ivalidator");

            validator = (IValidator)Activator.CreateInstance(_validatorType);

            base.RuntimeInitialize(method);
        }

        // Attribute'u uyguladığımız method'ın hangi aşamasında işlem yapacaksak o aşamayı temsil eden methodları override etmemiz yeterli olacaktır.
        public override void OnEntry(MethodExecutionArgs args)
        {
            Type entityType = _validatorType.BaseType.GetGenericArguments()[0];

            var entities = args.Arguments.Where(a => a.GetType() == entityType);

            foreach (var entity in entities)
            {
                ValidatorTool.FluentValidate(validator, entity);
            }
        }
    }
}
