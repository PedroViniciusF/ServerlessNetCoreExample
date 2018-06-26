using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using Amazon.Lambda.APIGatewayEvents;

using ServerlessNetCoreExample.ServerlessFunction;
using static ServerlessNetCoreExample.ServerlessFunction.Functions;

namespace ServerlessNetCoreExample.ServerlessFunction.Tests
{
    public class FunctionTest
    {
        public FunctionTest()
        {
        }

        [Fact]
        public void TestGetMethod()
        {
            TestLambdaContext context;
            APIGatewayProxyRequest request;
            APIGatewayProxyResponse response;

            Functions functions = new Functions();


            request = new APIGatewayProxyRequest();
            context = new TestLambdaContext();
            response = functions.Get(request, context);
            Assert.Equal(200, response.StatusCode);
            Assert.Equal("Hello AWS Serverless", response.Body);
        }

        [Fact]
        public void TestSomaValoresMethod()
        {
            TestLambdaContext context;
            APIGatewayProxyRequest request;
            APIGatewayProxyResponse response;

            Functions functions = new Functions();


            request = new APIGatewayProxyRequest();
            context = new TestLambdaContext();

            request.Body = "{ \"valor1\" : 1, \"valor2\" : 2 }";

            response = functions.SomaValores(request, context);
            Assert.Equal(200, response.StatusCode);
            Assert.Equal("3", response.Body);

            request = new APIGatewayProxyRequest();
            context = new TestLambdaContext();

            request.Body = "{ \"valor1\" : valorinvalido, \"valor2\" : valorinvalido }";

            response = functions.SomaValores(request, context);
            Assert.Equal(400, response.StatusCode);            
        }
    }
}
