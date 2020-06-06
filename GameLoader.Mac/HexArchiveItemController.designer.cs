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
	[Register ("HexArchiveItemController")]
	partial class HexArchiveItemController
	{
		[Outlet]
		AppKit.NSBox ItemBox { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ItemBox != null) {
				ItemBox.Dispose ();
				ItemBox = null;
			}
		}
	}
}
