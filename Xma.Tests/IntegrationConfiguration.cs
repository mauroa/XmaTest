using System;
using System.Configuration;
using System.Security;

namespace Xma.Tests
{
	public class IntegrationConfiguration
	{
		static readonly Random portRandomizer = new Random ();

		public string ServerAddress => ConfigurationManager.AppSettings["ServerAddress"];

		public int ServerPort
		{
			get
			{
				var portString = ConfigurationManager.AppSettings["ServerPort"];
				var port = default (int);

				if (int.TryParse (portString, out port)) {
					return port;
				} else {
					return portRandomizer.Next (minValue: 50000, maxValue: 60000);
				}
			}
		}

		public string ServerUser => ConfigurationManager.AppSettings["ServerUser"];

		public SecureString ServerPassword => ConfigurationManager.AppSettings["ServerPassword"].ToSecureString ();
	}
}
