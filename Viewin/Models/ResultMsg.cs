using System;

namespace Viewin
{
	public class ResultMsg<T>
	{
		public string Result {
			get;
			set;
		}

		public T Dato {
			get;
			set;
		}
	}
}

