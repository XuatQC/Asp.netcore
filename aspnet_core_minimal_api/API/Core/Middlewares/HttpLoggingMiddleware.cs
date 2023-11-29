using System.Text;
using Microsoft.IO;
using Serilog.Context;

namespace Core.Middlewares;

public class HttpLoggingMiddleware
{
    private readonly Serilog.ILogger logger;
    private readonly RequestDelegate next;
    private readonly RecyclableMemoryStreamManager recyclableMemoryStreamManager;

    public HttpLoggingMiddleware(RequestDelegate next, Serilog.ILogger logger)
    {
        this.next = next;
        this.logger = logger;
        this.recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
    }

    public async Task Invoke(HttpContext context)
    {
        // add logged-in/request user to log
        var email = context.User?.Claims.FirstOrDefault(x => x.Type == "Email")?.Value;
        if (!string.IsNullOrEmpty(email))
        {
            LogContext.PushProperty("Email", email);
        }

        await LogRequest(context);
        await LogResponse(context);
    }

    private async Task LogRequest(HttpContext context)
    {
        context.Request.EnableBuffering();

        await using var requestStream = this.recyclableMemoryStreamManager.GetStream();
        await context.Request.Body.CopyToAsync(requestStream);

        // TODO improve later
        this.logger.Information($"REQUEST Http Request Information:{Environment.NewLine}" +
                               $"Schema:{context.Request.Scheme} " +
                               $"Host: {context.Request.Host} " +
                               $"Path: {context.Request.Path} " +
                               $"QueryString: {context.Request.QueryString} " +
                               $"Request Body: {ReadStreamInChunks(requestStream)}");
        context.Request.Body.Position = 0;
    }

    private async Task LogResponse(HttpContext context)
    {
        var originalBodyStream = context.Response.Body;

        await using var responseBody = recyclableMemoryStreamManager.GetStream();
        context.Response.Body = responseBody;

        await next(context);

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
        context.Response.Body.Seek(0, SeekOrigin.Begin);

        // TODO improve later
        this.logger.Information($"RESPONSE Http Response Information:{Environment.NewLine}" +
                               $"Schema:{context.Request.Scheme} " +
                               $"Host: {context.Request.Host} " +
                               $"Path: {context.Request.Path} " +
                               $"QueryString: {context.Request.QueryString} " +
                               $"Response Body: {text}");

        await responseBody.CopyToAsync(originalBodyStream);
    }

    private static string ReadStreamInChunks(Stream stream)
    {
        const int readChunkBufferLength = 4096;

        stream.Seek(0, SeekOrigin.Begin);

        using var textWriter = new StringWriter();
        using var reader = new StreamReader(stream);

        var readChunk = new char[readChunkBufferLength];
        int readChunkLength;

        do
        {
            readChunkLength = reader.ReadBlock(readChunk, 0, readChunkBufferLength);
            textWriter.Write(readChunk, 0, readChunkLength);
        } while (readChunkLength > 0);

        return textWriter.ToString();
    }
}