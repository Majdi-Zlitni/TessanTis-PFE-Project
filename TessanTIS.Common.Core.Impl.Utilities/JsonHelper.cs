using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TessanTIS.Common.Core.Impl.Configuration;

namespace TessanTIS.Common.Core.Impl.Utilities
{
    public class JsonHelper
    {
        public static void SerializingObjectToJson<T>(List<T> genericList,string filepath)
        {
            try
            {
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }
                var settings = new Newtonsoft.Json.JsonSerializerSettings();
                settings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Objects;
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(genericList, genericList.GetType(), settings);
                using(StreamWriter streamWriter=new StreamWriter(filepath, true))
                {
                    streamWriter.WriteLine(json.ToString());
                    streamWriter.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static List<T> DeSerializingJsonToObject<T>(string filename)
        {
            string folderpath = ConfigurationHelper.Config["DataConfig:JsonFolderPath"];
            string filepath = folderpath + filename;
            List<T> configs = new List<T>();
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

            if (!File.Exists(filepath)) throw new FileNotFoundException("Cannot locate the file: \n   \"" + filepath + "\" \nPlease check the path and try again.");
            try
            {
                configs = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(filepath) , settings);
            }
            catch (Exception ex)
            {
                throw new FileLoadException("Unable to load:\nThe file \""+filepath+"\" does not load correctly.",ex);
            }
            return configs;
        }
    }
}
