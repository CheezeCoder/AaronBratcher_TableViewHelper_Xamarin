///
/// TableViewHelper.cs
/// AaronBratcherDatePickerExample
/// 
/// Original Created by: Aaron Bratcher on 10/02/2014.
/// Copyright (c) 2014 Aaron L. Bratcher. All rights reserved.
/// 
/// Modified by Chris Shields on 08/02/2016
/// This is a port to Xamarin to showcase the same example in C#.
/// 
using System;
using Foundation;
using UIKit;
using System.Collections.Generic;

namespace AaronBratcherDatePickerExample
{
	public class TableViewHelper
	{
		private class Cell {

			public int section;
			public string name;
			public UITableViewCell tableViewCell;
			public bool visible;

			public Cell(int section, string name, UITableViewCell tableViewCell)
			{
				this.section 		= section;
				this.name 			= name;
				this.tableViewCell 	= tableViewCell;
				visible 			= true;
			}
		}

		public UITableView tableView;
		List<Cell> cells;
		private Dictionary<NSIndexPath, Cell> indexedCells;
		private Dictionary<int, int> cellCount;

		public TableViewHelper (UITableView tableView)
		{
			this.tableView = tableView;
			cells = new List<Cell> ();
			cellCount = new Dictionary<int, int> ();
			indexedCells = new Dictionary<NSIndexPath, Cell> ();
		}

		public void addCell(int section, UITableViewCell cell, string name)
		{
			var newCell = new Cell (section, name, cell);
			cells.Add (newCell);
			NSIndexPath indexPath;
			int count;

			if (cellCount.TryGetValue (section, out count)) {
				cellCount [section] = count + 1;
				indexPath = NSIndexPath.FromItemSection (count, section);
			} else {
				cellCount [section] = 1;
				indexPath = NSIndexPath.FromItemSection (0, section);
			}

			indexedCells [indexPath] = newCell;

		}

		public void hideCell(string name)
		{
			var removePaths = new List<NSIndexPath> ();
			var removeSections = new NSMutableIndexSet ();

			for (int section = 0; section < numberOfSections(); section++) {
				for (int row = 0; row < numberOfRowsInSection(section); row++) {
					var indexPath = NSIndexPath.FromItemSection (row, section);
					var cell = indexedCells [indexPath];
					if (cell.name == name && cell.visible) {
						cell.visible = false;
						removePaths.Add (indexPath);
						cellCount [section] = cellCount [section] - 1;
						if (cellCount [section] == 0) {
							removeSections.Add ((nuint)section);
						}
					}
				}
			}

			recalcIndexedCells ();

			if (removeSections.Count == 0) {
				tableView.DeleteRows (removePaths.ToArray (), UITableViewRowAnimation.Top);
			} else {
				tableView.DeleteSections (removeSections, UITableViewRowAnimation.Top);
			}


		}

		public void showCell(string name)
		{
			var addPaths = new List<NSIndexPath> ();
			var cellSection = 0;
			var section = 0;
			var row = 0;
			foreach (Cell c in cells) {
				if (cellSection != c.section) {
					cellSection = c.section;
					if (row > 0) {
						section++;
					}
					row = 0;
				}
				if (c.visible) {
					row++;
				} else {
					if (c.name == name) {
						var indexPath = NSIndexPath.FromItemSection (row, section);
						c.visible = true;
						addPaths.Add (indexPath);
					}
				}

			}

			var initialCount = numberOfSections ();
			recalcIndexedCells ();

			if (initialCount == numberOfSections ()) {
				tableView.InsertRows (addPaths.ToArray (), UITableViewRowAnimation.Top);
			} else {
				tableView.ReloadData ();
			}
				
		}

		public string cellNameAtIndexPath(NSIndexPath indexPath)
		{
			foreach (var path in indexedCells.Keys) {
				if (path.Section == indexPath.Section && path.Row == indexPath.Row) {
					return indexedCells [path].name;
				}
			}

			return null;
		}

		public NSIndexPath indexPathForCellNamed(string name)
		{
			foreach (var path in indexedCells.Keys) {
				var cell = indexedCells [path];
				if (cell.name == name) {
					return path;
				}
			}

			return null;
		}

		public List<UITableViewCell> visibleCellWithName(string name)
		{
			var matchingCells = new List<UITableViewCell> ();
			foreach (var cell in cells) {
				if (cell.name == name && cell.visible) {
					matchingCells.Add (cell.tableViewCell);
				}
			}

			return matchingCells;
		}

		public bool cellIsVisible(string name)
		{
			var visible = true;
			foreach (var cell in cells) {
				if (cell.name == name && !cell.visible) {
					visible = false;
					break;
				}
			}

			return visible;
		}

		public int numberOfSections()
		{
			var count = 0;
			foreach (var section in cellCount.Keys) {
				if (cellCount [section] > 0) {
					count++;
				}
			}

			return count;
		}

		public int numberOfRowsInSection(nint section)
		{
			int count;

			return cellCount.TryGetValue ((int)section, out count) ? count : 0;
		}

		public UITableViewCell cellForRowAtIndexPath(NSIndexPath indexPath)
		{
			Cell cell = null;

			foreach (var path in indexedCells.Keys) {
				if (path.Section == indexPath.Section && path.Row == indexPath.Row) {
					cell = indexedCells [path];
					break;
				}
			}

			return (cell != null) ? cell.tableViewCell : null;
		}

		public void recalcIndexedCells(){
			
			var index 		= 0;
			var section 	= 0;
			var cellSection = 0;
			indexedCells 	= new Dictionary<NSIndexPath, Cell> ();
			cellCount 		= new Dictionary<int, int> ();

			foreach (var cell in cells) {
				if (cell.section != cellSection) {
					if (index > 0) {
						cellCount [section] = index;
						section++;
					}
					cellSection = cell.section;
					index = 0;
				}

				if (cell.visible) {
					var indexPath = NSIndexPath.FromItemSection (index, section);
					indexedCells [indexPath] = cell;
					index++;
				}
			}

			cellCount [section] = index;
		}



		
	}


}



