using System.Threading.Tasks;
using Xamarin.Messaging.Client;

namespace Xma.TestAgent
{
	public class EchoMessageHandler : RequestHandler<EchoMessage, EchoResponse>
	{
		protected override Task<EchoResponse> ExecuteAsync (EchoMessage message)
		{
			return Task.FromResult (new EchoResponse { Echo = message.Text });
		}
	}
}
