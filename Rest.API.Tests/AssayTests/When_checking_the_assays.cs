using Newtonsoft.Json;
using NUnit.Framework;
using Rest.API.Tests.Helpers;
using Rest.API.Tests.ObjectModels.AssayModel;
using System;
using System.Collections;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Rest.API.Tests.ObjectModels.Helpers;

namespace Rest.API.Tests.AssayTests
{
    [TestFixture]
    public class When_checking_the_assays
    {
        private HttpClient client;
        private string pageURL = @"/Assays";

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

        [Test]
        public async Task The_reponse_should_be_correct()
        {
            //Arrange
            var url = Connection.Url + pageURL;

            //Act
            HttpResponseMessage responseCode = await client.GetAsync(url);

            //Assert
            Assert.That(responseCode.StatusCode.ToString(), Is.EqualTo("OK"));
        }

        [Test]
        public async Task Getting_the_top_1_should_return_1_result()
        {
            //Arrange
            var url = Connection.Url + pageURL +"?$top=1";

            //Act
            string responseBody = await client.GetStringAsync(url);
            AssayResponse assayResponse = JsonConvert.DeserializeObject<AssayResponse>(responseBody);

            //Assert
            Assert.That(assayResponse.value.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task The_reponse_should_be_ok_for_filtering()
        {
            //Arrange
            var expectedValue = "BLEND1B";
            var url = Connection.Url + pageURL + "?$Filter=Name eq '" + expectedValue + "'";

            //Act
            HttpResponseMessage responseCode = await client.GetAsync(url);

            //Assert
            Assert.That(responseCode.StatusCode.ToString(), Is.EqualTo("OK"));
        }

        [Test]
        public async Task The_reponse_should_be_correct_for_filtering_on_name()
        {
            //Arrange
            var expectedValue = "BLEND1B";
            var url = Connection.Url + pageURL + "?$Filter=Name eq '" + expectedValue + "'";

            //Act
            string responseBody = await client.GetStringAsync(url);
            AssayResponse assayResponse = JsonConvert.DeserializeObject<AssayResponse>(responseBody);

            //Assert
            Assert.That(assayResponse.value[0].Name.ToString(), Is.EqualTo(expectedValue));
        }

        [Test]
        public async Task The_reponse_should_be_correct_for_filtering_on_id()
        {
            //Arrange
            var expectedValue = "Assay1a";
            var url = Connection.Url + pageURL + "?$Filter=AssayId eq '" + expectedValue + "'";

            //Act
            string responseBody = await client.GetStringAsync(url);
            AssayResponse assayResponse = JsonConvert.DeserializeObject<AssayResponse>(responseBody);

            //Assert
            Assert.That(assayResponse.value[0].AssayId.ToString(), Is.EqualTo(expectedValue));
        }

        [Test]
        public async Task Requesting_filtering_on_an_unknown_field_should_give_BadRequest()
        {
            //Arrange
            var url = Connection.Url + pageURL + "?$Filter=Test eq 'test'";

            //Act
            HttpResponseMessage responseCode = await client.GetAsync(url);

            //Assert
            Assert.That(responseCode.StatusCode.ToString(), Is.EqualTo("BadRequest"));
        }

        [Test]
        public async Task Requesting_filtering_on_an_unknown_field_should_the_correct_error()
        {
            //Arrange
            var expectedMessage ="The query specified in the URI is not valid. Could not find a property named 'Test' on type 'RestAPI.Assay'.";
            var url = Connection.Url + pageURL + "?$Filter=Test eq 'test'";

            //Act
            HttpResponseMessage responseCode = await client.GetAsync(url);
            var errormessage = await responseCode.Content.ReadAsStringAsync();
            ErrorResponse errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(errormessage);

            //Assert
            Assert.That(errorResponse.error.message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public async Task The_reponse_should_be_correct_for_ordering_by_name()
        {
            //Arrange
            var url = Connection.Url + pageURL + "?$orderby=Name asc";

            //Act
            HttpResponseMessage responseCode = await client.GetAsync(url);

            //Assert
            Assert.That(responseCode.StatusCode.ToString(), Is.EqualTo("OK"));
        }

        [Test]
        public async Task The_reponse_data_be_correct_for_ordering_by_name_asc()
        {
            //Arrange
            var url = Connection.Url + pageURL + "?$orderby=Name asc";

            string responseBody = await client.GetStringAsync(url);
            AssayResponse assayResponse = JsonConvert.DeserializeObject<AssayResponse>(responseBody);

            //Assert
            Assert.That(assayResponse.value.Select(o => o.Name),
                        Is.Ordered.Ascending
                        .Using((IComparer)StringComparer.OrdinalIgnoreCase));
        }

        [Test]
        public async Task The_reponse_data_be_correct_for_ordering_by_name_desc()
        {
            //Arrange
            var url = Connection.Url + pageURL + "?$orderby=Name desc";

            string responseBody = await client.GetStringAsync(url);
            AssayResponse assayResponse = JsonConvert.DeserializeObject<AssayResponse>(responseBody);

            //Assert
            Assert.That(assayResponse.value.Select(o => o.Name), 
                        Is.Ordered.Descending
                        .Using((IComparer)StringComparer.OrdinalIgnoreCase));
        }

        [Test]
        public async Task The_reponse_data_be_correct_for_ordering_by_Percent_desc()
        {
            //Arrange
            var url = Connection.Url + pageURL + "?$orderby=Percent desc";

            string responseBody = await client.GetStringAsync(url);
            AssayResponse assayResponse = JsonConvert.DeserializeObject<AssayResponse>(responseBody);

            //Assert
            Assert.That(assayResponse.value.Select(o => o.Percent),
                        Is.Ordered.Descending
                        .Using((IComparer)StringComparer.OrdinalIgnoreCase));
        }

        [Test]
        public async Task The_reponse_data_be_correct_for_ordering_by_SpecificGravity_asc()
        {
            //Arrange
            var url = Connection.Url + pageURL + "?$orderby=SpecificGravity asc";

            string responseBody = await client.GetStringAsync(url);
            AssayResponse assayResponse = JsonConvert.DeserializeObject<AssayResponse>(responseBody);

            //Assert
            Assert.That(assayResponse.value.Select(o => o.SpecificGravity),
                        Is.Ordered.Ascending
                        .Using((IComparer)StringComparer.OrdinalIgnoreCase));
        }
    }
}
