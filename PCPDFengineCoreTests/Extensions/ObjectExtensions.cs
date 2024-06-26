﻿using Newtonsoft.Json;

namespace PCPDFengineCoreTests.Extensions
{
    public static class ObjectExtensions
    {
        public static string DumpObject(this object obj)
        {
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            return json;
        }
    }
}
