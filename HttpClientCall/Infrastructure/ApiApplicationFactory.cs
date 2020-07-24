using HttpClientCall.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HttpClientCall.Infrastructure
{
    public class ApiApplicationFactory
    {
        public static Uri apiUri;
        private static Lazy<ApiClient> restClient = new Lazy<ApiClient>(
            () => new ApiClient(apiUri), System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);
        static ApiApplicationFactory()
        {
            apiUri = new Uri(ApplicationSettings.ApiUrl);
        }
        public static ApiClient newInstance
        {
            get
            {
                return restClient.Value;
            }
        }
    }
}
