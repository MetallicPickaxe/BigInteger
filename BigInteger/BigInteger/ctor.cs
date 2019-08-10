using System;
using System.Linq;

namespace BigInteger
{
	public partial struct BigInteger
	{
		#region .Ctor | Cast
		// T→大整数
		private static BigInteger 初始化(SByte 源数_输入)
		{
			// 定义
			Boolean? 正号_处理 = default;
			UInt64[] 数值组_处理 = default;

			// 赋值
			// 符号
			if(源数_输入 != default)
			{
				正号_处理 = 源数_输入 < 0 ? false : true;
			}
			else		// 即视为初始值，即null
			{
				// 占位
				//正号_处理 = default;
			}
			//
			// 数值
			数值组_处理 = new UInt64[] { 求绝对值(源数_输入) };

			return 初始化_核心(正号_处理, 数值组_处理);
		}
		private static BigInteger 初始化(Byte 源数_输入)
		{
			// 定义
			Boolean? 正号_处理 = default;
			UInt64[] 数值组_处理 = default;

			// 赋值
			// 符号
			if(源数_输入 != default)		// 一定是正数
			{
				正号_处理 = true;
			}
			else		// 即视为初始值，即：null
			{
				// 占位
				//正号_处理 = default;
			}
			//
			// 数值
			数值组_处理 = new UInt64[] { 源数_输入 };		// 省去了求绝对值的操作，相较于有符号数

			return 初始化_核心(正号_处理, 数值组_处理);
		}
		private static BigInteger 初始化(Int16 源数_输入)
		{
			// 定义
			Boolean? 正号_处理 = default;
			UInt64[] 数值组_处理 = default;

			// 赋值
			// 符号
			if(源数_输入 != default)
			{
				正号_处理 = 源数_输入 < 0 ? false : true;
			}
			else		// 即视为初始值，即null
			{
				// 占位
				//正号_处理 = default;
			}
			//
			// 数值
			数值组_处理 = new UInt64[] { 求绝对值(源数_输入) };

			return 初始化_核心(正号_处理, 数值组_处理);
		}
		private static BigInteger 初始化(UInt16 源数_输入)
		{
			// 定义
			Boolean? 正号_处理 = default;
			UInt64[] 数值组_处理 = default;

			// 赋值
			// 符号
			if(源数_输入 != default)		// 一定是正数
			{
				正号_处理 = true;
			}
			else		// 即视为初始值，即：null
			{
				// 占位
				//正号_处理 = default;
			}
			//
			// 数值
			数值组_处理 = new UInt64[] { 源数_输入 };		// 省去了求绝对值的操作，相较于有符号数

			return 初始化_核心(正号_处理, 数值组_处理);
		}
		private static BigInteger 初始化(Int32 源数_输入)
		{
			// 定义
			Boolean? 正号_处理 = default;
			UInt64[] 数值组_处理 = default;

			// 赋值
			// 符号
			if(源数_输入 != default)
			{
				正号_处理 = 源数_输入 < 0 ? false : true;
			}
			else		// 即视为初始值，即null
			{
				// 占位
				//正号_处理 = default;
			}
			//
			// 数值
			数值组_处理 = new UInt64[] { 求绝对值(源数_输入) };

			return 初始化_核心(正号_处理, 数值组_处理);
		}
		private static BigInteger 初始化(UInt32 源数_输入)
		{
			// 定义
			Boolean? 正号_处理 = default;
			UInt64[] 数值组_处理 = default;

			// 赋值
			// 符号
			if(源数_输入 != default)		// 一定是正数
			{
				正号_处理 = true;
			}
			else		// 即视为初始值，即：null
			{
				// 占位
				//正号_处理 = default;
			}
			//
			// 数值
			数值组_处理 = new UInt64[] { 源数_输入 };		// 省去了求绝对值的操作，相较于有符号数

			return 初始化_核心(正号_处理, 数值组_处理);
		}
		private static BigInteger 初始化(Int64 源数_输入)
		{
			// 定义
			Boolean? 正号_处理 = default;
			UInt64[] 数值组_处理 = default;

			// 赋值
			// 符号
			if(源数_输入 != default)
			{
				正号_处理 = 源数_输入 < 0 ? false : true;
			}
			else		// 即视为初始值，即null
			{
				// 占位
				//正号_处理 = default;
			}
			//
			// 数值
			数值组_处理 = new UInt64[] { 求绝对值(源数_输入) };

			return 初始化_核心(正号_处理, 数值组_处理);
		}
		private static BigInteger 初始化(UInt64 源数_输入)
		{
			// 定义
			Boolean? 正号_处理 = default;
			UInt64[] 数值组_处理 = default;

			// 赋值
			// 符号
			if(源数_输入 != default)		// 一定是正数
			{
				正号_处理 = true;
			}
			else		// 即视为初始值，即：null
			{
				// 占位
				//正号_处理 = default;
			}
			//
			// 数值
			数值组_处理 = new UInt64[] { 源数_输入 };		// 省去了求绝对值的操作，相较于有符号数

			return 初始化_核心(正号_处理, 数值组_处理);
		}
		// 兼有Single版
		public static BigInteger 初始化(Double 源数_输入)
		{
			BigInteger 的_输出 = default;
			(Int32 符号, UInt64 底数, Int32 指数) 容器 = default;

			if(Double.IsNaN(源数_输入))
			{
				的_输出 = 未定义;
			}
			else if(Double.IsInfinity(源数_输入))
			{
				if(Double.IsPositiveInfinity(源数_输入))
				{
					的_输出 = 正无穷;
				}
				else if(Double.IsNegativeInfinity(源数_输入))
				{
					的_输出 = 负无穷;
				}
			}
			else		// 正常的情况
			{
				容器 = GetDoublePart(Math.Truncate(源数_输入));		// Math.Floor()涵盖了2个操作：值切换为“应表值”（对应实际值，应表值是准确的）→向下取整（最终数值的小数部分归〇）
				的_输出 = 初始化_核心_Double版(容器);

				if(Is0(的_输出.数值组))		// 理论上数值组一定有值
				{
					的_输出 = 〇;
				}
				else
				{
					// 占位
				}
			}

			return 的_输出;
		}
		public static BigInteger 初始化(Decimal 源数_输入)
		{
			BigInteger 的_输出 = default;

			if(Decimal.Zero == 源数_输入)
			{
				的_输出 = 〇;
			}
			else
			{
				(Int32 符号, (UInt64 高双字, UInt64 低四字) 底数, Int32 指数) 容器 = GetDecimalPart(Decimal.Truncate(源数_输入));		// Decimal.Truncate()仅具有1个操作：向下取整（最终数值的小数部分归〇），？因Decimal精确表示其数值
				的_输出 = 初始化_核心_Decimal版(容器);
			}

			return 的_输出;
		}

