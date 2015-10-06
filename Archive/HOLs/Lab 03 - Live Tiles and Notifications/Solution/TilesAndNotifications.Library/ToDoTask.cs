using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace TilesAndNotifications.Library { 
    public class ToDoTask
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool? IsComplete { get; set; }

        public string ToJson()
        {
            using (var stream = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(typeof(ToDoTask));
                serializer.WriteObject(stream, this);
                stream.Position = 0;
                var jsonBytes = stream.ToArray();
                return Encoding.UTF8.GetString(jsonBytes, 0, jsonBytes.Length);
            }
        }

        public static ToDoTask FromJson(string json)
        {
            // note: throws exception if the json is not valid
            var jsonData = JsonObject.Parse(json);

            // exceptions will be thrown if the values do not match the types
            return new ToDoTask
            {
                Id = Guid.Parse(jsonData["Id"].GetString()),
                Description = jsonData["Description"].GetString(),
                IsComplete = jsonData["IsComplete"].GetBoolean()
            };
        }
    }
}