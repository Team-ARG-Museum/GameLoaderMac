using System;
using Foundation;
using AppKit;
namespace GameLoader.Mac
{
    public class HexArchiveCollectionViewDelegate : NSCollectionViewDelegateFlowLayout
    {
        public ViewController ParentViewController { get; set; }
        public HexArchiveCollectionViewDelegate(ViewController parentViewController) => ParentViewController = parentViewController;

        public override void ItemsSelected(NSCollectionView collectionView, NSSet indexPaths)
        {
            var paths = indexPaths.ToArray<NSIndexPath>();
            var index = (int)paths[0].Item;
            ParentViewController.SelectedHexArchive = ParentViewController.DataSource.Data[index];
        }

        public override void ItemsDeselected(NSCollectionView collectionView, NSSet indexPaths)
        {
			var paths = indexPaths.ToArray<NSIndexPath>(); //??
			var index = (int)paths[0].Item; //?
            ParentViewController.SelectedHexArchive = null;
        }
    }
}
