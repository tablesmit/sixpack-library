// Main.cs
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
using System.Threading;
using SixPack.Diagnostics;

namespace SixPack.Caching.Sample
{
	internal class MainClass
	{
		public static void Main(string[] args)
		{
			TestClass2 idleTest = new TestClass2();
			int? ares = idleTest.TestMethod2(4);
			if (ares.HasValue)
				Console.WriteLine("{1} - {0}", ares.Value, 4);
			else
				Console.WriteLine("{0} - NULL", 4);
			for (int i = 0; i < 10; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					TestClass2 tc = new TestClass2();
					int? res = tc.TestMethod2(j);
					if (res.HasValue)
						Console.WriteLine("{1} - {0}", res.Value, j);
					else
						Console.WriteLine("{0} - NULL", j);
				}
				Thread.Sleep(333);
			}
			Thread.Sleep(60000);
		}
	}

	public class TestClass2
	{
		public int? TestMethod2(int cacheTest)
		{
			//			return DateTime.Now;
			return PrefetchCache.Get<int>(delegate
			                              	{
			                              		Log.Instance.AddFormat("Inside TestMethod (cacheTest: {0})", cacheTest);
			                              		Thread.Sleep(500);
			                              		return cacheTest;
			                              	}, 1, PrefetchCacheOptions.None, cacheTest);
		}
	}
}