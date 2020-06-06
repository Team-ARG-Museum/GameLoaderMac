using System.Collections.Generic;
using System.Collections.ObjectModel;
using Foundation;
using Newtonsoft.Json;

namespace GameLoader.Mac.Model
{

    [Register(nameof(HexArchiveInfo))]
    public class HexArchiveInfo : ChangeNotifyingObject
    {
        private string title;
        private string details;
        private string version;
        private string genre;
        private string device;
        private string date;
        private string author;
        private string url;
        private string companion;
        private string sourceUrl;
        private string eMail;
        private string publisher;
        private string idea;
        private string code;
        private string art;
        private string sound;
        private string banner;
        private List<string> buttons;
        private List<BinaryInfo> binaries;
        private List<ScreenshotInfo> screenshots;

        [Export(nameof(Title))]
        [JsonProperty("title")]
        public string Title
        {
            get { return title; }
            set { Set(ref title, value, nameof(Title)); }
        }

        [Export(nameof(Details))]
        [JsonProperty("description")]
        public string Details
        {
            get { return details; }
            set { Set(ref details, value, nameof(Details)); }
        }

        [Export(nameof(Version))]
        [JsonProperty("version")]
        public string Version
        {
            get { return version; }
            set { Set(ref version, value, nameof(Version)); }
        }

        [Export(nameof(Genre))]
        [JsonProperty("genre")]
        public string Genre
        {
            get { return genre; }
            set { Set(ref genre, value, nameof(Genre)); }
        }

        [Export(nameof(Device))]
        [JsonProperty("device")]
        public string Device
        {
            get { return device; }
            set { Set(ref device, value, nameof(Device)); }
        }

        [Export(nameof(Date))]
        [JsonProperty("date")]
        public string Date
        {
            get { return date; }
            set { Set(ref date, value, nameof(Date)); }
        }

        [Export(nameof(Author))]
        [JsonProperty("author")]
        public string Author
        {
            get { return author; }
            set { Set(ref author, value, nameof(Author)); }
        }

        [Export(nameof(Url))]
        [JsonProperty("url")]
        public string Url
        {
            get { return url; }
            set { Set(ref url, value, nameof(Url)); }
        }

        [Export(nameof(Companion))]
        [JsonProperty("companion")]
        public string Companion
        {
            get { return companion; }
            set { Set(ref companion, value, nameof(Companion)); }
        }

        [Export(nameof(SourceUrl))]
        [JsonProperty("sourceUrl")]
        public string SourceUrl
        {
            get { return sourceUrl; }
            set { Set(ref sourceUrl, value, nameof(SourceUrl)); }
        }

        [Export(nameof(EMail))]
        [JsonProperty("email")]
        public string EMail
        {
            get { return eMail; }
            set { Set(ref eMail, value, nameof(EMail)); }
        }

        [Export(nameof(Publisher))]
        [JsonProperty("publisher")]
        public string Publisher
        {
            get { return publisher; }
            set { Set(ref publisher, value, nameof(Publisher)); }
        }

        [Export(nameof(Idea))]
        [JsonProperty("idea")]
        public string Idea
        {
            get { return idea; }
            set { Set(ref idea, value, nameof(Idea)); }
        }

        [Export(nameof(Code))]
        [JsonProperty("code")]
        public string Code
        {
            get { return code; }
            set { Set(ref code, value, nameof(Code)); }
        }

        [Export(nameof(Art))]
        [JsonProperty("art")]
        public string Art
        {
            get { return art; }
            set { Set(ref art, value, nameof(Art)); }
        }

        [Export(nameof(Sound))]
        [JsonProperty("sound")]
        public string Sound
        {
            get { return sound; }
            set { Set(ref sound, value, nameof(Sound)); }
        }


        [Export(nameof(Banner))]
        [JsonProperty("banner")]
        public string Banner
        {
            get { return banner; }
            set { Set(ref banner, value, nameof(Banner)); }
        }

      //  [Export(nameof(Buttons))]
     //   [JsonProperty("buttons")]
     //   public List<string> Buttons
      //  {
      //      get { return buttons; }
      //      set { Set(ref buttons, value, nameof(Buttons)); }
      //  }

      //  [Export(nameof(Binaries))]
        [JsonProperty("binaries")]
        public List<BinaryInfo> Binaries
        {
            get { return binaries; }
            set { Set(ref binaries, value, nameof(Binaries)); }
        }

  //      [Export(nameof(Screenshots))]
        [JsonProperty("screenshots")]
        public List<ScreenshotInfo> Screenshots
        {
            get { return screenshots; }
            set { Set(ref screenshots, value, nameof(Screenshots)); }
        }

        public HexArchiveInfo()
        {
            //Buttons = new List<string>();
            //Binaries = new List<BinaryInfo>();
            //Screenshots = new List<ScreenshotInfo>();
        }
    }
}
