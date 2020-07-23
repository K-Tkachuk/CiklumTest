using CiklumTest.Enums;
using CiklumTest.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace CiklumTest.Helpers
{
	public class CiklumTestException : Exception
	{
		public ContentResult Result;
		private string errorMessage { get; set; }
		private HttpStatusCode statusCode { get; set; }

		public CiklumTestException() : base() { }
        
		public CiklumTestException(Errors message)
		{
			switch (message)
			{
				case Errors.UserNotAuthorized:
					statusCode = HttpStatusCode.Unauthorized;
					errorMessage = Errors.UserNotAuthorized.Description();
					break;
				case Errors.UserNotExist:
					statusCode = HttpStatusCode.Unauthorized;
					errorMessage = Errors.UserNotExist.Description();
					break;
				case Errors.EmptyData:
					statusCode = HttpStatusCode.NoContent;
					errorMessage = Errors.EmptyData.Description();
					break;
				case Errors.IncorrectEmailOrPassword:
					statusCode = HttpStatusCode.BadRequest;
					errorMessage = Errors.IncorrectEmailOrPassword.Description();
					break;
				case Errors.DataNotFound:
					statusCode = HttpStatusCode.NotFound;
					errorMessage = Errors.DataNotFound.Description();
					break;
				case Errors.InternalServerError:
					statusCode = HttpStatusCode.InternalServerError;
					errorMessage = Errors.InternalServerError.Description();
					break;
				case Errors.ServerIgnor:
					statusCode = HttpStatusCode.ServiceUnavailable;
					errorMessage = Errors.ServerIgnor.Description();
					break;
				case Errors.InvalidToken:
					statusCode = HttpStatusCode.Unauthorized;
					errorMessage = Errors.InvalidToken.Description();
					break;
				default:
					statusCode = HttpStatusCode.InternalServerError;
					errorMessage = Errors.SomethingWentWrong.Description();
					break;
			}

			Result = new ContentResult
			{
				StatusCode = (int)statusCode,
				Content = errorMessage
			};
		}
	}
}
