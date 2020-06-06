// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace GameLoader.Mac
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSScrollView ArchiveCollectionView { get; set; }

		[Outlet]
		AppKit.NSProgressIndicator BusyIndicator { get; set; }

		[Outlet]
		AppKit.NSTextField BusyTextLabel { get; set; }

		[Outlet]
		AppKit.NSCollectionView HexArchiveCollection { get; set; }

		[Outlet]
		AppKit.NSButtonCell UploadArchiveButton { get; set; }

		[Outlet]
		AppKit.NSButton UploadSelectedHexArchiveButton { get; set; }

		[Action ("UploadHexArchiveAction:")]
		partial void UploadHexArchiveAction (Foundation.NSObject sender);

		[Action ("UploadSelectedHexArchiveAction:")]
		partial void UploadSelectedHexArchiveAction (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (ArchiveCollectionView != null) {
				ArchiveCollectionView.Dispose ();
				ArchiveCollectionView = null;
			}

			if (BusyIndicator != null) {
				BusyIndicator.Dispose ();
				BusyIndicator = null;
			}

			if (BusyTextLabel != null) {
				BusyTextLabel.Dispose ();
				BusyTextLabel = null;
			}

			if (HexArchiveCollection != null) {
				HexArchiveCollection.Dispose ();
				HexArchiveCollection = null;
			}

			if (UploadSelectedHexArchiveButton != null) {
				UploadSelectedHexArchiveButton.Dispose ();
				UploadSelectedHexArchiveButton = null;
			}

			if (UploadArchiveButton != null) {
				UploadArchiveButton.Dispose ();
				UploadArchiveButton = null;
			}
		}
	}
}