		// 兼有：8（SByte、Byte）、16（Int16、UInt16、部分Char）、32（Int32、UInt32）、64（Int64、UInt64）位的数组
		unsafe private static BigInteger 初始化<T>(T[] 源数_输入, Boolean Is右向_输入 = default, 符号 符号_输入 = 符号.默认, Boolean Is补码_输入 = true) where T : unmanaged => 初始化(转换数组<UInt64, T>(源数_输入), Is右向_输入, 符号_输入, Is补码_输入);
		private static BigInteger 初始化(UInt64[] 源数_输入, Boolean Is右向_输入 = default, 符号 符号_输入 = 符号.默认, Boolean Is补码_输入 = true)
		{
			// 异常情况
			if(IsNullOrEmpty(源数_输入))
			{
				throw new ArgumentNullException();
			}
			else
			{
				// 占位
			}

			// 预处理
			if(Is右向_输入)
			{
				Array.Reverse(源数_输入);
			}
			else
			{
				// 占位
			}


			Boolean? 正号_处理 = Is补码_输入 ? default : 转换符号(符号_输入);
			Boolean Is负数 = default;

			if(Is补码_输入 == true)
			{
				Is负数 = 获取符号(源数_输入);		// 也可以使用移位的版本：(源数_输入[^ToInt32(一索引化(default))] >> 向前(四字位数)) != default

				if(Is负数)		// 负数
				{
					源数_输入 = 求2的补码_核心(源数_输入);		// 是负数，执行操作

					正号_处理 = false;
				}
				else		// 正数、〇
				{
					if(Is0_纯粹版(源数_输入))		// 〇的情况
					{
						// 占位
					}
					else		// 正数的情况
					{
						正号_处理 = true;
					}
				}
			}
			else		// 正数、〇、原码的负数
			{
				if(Is0(源数_输入))		// 〇的情况
				{
					if(正号_处理 != default)
					{
						正号_处理 = default;		// 即以数值为准
					}
					else
					{
						// 占位
					}
				}
				else		// 非〇的情况
				{
					if(正号_处理 == default)
					{
						throw new Exception("Pattern Error");
					}
					else
					{
						// 占位
					}
				}
			}

			return 初始化_核心(正号_处理, 源数_输入);
		}
		//
		private static BigInteger 初始化(Char 源数_输入, Boolean Is字符_输入 = true)
		{
			UInt64 容器 = default;

			if(Is字符_输入)
			{
				容器 = ToUInt64(源数_输入);
			}
			else
			{
				容器 = 源数_输入;
			}

			return 初始化(容器);
		}
		//
		private static BigInteger 初始化(Char[] 源数_输入, Boolean Is右向_输入 = default, 进制 进制_输入 = 进制.十进制, 分隔符 分隔符_输入 = 分隔符.空格, 符号 符号_输入 = 符号.默认, Boolean Is补码_输入 = default) => 初始化(new String(源数_输入), Is右向_输入, 进制_输入, 分隔符_输入, 符号_输入, Is补码_输入);
		//
		// Is右向即Is大端序
		private static BigInteger 初始化(String 源数_输入, Boolean Is右向_输入 = default, 进制 进制_输入 = 进制.十进制, 分隔符 分隔符_输入 = 分隔符.空格, 符号 符号_输入 = 符号.默认, Boolean Is补码_输入 = default)
		{
			// 异常情况处理
			// 空串、空引用处理
			if(IsNullOrEmpty(源数_输入))
			{
				throw new NullReferenceException();
			}
			else
			{
				// 占位
			}

			Boolean Is含符号 = default;
			Boolean? 正号_处理 = default;
			Boolean Is未定义 = default;
			String 首字符 = 源数_输入[default].ToString();
			Boolean 无穷_处理 = default;
			Char[] 源数 = default;
			UInt64[] 运算器 = default;
			UInt64 因数 = default;
			UInt64[] 数值组_处理 = default;

			// 预处理
			源数_输入 = 源数_输入.ToUpperInvariant();		// 字母写法标准化
			源数_输入 = 去除分隔符(源数_输入, 分隔符_输入);
			//
			//
			// 以左向为处理核心
			if(Is右向_输入)
			{
				源数_输入 = new String(源数_输入.Reverse().ToArray());		// 相较出现该函数之前的先ToCharArray()再Reverse()再new String()减少了1个步骤
			}
			else
			{
				// 占位
			}

			if(源数_输入 == $@"Undefined".ToUpperInvariant())
			{
				Is未定义 = true;
			}
			else
			{
				if(首字符 == $@"+" || 首字符 == $@"−" || 首字符 == $@"-")
				{
					Is含符号 = true;
				}
				else
				{
					// 占位
				}
				//
				if(Is补码_输入)
				{
					if(进制_输入 == 进制.十六进制)
					{
						正号_处理 = ((ToInt64(首字符[default]) & 0B_1000) != default) ? false : true;
					}
					else if(进制_输入 == 进制.二进制)
					{
						进制_输入 = 进制.二进制;

						正号_处理 = (ToInt64(首字符[default]) != default) ? false : true;
					}
					else		// 十进制不存在所谓补码；四进制、八进制等不受支持
					{
						throw new Exception("Pattern Error");
					}
				}
				else if(符号_输入 != 符号.默认)
				{
					if(Is含符号)
					{
						throw new Exception("Syntax Error");
					}
					else
					{
						正号_处理 = 转换符号(符号_输入);
					}
				}
				else		// Is含符号 == true的情况，包括正号的省略情况
				{
					// 补齐符号，减少后续判断的特殊情况
					if
					(
						首字符 == $@"0" || 首字符 == $@"1" || 首字符 == $@"2" || 首字符 == $@"3" || 首字符 == $@"4" || 首字符 == $@"5" || 首字符 == $@"6" || 首字符 == $@"7" || 首字符 == $@"8" || 首字符 == $@"9"
						|| 首字符 == $@"A" || 首字符 == $@"B" || 首字符 == $@"C" || 首字符 == $@"D" || 首字符 == $@"E" || 首字符 == $@"F"
						|| 首字符 == $@"∞"
					)
					{
						源数_输入 = $@"+" + 源数_输入;
					}
					else if(首字符 == $@"-")
					{
						// 标准化负号
						源数_输入 = 源数_输入.Substring(ToInt32(一索引化(default)));
						源数_输入 = $@"−" + 源数_输入;
					}
					else
					{
						// 占位
					}

					// 符号逻辑化
					首字符 = 源数_输入[default].ToString();
					//
					正号_处理 = 转换符号(首字符);
					// 去除符号字符
					源数_输入 = 源数_输入.Substring(ToInt32(一索引化(default)));
				}

				// 处理无穷值
				首字符 = 源数_输入[default].ToString();
				//
				if(源数_输入 ==$@"∞")
				{
					无穷_处理 = true;
				}
				else		// 正常数值情况
				{
					数值组_处理 = 转换数值(源数_输入, 进制_输入);
				}
			}

			if(Is补码_输入 & (无穷_处理 == false))		// 含补码、不是无穷的情况
			{
				数值组_处理 = 求2的补码_核心(数值组_处理);
			}
			else
			{
				// 占位
			}

			// 终处理
			// 将前面各情况下的〇值得错误符号还原
			if(Is0(数值组_处理) & (无穷_处理 == false) & (Is未定义 == false))
			{
				正号_处理 = default;
			}
			else
			{
				// 占位
			}

			return 初始化_核心(正号_处理, 数值组_处理, 无穷_处理, Is未定义);
		}
		#endregion

