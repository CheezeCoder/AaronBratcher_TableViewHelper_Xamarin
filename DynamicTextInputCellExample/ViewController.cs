using System;
using UIKit;

namespace DynamicTextInputCellExample
{
	///<summary>
	///
	///</summary>
	class ViewController : UIViewController
	{
		//========================================================================================================================================
		//  PRIVATE CLASS PROPERTIES
		//========================================================================================================================================
		private TableViewSource _source;
		private UITableView _tableView;
		private UITextView _textViewReference;
		//========================================================================================================================================
		//  PUBLIC CLASS PROPERTIES
		//========================================================================================================================================
		//========================================================================================================================================
		//  Constructor
		//========================================================================================================================================
		/// <summary>
		/// Initializes a new instance of the <see cref="SaltAndPepper.ios.ViewController"/> class.
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
			this._tableView 		= new UITableView ();
			this._source 			= new TableViewSource ();
			this._tableView.Source 	= _source;

			this.Add (this._tableView);

			Foundation.NSNotificationCenter.DefaultCenter.AddObserver (UIKeyboard.WillShowNotification, keyboardWillShow);
			Foundation.NSNotificationCenter.DefaultCenter.AddObserver (UIKeyboard.WillHideNotification, keyboardWillHide);

		}

		public override void ViewDidLayoutSubviews ()
		{
			base.ViewDidLayoutSubviews ();
			_tableView.Frame = new CoreGraphics.CGRect(this.View.Bounds.X, this.View.Bounds.Y + this.TopLayoutGuide.Length, this.View.Bounds.Width, this.View.Bounds.Height - this.TopLayoutGuide.Length);
		}
		//========================================================================================================================================
		//  PUBLIC METHODS
		//========================================================================================================================================
		//========================================================================================================================================
		//  PRIVATE METHODS
		//========================================================================================================================================
		private void setDelegates()
		{
			this._source._textViewCell.TextView.Changed += (object sender, EventArgs e) => {
				textViewDidChange(this._source._textViewCell.TextView);
			};

			this._source._textViewCell.TextView.Started += (object sender, EventArgs e) => {
				textViewDidBeginEditing(this._source._textViewCell.TextView);
			};


		}
		private nfloat textViewheightForRow()
		{
			UITextView calculationView = this._textViewReference;
			nfloat textViewWidth = calculationView.Frame.Size.Width;

			if (calculationView.AttributedText == null) {
				calculationView = new UITextView ();
				Foundation.NSAttributedString astring = new Foundation.NSAttributedString ("This is some test text");
				calculationView.AttributedText = astring;
				textViewWidth = 320;
			}

			CoreGraphics.CGSize size = calculationView.SizeThatFits (new CoreGraphics.CGSize(textViewWidth, float.MaxValue));
			return size.Height;


		}

		private void textViewDidChange(UITextView textView)
		{
			this._tableView.BeginUpdates ();
			this._tableView.EndUpdates ();

			this.scrollToCursorForTextView (textView);
		}

		private void textViewDidBeginEditing(UITextView textView)
		{
			this.scrollToCursorForTextView (textView);
		}

		private void scrollToCursorForTextView(UITextView textView)
		{
			CoreGraphics.CGRect cursorRect = textView.GetCaretRectForPosition (textView.SelectedTextRange.Start);
			cursorRect = this._tableView.ConvertRectFromView (cursorRect, textView);

			if(this.rectVisible(cursorRect))
			{
				CoreGraphics.CGSize tempSize = cursorRect.Size;
				tempSize.Height += 8;
				cursorRect.Size = tempSize;
				this._tableView.ScrollRectToVisible (cursorRect, true);
			}
		}

		private bool rectVisible(CoreGraphics.CGRect rect)
		{
			CoreGraphics.CGRect visibleRect;
			visibleRect.Location 	= this._tableView.ContentOffset;
			visibleRect.Location.Y 	= this._tableView.ContentInset.Top;
			visibleRect.Size 		= this._tableView.Bounds.Size;
			visibleRect.Size.Height -= this._tableView.ContentInset.Top + this._tableView.ContentInset.Bottom;

			return visibleRect.Contains (rect);
		}

		private void keyboardWillShow(Foundation.NSNotification aNotification)
		{
			Foundation.NSDictionary info = aNotification.UserInfo;
			CoreGraphics.CGSize kbSize = (info.ObjectForKey (UIKeyboard.FrameBeginUserInfoKey) as Foundation.NSValue).RectangleFValue.Size;

			UIEdgeInsets contentInsets = new UIEdgeInsets (this._tableView.ContentInset.Top, 0.0, kbSize.Height, 0);
			this._tableView.ContentInset = contentInsets;
			this._tableView.ScrollIndicatorInsets = contentInsets;

		}

		private void keyboardWillHide(Foundation.NSNotification aNotificition)
		{
			UIView.BeginAnimations (null);
			UIView.SetAnimationDuration (0.35);
			UIEdgeInsets constentInsets = new UIEdgeInsets (this._tableView.ContentInset.Top, 0.0, 0.0, 0.0);

			this._tableView.ContentInset = constentInsets;
			this._tableView.ScrollIndicatorInsets = constentInsets;

			UIView.CommitAnimations ();
		}



			
	}
}