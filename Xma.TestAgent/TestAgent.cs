using System.Threading.Tasks;
using Xamarin.Messaging.Client;

namespace Xma.TestAgent
{
	public class TestAgent : Agent
	{
		public override string Name => TestAgentInfo.Instance.Name;

		public override string Version => TestAgentInfo.Instance.Version;

		protected override async Task RegisterCustomHandlersAsync (MessageHandlerManager manager)
		{
			await manager.RegisterHandlerAsync (new EchoMessageHandler ());
		}
	}
}
