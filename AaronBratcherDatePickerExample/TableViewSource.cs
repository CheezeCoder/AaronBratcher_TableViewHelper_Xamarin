///
/// TableViewSource.cs
/// AaronBratcherDatePickerExample
/// 
/// Original Created by: Aaron Bratcher on 10/02/2014.
/// Copyright (c) 2014 Aaron L. Bratcher. All rights reserved.
/// 
/// Modified by Chris Shields on 08/02/2016
/// This is a port to Xamarin to showcase the same example in C#.
/// 
using System;
using UIKit;

namespace AaronBratcherDatePickerExample
{
	public class TableViewSource : UITableViewSource
	{
		private TableViewHelper helper;

		public TableViewSource (TableViewHelper helper)
		{
			this.helper = helper;
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			var count = helper.numberOfSections ();
			return count;
		}

		public override nint RowsInSection (UITableView tableView, nint section)
		{
			return helper.numberOfRowsInSection (section);
		}

		public override nfloat GetHeightForRow (UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			if (indexPath.Section == 0 && indexPath.Row == 1) {
				return 176;
			}

			return tableView.RowHeight;
		}

		public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			return helper.cellForRowAtIndexPath (indexPath);
		}

		public override void RowSelected (UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			tableView.DeselectRow (indexPath, true);
			string name;

			if(!String.IsNullOrEmpty (name = helper.cellNameAtIndexPath (indexPath)))
			{
				switch (name) {
				case "S0R0":
					if (!helper.cellIsVisible ("S0R1")) {
						helper.showCell ("S0R1");
					} else {
						helper.hideCell ("S0R1");
					}
					break;
				case "S0R1":
					helper.hideCell (name);
					break;
				case "S1R0":
					helper.showCell ("S2R0");
					helper.showCell ("S2R1");
					helper.showCell ("S2R2");
					helper.hideCell (name);
					break;
				case "S3R0":
					break;

				default:
					helper.hideCell (name);
					helper.showCell ("S1R0");
					break;
				}

				if (name != "S0R0") {
					helper.hideCell ("S0R1");
				}

			}



		}

		public void showHide(Object sender, EventArgs args)
		{
			var button 	= sender as UIButton;
			var label 	= button.TitleLabel;
			var title	 = label.Text;

			if(helper.cellIsVisible(title)){
				helper.hideCell(title);
			} else{
				helper.showCell (title);
			}
		}
	}
}

