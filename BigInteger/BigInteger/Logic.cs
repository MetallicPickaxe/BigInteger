using System;

namespace BigInteger
{
	public partial struct BigInteger
	{
		#region 逻辑运算
		// 逻辑运算
		#region 表达式逻辑运算
		// 表达式逻辑运算
		public static Boolean 相等于(BigInteger 源数_左_输入, BigInteger 源数_右_输入)
		{
			Boolean 结果 = default;

			if(源数_左_输入.正号 == 源数_右_输入.正号)
			{
				if(源数_左_输入.长度 == 源数_右_输入.长度)
				{
					if(相等_核心(源数_左_输入.数值组, 源数_右_输入.数值组))
					{
						结果 = true;
					}
					else
					{
						// 占位
					}
				}
				else
				{
					// 占位
				}
			}
			else
			{
				// 占位
			}

			return 结果;
		}
		// 相等的wrap
		public static Boolean 不等于(BigInteger 源数_左_输入, BigInteger 源数_右_输入) => !相等于(源数_左_输入, 源数_右_输入);
		public static Boolean 大于(BigInteger 源数_左_输入, BigInteger 源数_右_输入)
		{
			if(源数_左_输入.Is未定义 | 源数_右_输入.Is未定义)
			{
				throw new Exception("Syntax Error");
			}
			else
			{
				// 占位
			}

			if(源数_左_输入.Is无穷 & 源数_右_输入.Is无穷)
			{
				if(源数_左_输入.正号 == 源数_右_输入.正号)
				{
					return false;
				}
				else		// 异号的情况
				{
					if(源数_左_输入.Is正数)
					{
						return true;
					}
					else		// 负数的情况
					{
						return false;
					}
				}
			}
			else if(源数_左_输入.Is无穷)
			{
				if(源数_左_输入.Is正数)
				{
					return true;
				}
				else		// 负数的情况
				{
					return false;
				}
			}
			else if(源数_右_输入.Is无穷)
			{
				if(源数_右_输入.Is正数)
				{
					return false;
				}
				else		// 负数的情况
				{
					return true;
				}
			}
			else		// 正常的情况
			{
				// 占位
			}

			// 不包括相等

			Boolean 结果 = default;
			UInt64 长度 = ToUInt64(Max(ToInt64(源数_左_输入.长度), ToInt64(源数_右_输入.长度)));

			// 比较符号
			if(源数_左_输入.正号 == 源数_右_输入.正号)
			{
				if(源数_左_输入.正号 == true)
				{
					结果 = 比_大_核心(源数_左_输入, 源数_右_输入, 源数_左_输入.正号 == false);
				}
				else if(源数_左_输入.正号 == false)		// 负数
				{
					结果 = 比_大_核心(源数_左_输入, 源数_右_输入, 源数_左_输入.正号 == false);
				}
				else		// 符号为null，即为〇
				{
					结果 = false;		// 值相同，自然不会大于
				}
			}
			else		// 异号的情况
			{
				if(源数_左_输入.正号 == true)
				{
					结果 = true;
				}
				else if(源数_左_输入.正号 == false)		// 左负右正的情况
				{
					结果 = false;
				}
				else		// 符号为null，即为〇
				{
					if(源数_右_输入.正号 == true)
					{
						结果 = false;		// 〇 ＜ 正数
					}
					else if(源数_右_输入.正号 == false)
					{
						结果 = true;		// 〇 > 负数
					}
					else		// 符号为null，即为〇
					{
						结果 = false;		// 值相同，自然不会大于
					}
				}
			}

			return 结果;
		}
		// 大于的wrap
		public static Boolean 小于(BigInteger 源数_左_输入, BigInteger 源数_右_输入) => 大于(源数_右_输入, 源数_左_输入);
		// 先实现分部的，再实现整体的，便于活用；但实际上整体的思路是更“传承”的，即>是一种精简版的≥
		// 小于的wrap，逻辑上是大于() + 相等于()
		// 相当于≮，是一种取巧的思路
		// 另一种实现思路是“≥” = “>” + “=”
		public static Boolean 小于等于(BigInteger 源数_左_输入, BigInteger 源数_右_输入) => !大于(源数_左_输入, 源数_右_输入);
		// 大于的wrap，逻辑上是小于() + 相等于()
		// 相当于≯，是一种取巧的思路
		// 另一种实现思路是“≤” = “<” + “=”
		public static Boolean 大于等于(BigInteger 源数_左_输入, BigInteger 源数_右_输入) => !小于(源数_左_输入, 源数_右_输入);

