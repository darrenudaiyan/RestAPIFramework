using NUnit.Framework;
using Rest.API.Tests.Helpers;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Rest.API.Tests.PerformanceTests
{
    [TestFixture]
    public class When_checking_the_perfomance
    {
        private HttpClient client;

        [OneTimeSetUp]
        public async Task SetupAsync()
        {
            client = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true });
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            client.Dispose();
        }

        [Test, MaxTime(10000)]
        public async Task Ten_parallel_calls_to_assays_should_take_less_than_10_seconds()
        {
            Task[] tasks = new Task[10];

            for (int i = 0; i < 10; ++i)
            {
                tasks[i] = _getAssays();
            }

            await Task.WhenAll(tasks);
        }

        [Test, MaxTime(10000)]
        public async Task Ten_parallel_calls_to_Whiskeys_should_take_less_than_10_seconds()
        {
            Task[] tasks = new Task[10];

            for (int i = 0; i < 10; ++i)
            {
                tasks[i] = _getWhiskeys();
            }

            await Task.WhenAll(tasks);
        }

        public async Task<HttpStatusCode> _getAssays()
        {
            var url = Connection.Url + @"/Assays";

            HttpResponseMessage responseCode = await client.GetAsync(url);

            return responseCode.StatusCode;
        }

        public async Task<HttpStatusCode> _getWhiskeys()
        {
            var url = Connection.Url + @"/Whiskeys";

            HttpResponseMessage responseCode = await client.GetAsync(url);

            return responseCode.StatusCode;
        }

        [Test, MaxTime(15000)]
        public async Task Ten_continuous_calls_to_Whiskeys_should_take_less_than_15_seconds()
        {
            var url = Connection.Url + @"/Whiskeys";

            for (int i = 0; i < 10; ++i)
            {
                HttpResponseMessage responseCode = await client.GetAsync(url);
            }
        }

        [Test, MaxTime(15000)]
        public async Task Ten_continuous_calls_to_Assays_should_take_less_than_15_seconds()
        {
            var url = Connection.Url + @"/Assays";

            for (int i = 0; i < 10; ++i)
            {
                HttpResponseMessage responseCode = await client.GetAsync(url);
            }
        }

    }
}
