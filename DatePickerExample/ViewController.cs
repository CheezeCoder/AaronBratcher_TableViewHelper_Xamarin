using System;
using UIKit;

namespace DatePickerExample
{
	///<summary>
	///
	///</summary>
	class ViewController : UIViewController
	{
		//========================================================================================================================================
		//  PRIVATE CLASS PROPERTIES
		//========================================================================================================================================
		UITableView _tableView;
		UITableViewSource _source;
		//========================================================================================================================================
		//  PUBLIC CLASS PROPERTIES
		//========================================================================================================================================
		//========================================================================================================================================
		//  Constructor
		//========================================================================================================================================
		/// <summary>
		/// Initializes a new instance of the <see cref="SaltAndPepper.ios.MyClass"/> class.
		/// </summary>
		public ViewController ()
		{
			
		}
		//========================================================================================================================================
		//  PUBLIC OVERRIDES
		//========================================================================================================================================
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			_tableView 			= new UITableView ();
			_source 			= new TableViewSource ();

			this._tableView.RowHeight = UITableView.AutomaticDimension;
			this._tableView.EstimatedRowHeight = 176;
			_tableView.Source 	= _source;
			this.Add (_tableView);


		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
		}

		public override void ViewDidLayoutSubviews ()
		{
			base.ViewDidLayoutSubviews ();
			_tableView.Frame = new CoreGraphics.CGRect(this.View.Bounds.X, this.View.Bounds.Y + 22, this.View.Bounds.Width, this.View.Bounds.Height - 22);
		}
		//========================================================================================================================================
		//  PUBLIC METHODS
		//========================================================================================================================================
		//========================================================================================================================================
		//  PRIVATE METHODS
		//========================================================================================================================================
	}
}