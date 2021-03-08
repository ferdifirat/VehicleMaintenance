using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleMaintenance.Core.Utilities
{
    public static class Extentions
    {

        public static string JsonSerialize(this object o, Formatting formatting = Formatting.None)
        {
            return JsonConvert.SerializeObject(o, formatting);
        }

        public static T JsonDeserialize<T>(this string s)
        {
            return (T)JsonConvert.DeserializeObject(s, typeof(T));
        }
    }
}
