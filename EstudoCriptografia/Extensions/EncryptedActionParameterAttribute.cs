using System;
using System.Web.Mvc;

namespace EstudoCriptografia.Extensions
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class EncryptedActionParameterAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (filterContext.Result is ViewResultBase result)
            {
                Cryptographer.Cryptographer service = new Cryptographer.Cryptographer();
                service.ToView(result.Model);
            }

            if (filterContext.Result is JsonResult jsonResult)
            {
                Cryptographer.Cryptographer service = new Cryptographer.Cryptographer();
                service.ToView(jsonResult.Data);
            }

            base.OnResultExecuting(filterContext);
        }
    }
}