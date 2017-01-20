using Xamarin.Messaging;

namespace Xma.TestAgent
{
	[Topic ("test/echo")]
	public class EchoMessage
	{
		public string Text { get; set; }
	}
}
