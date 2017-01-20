using System.Threading.Tasks;
using Xamarin.Messaging;
using Xma.TestAgent;
using Xunit;

namespace Xma.Tests
{
	public class IntegrationSpec
	{
		readonly IntegrationContext context;

		public IntegrationSpec ()
		{
			context = new IntegrationContext ();
		}

		[Fact]
		public async Task When_Connecting_To_New_Broker_Then_Succeeds ()
		{
			var messagingService = await context.ConnectNewBrokerAsync ();

			Assert.True (messagingService.IsConnected);

			await messagingService.DisconnectAsync ();
		}

		[Fact]
		public async Task When_Starting_Test_Agent_And_Send_Messages_Then_Succeeds ()
		{
			var messagingService = await context.ConnectNewBrokerAsync ();
			var messageText = "Foo Message";

			await messagingService.StartAgentAsync (TestAgentInfo.Instance);

			var response = await messagingService.MessagingClient.PostAsync<EchoMessage, EchoResponse> (new EchoMessage { Text = messageText });

			Assert.True (messagingService.IsConnected);
			Assert.Equal (AgentStatus.Running, messagingService.GetAgentStatus (TestAgentInfo.Instance));
			Assert.Equal (messageText, response.Echo);

			await messagingService.DisconnectAsync ();
		}
	}
}
