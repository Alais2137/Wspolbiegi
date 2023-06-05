using System.Collections.ObjectModel;
using System.Text.Json;
using System.Timers;

namespace Data
{
    public class Logger
    {
        private object _lock = new object();
        private static ObservableCollection<DataAPI> _balls = new();
        private System.Timers.Timer timer;
        public Logger(ObservableCollection<DataAPI> balls)
        {
            _balls = balls;
            timer = new System.Timers.Timer(10000);
            timer.Elapsed += SaveLogsToFile;
            timer.Start();
        }

        public void SaveLogsToFile(object sender, ElapsedEventArgs e) {
        
            var jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            foreach (DataAPI ball in _balls)
            {
                var objectToSerialize = new
                {
                    Timestamp = DateTime.Now,
                    Ball = ball
                };

                string logs = JsonSerializer.Serialize(objectToSerialize, jsonOptions);

                
                    File.AppendAllText(Path.GetFullPath(@"..\..\..\..\..\logs.json"), logs);
                
            }
        }
        public void Stop()
        {
            timer.Stop();
            timer.Dispose();
        }
    }
}
