using Xamarin.Messaging.Client;

namespace Xma.TestAgent
{
	public class Program
	{
		public static void Main (string[] args)
		{
			var agent = new TestAgent ();
			var runner = new AgentConsoleRunner (agent, args);

			runner.RunAsync ().Wait ();
		}
	}
}
