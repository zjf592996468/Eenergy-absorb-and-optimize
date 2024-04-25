using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eenergy_absorb_and_optimize
{
	class GlobalDATA
	{
		//单帽型
		public static int dmdata_yhhtn;
		public static double[,] dmdata_a;
		public static double[,] dmdata_b;
		public static double[,] dmdata_t1;
		public static double[,] dmdata_t2;
		public static string[] dmdata_mat1;
		public static string[] dmdata_mat2;
		public static double[,] dmdata_eng;
		public static double[,] dmdata_mass;
		public static double dmdata_time;
		//多胞型
		public static int smcdata_yhhtn;
		public static double[,] smcdata_a;
		public static double[,] smcdata_b;
		public static double[,] smcdata_t;
		public static string[] smcdata_mat;
		public static double[,] smcdata_eng;
		public static double[,] smcdata_mass;
		public static double smcdata_time;
	}
}
