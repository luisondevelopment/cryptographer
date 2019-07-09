using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace EstudoCriptografia.Extensions
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class EncryptedActionParameterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Dictionary<string, object> decryptedParameters = new Dictionary<string, object>();

            if (HttpContext.Current.Request.QueryString.Get("q") != null)
            {
                string encryptedQueryString = HttpContext.Current.Request.QueryString.Get("q");
                string decrptedString = Decrypt(encryptedQueryString.ToString());
                string[] paramsArrs = decrptedString.Split('?');

                for (int i = 0; i < paramsArrs.Length; i++)
                {
                    string[] paramArr = paramsArrs[i].Split('=');
                    decryptedParameters.Add(paramArr[0], Convert.ToInt32(paramArr[1]));
                }
            }
            for (int i = 0; i < decryptedParameters.Count; i++)
            {
                filterContext.ActionParameters[decryptedParameters.Keys.ElementAt(i)] = decryptedParameters.Values.ElementAt(i);
            }

            base.OnActionExecuting(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (filterContext.Result is ViewResultBase result)
            {
                var model = result.Model;

                if (model != null)
                {
                    foreach (var attr in model.GetType().GetProperties())
                    {
                        if (attr.CustomAttributes != null && attr.GetCustomAttribute(typeof(EncryptedBuddy)) != null)
                        {
                            if (attr.PropertyType.IsGenericType && attr.PropertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                                MaybeShouldBeEncrypted(attr, model);
                            else
                                attr.SetValue(model, "5555");
                        }
                    }
                }
            }

            base.OnResultExecuting(filterContext);
        }

        private void MaybeShouldBeEncrypted(PropertyInfo attrs, object model)
        {
            var x = (IEnumerable)attrs.GetValue(model, null);

            foreach(var element in x)
            {
                foreach (var attr in element.GetType().GetProperties())
                {
                    if (attr.CustomAttributes != null && attr.GetCustomAttribute(typeof(EncryptedBuddy)) != null)
                    {
                        if (attr.PropertyType.IsGenericType && attr.PropertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                            MaybeShouldBeEncrypted(attr, element);
                        else
                            attr.SetValue(element, "5555");
                    }
                }
            }
        }

        private string Decrypt(string encryptedText)
        {

            string key = "jdsg432387#";
            byte[] DecryptKey = { };
            byte[] IV = { 55, 34, 87, 64, 87, 195, 54, 21 };
            byte[] inputByte = new byte[encryptedText.Length];

            DecryptKey = System.Text.Encoding.UTF8.GetBytes(key.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByte = Convert.FromBase64String(encryptedText);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(DecryptKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByte, 0, inputByte.Length);
            cs.FlushFinalBlock();
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }
    }
}