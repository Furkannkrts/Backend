﻿using Core.Utilities.Interceptors;
using Core.Utilities.Messages;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.CrossCuttingConcerns.Validation;
using Castle.DynamicProxy;

namespace Core.Aspect.Autofac.Validation
{
    public class ValidationAspect:MethodInterceptions
    {
        private Type _validatorType;//validatortype göndermek için ekliyoruz
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))//gönderilen validator type bir ıvalidator değilse
            {
                throw new Exception(AspectMessages.WrongValidationType);
            }
            _validatorType = validatorType; 
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator=(IValidator)Activator.CreateInstance(_validatorType);
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
             var entities =invocation.Arguments.Where(t=>t.GetType() == entityType);
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
