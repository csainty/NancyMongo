using System.Linq;
using MongoDB.Driver;
using Nancy;
using NancyMongo.Models;

namespace NancyMongo
{
	public class ApiModule : NancyModule
	{
		private readonly MongoCollection<Message> _Messages;

		public ApiModule(MongoCollection<Message> messages)
			: base("/api") {
			_Messages = messages;

			Get["/messages"] = GetMessages;
			Post["/messages"] = AddMessage;
		}

		private Response GetMessages(dynamic parameters) {
			return Response.AsJson(_Messages.FindAll().SetLimit(100).ToArray());
		}

		private Response AddMessage(dynamic parameters) {
			if (!Request.Form.Message.HasValue)
				return HttpStatusCode.BadRequest;

			_Messages.Save(new Message {
				Content = Request.Form.Message
			});

			return HttpStatusCode.OK;
		}
	}
}