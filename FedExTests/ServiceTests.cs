using FedExDay;
using FedExDay.Constants;
using FedExDay.Model;
using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Newtonsoft.Json;

namespace FedExTests
{
    [TestClass]
    public class ServiceTests
    {
        public const string BaseApiUrl = "https://www.bungie.net/Platform";
        public const string ApiKey = "API_KEY_GOES_HERE";

        [TestMethod]
        public void VerifyGetCharacterResponseParsing()
        {
            string jsonObj = "{\"Response\":{\"character\":{\"data\":{\"membershipId\":\"4611686018432111251\",\"membershipType\":1,\"characterId\":\"2305843009266080247\",\"dateLastPlayed\":\"2022-02-01T03:48:22Z\",\"minutesPlayedThisSession\":\"5\",\"minutesPlayedTotal\":\"27\",\"light\":1360,\"stats\":{\"1935470627\":1360,\"2996146975\":25,\"392767087\":41,\"1943323491\":42,\"1735777505\":42,\"144602215\":40,\"4244567218\":30},\"raceHash\":2803282938,\"genderHash\":2204441813,\"classHash\":2271682572,\"raceType\":1,\"classType\":2,\"genderType\":1,\"emblemPath\":\"/common/destiny2_content/icons/24e9133c9cc157853762de5a2c3853aa.jpg\",\"emblemBackgroundPath\":\"/common/destiny2_content/icons/73f5f779f40bfecb4690c395bc1941b9.jpg\",\"emblemHash\":1907674137,\"emblemColor\":{\"red\":109,\"green\":62,\"blue\":4,\"alpha\":255},\"levelProgression\":{\"progressionHash\":1716568313,\"dailyProgress\":0,\"dailyLimit\":0,\"weeklyProgress\":0,\"weeklyLimit\":0,\"currentProgress\":0,\"level\":50,\"levelCap\":50,\"stepIndex\":50,\"progressToNextLevel\":0,\"nextLevelAt\":0},\"baseCharacterLevel\":50,\"percentToNextLevel\":0.0},\"privacy\":1},\"uninstancedItemComponents\":{}},\"ErrorCode\":1,\"ThrottleSeconds\":0,\"ErrorStatus\":\"Success\",\"Message\":\"Ok\",\"MessageData\":{}}";
            var val = JsonConvert.DeserializeObject<GetCharacterResponse>(jsonObj);

            Assert.IsNotNull(val);
        }

        [TestMethod]
        public async Task TestFlurlUserSearch()
        {
            FlurlHttp.Configure(settings => {
                settings.Redirects.ForwardHeaders = true;
            });

            var result = await BaseApiUrl
                .AppendPathSegment(Routes.SEARCH)
                .WithHeader(Headers.API_KEY, ApiKey)
                .PostJsonAsync(new UserSearchPrefixRequest("achaia"))
                .ReceiveJson<UserSearchResponse>();

            Assert.IsNotNull(result);
        }
    }
}