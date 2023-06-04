using System.Text.Json;

namespace Data
{
    public class Logger
    {
        private object _lock = new object();

        public void SaveLogsToFile(DataAPI ball) {
        
            var jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var objectToSerialize = new
            {
                Timestamp = DateTime.Now,
                Ball = ball
            };

            string logs = JsonSerializer.Serialize(objectToSerialize, jsonOptions);

            lock (_lock)
            {
                File.AppendAllText(Path.GetFullPath(@"..\..\..\..\..\logs.json"), logs);
            }
        }
    }
}
