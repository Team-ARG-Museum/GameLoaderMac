using Foundation;
using Newtonsoft.Json;

namespace GameLoader.Mac.Model
{
    [Register(nameof(BinaryInfo))]
    public class BinaryInfo : ChangeNotifyingObject
    {
        private string title;
        private string filename;
        private string device;

        [JsonProperty("title")]
        public string Title
        {
            get { return title; }
            set { Set(ref title, value, nameof(Title)); }
        }

        [JsonProperty("filename")]
        public string Filename
        {
            get { return filename; }
            set { Set(ref filename, value, nameof(Filename)); }
        }

        [JsonProperty("device")]
        public string Device
        {
            get { return device; }
            set { Set(ref device, value, nameof(Device)); }
        }
    }
}
