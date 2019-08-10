using System;
using System.Linq;
using System.Text;

namespace BigInteger
{
	public partial struct BigInteger
	{
		// 类型转换
		// 大整数→T
		// 显示真·正负号，正号显示可自选
		public static SByte 转SByte(BigInteger 源数_输入) => ToSByte(转Int64(源数_输入));
		public static Byte 转Byte(BigInteger 源数_输入) => ToByte(转UInt64(源数_输入));
		public static Int16 转Int16(BigInteger 源数_输入) => ToInt16(转Int64(源数_输入));
		public static UInt16 转UInt16(BigInteger 源数_输入) => ToUInt16(转UInt64(源数_输入));
		public static Int32 转Int32(BigInteger 源数_输入) => ToInt32(转Int64(源数_输入));
		public static UInt32 转UInt32(BigInteger 源数_输入) => ToUInt32(转UInt64(源数_输入));
		public static Int64 转Int64(BigInteger 源数_输入)
		{
			Int64 的_输出 = default;
			Int64 长度 = ToInt64(源数_输入.长度);
			UInt64[] 容器 = new UInt64[长度];

			Array.Copy(源数_输入.数值组, 容器, 长度);

			if(源数_输入.Is负数)
			{
				容器 = 求2的补码_核心(容器);
			}
			else
			{
				// 占位
			}

			if(源数_输入 > Int64.MaxValue)
			{
				throw new OverflowException();
			}
			else if(源数_输入 < Int64.MinValue)
			{
				throw new OverflowException();
			}
			else
			{
				的_输出 = ToInt64(容器[default]);
			}

			return 的_输出;
		}
		public static UInt64 转UInt64(BigInteger 源数_输入)
		{
			UInt64 的_输出 = default;
			Int64 长度 = ToInt64(源数_输入.长度);
			UInt64[] 容器 = new UInt64[长度];

			Array.Copy(源数_输入.数值组, 容器, 长度);

			if(源数_输入.Is负数)
			{
				容器 = 求2的补码_核心(容器);
			}
			else
			{
				// 占位
			}

			if(源数_输入 > UInt64.MaxValue)
			{
				throw new OverflowException();		// ！BigInteger将之视为Double.PositiveInfinity
			}
			else if(源数_输入 < 0)
			{
				throw new OverflowException();		// ！BigInteger将之视为Double.NegativeInfinity
			}
			else
			{
				的_输出 = 容器[default];
			}

			return 的_输出;
		}
		//
		// 兼有Single版
		public static Double 转Double(BigInteger 源数_输入)
		{
			Double 的_输出 = default;

			if(源数_输入.Is未定义)
			{
				的_输出 = Double.NaN;
			}
			else if(源数_输入.Is无穷)
			{
				if(源数_输入.Is正数)
				{
					的_输出 = Double.PositiveInfinity;
				}
				else if(源数_输入.Is负数)
				{
					的_输出 = Double.NegativeInfinity;
				}
				else
				{
					// 占位
				}
			}
			else
			{
				if(源数_输入 > (BigInteger)Double.MaxValue)
				{
					throw new OverflowException();
				}
				else if(源数_输入 < (BigInteger)Double.MinValue)
				{
					throw new OverflowException();
				}
				else
				{
					的_输出 = Double.Parse(源数_输入.转字符串());
				}
			}

			return 的_输出;
		}
		public static Decimal 转Decimal(BigInteger 源数_输入) => Decimal.Parse(源数_输入.转字符串());
		unsafe public T[] 转数组<T>(Boolean Is右向_输入 = default, Boolean Is补码_输入 = true) where T : unmanaged => 转数组<T>(this, Is右向_输入, Is补码_输入);
		// 兼有：8（SByte、Byte）、16（Int16、UInt16、部分Char）、32（Int32、UInt32）、64（Int64、UInt64）位的数组
		unsafe private static T[] 转数组<T>(BigInteger 源数_输入, Boolean Is右向_输入 = default, Boolean Is补码_输入 = true) where T : unmanaged
		{
			// 异常值处理
			if(源数_输入.Is未定义)
			{
				throw new OverflowException();
			}
			else
			{
				// 占位
			}
			//
			if(源数_输入.Is无穷)
			{
				return default;
			}
			else
			{
				// 占位
			}

			Int64 长度 = 向后(ToInt64(源数_输入.长度));		// 增加得这1Cell巧妙解决负数原码最高位有值而无空位写符号导致补码首位为〇与正数混淆的问题
			Int64 转换粒度 = sizeof(UInt64) / sizeof(T);
			UInt64[] 容器 = new UInt64[长度];
			T[] 容器组 = default;
			T[] 的_输出 = default;
			Boolean Is首位有值 = default;
			Int64 最高有效位 = default;
			Int64 最高有效字节 = default;
			Int32 的长度 = default;
			Int64 高位〇值数 = default;

			Array.Copy(源数_输入.数值组, 容器, ToInt64(源数_输入.长度));

			高位〇值数 = 高位始空白〇值计数(容器[^ToInt32(向后(一索引化(default)))]);		// 向后()为了获得Last真·数值Cell
			高位〇值数= ToInt64(Flooring倍数化(ToUInt64(高位〇值数), 字节位数));
			最高有效位 = 四字位数 - 高位〇值数;
			最高有效字节 = 最高有效位 / 字节位数;

			if(Is补码_输入 == true)
			{
				if(源数_输入.Is负数)		// 负数
				{
					容器 = 求2的补码_核心(容器);		// 是负数，执行操作
				}
				else		// 正数、〇
				{
					// 占位
				}
			}
			else		// 正数、〇、原码的负数
			{
				// 占位
			}

			Is首位有值 = ((容器[^ToInt32(向后(一索引化(default)))] << ToInt32(四字位数 - 最高有效位)) & 0X_8000_0000_0000_0000) != default;
			//
			if(源数_输入.Is负数 & (Is首位有值 == false))
			{
				最高有效字节++;
			}
			else if(源数_输入.Is正数 & Is首位有值)
			{
				最高有效字节++;
			}
			else		// 不需要额外Cell充当符号的“正常”|其他情况
			{
				if(源数_输入.Is〇)
				{
					最高有效字节++;		// 理论上等同于：最高有效位 = 1
				}
				else
				{
					// 占位
				}
			}

			容器组 = 转换数组<T, UInt64>(容器, false);
			的长度 = ToInt32(向前(向前(容器组.Length / 转换粒度)) * 字节位数 + 最高有效字节);
			的_输出 = new T[的长度];
			Buffer.BlockCopy(容器组, default, 的_输出, default, 的长度);

			if(Is右向_输入)
			{
				Array.Reverse(的_输出);
			}
			else
			{
				// 占位
			}

			return 的_输出;
		}
		//
		public static Char 转Char(BigInteger 源数_输入) => ToChar(转UInt64(源数_输入));

