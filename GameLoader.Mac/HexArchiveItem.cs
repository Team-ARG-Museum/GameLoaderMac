using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;

namespace GameLoader.Mac
{
    public partial class HexArchiveItem : AppKit.NSView
    {
        #region Constructors

        // Called when created from unmanaged code
        public HexArchiveItem(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        // Called when created directly from a XIB file
        [Export("initWithCoder:")]
        public HexArchiveItem(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        // Shared initialization code
        void Initialize()
        {
        }

        #endregion
    }
}
