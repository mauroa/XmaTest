using System;
using System.Reflection;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Messaging.Ssh;
using Xunit.Abstractions;

namespace Xma.Tests
{
	public class IntegrationContext
    {
		public IntegrationContext (ITestOutputHelper output = null)
		{
			Output = new TestOutput (output);
			Configuration = new IntegrationConfiguration ();
		}

		public ITestOutputHelper Output { get; }

		public IntegrationConfiguration Configuration { get; }
		
		public Task<IMessagingService> ConnectNewBrokerAsync ()
		{
			var settings = GetDefaultConnectionSettings ();

			settings.StartBroker = true;

			return ConnectAsync (settings);
		}

		public Task<IMessagingService> ConnectExistingBrokerAsync ()
		{
			var settings = GetDefaultConnectionSettings ();

			return ConnectAsync (settings);
		}

		ConnectionSettings GetDefaultConnectionSettings ()
		{
			return new ConnectionSettings {
				ClientIdPrefix = "test",
				Port = Configuration.ServerPort,
				ZipsPath = Assembly.GetExecutingAssembly ().Location,
				ReconnectAutomatically = false,
				RestartAgentsAutomatically = false,
				DeveloperMode = true
			};
		}

		async Task<IMessagingService> ConnectAsync (ConnectionSettings settings)
		{
			var messagingService = new MessagingService ();
			Func<SecureString> passwordProvider = () => Configuration.ServerPassword;
			var sshInformationProvider = new DefaultSshInformationProvider ();

			SubscribeUploadingProgress (messagingService);

			Output.WriteLine ("---------------------------------------------------------------------------------------------------------");
			Output.WriteLine ("Connection Information: Address = {0}, Port = {1}, User = {2}", Configuration.ServerAddress, Configuration.ServerPort, Configuration.ServerUser);
			Output.WriteLine ("---------------------------------------------------------------------------------------------------------");
			Output.WriteLine ("Ensuring the SSH keys to connect safely...");

			await messagingService
				.ConfigureHostAsync (Configuration.ServerAddress, Configuration.ServerUser, passwordProvider, sshInformationProvider, settings);

			Output.WriteLine ("Connecting to the Mac...", Configuration.ServerAddress, Configuration.ServerUser);

			var result = await messagingService
				.ConnectAsync (Configuration.ServerAddress, Configuration.ServerUser, sshInformationProvider, settings, CancellationToken.None);

			if (result.ErrorInfo != null) {
				var ex = result.ErrorInfo.SourceException;

				Output.WriteLine ("An error occurred while connecting to the Mac: {0}", ex.Message);

				result.ErrorInfo.Throw ();
			}

			Output.WriteLine ("Successfully connected to the Mac!");

			return messagingService;
		}

		void SubscribeUploadingProgress (IMessagingService messagingService)
		{
			messagingService.FileManager.Uploading += (sender, e) => {
				if (((int)e.Progress) % 25 == 0) {
					Output.WriteLine ("Uploading '{0}' ({1}/{2} KB) {3:F2}%...", e.Filename, e.Uploaded / 1024, e.Size / 1024, e.Progress);
				}
			};
		}
	}
}
