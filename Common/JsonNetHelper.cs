using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml;

namespace RuRo.Common
{
    public class JsonNetHelper
    {
        public static string SerializeXmlNode(XmlNode node)
        {
            return JsonConvert.SerializeXmlNode(node);
        }
        public static string SerializeObject(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        public static T DeserializeObject<T>(string jsonStr)
        {
            return JsonConvert.DeserializeObject<T>(jsonStr);
        }
    }
}
