using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountBooks.Repository
{
    public class UnityConfig
    {

          #region 属性

        public static IUnityContainer Container
        {
            get
            {
                return container.Value;
            }
        }

        #endregion

        #region 方法

        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(
            () =>
            {
                var container = new UnityContainer();

                RegisterTypes(container);

                return container;
            });

        private static void RegisterTypes(IUnityContainer container)
        {
            container.LoadConfiguration();
        }

        public static T Resolve<T>(IDictionary<string, object> paramDict = null)
        {
            var list = new ParameterOverrides();

            if (paramDict != null && paramDict.Count > 0)
            {
                foreach (var item in paramDict)
                {
                    list.Add(item.Key, item.Value);
                }
            }

            return Container.Resolve<T>(list);
        }
 
        #endregion

    }
}