using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Cryptographer
{
    public class Cryptographer
    {
        //public void ToView(object model)
        //{
        //    if (model != null)
        //    {
        //        foreach (var attr in model.GetType().GetProperties())
        //        {
        //            if (attr.CustomAttributes != null && attr.GetCustomAttribute(typeof(EncryptedOnView)) != null)
        //            {
        //                if (attr.PropertyType.IsGenericType && attr.PropertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
        //                    MaybeShouldBeEncrypted(attr, model);
        //                else //aplica a criptografia
        //                    attr.SetValue(model, "5555");
        //            }
        //        }
        //    }
        //}

        public void ToView(object model)
        {
            if (model != null)
            {
                if (model.GetType().GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
                {
                    var list = new[] { model };
                    foreach (var obj in (dynamic)list[0])
                    {
                        ToView(obj);
                    }
                }
                else
                {
                    foreach (var attr in model.GetType().GetProperties())
                    {
                        if (attr.CustomAttributes != null && attr.GetCustomAttribute(typeof(EncryptedOnView)) != null)
                        {
                            if (attr.PropertyType.IsGenericType && attr.PropertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                                MaybeShouldBeEncrypted(attr, model);
                            else //aplica a criptografia
                                attr.SetValue(model, "5555");
                        }
                    }
                }
            }
        }

        public void ToServer(object model)
        {
            if (model != null)
            {
                foreach (var attr in model.GetType().GetProperties())
                {
                    if (attr.CustomAttributes != null && attr.GetCustomAttribute(typeof(EncryptedOnView)) != null)
                    {
                        if (attr.PropertyType.IsGenericType && attr.PropertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                            MaybeShouldBeEncrypted(attr, model);
                        else //remove a criptografia
                            attr.SetValue(model, "5");
                    }
                }
            }
        }

        private void MaybeShouldBeEncrypted(PropertyInfo attrs, object model)
        {
            var x = (IEnumerable)attrs.GetValue(model, null);

            foreach (var element in x)
            {
                foreach (var attr in element.GetType().GetProperties())
                {
                    if (attr.CustomAttributes != null && attr.GetCustomAttribute(typeof(EncryptedOnView)) != null)
                    {
                        if (attr.PropertyType.IsGenericType && attr.PropertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                            MaybeShouldBeEncrypted(attr, element);
                        else
                            attr.SetValue(element, "5555");
                    }
                }
            }
        }
    }
}
