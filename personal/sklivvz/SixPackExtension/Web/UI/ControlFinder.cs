using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.UI;
using SixPack.Collections.Generic;
using SixPack.Text;

namespace SixPack.Web.UI
{
	public class ControlFinder
	{
		private readonly Control root;

		public ControlFinder(Control rootControl)
		{
			root = rootControl;
		}

		public IFullList<Control> Find(string expression)
		{
			return Find(expression, TextSearchOptions.CaseInsensitive | TextSearchOptions.Partial);
		}

		public IFullList<Control> Find(string expression, TextSearchOptions searchOptions)
		{
			return Find(expression, searchOptions, true);
		}

		public IFullList<Control> Find(string expression, TextSearchOptions searchOptions, bool recursive)
		{
			bool caseInsensitive = (searchOptions & TextSearchOptions.CaseInsensitive) != 0;

			IFullList<Control> result = new FullList<Control>();

			if ((searchOptions & TextSearchOptions.Regex) == 0)
			{
				bool partial = (searchOptions & TextSearchOptions.Partial) != 0;
				// text
				if (caseInsensitive)
				{
					// case insensitive text
					expression = expression.ToUpperInvariant();

					if (partial)
					{
						// case insensitive partial text
						doFind(root, result, delegate(Control c)
						                     	{
						                     		string test = c.ID;
						                     		if (string.IsNullOrEmpty(test))
						                     			return false;
						                     		test = test.ToUpperInvariant();
						                     		return (test.Contains(expression));
						                     	}, recursive);
					}
					else
					{
						// case insensitive exact text
						doFind(root, result,
						       delegate(Control c) { return (string.Compare(c.ID, expression, StringComparison.OrdinalIgnoreCase) == 0); },
						       recursive);
					}
				}
				else
				{
					// case sensitive text
					if (partial)
					{
						// case sensitive partial text
						doFind(root, result, delegate(Control c) { return (c.ID.Contains(expression)); }, recursive);
					}
					else
					{
						// case sensitive exact text
						doFind(root, result,
						       delegate(Control c) { return (string.Compare(c.ID, expression, StringComparison.Ordinal) == 0); },
						       recursive);
					}
				}
			}
			else
			{
				// regex
				// we silently ignore the partial flag
				RegexOptions opts = caseInsensitive ? RegexOptions.IgnoreCase : RegexOptions.None;
				doFind(root, result, delegate(Control c) { return (Regex.IsMatch(c.ID, expression, opts)); }, recursive);
			}

			return result;
		}

		private static void doFind(Control target, ICollection<Control> result, ControlSieve matches, bool recursive)
		{
			if (target == null || !target.HasControls())
				return;
			foreach (Control c in target.Controls)
			{
				if (matches(c))
				{
					result.Add(c);
				}
				if (recursive)
				{
					doFind(c, result, matches, true);
				}
			}
		}
	}

	internal delegate bool ControlSieve(Control target);
}