		// T→大整数
		private static Char[] 转字符组(BigInteger 源数_输入) => 转字符串(源数_输入).ToCharArray();
		public String 转字符串(Boolean Is终端表示_输入 = default, Boolean Is右向_输入 = default, 进制 进制_输入 = 进制.十进制) => 转字符串(this, Is右向_输入, Is终端表示_输入, 进制_输入);

		// 逻辑参照：BigNumber.FormatBigInteger()
		// 来源：https://github.com/dotnet/corefx/blob/master/src/System.Runtime.Numerics/src/System/Numerics/BigNumber.cs
		public static String 转字符串(BigInteger 源数_输入, Boolean Is终端表示_输入 = default, Boolean Is右向_输入 = default, 进制 进制_输入 = 进制.十进制, Boolean Is补码_输入 = default)
		{
			// 1844 6744 0737 0955 1615		FFFF FFFF FFFF FFFF			20，64，16
			// 						  42 9496 7295						FFFF FFFF			10，32，	8
			// 1844 6744 1694 1458 4320		FFFF FFFF 0000 0000		20，64，16

			// 						42 9496 7295						FFFF FFFF			10，32，	8
			// 								   6 5535								FFFF			  5，16，	4
			// 						42 9490 1760						FFFF 0000		10，32，	8

			StringBuilder 容器串 = new StringBuilder();
			String 的_输出 = default;
			Int64 长度 = ToInt64(源数_输入.长度);
			UInt64[] 容器 = new UInt64[长度];

			// 异常情况处理
			if(源数_输入.Is未定义)
			{
				// 占位
				的_输出 = $@"Undefined";		// 仅此1处
			}
			else
			{
				// 处理符号
				if(Is补码_输入)
				{
					// 占位
				}
				else
				{
					if(源数_输入.正号 == false)
					{
						容器串.Append(Is终端表示_输入 ? '−' : '-');		// 使用常用的负号用作显示：Hyphen-Minus，+002D，-
																								// 使用标准的负号用作显示：Minus Sign，+2212，−
					}
					else if(源数_输入.正号 == default)
					{
						// 占位
					}
					else		// 源数_输入.正号 == true，即：正数
					{
						// 占位
						//容器串.Append('+');
					}
				}

				if(源数_输入.Is无穷)
				{
					容器串.Append('∞');
				}
				else
				{
					Int32 的索引上限 = default;
					UInt32[] 的数组 = new UInt32[源数_输入.长度 * 2 * 10 / 9 + 2];
					UInt64 数_四字 = default;
					UInt32 数 = default;

					Array.Copy(源数_输入.数值组, 容器, 长度);
					//
					if(Is补码_输入 & 源数_输入.Is负数)
					{
						if(进制_输入 == 进制.十六进制)
						{
							容器 = 求2的补码_核心(容器);

							if((容器[^ToInt32(一索引化(default))] & 0X_8000_0000_0000_0000) == default)
							{
								UInt64[] 转换容器 = new UInt64[向后(长度)];
								Array.Copy(容器, 转换容器, 向后(长度));
								容器[^ToInt32(一索引化(default))] = UInt64.MaxValue;
							}
							else
							{
								// 占位
							}
						}
						else if(进制_输入 == 进制.二进制)
						{
							容器 = 求2的补码_核心(容器);

							if((容器[^ToInt32(一索引化(default))] & 0X_8000_0000_0000_0000) == default)
							{
								UInt64[] 转换容器 = new UInt64[向后(长度)];
								Array.Copy(容器, 转换容器, 向后(长度));
								容器[^ToInt32(一索引化(default))] = UInt64.MaxValue;
							}
							else
							{
								// 占位
							}
						}
						else		// 十进制不用
						{
							// 占位
						}
					}
					else
					{
						// 占位
					}

					// 虽然if()套在里面每次循环都要判定看起来消耗大，但总比2层for()套在if()里面代码|消耗小
					for(Int64 索引 = 〇索引化(长度); 索引 >= 0; 索引--)
					{
						// 指定进制
						if(进制_输入 == 进制.二进制)
						{
							// 高双字
							容器串.Append(Convert.ToString(ToInt64(容器[索引] >> 双字位数), Convert.ToByte(进制_输入)));
							// 低双字
							容器串.Append(Convert.ToString(ToInt64(容器[索引]), Convert.ToByte(进制_输入)));
						}
						else if(进制_输入 == 进制.十进制)
						{
							数_四字 = 容器[索引];

							// 高双字
							数 = ToUInt32(数_四字 >> 双字位数);

							// 2019.04.10，2nd次起执行
							for(Int32 的索引 = default; 的索引 < 的索引上限; 的索引++)
							{
								UInt64 结果 = ((UInt64)的数组[的索引] << 双字位数) | 数;
								的数组[的索引] = (UInt32)(结果 % 10_0000_0000);
								数 = (UInt32)(结果 / 10_0000_0000);
							}

							if(数 != default)
							{
								的数组[的索引上限++] = 数 % 10_0000_0000;
								数 /= 10_0000_0000;

								if(数 != default)
								{
									的数组[的索引上限++] = 数;
								}
							}

							// 低双字
							数 = ToUInt32(数_四字 & 双字掩码);

							// 2019.04.10，2nd次起执行
							for(Int32 的索引 = default; 的索引 < 的索引上限; 的索引++)
							{
								UInt64 结果 = ((UInt64)的数组[的索引] << 双字位数) | 数;
								的数组[的索引] = (UInt32)(结果 % 10_0000_0000);
								数 = (UInt32)(结果 / 10_0000_0000);
							}

							if(数 != default)
							{
								的数组[的索引上限++] = 数 % 10_0000_0000;
								数 /= 10_0000_0000;

								if(数 != default)
								{
									的数组[的索引上限++] = 数;
								}
							}
						}
						else if(进制_输入 == 进制.十六进制)
						{
							// 高双字
							容器串.Append(Convert.ToString(ToInt64(容器[索引] >> 双字位数), Convert.ToByte(进制_输入)));
							// 低双字
							容器串.Append(Convert.ToString(ToInt64(容器[索引]), Convert.ToByte(进制_输入)));
						}
					}

					if(进制_输入 == 进制.十进制)
					{
						Int32 的数组非〇索引 = default;

						for(的数组非〇索引 = ToInt32(〇索引化(的数组.Length)); 的数组非〇索引 >= 0; 的数组非〇索引--)
						{
							if(的数组[的数组非〇索引] != default)
							{
								break;
							}
						}

						if(的数组非〇索引 < 0)		// 专为0作处理
						{
							的数组非〇索引 = default;
						}

						for(Int32 的索引 = 的数组非〇索引; 的索引 >= 0; 的索引--)
						{
							if
							(
								的数组非〇索引 != default
								&& 的索引 != 的数组非〇索引
							)
							{
								容器串.Append(的数组[的索引].ToString().PadLeft(9, '0'));

							}
							else
							{

								容器串.Append(的数组[的索引]);
							}

						}
					}

					的_输出 = 容器串.ToString();

					// 终处理
					if(Is右向_输入)
					{
						的_输出 = new String(的_输出.Reverse().ToArray());
					}
					else
					{
						// 占位
					}
				}
			}

			return 的_输出;
		}
	}
}