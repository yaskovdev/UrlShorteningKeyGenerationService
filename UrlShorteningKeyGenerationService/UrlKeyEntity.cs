using Newtonsoft.Json;

namespace UrlShorteningKeyGenerationService
{
    public class UrlKeyEntity
    {
        public UrlKeyEntity(string id, bool isTaken)
        {
            Id = id;
            IsTaken = isTaken;
        }

        [JsonProperty("id")]
        public string Id { get; }
        
        [JsonProperty("taken")]
        public bool IsTaken { get; }
    }
}
