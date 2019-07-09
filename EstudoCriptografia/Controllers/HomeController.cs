using EstudoCriptografia.Extensions;
using EstudoCriptografia.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EstudoCriptografia.Controllers
{
    public class HomeController : Controller
    {
        [EncryptedActionParameter]
        public ActionResult Index()
        {
            var vm = new ViewModel
            {
                Id = "5",
                Nome = "Teste",
                ListInnerViewModel = new List<InnerViewModel>()
                {
                    new InnerViewModel
                    {
                        Nome = "TesteInner",
                        ListInnerInner = new List<InnerInnerViewModel>()
                        {
                            new InnerInnerViewModel
                            {
                                 Id = "TesteInner"
                            }
                        }
                    }
                }
            };

            return View(vm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}