		#region 构造器|类型转换
		//  构造器|类型转换
		// 大整数→T

		// T→大整数
		// Decimal用
		private static BigInteger 初始化_核心_Decimal版((Int32 符号, (UInt64 高双字, UInt64 低四字) 底数, Int32 指数) 源_输入)
		{
			BigInteger 的_输出 = default;
			UInt64[] 底数 = new UInt64[]{ 源_输入.底数.低四字, 源_输入.底数.高双字 };

			(的_输出, _) = 除以(new BigInteger(转换符号(源_输入.符号), 底数), 求幂(10, 源_输入.指数));

			return 的_输出;
		}
		// Double用
		private static BigInteger 初始化_核心_Double版((Int32 符号, UInt64 底数, Int32 指数) 源_输入)
		{
			BigInteger 的_输出 = default;
			UInt64[] 底数 = new UInt64[]{ 源_输入.底数 };
			Boolean Is负数 = 源_输入.符号 < 0 ? true : false;

			的_输出.正号 = !Is负数;
			的_输出.数值组 = 左移位_核心(底数, 源_输入.指数);		// ！浅复制

			return 的_输出;
		}
		// 真·构造器的一般使用数组版，便于多处调用
		private static BigInteger 初始化_核心(Boolean? 正号_输入, UInt64[] 数值组_输入, Boolean 无穷_输入 = default, Boolean 未定义_输入 = default)
		{
			BigInteger 的_输出 = default;
			UInt64 长度_处理 = default;

			// 未定义标识
			if(未定义_输入 == true)
			{
				return 的_输出;
			}
			else
			{
				// 正号标识赋值
				的_输出.正号 = 正号_输入;

				// 无穷标识
				的_输出.无穷 = 无穷_输入;
				//
				if(无穷_输入 == true)
				{
					return 的_输出;
				}
				else
				{
					// 设置数值
					// 预处理
					数值组_输入=Trim(数值组_输入);

					if(Is0_纯粹版(数值组_输入))
					{
						的_输出.正号 = default;
						的_输出.数值组 = new UInt64[] { default };
					}
					else
					{
						长度_处理 = 求非〇值Cell长(数值组_输入);

						的_输出.数值组 = new UInt64[长度_处理];
						Array.Copy(数值组_输入, 的_输出.数值组, ToInt64(长度_处理));
					}
				}
			}

			return 的_输出;
		}
		#endregion
	}
}