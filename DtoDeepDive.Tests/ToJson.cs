using Newtonsoft.Json;

namespace DtoDeepDive.IntegrationTests {
    public static class JsonHelpers {
        public static string ToJson(this object obj) {
            return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
        }
    }
}
