namespace FedExDay.Model
{
    public class UserSearchPrefixRequest
    {
        public UserSearchPrefixRequest(string username) { displayNamePrefix = username; }
        public string? displayNamePrefix { get; set; }
    }
}
