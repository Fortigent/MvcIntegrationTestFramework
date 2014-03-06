using System.Web;
using System.Web.Mvc;

namespace MyMvcApplication.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			ViewBag.Message = "Welcome to ASP.NET MVC!";

			return View();
		}

		public ActionResult About()
		{
			return View();
		}

		public ActionResult DoStuffWithSessionAndCookies()
		{
			Session["myIncrementingSessionItem"] = ((int?) (Session["myIncrementingSessionItem"] ?? 0)) + 1;
			Response.Cookies.Add(new HttpCookie("mycookie", "myval"));
			return Content("OK");
		}

		public JsonResult GetJsonData()
		{
			return Json(new HomeViewModel {Title = "xyz"}, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult SaveJsonData(HomeViewModel cmd)
		{
			return Json(cmd.Title + "Saved", JsonRequestBehavior.AllowGet);
		}

		[Authorize]
		public ActionResult SecretAction()
		{
			return Content("Hello, you're logged in as " + User.Identity.Name);
		}
	}

	public class HomeViewModel
	{
		public string Title { get; set; }
	}
}