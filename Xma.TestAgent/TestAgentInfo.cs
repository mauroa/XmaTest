using Xamarin.Messaging;

namespace Xma.TestAgent
{
	public class TestAgentInfo : AgentInfo
	{
		public TestAgentInfo ()
			: base (name: "TestAgent", version: "1.0.0")
		{
		}

		static TestAgentInfo ()
		{
			Instance = new TestAgentInfo ();
		}

		public static AgentInfo Instance { get; private set; }
	}
}
