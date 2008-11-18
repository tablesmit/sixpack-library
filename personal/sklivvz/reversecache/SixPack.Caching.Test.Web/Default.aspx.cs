// Default.aspx.cs
//
//  Copyright (C) 2008 Fullsix Marketing Interactivo LDA
//  Author: Marco Cecconi <marco.cecconi@gmail.com>
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA 
//
//

using System;
using System.Web;
using System.Web.UI;

using SixPack.Caching;

namespace ReverseCachingTest
{
	
	
	public partial class Default : System.Web.UI.Page
	{
		public class TestClass2
		{
			public int? TestMethod2(int cacheTest)
			{
				//			return DateTime.Now;
				return PrefetchCache.Get(delegate() {
					System.Threading.Thread.Sleep(500);
					return cacheTest;
				},
				1,
				PrefetchCacheOptions.None,
				cacheTest);
			}
		}
		
		public virtual void button1Clicked(object sender, EventArgs args)
		{
			button1.Text = "You clicked me";
			for (int j=0; j<3; j++)
			{
				TestClass2 tc = new TestClass2();
				int? res = tc.TestMethod2(j);
				if (res.HasValue)
					Response.Write(string.Format("{1} - {0}<br>",res.Value,j));
				else
					Response.Write(string.Format("{0} - NULL<br>",j));
			}
		}
	}
}
