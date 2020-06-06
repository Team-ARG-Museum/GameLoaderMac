using System.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using AppKit;
using SharpCompress.Archives.Zip;
using System.Linq;
using Foundation;

namespace GameLoader.Mac.Model
{
    [Register(nameof(HexArchive))]
    public class HexArchive : ChangeNotifyingObject
    {
        private const string INFO_FILENAME = "info.json";
        private const string BANNER_FILENAME = "banner.png";
        private const string CONTROLS_FILENAME = "controls.png";
        private const string SCREENSHOT_FILENAME_PREFIX = "screenshot";
        private const string SCREENSHOT_FILENAME_SUFFIX = "png";

        public string Filename { get; set; }

        private HexArchiveInfo info;


        [Export(nameof(Info))]
        public HexArchiveInfo Info
        {
            get { return info; }
            set { Set(ref info, value, nameof(Info)); }
        }


        public NSImage Banner { get; set; }


        public List<NSImage> Screenshots { get; set; }


        public NSImage Controls { get; set; }

        public string SupportedDevices { get; set; }

        public static object JSonConvert { get; private set; }

        public static Optional<HexArchive> FromFile(string filename)
        {
            try
            {
                var hexArchive = new HexArchive();

                using (var zipArchive = ZipArchive.Open(filename))
                {
                    hexArchive.Filename = filename;
                    hexArchive.Info = ExtractInfo(zipArchive);
                    hexArchive.Banner = ExtractNSImage(zipArchive, hexArchive.Info.Banner).Value;
                    hexArchive.Controls = ExtractNSImage(zipArchive, CONTROLS_FILENAME).Value;
                    hexArchive.SupportedDevices = ExtractSupportedDevicesFromInfo(hexArchive.Info);
                    PopulateScreenshotBitmapsFromInfo(zipArchive, hexArchive.Info);
                };

                return new Optional<HexArchive>(hexArchive);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new Optional<HexArchive>();
            }
        }

        private static string ExtractSupportedDevicesFromInfo(HexArchiveInfo info)
        {
            try
            {                
                return info.Binaries.Select(b => b.Device).Distinct().Aggregate((l, r) => $"{l}, {r}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return String.Empty;
            }
        }

        private static void PopulateScreenshotBitmapsFromInfo(ZipArchive zipArchive, HexArchiveInfo hexArchiveInfo)
        {
            foreach (var screenshot in hexArchiveInfo.Screenshots)
            {
                var bitmapEntry = zipArchive.Entries.FirstOrDefault(n => n.Key == screenshot.Filename);
                if (bitmapEntry != null)
                {
                    var bitmap = ExtractNSImage(zipArchive, bitmapEntry.Key);

                    if (bitmap.HasValue)
                    {
                        screenshot.Bitmap = bitmap.Value;
                    }
                }
            }
        }

        private static Optional<NSImage> ExtractNSImage(ZipArchive zipArchive, string NSImageFilename)
        {
            NSImage bitmap = null;

            try
            {
                var bitmapEntry = zipArchive.Entries.First(e => e.Key == NSImageFilename);

                using (var stream = bitmapEntry.OpenEntryStream())
                using (var reader = new StreamReader(stream))
                {
                    var data = new byte[stream.Length];
                    stream.Read(data, 0, data.Length);
                    var nsData = new NSData(Convert.ToBase64String(data), Foundation.NSDataBase64DecodingOptions.None);
                    bitmap = new NSImage(nsData);
                    nsData.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load NSImage: {NSImageFilename}: {ex.Message}");
            }

            if (bitmap != null)
            {
                return new Optional<NSImage>(bitmap);
            }
            else
            {
                return new Optional<NSImage>();
            }
        }

        private static HexArchiveInfo ExtractInfo(ZipArchive zipArchive)
        {
            var infoEntry = zipArchive.Entries.First(e => e.Key.EndsWith(".json", StringComparison.Ordinal));

            using (var stream = infoEntry.OpenEntryStream())
            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                var settings = new JsonSerializerSettings()
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                };
                return JsonConvert.DeserializeObject<HexArchiveInfo>(json, settings);
            }
        }

        public byte[] ExtactHexData()
        {
            using (var zipArchive = ZipArchive.Open(Filename))
            {
                var hexEntry = zipArchive.Entries.First(e => e.Key.ToLower().EndsWith(".hex", StringComparison.Ordinal));

                using (var stream = hexEntry.OpenEntryStream())
                using (var reader = new BinaryReader(stream))
                {
                    return reader.ReadBytes((int)hexEntry.Size);
                }
            }
        }

        public byte[] ExtactHexData(string archivedFilename)
        {
            using (var zipArchive = ZipArchive.Open(Filename))
            {
                var hexEntry = zipArchive.Entries.First(e => e.Key.ToLower() == archivedFilename.ToLower());

                using (var stream = hexEntry.OpenEntryStream())
                using (var reader = new BinaryReader(stream))
                {
                    return reader.ReadBytes((int)hexEntry.Size);
                }
            }
        }

        public override string ToString()
        {
            return $"{Info.Title}";
        }
    }
}

