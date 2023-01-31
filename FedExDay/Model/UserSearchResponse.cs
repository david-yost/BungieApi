namespace FedExDay.Model
{
    public class UserSearchResponse
    {
        public SearchResponse? Response { get; set; }
        public int? ErrorCode { get; set; }
        public int? ThrottleSeconds { get; set; }
        public string? ErrorStatus { get; set; }
        public string? Message { get; set; }
    }

    public class SearchResponse
    {
        public SearchResults[]? searchResults { get; set; }
    }

    public class SearchResults
    {
        public string? bungieGlobalDisplayName { get; set; }
        public string? bungieGlobalDisplayNameCode { get; set; }
        public string? bungieNetMembershipId { get; set; }
        public DestinyMembership[]? destinyMemberships { get; set; }
        public int? page { get; set; }
        public bool? hasMore { get; set; }
    }

    public class DestinyMembership
    {
        public string? iconPath { get; set; }
        public int crossSaveOverride { get; set; }
        public int[]? applicableMembershipTypes { get; set; }
        public bool isPublic { get; set; }
        public int membershipType { get; set; }
        public string? membershipId { get; set; }
        public string? displayName { get; set; }
        public string? bungieGlobalDisplayName { get; set; }
        public int? bungieGlobalDisplayNameCode { get; set; }
    }
}