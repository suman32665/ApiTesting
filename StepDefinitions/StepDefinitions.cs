using ApiTesting.Model;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System.Diagnostics;
using System.Net;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace ApiTesting.StepDefinitions
{
    [Binding]
    internal class StepDefinitions
    {
        private const string BaseAddress = "https://reqres.in/";
        public RestClient Client { get; set; } = null!;
        private RestRequest Request { get; set; } = null!;
        private RestResponse Response { get; set; } = null!;

        Stopwatch stopwatch = new Stopwatch();

        [Given(@"I am a client")]
        public void GivenIAmAClient()
        {
            Client = new RestClient(BaseAddress);
        }

        [When(@"I make a GET request to '(.*)'")]
        public void WhenIMakeAgetRequestTo(string endpoint)
        {
            Request = new RestRequest(endpoint, Method.Get);
            
            
            stopwatch.Start();

            Response = Client.ExecuteGet(Request);

            stopwatch.Stop();

        }

        [Then(@"the response status code is '(.*)'")]
        public void ThenTheResponseStatusCodeIs(int statusCode)
        {

            Console.WriteLine($"Response Status Code: {Response.StatusCode}");
            Console.WriteLine($"Response Content: {Response.Content}");
            var expected = (HttpStatusCode)statusCode;
            Assert.AreEqual(expected, Response.StatusCode);
        }

        [Then(@"the response time is less than '([^']*)' milliseconds")]
        public void ThenTheResponseTimeLessThanMilliseconds(long expectedMS)
        {
            Console.WriteLine($"Response Time: {stopwatch.ElapsedMilliseconds} ms");
            if (stopwatch.ElapsedMilliseconds > expectedMS)
            {
                Assert.Fail("Response Time is greater than " + expectedMS + " milliseconds.\nResponse Time is " + stopwatch.ElapsedMilliseconds + " milliseconds");
            }
        }

        [When(@"I make a POST request to '([^']*)' with following data")]
        public void WhenIMakeAPOSTRequestToWithFollowingData(string endpoint, Table table)
        {
            var Rows = table.CreateSet<User>();
            var row = Rows.ElementAt(0);

            Request = new RestRequest(endpoint, Method.Post);
            Request.AddHeader("Content-Type", "application/json");

            var body = new
            {
                name = row.name,
                job = row.job,
            };
            Request.AddJsonBody(body);
            Response = Client.ExecutePost(Request);

        }
        
        [Then(@"the response content should be following")]
        public void ThenTheResponseContentShouldBeFollowing(Table table)
        {
            var Rows = table.CreateSet<User>();
            var row = Rows.ElementAt(0);

            User user = JsonConvert.DeserializeObject<User>(Response.Content);

            Assert.AreEqual(row.name, user.name, "Name does not match");
            Assert.AreEqual(row.job, user.job, "Job does not match");
        }

        [When(@"I make a PUT  request to '([^']*)' with following data")]
        public void WhenIMakeAPUTRequestToWithFollowingData(string endpoint, Table table)
        {
            var Rows = table.CreateSet<User>();
            var row = Rows.ElementAt(0);

            Request = new RestRequest(endpoint, Method.Put);
            Request.AddHeader("Content-Type", "application/json");

            var body = new
            {
                name = row.name,
                job = row.job,
            };
            Request.AddJsonBody(body);
            Response = Client.ExecutePut(Request);
        }

        [When(@"I make a DELETE  request to '([^']*)'")]
        public void WhenIMakeADELETERequestTo(string endpoint)
        {
            Request = new RestRequest(endpoint, Method.Delete);
           
            Response = Client.ExecuteDelete(Request);
        }

    }
}
