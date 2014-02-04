using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserService.Messages.Commands;

namespace ExampleWeb.Controllers
{
    public class HomeController : Controller
    {
		private static Queue<string> _recentlyCreatedUsers = new Queue<string>();

		internal static Queue<string> RecentlyCreatedUsers
		{
			get { return _recentlyCreatedUsers; }
		}

		public ActionResult Index()
		{
			return Json(new { text = "Hello world." });
		}

		public ActionResult CreateUser(string name, string email)
		{
			var cmd = new CreateNewUserCmd
			{
				Name = name,
				EmailAddress = email
			};

			ServiceBus.Bus.Send(cmd);

			return Json(new { sent = cmd });
		}

        public ActionResult VerifyUser(string email, string code)
        {
            var cmd = new UserVerifyingEmailCmd
            {
                EmailAddress = email,
                VerificationCode = code
            };

            ServiceBus.Bus.Send(cmd);

            return Json(new { sent = cmd });
        }

		public ActionResult RecentUsers()
		{
			return Json(new { recentUsers = RecentlyCreatedUsers.ToList() });
		}

		protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
		{
			return base.Json(data, contentType, contentEncoding,
			  JsonRequestBehavior.AllowGet);
		}
    }
}
