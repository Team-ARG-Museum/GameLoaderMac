using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using GameLoader.Mac.Model;

namespace GameLoader.Mac
{
    public partial class HexArchiveItemController : NSCollectionViewItem
    {

        private HexArchive hexArchive;

        #region Constructors

        // Called when created from unmanaged code
        public HexArchiveItemController(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        // Called when created directly from a XIB file
        [Export("initWithCoder:")]
        public HexArchiveItemController(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        // Call to load from the XIB/NIB file
        public HexArchiveItemController() : base("HexArchiveItem", NSBundle.MainBundle)
        {
            Initialize();
        }

        // Shared initialization code
        void Initialize()
        {
        }

        #endregion

        //strongly typed view accessor
        public new HexArchiveItem View
        {
            get
            {
                return (HexArchiveItem)base.View;
            }
        }

        [Export("HexArchive")]
        public HexArchive HexArchive
        {
            get { return hexArchive; }
            set 
            {
                WillChangeValue(nameof(HexArchive));
                hexArchive = value;
                DidChangeValue(nameof(HexArchive));
            }
        }

        public NSColor BackgroundColor 
        {
            get { return ItemBox.FillColor; }    
            set { ItemBox.FillColor = value; }
        }

		public override bool Selected
		{
			get
			{
				return base.Selected;
			}
			set
			{
                base.Selected = value;
                BackgroundColor = value ? NSColor.FromRgb(200, 200, 200) : NSColor.Control;
			}
		}
    }
}
