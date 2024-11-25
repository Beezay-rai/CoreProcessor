using System.Text;
using System.Text.Json;
using Yarp.ReverseProxy.Transforms;

namespace ApiGateway.Services
{
    public class CustomResponseTransformer : ResponseTransform
    {
        //private readonly ILogger<CustomResponseTransformer> _logger;

        //public CustomResponseTransformer(ILogger<CustomResponseTransformer> logger)
        //{
        //    _logger = logger;
        //}
        public override async ValueTask ApplyAsync(ResponseTransformContext context)
        {
            if(context.ProxyResponse != null)
            {
                var originalResponseStream = await context.ProxyResponse.Content.ReadAsStreamAsync();
                using (var memoryStream = new MemoryStream())
                {
                    await originalResponseStream.CopyToAsync(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    var responseText = await new StreamReader(memoryStream).ReadToEndAsync();
                    Console.WriteLine("Response From Upstream : "+ responseText);
                    var modifiedBytes = Encoding.UTF8.GetBytes(responseText);
                    context.HttpContext.Response.ContentLength = modifiedBytes.Length;

                    await context.HttpContext.Response.Body.WriteAsync(modifiedBytes);

                }
            }
            
           

        }
    }

    public class CustomRequestTransformer : RequestTransform
    {
        public override async ValueTask ApplyAsync(RequestTransformContext context)
        {
            //if (context.ProxyRequest != null) 
            //{
            //    var originalStream = context.HttpContext.Request.Body;
            //    using(var memoryStream = new MemoryStream())
            //    {
            //        await originalStream.CopyToAsync(memoryStream);
            //        originalStream.Position = 0;
            //        memoryStream.Seek(memoryStream.Position, SeekOrigin.Begin);
            //        var reader = new StreamReader(memoryStream);
            //        var body = await reader.ReadToEndAsync();

            //    }

            //}

            context.HttpContext.Request.EnableBuffering();
            var reader = new StreamReader(context.HttpContext.Request.Body);
            var body = await reader.ReadToEndAsync();
            Console.WriteLine("Request Body : " + body);
            context.HttpContext.Request.Body.Position = 0;




        }
    }
}
