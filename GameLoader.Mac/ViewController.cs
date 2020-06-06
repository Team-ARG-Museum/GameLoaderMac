using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AppKit;
using Foundation;
using GameLoader.Mac.Model;

namespace GameLoader.Mac
{
    public partial class ViewController : NSViewController
    {
        
        private HexArchive selectedHexArchive;

        public delegate void SelectionChangedDelegate();
        public event SelectionChangedDelegate SelectionChanged;

        public HexArchiveCollectionViewDataSource DataSource { get; set; }

        [Export("SelectedHexArchive")]
        public HexArchive SelectedHexArchive
        {
            get { return selectedHexArchive; }
            set
            {
                WillChangeValue(nameof(SelectedHexArchive));
                selectedHexArchive = value;
                DidChangeValue(nameof(SelectedHexArchive));
                SelectionChanged?.Invoke();

                UploadSelectedHexArchiveButton.Enabled = SelectedHexArchive != null;
                UploadSelectedHexArchiveButton.Title = $"Upload {SelectedHexArchive?.Info?.Title}" ?? "Upload Game";
            }
        }

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {            
            base.AwakeFromNib();

            ConfigureCollectionView();
            PopulateWithData();

            UploadSelectedHexArchiveButton.Enabled = false;
        }

        private void PopulateWithData()
        {
			var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Arduboy");
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}

            DataSource = new HexArchiveCollectionViewDataSource(HexArchiveCollection);

            DataSource.Data.AddRange(Directory.EnumerateFiles(path, "*.arduboy")
                                              .Select(a => HexArchive.FromFile(a))
                                              .Where(o => o.HasValue)
                                              .Select(o => o.Value)
                                              .ToList());
        }

        private void ConfigureCollectionView()
        {
            HexArchiveCollection.RegisterClassForItem(typeof(HexArchiveItemController), "HexArchiveCell");

            var flowLayout = new NSCollectionViewFlowLayout()
            {
                ItemSize = new CoreGraphics.CGSize(200, 70),
                SectionInset = new NSEdgeInsets(2, 2, 2, 2),
                MinimumLineSpacing = 2,
                MinimumInteritemSpacing = 2
            };

            HexArchiveCollection.Selectable = true;
            HexArchiveCollection.WantsLayer = true;
            HexArchiveCollection.CollectionViewLayout = flowLayout;
            HexArchiveCollection.Delegate = new HexArchiveCollectionViewDelegate(this);
        }

        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
            } 
        }

        async partial void UploadHexArchiveAction(NSObject sender)
        {
            var filename = ShowOpenHexFileDialog();
            await UploadHexOrArchive(filename);

        }

        async partial void UploadSelectedHexArchiveAction(NSObject sender)
        {
            if (SelectedHexArchive == null)
            {
                return;
            }

            await UploadHexOrArchive(selectedHexArchive.Filename);
        }

		private async Task UploadHexOrArchive(string filename)
		{
			BusyTextLabel.StringValue = "";
			UploadArchiveButton.Enabled = false;
            UploadSelectedHexArchiveButton.Enabled = false;

			if (!String.IsNullOrWhiteSpace(filename))
			{
				if (filename.EndsWith(("arduboy"), StringComparison.Ordinal))
				{
					var hexArchive = HexArchive.FromFile((filename));
					if (hexArchive.HasValue)
					{
						var hexData = hexArchive.Value.ExtactHexData();
						filename = Path.GetTempFileName();
						File.WriteAllBytes(filename, hexData);
					}
				}


				BusyIndicator.StartAnimation(this);
				BusyTextLabel.StringValue = "Uploading...";


				await Task.Run(() =>
				{
					var arduboy = new Arduboy();

					if (arduboy.TryGetBootloader(out var bootloader))
					{
						Debug.WriteLine($"Bootloader found: {bootloader.ComName}");
						var avrdude = new AvrDudeInvoker(filename, bootloader.ComName);
						var progress = new Progress<string>(i => Debug.WriteLine((i)));
						avrdude.Invoke(progress);
						InvokeOnMainThread(() => BusyTextLabel.StringValue = "Upload complete.");
					}
					else
					{
						InvokeOnMainThread(() => BusyTextLabel.StringValue = "Upload failed, Arduboy bootloader not found");
					}
				});

				BusyIndicator.StopAnimation(this);
				UploadArchiveButton.Enabled = true;
                UploadSelectedHexArchiveButton.Enabled = true;
			}
		}

        private string ShowOpenHexFileDialog()
        {
            var dialog = NSOpenPanel.OpenPanel;
            dialog.CanChooseFiles = true;
            dialog.CanChooseDirectories = false;
            dialog.AllowedFileTypes = new string[] { "hex", "arduboy" };

            if (dialog.RunModal() == 1)
            {
                return dialog.Url.Path;
            }
            else
            {
                return String.Empty;
            }
        }
    }
}
