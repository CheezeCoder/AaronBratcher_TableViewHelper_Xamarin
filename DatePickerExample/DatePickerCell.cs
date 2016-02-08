using System;
using UIKit;

namespace DatePickerExample
{
	public delegate void DateUpdateEvent (Object sender, DateUpdateArgs args);
	///<summary>
	///
	///</summary>
	class DatePickerCell : UITableViewCell
	{
		//========================================================================================================================================
		//  PRIVATE CLASS PROPERTIES
		//========================================================================================================================================
		private int _cellHeight = 176;
		private UIDatePicker _datePicker;
		private string _rID = "datePicker";
		public event DateUpdateEvent didUpdateDatePicker;
		//========================================================================================================================================
		//  PUBLIC CLASS PROPERTIES
		//========================================================================================================================================
		public int CellHeight {
			get{ 
				return this._cellHeight;
			}
			set{ 
				return;
			}
		}

		public string RId {
			get{ 
				return this._rID;
			}
			set{ 
				return;
			}
		}
		//========================================================================================================================================
		//  Constructor
		//========================================================================================================================================
		/// <summary>
		/// Initializes a new instance of the <see cref="SaltAndPepper.ios.DatePickerCell"/> class.
		/// </summary>
		public DatePickerCell () : base(UITableViewCellStyle.Default, "datePicker")
		{
			initCell ();
		}
		//========================================================================================================================================
		//  PUBLIC OVERRIDES
		//========================================================================================================================================
		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			this._datePicker.Frame 	= new CoreGraphics.CGRect(this.Bounds.Top, this.Bounds.Left, this.Bounds.Width, this._cellHeight);
		}



		//========================================================================================================================================
		//  PUBLIC METHODS
		//========================================================================================================================================
		//========================================================================================================================================
		//  PRIVATE METHODS
		//========================================================================================================================================
		private void initCell()
		{
			this._datePicker 		= new UIDatePicker ();
			this._datePicker.Mode 	= UIDatePickerMode.DateAndTime;
			this._datePicker.ValueChanged += (object sender, EventArgs e) => {
				DateUpdateArgs dua = new DateUpdateArgs();
				Foundation.NSDateFormatter Dformatter = new Foundation.NSDateFormatter();
				Dformatter.DateFormat = "MMM dd', 'YYYY'         'HH:mm";
				dua.Date = Dformatter.ToString(this._datePicker.Date);
				this.didUpdateDatePicker(sender, dua);
			};
			this.AddSubview (_datePicker);
		}
	}
}