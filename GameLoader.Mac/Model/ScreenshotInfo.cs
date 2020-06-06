using System;
using AppKit;
using Foundation;

namespace GameLoader.Mac.Model
{
    [Register(nameof(ScreenshotInfo))]
    public class ScreenshotInfo : ChangeNotifyingObject
    {
        private string title;
        private string filename;
        private NSImage bitmap;

        [Export(nameof(Title))]
        public string Title
        {
            get { return title; }
            set { Set(ref title, value, nameof(Title)); }
        }

        [Export(nameof(Filename))]
        public string Filename
        {
            get { return filename; }
            set { Set(ref filename, value, nameof(Filename)); }
        }

        [Export(nameof(Bitmap))]
        public NSImage Bitmap
        {
            get { return bitmap; }
            set { Set(ref bitmap, value, nameof(Filename)); }
        }
    }
}
