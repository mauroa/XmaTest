using System;
using System.Diagnostics;
using Xunit.Abstractions;

namespace Xma.Tests
{
	public class TestOutput : ITestOutputHelper
	{
		readonly ITestOutputHelper innerOutput;

		public TestOutput (ITestOutputHelper innerOutput = null)
		{
			this.innerOutput = innerOutput;
		}

		public void WriteLine (string format, params object[] args)
		{
			innerOutput?.WriteLine (format, args);
			Debug.WriteLine (format, args);
			Console.WriteLine (format, args);
		}

		public void WriteLine (string message)
		{
			innerOutput?.WriteLine (message);
			Debug.WriteLine (message);
			Console.WriteLine (message);
		}
	}
}
