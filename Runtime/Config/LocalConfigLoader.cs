using System;
using System.IO;
using Newtonsoft.Json;

namespace UnityUtility
{
    public static class LocalConfigLoader
    {
        public static T Load<T>(string rootPath, string fileName)
        {
            if (!FindFileInParent.Exec(rootPath, fileName, out var configPath))
            {
                Log.Error("File not found: '{0}'", fileName);
                return default;
            }

            try
            {
                var configJson = File.ReadAllText(configPath);
                var config = JsonConvert.DeserializeObject<T>(configJson);
                return config;
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return default;
            }
        }
    }
}
