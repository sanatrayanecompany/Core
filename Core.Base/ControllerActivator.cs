using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Core.Base
{
    internal class CoreControllerActivator : IHttpControllerActivator
    {
        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor,
            Type controllerType)
        {
            try
            {

                var resolver = new Resolver();

                resolver.RegisterType<IContext, Context>();
                resolver.RegisterType<IToken, Token>();

                foreach (ConstructorInfo ctor in controllerType.GetConstructors())
                {
                    ParameterInfo[] parameters = ctor.GetParameters();

                    var ctorParams = new object[parameters.Length];

                    parameters = parameters.Where(x => x.ParameterType.BaseType == typeof(_Service)).ToArray();

                    for (int i = 0; i < parameters.Length; i++)
                    {
                        ctorParams[i] = Activator.CreateInstance(parameters[i].ParameterType);

                        foreach (FieldInfo fi in ctorParams[i].GetType().BaseType
                            .GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
                        {
                            fi.SetValue(ctorParams[i], resolver.GetType(fi.FieldType));
                        }
                    }

                    return ctor.Invoke(ctorParams) as IHttpController;
                }

                return Activator.CreateInstance(controllerType) as IHttpController;
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }
    }
}