﻿using System;
using UIKit;

namespace DatePickerExample
{
	///<summary>
	///
	///</summary>
	class DateShowerCell : UITableViewCell
	{
		//========================================================================================================================================
		//  PRIVATE CLASS PROPERTIES
		//========================================================================================================================================
		private string _rID = "dateChooser";
		private UITextField tf;
		//========================================================================================================================================
		//  PUBLIC CLASS PROPERTIES
		//========================================================================================================================================
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
		/// Initializes a new instance of the <see cref="SaltAndPepper.ios.DateShowerCell"/> class.
		/// </summary>
		public DateShowerCell () : base(UITableViewCellStyle.Default, "dateChooser")
		{
			initCell ();
		}
		//========================================================================================================================================
		//  PUBLIC OVERRIDES
		//========================================================================================================================================
		//========================================================================================================================================
		//  PUBLIC METHODS
		//========================================================================================================================================
		public void updateCellText(Object sender, DateUpdateArgs args)
		{
			tf.Text = args.Date;
			tf.SizeToFit ();
		}
		//========================================================================================================================================
		//  PRIVATE METHODS
		//========================================================================================================================================
		private void initCell()
		{
			this.TextLabel.Text = "On:";
			this.TextLabel.TextColor = UIColor.LightGray;
			this.tf = new UITextField ();
			this.AccessoryView = tf;
		}
	}
}