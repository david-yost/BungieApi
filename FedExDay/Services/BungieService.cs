using FedExDay.Constants;
using FedExDay.Model;
using Newtonsoft.Json;

namespace FedExDay.Services
{
    public interface IBungieService
    {
        Task<UserSearchResponse> SearchUsersAsync(string username);
        Task<HistoricalStatsForAccountResponse> GetHistoricalStatsForAccountAsync(int accountType, string destinyMembershipId);
        Task<GetCharacterResponse> GetCharactersAsync(int membershipType, string destinyMembershipId, string characterId);
    }

    public class BungieService : IBungieService
    {
        static readonly HttpClient client = new HttpClient();

        private readonly IConfiguration _configuration;
        private readonly string _baseApiUrl;
        private readonly string _apikey;


        public BungieService(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseApiUrl = _configuration.GetValue<string>(ConfigurationProps.BASE_API_URL) ?? throw new ApplicationException(string.Format(ExceptionMessages.CONFIG_MISSING, ConfigurationProps.BASE_API_URL));
            _apikey = _configuration.GetValue<string>(ConfigurationProps.API_KEYS) ?? throw new ApplicationException(string.Format(ExceptionMessages.CONFIG_MISSING, ConfigurationProps.API_KEYS));
        }

        public async Task<UserSearchResponse> SearchUsersAsync(string username)
        {
            var content = JsonConvert.SerializeObject(new UserSearchPrefixRequest(username));
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(_baseApiUrl + Routes.SEARCH),
                Method = HttpMethod.Post,
                Content = new ByteArrayContent(System.Text.Encoding.UTF8.GetBytes(content))
            };
            request.Headers.Add(Headers.API_KEY, _apikey);

            using HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<UserSearchResponse>(responseBody);
        }

        public async Task<HistoricalStatsForAccountResponse> GetHistoricalStatsForAccountAsync(int membershipType, string destinyMembershipId)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(_baseApiUrl + string.Format(Routes.STATS, membershipType, destinyMembershipId)),
                Method = HttpMethod.Get
            };
            request.Headers.Add("X-API-Key", _apikey);

            using HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<HistoricalStatsForAccountResponse>(responseBody);
        }

        public async Task<GetCharacterResponse> GetCharactersAsync(int membershipType, string destinyMembershipId, string characterId)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(_baseApiUrl + string.Format(Routes.CHARACTERS, membershipType, destinyMembershipId, characterId)),
                Method = HttpMethod.Get
            };
            request.Headers.Add("X-API-Key", _apikey);

            using HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            Console.WriteLine(responseBody);

            var val = JsonConvert.DeserializeObject<GetCharacterResponse>(responseBody);

            return val;
        }
    }
}
