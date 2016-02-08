using System;
using UIKit;

namespace DynamicTextInputCellExample
{
	///<summary>
	///
	///</summary>
	class TableViewSource : UITableViewSource
	{
		//========================================================================================================================================
		//  PRIVATE CLASS PROPERTIES
		//========================================================================================================================================
		public DynamicTextViewCell _textViewCell;
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
		}
		//========================================================================================================================================
		//  PUBLIC OVERRIDES
		//========================================================================================================================================

		public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var row = indexPath.Row;
			var cell = tableView.DequeueReusableCell (this._textViewCell.ReuseIdentifier);

			if (cell == null)
				cell = new DynamicTextViewCell ();

			this._textViewReference = (cell as DynamicTextViewCell).TextView;
//			(cell as DynamicTextViewCell).TextView.Delegate = this;
			return cell;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return 1;
		}



		public override void RowSelected (UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			
		}

		//========================================================================================================================================
		//  PUBLIC METHODS
		//========================================================================================================================================
		//========================================================================================================================================
		//  PRIVATE METHODS
		//========================================================================================================================================







	}
}