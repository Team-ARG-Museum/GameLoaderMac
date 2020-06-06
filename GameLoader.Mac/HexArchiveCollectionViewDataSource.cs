using System;
using AppKit;
using System.Collections.Generic;
using Foundation;
using GameLoader.Mac.Model;

namespace GameLoader.Mac
{
    public class HexArchiveCollectionViewDataSource : NSCollectionViewDataSource
    {
        public NSCollectionView ParentCollectionView { get; set; }

        public List<HexArchive> Data { get; set; } = new List<HexArchive>();


        public HexArchiveCollectionViewDataSource(NSCollectionView parent)
        {
            ParentCollectionView = parent;
            parent.DataSource = this;
        }

		public override nint GetNumberOfSections(NSCollectionView collectionView) => 1;
        public override nint GetNumberofItems(NSCollectionView collectionView, nint section) => Data.Count;

        public override NSCollectionViewItem GetItem(NSCollectionView collectionView, NSIndexPath indexPath)
        {
            var item = collectionView.MakeItem("HexArchiveCell", indexPath) as HexArchiveItemController;
            item.HexArchive = Data[(int)indexPath.Item];
            return item;        
        }
    }
}
