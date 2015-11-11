using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DtoDeepDive.UnitTests {
    public static class JsonHelpers {
        public static string ToJson(this object obj) {
            return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings {
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }
    }
}
