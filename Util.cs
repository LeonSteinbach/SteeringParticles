using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steering_Text
{
	class Util
	{
		private static Random rnd = new Random();

		public static int GetRandom(int min, int max)
		{
			return rnd.Next(min, max);
		}

		public static float map(float originalStart, float originalEnd, float newStart, float newEnd, float value)
		{
			double scale = (double)(newEnd - newStart) / (originalEnd - originalStart);
			return (float)(newStart + ((value - originalStart) * scale));
		}
	}
}
