using System;
using UIKit;
using Foundation;
namespace DatePickerExample
{
	///<summary>
	///
	///</summary>
	class TableViewSource : UITableViewSource
	{
		//========================================================================================================================================
		//  PRIVATE CLASS PROPERTIES
		//========================================================================================================================================
		private DatePickerCell dpc;
		private DateShowerCell dsc;
		private NSMutableDictionary heightAtIndexPath;
		private bool showDatePicker;
		private int numRows;
		//========================================================================================================================================
		//  PUBLIC CLASS PROPERTIES
		//========================================================================================================================================
		//========================================================================================================================================
		//  Constructor
		//========================================================================================================================================
		/// <summary>
		/// Initializes a new instance of the <see cref="TableViewSource"/> class.
		/// </summary>
		public TableViewSource ()
		{
			this.dpc = new DatePickerCell ();
			this.dsc = new DateShowerCell ();
			dpc.didUpdateDatePicker += dsc.updateCellText;
			heightAtIndexPath = new Foundation.NSMutableDictionary ();
			this.showDatePicker = false;
			numRows = 2;
		}
		//========================================================================================================================================
		//  PUBLIC OVERRIDES
		//========================================================================================================================================

		public override nfloat EstimatedHeight (UITableView tableView, NSIndexPath indexPath)
		{
			//NSString celltype = tableView.CellAt (indexPath).ReuseIdentifier;
//			NSNumber height = (NSNumber)this.heightAtIndexPath.ObjectForKey(indexPath);
//			if (height != null) {
//				return height.FloatValue;
//			} else {
//				return UITableView.AutomaticDimension;
//			}

//			if (celltype == "datePicker") {
//				return 176f;
//			} else {
//				return UITableView.AutomaticDimension;
//			}
			return 176;

		}

		public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var row = indexPath.Row;
			var cell = tableView.DequeueReusableCell (dsc.RId);
			if (cell == null)
				cell = dsc;

			if (row == 1) {
				cell = tableView.DequeueReusableCell (dpc.RId);
				if (cell == null)
					cell = dpc;
			}
			if (row == 2) {
				cell = tableView.DequeueReusableCell ("NewBlankCell");
				if (cell == null){
					cell = new UITableViewCell (UITableViewCellStyle.Default, "NewBlankCell");
					cell.TextLabel.Text = "I Am the row underneath";
				}
			}

			return cell;
		}

		public override void WillDisplay (UITableView tableView, UITableViewCell cell, NSIndexPath indexPath)
		{
			NSNumber height = new NSNumber((float)cell.Frame.Size.Height);
			this.heightAtIndexPath.Add (indexPath, height);
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return numRows;
		}

		public override nfloat GetHeightForRow (UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			if (indexPath.Row == 1)
				return dpc.CellHeight;
			return 44;
		}

		public override void RowSelected (UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			if (indexPath.Row == 0 && !showDatePicker) {
				openDatePicker (tableView, indexPath);
			}
			else if (indexPath.Row == 0 && showDatePicker) {
				closeDatePicker (tableView, indexPath);
			}
		}

		//========================================================================================================================================
		//  PUBLIC METHODS
		//========================================================================================================================================
		//========================================================================================================================================
		//  PRIVATE METHODS
		//========================================================================================================================================
		private void openDatePicker(UITableView tv, Foundation.NSIndexPath ip)
		{
			tv.BeginUpdates ();
			showDatePicker = true;
			var ips = new []{ NSIndexPath.FromRowSection (ip.Row + 1, 0) };
			numRows++;
			tv.InsertRows (ips, UITableViewRowAnimation.Top);
			tv.DeselectRow (ip, true);
			tv.EndUpdates ();

		}

		private void closeDatePicker(UITableView tv, Foundation.NSIndexPath ip)
		{
			tv.BeginUpdates ();
			Foundation.NSIndexPath[] ips = new Foundation.NSIndexPath[]{ Foundation.NSIndexPath.FromRowSection (ip.Row + 1, 0) };
			var cellPath = NSIndexPath.FromRowSection (ip.Row + 1, 0);

			var cellToAnimate = tv.CellAt (cellPath);
			var point = cellToAnimate.Center;
			Console.WriteLine (cellToAnimate.Center);
			Console.WriteLine (cellToAnimate.Frame);
			Console.WriteLine (tv.Center);
			Console.WriteLine (tv.Frame);
			Console.WriteLine (cellToAnimate.Center.Y - 176);
			UIView.Animate(6, 0, UIViewAnimationOptions.CurveEaseInOut,
				()=> {
					cellToAnimate.Center =
						new CoreGraphics.CGPoint(cellToAnimate.Center.X, cellToAnimate.Center.Y + 176);},
				()=> {
					cellToAnimate.Center = point;}
			);
			showDatePicker = false;
			
			numRows--;
			tv.DeleteRows (ips, UITableViewRowAnimation.None);
			tv.DeselectRow (ip, true);
			tv.EndUpdates ();

//			tv.BeginUpdates ();
//			showDatePicker = false;
//			Foundation.NSIndexPath[] ips = new Foundation.NSIndexPath[]{ Foundation.NSIndexPath.FromRowSection (ip.Row + 1, 0) };
//			numRows--;
//			tv.DeleteRows (ips, UITableViewRowAnimation.Top);
//			tv.DeselectRow (ip, true);
//			tv.EndUpdates ();
		}



	}
}