using UnityEngine;
using System.Collections;

namespace Codingriver
{
	public interface FastEnumerable<T>
	{
		void Enumerate (FastList<T> output);
	}
}