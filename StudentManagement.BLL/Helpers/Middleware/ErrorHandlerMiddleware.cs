using System.Net;
using System.Text.Json;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using StudentManagement.DAL;
using StudentManagement.DAL.AppException;

namespace StudentManagement.BLL.Helpers.Middleware;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            switch (error)
            {
                case AppException e:
                    // custom application error
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case KeyNotFoundException e:
                    // not found error
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    // unhandled error
                    _logger.LogError(error, error.Message);
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonSerializer.Serialize(new { message = error?.Message });

            // Check if the request accepts XML
            var acceptHeader = context.Request.Headers["Accept"].ToString().ToLower();
            if (acceptHeader.Contains("application/xml"))
            {
                // Send XML response
                response.ContentType = "application/xml";
                var xmlResult = FormatXmlResult(result);
                await response.WriteAsync(xmlResult);
            }
            else
            {
                // Send JSON response by default
                await response.WriteAsync(result);
            }
        }
    }

    private string FormatXmlResult(string jsonResult)
    {
        // Convert JSON to XML (this is a basic example, may need refinement based on your requirements)
        var jsonObject = JObject.Parse(jsonResult);
        var xmlDocument = new XDocument(new XElement("Error",
            new XElement("Message", jsonObject["message"])
        ));
        return xmlDocument.ToString();
    }
}