		private static Boolean 比_大_左对齐(UInt64[] 比较数_左_输入, UInt64[] 比较数_右_输入)
		{
			Boolean 的_输出 = default;

			if(比较数_左_输入.Length != 比较数_右_输入.Length)		// 一般来说是左Dividend、右Divisor，故左.长度 ≥ 右.长度，故下面仅处理右
			{
				比较数_右_输入 = 等长化_核心(比较数_右_输入, 比较数_左_输入.Length);
			}
			else
			{
				// 占位
			}

			的_输出 = 比_大_核心(左对齐_核心(比较数_左_输入), 左对齐_核心(比较数_右_输入));		// 即高位起比较
																																				// 严格大于：即＞，非≥

			if(比较数_左_输入 == 比较数_右_输入)
			{
				的_输出 = 的_输出 && true;		// 依附型添加
			}
			else
			{
				// 占位
			}

			return 的_输出;
		}
		#endregion

		#region 位逻辑运算
		// 位逻辑运算
		private static BigInteger 按位取反(BigInteger 源数_输入, Boolean IsInt逻辑_输入 = default, Boolean Is含符号_输入 = default)
		{
			BigInteger 源数_输出 = default;

			// 特殊情况处理
			if(源数_输入.Is无穷)
			{
				return 源数_输入;
			}
			else
			{
				// 占位
			}

			if(IsInt逻辑_输入)
			{
				源数_输出 = -(源数_输入 + 正一);
			}
			else		// 即：自定版逻辑
			{
				// 特殊情况处理
				if(源数_输入 == 〇)
				{
					return 源数_输入;
				}
				else
				{
					// 占位
				}

				源数_输出.正号 = Is含符号_输入 ? !源数_输入.正号 : 源数_输出.正号;

				源数_输出.数值组 = 按位取反_核心(源数_输入.数值组);
				源数_输出.数值组 = Trim(源数_输入.数值组);
			}

			return 源数_输出;
		}
		#endregion
		#endregion

		#region 逻辑运算-核心
		// 逻辑运算
		#region 表达式逻辑运算
		// 表达式逻辑运算
		// 左 ＞ 右，不含=
		private static Boolean 比_大_核心(BigInteger 源数_左_输入, BigInteger 源数_右_输入, Boolean 是负数)
		{
			Boolean 结果 = default;
			UInt64 长度 = ToUInt64(Max(ToInt64(源数_左_输入.长度), ToInt64(源数_右_输入.长度)));

			// 默认按照正数算
			// 直接按照占用的空间
			if(是负数 == true)
			{
				结果 = 比_大_核心(源数_右_输入.数值组, 源数_左_输入.数值组);
			}
			else		// 正数的情况
			{
				结果 = 比_大_核心(源数_左_输入.数值组, 源数_右_输入.数值组);
			}

			return 结果;
		}
		// 左 ＞ 右，不含=
		private static Boolean 比_大_核心(UInt64[] 源数_左_输入, UInt64[] 源数_右_输入)
		{
			Boolean 结果 = default;
			Int32 长度差 = Trim(源数_左_输入).Length - Trim(源数_右_输入).Length;

			// 默认按照正数算
			// 直接按照占用的空间
			if(长度差 > 0)
			{
				结果 = true;
			}
			else if(长度差 < 0)
			{
				结果 = false;		// 理论上default就是false，故此语句重复，可优化
			}
			else		// 长度相等
			{
				for(Int64 索引 = 〇索引化(源数_左_输入.Length); 索引 >= 0; 索引--)		// 高位之差甚于余下，故可行
				{
					if(源数_左_输入[索引] > 源数_右_输入[索引])
					{
						结果 = true;

						break;
					}
					else if(源数_左_输入[索引] < 源数_右_输入[索引])
					{
						结果 = false;

						break;
					}
					else		// 相等的情况，继续比较下1个双字
					{
						// 占位
						//continue;
					}
				}
			}

			return 结果;
		}
		private static UInt64[] 同或_核心(UInt64[] 源数_左_输入, UInt64[] 源数_右_输入)
		{
			Int64 长度 = Max(源数_左_输入.Length, 源数_右_输入.Length);
			UInt64[] 的_输出 = new UInt64[长度];

			for(Int32 索引 = default; 索引 <= ToInt32(〇索引化(长度)); 索引++)
			{
				的_输出[索引] = ~(源数_左_输入[索引] ^ 源数_右_输入[索引]);
			}

			return 的_输出;
		}
		#endregion

		#region 位逻辑运算
		// 位逻辑运算
		private static UInt64[] 按位取反_核心(UInt64[] 数值组_输入)
		{
			Int64 长度 = 数值组_输入.LongLength;
			UInt64[] 数值组_输出 = new UInt64[长度];

			for(Int32 索引=default; 索引 <= ToInt32(〇索引化(长度)); 索引++)		// ！Int64 → Int32
			{
				数值组_输出[索引] = ~数值组_输入[索引];
			}

			return 数值组_输出;
		}
		#endregion
		#endregion
	}
}