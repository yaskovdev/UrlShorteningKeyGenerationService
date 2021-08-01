using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using static UrlShorteningKeyGenerationService.Services.IConstants;

namespace UrlShorteningKeyGenerationService
{
    public class GetKeysRequest
    {
        [Range(1, CacheCapacity)]
        [FromQuery(Name = "limit")]
        public int? Limit { get; set; }
    }
}
