///
/// ViewController.cs
/// AaronBratcherDatePickerExample
/// 
/// Original Created by: Aaron Bratcher on 10/02/2014.
/// Copyright (c) 2014 Aaron L. Bratcher. All rights reserved.
/// 
/// Modified by Chris Shields on 08/02/2016
/// This is a port to Xamarin to showcase the same example in C#.
/// 
using UIKit;
namespace AaronBratcherDatePickerExample
{
	public partial class ViewController : UIViewController
	{
		UITableView tableView;
		UITableViewSource source;
		TableViewHelper helper;

		public ViewController ()
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			tableView 	= new UITableView ();
			helper 		= new TableViewHelper (tableView);


			var s0r0 = buildCell (UITableViewCellStyle.Default, "S0R0", "Tap to hide/show Date Selector");
			var s0r1 = buildDateCell (UITableViewCellStyle.Default, "S0R1");
					 
			var s1r0 = buildCell (UITableViewCellStyle.Default, "S1R0", "Tap to fill 3rd Section");
					 
			var s2r0 = buildCell (UITableViewCellStyle.Default, "S2R0", "3rd Section - Row 0", "Tap to Delete");
			var s2r1 = buildCell (UITableViewCellStyle.Default, "S2R1", "3rd Section - Row 1", "Tap to Delete");
			var s2r2 = buildCell (UITableViewCellStyle.Default, "S2R2", "3rd Section - Row 2", "Tap to Delete");
					 
			var s3r0 = buildCell (UITableViewCellStyle.Default, "S3R0", "This one doesn't do anything");

			helper.addCell (0, s0r0, "S0R0");
			helper.addCell (0, s0r1, "S0R1");

			helper.addCell (1, s1r0, "S1R0");

			helper.addCell (2, s2r0, "S2R0");
			helper.addCell (2, s2r1, "S2R1");
			helper.addCell (2, s2r2, "S2R2");

			helper.addCell (3, s3r0, "S3R0");

			helper.hideCell ("S0R1");
			helper.hideCell ("S1R0");

			source 				= new TableViewSource (helper);
			tableView.Source 	= source;
			this.Add (tableView);

		}

		public override void ViewDidLayoutSubviews ()
		{
			this.tableView.Frame = new CoreGraphics.CGRect (this.View.Bounds.X, this.View.Bounds.Y + this.TopLayoutGuide.Length, this.View.Bounds.Width, this.View.Bounds.Height - this.TopLayoutGuide.Length);

		}





		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		public UITableViewCell buildCell(UITableViewCellStyle style, string identifier, string label, string subLabel = null)
		{
			var cell = new UITableViewCell (style, identifier);
			cell.TextLabel.Text = label;

			if (style == UITableViewCellStyle.Value1) {
				cell.DetailTextLabel.Text = subLabel;
			}

			return cell;

		}

		public UITableViewCell buildDateCell(UITableViewCellStyle style, string identifier)
		{
			var cell = buildCell (style, identifier, null, null);
			var datepicker 					= new UIDatePicker ();
			datepicker.Mode 				= UIDatePickerMode.Date;
			datepicker.Locale 				= Foundation.NSLocale.CurrentLocale;
			datepicker.MinuteInterval 		= 1;
			datepicker.Date 				= Foundation.NSDate.Now;
			datepicker.HorizontalAlignment	= UIControlContentHorizontalAlignment.Center;
			datepicker.VerticalAlignment 	= UIControlContentVerticalAlignment.Center;

			cell.AddSubview (datepicker);

			cell.AddConstraint (
				NSLayoutConstraint.Create(cell, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, datepicker, NSLayoutAttribute.CenterY, 1, 0)
			);
			cell.AddConstraint (
				NSLayoutConstraint.Create(cell, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, datepicker, NSLayoutAttribute.CenterX, 1, 0)
			);

			return cell;
		}


	}
}

