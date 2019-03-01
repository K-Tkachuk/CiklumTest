using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Logging;

namespace CiklumTest.Helpers
{
	public class FileLogger : ILogger
	{
		string filePath;
		object _lock = new object();

		public FileLogger(string path)
		{
			filePath = path;
		}

		public IDisposable BeginScope<TState>(TState state)
		{
			return null;
		}

		public bool IsEnabled(LogLevel logLevel)
		{
			return true;
		}

		public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
		{
			if (formatter != null)
			{
				lock (_lock)
				{
					File.AppendAllText(filePath, $"{DateTime.UtcNow:hh:mm:ss} | " +
					                   formatter(state, exception) + 
					                   Environment.NewLine + "----------------------------------------------------------------------------------------" + Environment.NewLine);
				}
			}
		}
	}
}