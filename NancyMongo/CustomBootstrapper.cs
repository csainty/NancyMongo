using System.Configuration;
using System.Linq;
using MongoDB.Driver;
using Nancy;
using Nancy.Conventions;
using NancyMongo.Models;
using TinyIoC;

namespace NancyMongo
{
	public class CustomBootstrapper : DefaultNancyBootstrapper
	{
		protected override void ConfigureConventions(NancyConventions nancyConventions) {
			nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("Scripts"));
		}

		protected override void ConfigureApplicationContainer(TinyIoCContainer container) {
			var connString = ConfigurationManager.AppSettings["MONGOHQ_URL"];
			var databaseName = connString.Split('/').Last();
			var server = MongoServer.Create(connString);
			var database = server.GetDatabase(databaseName);

			if (!database.CollectionExists("Messages"))
				database.CreateCollection("Messages");

			container.Register<MongoServer>(server);
			container.Register<MongoDatabase>(database);
			container.Register<MongoCollection<Message>>(database.GetCollection<Message>("Messages"));
		}
	}
}