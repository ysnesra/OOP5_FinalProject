using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;


namespace Core.Aspects.Autofac.Validation
{
        //ValidationAspect isminde Attribute oluştururuz
        public class ValidationAspect : MethodInterception
        {
            private Type _validatorType;
            public ValidationAspect(Type validatorType)   //hangi validator tipine göre doğrulayacak onu yazarız
            {
                if (!typeof(IValidator).IsAssignableFrom(validatorType))   //validatorType bir IValidator mı onu sorar
            {
                    throw new System.Exception("Bu bir doğrulama sınıfı değil");
                }

                _validatorType = validatorType;
            }
            protected override void OnBefore(IInvocation invocation)
            {
                //"Activator.CreateInstance" ile çalışma anında instance oluşturma ,yani newleyip nesne oluşturur
                var validator = (IValidator)Activator.CreateInstance(_validatorType);    //ProductValidatorun instance nı oluştur
                var entityType = _validatorType.BaseType.GetGenericArguments()[0];   //ProductValidatorun çalışma tipini bul
                var entities = invocation.Arguments.Where(t => t.GetType() == entityType);   //metotun parametrelerini bul 
                foreach (var entity in entities)
                {
                    ValidationTool.Validate(validator, entity);  //gelen parametreyi Validate et

                }
            }
        }
    
}
