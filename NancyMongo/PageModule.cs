using Nancy;

namespace NancyMongo
{
	public class PageModule : NancyModule
	{
		public PageModule() {
			Get["/"] = HomePage;
		}

		private Response HomePage(dynamic parameters) {
			return View["HomePage"];
		}
	}
}