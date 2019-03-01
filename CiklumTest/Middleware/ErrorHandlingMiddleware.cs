using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CiklumTest.Enums;
using CiklumTest.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CiklumTest.Middleware
{
	public class ErrorHandlingMiddleware
	{
		private readonly ILogger<ErrorHandlingMiddleware> logger;
		private readonly RequestDelegate next;

		public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
		{
			this.next = next;
			this.logger = logger;
		}

		private static Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			context.Response.ContentType = "application/json";
			var ciklumEx = exception is CiklumTestException ? (CiklumTestException)exception : new CiklumTestException(Errors.InternalServerError);
			context.Response.StatusCode = (int)ciklumEx.Result.StatusCode;
			return context.Response.WriteAsync(JsonConvert.SerializeObject(new { error = ciklumEx.Result.Content }));
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
				var ciklumEx = ex as CiklumTestException;
				if (ciklumEx == null)
				{
					var message = ex.Message;
					var stackTrace = ex.StackTrace;
					var method = context.Request.Method;
					var url = context.Request.Path;
					var log = $"{method} | {url} | {message}" +
							  $"\r\n{stackTrace}";
					logger.LogError(default(EventId), ex, log);
				}

				await HandleExceptionAsync(context, ex);
			}
		}
	}
}
