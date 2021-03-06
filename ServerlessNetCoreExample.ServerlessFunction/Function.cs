using System.Collections.Generic;
using System.Net;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;
using System;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace ServerlessNetCoreExample.ServerlessFunction
{
    public class Functions
    {                
        /// <summary>
        /// A Lambda function to respond to HTTP Get methods from API Gateway
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The list of blogs</returns>
        public APIGatewayProxyResponse Get(APIGatewayProxyRequest request, ILambdaContext context)
        {
            context.Logger.LogLine("Get Request\n");

            var response = new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = "Hello AWS Serverless",
                Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
            };

            return response;
        }

        public class Valores
        {
            public int Valor1 { get; set; }
            public int Valor2 { get; set; }

            public int SomaDosValores { get { return Valor1 + Valor2; } }
        }

        /// <summary>
        /// Uma Lambda function que responde a chamadas HTTP Post para API Gateway
        /// </summary>       
        /// <returns>Valor da soma de dois valores</returns>
        public APIGatewayProxyResponse SomaValores(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {

                var valoresBody = JsonConvert.DeserializeObject<Valores>(request.Body);

                var response = new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Body = valoresBody.SomaDosValores.ToString(),
                    Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
                };

                return response;
            }
            catch (Exception e)
            {
                var response = new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = e.Message,
                    Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
                };

                return response;
            }
        }
    }
}
