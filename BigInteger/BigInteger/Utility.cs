using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace BigInteger
{
	public partial struct BigInteger
	{
		#region 自制函数
		unsafe public static T[] Trim<T>(T[] 数值组_输入) where T : unmanaged
		{
			Int64 长度 = default;

			长度 = ToInt64(求非〇值Cell长(数值组_输入));

			T[] 数_输出 = new T[长度];
			Array.Copy(数值组_输入, 数_输出, 长度);

			return 数_输出;
		}
		private static Int64 高位连续一值计数(BigInteger 幂_输入)
		{
			Int64 的_输出 = ToInt64(幂_输入.数位数);		// 针对全一值的情况

			for(Int64 一值计数索引 = ToInt64(幂_输入.数位数); 一值计数索引 >= 一索引化(default); 一值计数索引--)
			{
				if(幂_输入.IndexOf(一值计数索引) == default)
				{
					的_输出 -= 一值计数索引;		// 实为：幂_输入.数位数 − 一值计数索引之省略

					break;
				}
				else
				{
					// 占位
				}
			}

			return 的_输出;
		}
		private static UInt64[] 转换数值(String 源数_输入, 进制 进制_输入)
		{
			Char[] 源数 = 源数_输入.ToCharArray();		// 索引：小→大，数位：高→低，即大端序
			UInt64[] 运算器 = new UInt64[]{ default };
			UInt64 因数 = default;
			UInt64[] 的_输出 = default;

			for(Int64 索引 = default; 索引 <= 〇索引化(源数.LongLength); 索引++)
			{
				// 预处理
				因数 = ToUInt64(源数[索引]);

				if(进制_输入 == 进制.十进制)
				{
					// 相当于十进制下当前数位左移1位
					运算器 = 乘_核心(运算器, 10);		// 首次操作与〇相乘，相当于2nd次开始操作
				}
				else if(进制_输入 == 进制.二进制)
				{
					// 相当于二进制下当前数位左移1位
					运算器 = 左移位_核心(运算器, 1);		// 首次操作与〇相乘，相当于2nd次开始操作
																			// 也可写作：乘_核心(运算器, 2)
				}
				else if(进制_输入 == 进制.十六进制)
				{
					// 相当于十进制下当前数位左移1位
					运算器 = 左移位_核心(运算器, 4);		// 首次操作与〇相乘，相当于2nd次开始操作
																			// 也可写作：乘_核心(运算器, 16)
				}
				else		// 理论上不存在
				{
					// 占位
				}

				运算器 = 加_核心(运算器, 因数);

				// 终处理
				运算器 = Trim(运算器);
			}

			的_输出 = Trim(运算器);

			return 的_输出;
		}
		private static Boolean? 转换符号(String 源_输入)
		{
			Boolean? 的_输出 = default;

			if(源_输入 == $@"+")		// 此处包含了〇，因其表征与正数一致
			{
				的_输出 = true;
			}
			else if(源_输入 == $@"−")
			{
				的_输出 = false;
			}
			else		// 错误的情况
			{
				throw new Exception("Syntax Error");
			}

			return 的_输出;
		}
		private static 符号 转换符号(Int32 源_输入)
		{
			符号 的_输出 = 符号.默认;

			if(源_输入 > 0)		// 此处包含了〇，因其表征与正数一致
			{
				的_输出 = 符号.正;
			}
			else if(源_输入 < 0)
			{
				的_输出 = 符号.负;
			}
			else if(源_输入 == default)
			{
				的_输出 = 符号.〇;
			}
			else		// 错误的情况
			{
				throw new Exception("Syntax Error");
			}

			return 的_输出;
		}
		private static Boolean? 转换符号(符号 符号_输入)
		{
			Boolean? 的_输出 = default;

			if(符号_输入 == 符号.正)
			{
				的_输出 = true;
			}
			else if(符号_输入 == 符号.负)
			{
				的_输出 = false;
			}
			else		// 〇的情况
			{
				// 占位
				//的_输出 = default;
			}

			return 的_输出;
		}
		private static 符号 转换符号(Boolean? 符号_输入)
		{
			符号 的_输出 = default;

			if(符号_输入 == true)
			{
				的_输出 = 符号.正;
			}
			else if(符号_输入 == false)
			{
				的_输出 = 符号.负;
			}
			else		// 〇的情况
			{
				的_输出 = 符号.〇;
			}

			return 的_输出;
		}

		// 多亏了炫酷的C# 7.3！
		unsafe public static D[] 转换数组<D, S>(S[] 源_输入, Boolean Is补码_输入 = true)	where D : unmanaged
																																		where S : unmanaged
		{
			if(IsNullOrEmpty(源_输入))
			{
				throw new ArgumentNullException();
			}
			else
			{
				// 占位
			}

			Boolean Is大转小 = sizeof(S) > sizeof(D);
			Int64 转换粒度 = Is大转小 ? (sizeof(S) / sizeof(D)) : (sizeof(D) / sizeof(S));
			Int64 长度 = Is大转小 ? (源_输入.LongLength * 转换粒度) : Ceiling倍数化除以(源_输入.LongLength, 转换粒度);
			D[] 的_输出 = new D[长度];
			Boolean Is负数 = Is补码_输入 ? 获取符号(源_输入) : default;

			if(Is大转小 == false)		// 小转大
			{
				if(Is补码_输入)
				{
					if(Is负数)
					{
						Buffer.BlockCopy(new UInt64[] { 四字掩码 }, default, 的_输出, ToInt32(sizeof(D) * 向前(的_输出.LongLength)), sizeof(D));
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

			Buffer.BlockCopy(源_输入, default, 的_输出, default, 源_输入.Length * sizeof(S));

			return 的_输出;
		}
		private static UInt64[] 求原码(UInt64[] 源_输入)
		{
			Int64 长度 = 源_输入.LongLength;
			UInt64[] 的_输出 = new UInt64[长度];
			Array.Copy(源_输入, 的_输出, 长度);
			UInt64 最高四字 = 的_输出[^ToInt32(一索引化(default))];
			Int32 最高有效位 = ToInt32(Flooring倍数化(高位始空白〇值计数(最高四字), 字节位数));
			Boolean Is负数 = 获取符号(的_输出);

			if(Is负数)		// 负数
			{
				的_输出 = 求2的补码_核心(的_输出);		// 是负数，执行操作
			}
			else		// 正数、〇
			{
				// 占位
			}

			的_输出 = Trim(的_输出);

			return 的_输出;
		}
		unsafe private static Boolean 获取符号<T>(T[] 源_输入) where T : unmanaged
		{
			Byte[] 容器 = new Byte[源_输入.LongLength * sizeof(T)];
			Buffer.BlockCopy(源_输入, default, 容器, default, 源_输入.Length * sizeof(T));
			Byte 最高Cell = 容器[^ToInt32(一索引化(default))];
			Boolean 的_输出 = (最高Cell & 0B_1000_0000) != default;

			return 的_输出;
		}
		unsafe public static D[] 转换数组2<D, S>(S[] 源_输入, Boolean Is补码_输入 = true)	where D : unmanaged
																																	where S : unmanaged
		{
			Int64 源转换粒度 = sizeof(UInt64) / sizeof(S);
			Int64 的转换粒度 = sizeof(UInt64) / sizeof(D);
			Boolean Is大转小 = sizeof(S) > sizeof(D);
			UInt64[] 容器组 = new UInt64[一索引化(default)];		// 实际需要使S的最高位移位到UInt64的最高位
			//
			// 各偏移量以字节的〇索引化索引值计量
			Buffer.BlockCopy(源_输入, ToInt32(〇索引化(源_输入.LongLength)) * sizeof(S), 容器组, default, sizeof(S));
			容器组[default] = 容器组[default] << ToInt32(Flooring倍数化(高位始空白〇值计数(容器组[default]), 字节位数));
			Boolean Is负数 = (容器组[default] & 0X_8000_0000_0000_0000) != default;
			//
			Int64 长度 = Is大转小 ? (源_输入.LongLength * 的转换粒度) : Ceiling倍数化除以(源_输入.LongLength, 的转换粒度);
			//
			D[] 的_输出 = new D[长度];

			// 增加负号
			if(Is补码_输入)
			{
				if(Is大转小)
				{
					// 占位
				}
				else		// 小转大的情况
				{
					if(Is负数)
					{
						Buffer.BlockCopy(new UInt64[] { 0X_FFFF_FFFF_FFFF_FFFF }, default, 的_输出, ToInt32(sizeof(D) * 〇索引化(的_输出.LongLength)), sizeof(D));
					}
					else
					{
						// 占位
					}
				}
			}
			else
			{
				// 占位
			}

			Buffer.BlockCopy(源_输入, default, 的_输出, default, ToInt32(长度));

			return 的_输出;
		}

		private static String 去除分隔符(String 源数_输入, 分隔符 分隔符_输入)
		{
			String 的_输出 = default;

			if(分隔符_输入 ==  分隔符.无)
			{
				的_输出 = 源数_输入;
			}
			else if(分隔符_输入 == 分隔符.空格)
			{
				的_输出 = 源数_输入.Replace(" ", String.Empty);
			}
			else if(分隔符_输入 == 分隔符.下划线)
			{
				的_输出 = 源数_输入.Replace("_", String.Empty);
			}
			else if(分隔符_输入 == 分隔符.撇号)
			{
				的_输出 = 源数_输入.Replace("'", String.Empty);
			}
			else if(分隔符_输入 == 分隔符.点号)
			{
				的_输出 = 源数_输入.Replace(".", String.Empty);
			}
			else if(分隔符_输入 == 分隔符.连词符)
			{
				的_输出 = 源数_输入.Replace("-", String.Empty);
			}
			else if(分隔符_输入 == 分隔符.逗号)
			{
				的_输出 = 源数_输入.Replace(",", String.Empty);
			}
			else
			{
				// 占位
			}

			// 终处理
			的_输出 = 源数_输入.Replace("\0", String.Empty);

			return 的_输出;
		}
		[StructLayout(LayoutKind.Explicit)]
		public struct DecimalToUInt64转换体
		{
			[FieldOffset(0)]		// 模拟共用体
			public Decimal Decimal载体接口;
			[FieldOffset(0)]		// 模拟共用体
			public (UInt64 低四字, UInt64 高四字) 模拟UInt128载体接口;
		}
		public static (Int32 符号, (UInt64 高双字, UInt64 低四字) 底数, Int32 指数) GetDecimalPart(Decimal 源_输入)
		{
			// The lo, mid, hi, and flags fields contain the representation of the Decimal value.
			// The lo, mid, and hi fields contain the 96-bit integer part of the Decimal.
			// Bits 0-15 (the lower word) of the flags field are unused and must be zero;
			// bits 16-23 contain must contain a value between 0 and 28, indicating the power of 10 to divide the 96-bit integer part by to produce the Decimal value;
			// bits 24-30 are unused and must be zero;
			// and finally bit 31 indicates the sign of the Decimal value, 0 meaning positive and 1 meaning negative.

			// 定义源自IEEE 754，Decimal128
			// Decimal数据类型位数划分：
			// Double公式：
			// (-1)^S x M（十进制） x 10^E
			// Part														Bit				位数
			// Significand | Mantissa							0~95			96
			// Exponent												112~119		8
			// Sign（0 = Positive, 1 = Negative）		127				1
			// 示意结构：
			// | 128 ╎ 127 126 125 124 123 122 121 ╎ 120 119 118 117 116 115 114 113 | 112 111 110 109 108 107 106 105 104 103 102 101 100 99 98 97 | 96 95 94 93 92 91 90 89 88 87 86 85 84 83 82 81 | 80 79 78 77 76 75 74 73 72 71 70 69 68 67 66 65 | 64 63 62 61 60 59 58 57 56 55 54 53 52 51 50 49 | 48 47 46 45 44 43 42 41 40 39 38 37 36 35 34 33 | 32 31 30 29 28 27 26 25 24 23 22 21 20 19 18 17 | 16 15 14 13 12 11 10 09 08 07 06 05 04 03 02 01 | 00
			// | (X)   ╎ 					 (210)U                   ╎ TS 					                                   | 					 RQPO 					                                                           | 					 NMLK 					                                 | 					 JIHG 					                                   | 					 FEDC 					                                 | 					 BA98 					                                   | 					 7654 					                                 | 					 3210 					                                   | 00
			// | 符号 ╎ 					 (000)00                 ╎ 					 指数                               | 					 0000 					                                                           | 					 底数位-高位 					                       ╵ 											                                     ╎ 					 底数位-中位 										    ╵ 										                                      ╎ 					 底数位-低位（？64th位为小数点） 	   ╵ 										                                       | 00

			// 初始化
			(Int32 符号, (UInt64 高双字, UInt64 低四字) 底数, Int32 指数) 的_输出 = default;
			DecimalToUInt64转换体 容器;
			容器.模拟UInt128载体接口 = default;		// 共用体空间清〇
			容器.Decimal载体接口 = 源_输入;		// 共用体赋值

			// 达成{0x01, 0x00}集合到{-1, 1}集合的简单映射，即{0x01, 0x00} → {0x02, 0x00} → {(1 - 2), (1 - 0)} → {-1, 1}
			的_输出.符号 = ((Int32)(容器.模拟UInt128载体接口.低四字 >> ToInt32(向前(双字位数))) & 0B_0001) == default ? 1 : -1;

			// 获取底数位
			的_输出.底数.高双字 = 容器.模拟UInt128载体接口.低四字 >> 双字位数;		// 64位的高32位掩码
			//
			的_输出.底数.低四字 = 容器.模拟UInt128载体接口.高四字;
			//
			// 获取指数位
			的_输出.指数 = (Int32)(容器.模拟UInt128载体接口.低四字 & 0X_FF00);		// 64位的17~24这位掩码

			return 的_输出;
		}
		// 用于深复制
		public void 赋值(ref BigInteger 源数_右_输入) => 赋值(this, ref 源数_右_输入);
		// 左源右的
		public static void 赋值(BigInteger 源数_左_输入, ref BigInteger 源数_右_输入)
		{
			Int64 长度 = 源数_左_输入.数值组.LongLength;
			源数_右_输入.数值组 = new UInt64[长度];

			源数_右_输入.无穷 = 源数_左_输入.无穷;
			源数_右_输入.正号 = 源数_左_输入.正号;
			Array.Copy(源数_左_输入.数值组, 源数_右_输入.数值组, 长度);
		}

		public static BigInteger 左移位(BigInteger 源数_输入, Int32 移位数_输入)		// 实际可移位空间为2^38，故Int32的2^31不够用
		{
			if(源数_输入.Is未定义)
			{
				return default;
			}
			else if(源数_输入.Is无穷)
			{
				return 源数_输入;		// 取巧：来得是什么符号，去得是什么符号
			}
			else
			{
				// 占位
			}

			BigInteger 的_输出 = default;
			UInt64[] 容器 = default;

			容器 = 左移位_核心(源数_输入.数值组, 移位数_输入);
			容器 = Trim(容器);

			// 终处理
			// 处理特殊值产生的〇的正号
			if(Is0(源数_输入.数值组))
			{
				的_输出 = 〇;
			}
			else
			{
				的_输出 = new BigInteger(转换符号(源数_输入.正号), 容器);
			}

			return 的_输出;
		}

		// 考虑增设“移位到”版本的左移位|右移位，需要考虑〇索引还是一索引的值的问题

		public static BigInteger 右移位(BigInteger 源数_输入, Int32 移位数_输入)		// 实际可移位空间为2^38，故Int32的2^31不够用
																																// 这里是“移位了”，不是“移位到”
		{
			if(源数_输入.Is未定义)
			{
				return default;
			}
			else if(源数_输入.Is无穷)
			{
				return 源数_输入;		// 取巧：来得是什么符号，去得是什么符号
			}
			else
			{
				// 占位
			}

			BigInteger 的_输出 = default;
			UInt64[] 容器 = default;

			容器 = 右移位_核心(源数_输入.数值组, 移位数_输入);
			容器 = Trim(容器);

			//源数_输入_输出.长度 = 求非〇值Cell长(源数_输入_输出.数值组);

			// 终处理
			// 处理特殊值产生的〇的正号
			if(Is0(源数_输入.数值组))
			{
				的_输出 = 〇;
			}
			else
			{
				的_输出 = new BigInteger(转换符号(源数_输入.正号), 容器);		// 防止浅复制生成新的大整数产生问题
			}

			return 的_输出;
		}

		// 相当于Trim()后求长()，但没更改源数
		unsafe private static UInt64 求非〇值Cell长<T>(T[] 数值组_输入) where T : unmanaged
		{
			UInt64 长度 = default;		// 默认值主要为null准备

			// 特殊情况
			if(数值组_输入 == null)
			{
				return 长度;
			}
			else		// 对象存在，则最小为0，故其长度默认为1
			{
				长度 = 一索引化(default(UInt64));		// 为〇准备
			}

			UInt64 总长度 = (UInt64)数值组_输入.LongLength;

			for(Int64 索引 = ToInt64(〇索引化(总长度)); 索引 >= 0; 索引--)
			{
				if(数值组_输入[default] is Byte)
				{
					if((Byte)(Object)数值组_输入[索引] != default)
					{
						长度 = ToUInt64(一索引化(索引));

						break;
					}
					else
					{
						// 占位
					}
				}
				else if((UInt64)(Object)数值组_输入[索引] != default)
				{
					长度 = ToUInt64(一索引化(索引));

					break;
				}
				else
				{
					// 占位
				}
			}

			return 长度;
		}

		private static Int32 求模(Int32 被除数_输入, Int32 除数_输入)
		{
			Int32 余数_输出 = default;

			if(被除数_输入 % 除数_输入 == default)		// 另1种映射是使用Next(除数_输入)作为除数，但是结果会比实际序号都小1，像0-Indexed了一般
			{
				余数_输出 = 除数_输入;
			}
			else
			{
				余数_输出 = 被除数_输入 % 除数_输入;
			}

			return 余数_输出;
		}

		// 以左数为主动|施动数
		// 通用计算表：
		// 左\右		∞		−∞		1		−1		N		−N		0
		// ∞																				
		// −∞																			
		// 1																				
		// −1																			
		// N																				
		// −N																			
		// 0																				
		private static BigInteger? 异常值计算(BigInteger 源_左_输入, BigInteger 源_右_输入, 算术运算 运算_输入)
		{
			BigInteger? 的_输出 = null;

			if(源_左_输入.Is未定义 | 源_右_输入.Is未定义)
			{
				throw new Exception("Syntax Error");
			}
			else if(运算_输入 == 算术运算.加)
			{
				// 加：
				// 左\右		∞			−∞		1			−1		N			−N		0
				// ∞				∞			未			∞			∞			∞			∞			∞
				// −∞			未			−∞		−∞		−∞		−∞		−∞		−∞
				// 1				∞			−∞		N			0			N			−N		1
				// −1			∞			−∞		0			−N		N			−N		−1
				// N				∞			−∞		N			N			N			泛N		N
				// −N			∞			−∞		−N		−N		泛N		−N		−N
				// 0				∞			−∞		1			−1		N			−N		0
				// 化简型：
				// 左\右		∞			−∞		N			−N		0
				// ∞				∞			未			∞			∞			∞
				// −∞			未			−∞		−∞		−∞		−∞
				// N				∞			−∞		N			泛N		N
				// −N			∞			−∞		泛N		−N		−N
				// 0				∞			−∞		N			−N		0

				// 定义范围外
				if(源_左_输入.Is无穷 ^ 源_右_输入.Is无穷)		// 有且仅有1个加数是无穷的情况
				{
					if(源_左_输入.Is无穷)		// 含取巧：是正无穷则返回正无穷；是负无穷则返回负无穷。避开了无穷的正负判断与结果符号的对应问题
					{
						的_输出 = 源_左_输入;
					}
					else		// 即：源_右_输入.Is无穷
					{
						的_输出 = 源_右_输入;
					}
				}
				else if(源_左_输入.Is无穷 & 源_右_输入.Is无穷)
				{
					if(源_左_输入.正号 != 源_右_输入.正号)		// 理论上只有true、false这2种情况
					{
						的_输出 = 未定义;		// 即：NaN等
					}
					else		// 符号相等的情况。省略了与结果符号的匹配
					{
						的_输出 = 源_左_输入;		// 源_右_输入也一样
					}
				}
				else		// 正常情况
				{
					if(源_左_输入.Is〇 & 源_右_输入.Is〇)
					{
						的_输出 = 〇;
					}
					else if(源_左_输入.Is〇)
					{
						的_输出 = 源_右_输入;
					}
					else if(源_右_输入.Is〇)
					{
						的_输出 = 源_左_输入;
					}
					else
					{
						// 占位
						//的_输出 = null;
					}
				}
			}
			else if(运算_输入 == 算术运算.减)
			{
				// 减：
				// 左\右		∞			−∞		1			−1		N			−N		0
				// ∞				未			∞			∞			∞			∞			∞			∞
				// −∞			−∞		未			−∞		−∞		−∞		−∞		−∞
				// 1				−∞		∞			0			N			−N		N			1
				// −1			−∞		∞			−N		0			−N		N			−1
				// N				−∞		∞			N			N			泛N		N			N
				// −N			−∞		∞			−N		−N		−N		泛N		−N
				// 0				−∞		∞			−1		1			−N		N			0
				// 化简型：
				// 左\右		∞			−∞		N			−N		0
				// ∞				未			∞			∞			∞			∞
				// −∞			−∞		未			−∞		−∞		−∞
				// N				−∞		∞			泛N		N			N
				// −N			−∞		∞			−N		泛N		−N
				// 0				−∞		∞			−N		N			0

				// 定义范围外
				if(源_左_输入.Is无穷 ^ 源_右_输入.Is无穷)		// 有且仅有1个数是无穷的情况
				{
					if(源_左_输入.Is无穷)		// 含取巧：是正无穷则返回正无穷；是负无穷则返回负无穷。避开了无穷的正负判断与结果符号的对应问题
					{
						的_输出 = 源_左_输入;
					}
					else		// 即：源_右_输入.Is无穷
					{
						的_输出 = -源_右_输入;		// 减数与差的符号是对应关系
					}
				}
				else if(源_左_输入.Is无穷 & 源_右_输入.Is无穷)
				{
					if(源_左_输入.正号 == 源_右_输入.正号)		// 理论上只有true、false这2种情况
					{
						的_输出 = 未定义;		// 即：NaN等
					}
					else		// 符号不等的情况。省略了与结果符号的匹配
					{
						的_输出 = 源_左_输入;		// 仅被减数一致
					}
				}
				else		// 正常情况
				{
					if(源_左_输入.Is〇 & 源_右_输入.Is〇)
					{
						的_输出 = 〇;
					}
					else if(源_左_输入.Is〇)
					{
						的_输出 = -源_右_输入;		// 减数与差的符号是对应关系
					}
					else if(源_右_输入.Is〇)
					{
						的_输出 = 源_左_输入;
					}
					else
					{
						// 占位
						//的_输出 = null;
					}
				}
			}
			else if(运算_输入 == 算术运算.乘)
			{
				// 乘：
				// 左\右		∞			−∞		1			−1		N			−N		0
				// ∞				∞			−∞		∞			−∞		∞			−∞		0
				// −∞			−∞		∞			−∞		∞			−∞		∞			0
				// 1				∞			−∞		1			−1		N			−N		0
				// −1			−∞		∞			−1		1			−N		N			0
				// N				∞			−∞		N			−N		N			−N		0
				// −N			−∞		∞			−N		N			−N		N			0
				// 0				0			0			0			0			0			0			0

				// 定义范围外
				if(源_左_输入.Is〇 | 源_右_输入.Is〇)
				{
					的_输出 = 〇;
				}
				else if(源_左_输入.Is无穷 | 源_右_输入.Is无穷)		// 有因数是无穷的情况
				{
					if(源_左_输入.正号 != 源_右_输入.正号)
					{
						的_输出 = 负无穷;
					}
					else		// 即：符号相等，含都是正数、都是负数2种
					{
						的_输出 = 正无穷;
					}
				}
				else		// 正常情况
				{
					if(源_左_输入.Is一 & 源_右_输入.Is一)
					{
						if(源_左_输入.正号 != 源_右_输入.正号)
						{
							的_输出 = 负一;
						}
						else		// 即：符号相等，含都是正数、都是负数2种
						{
							的_输出 = 正一;
						}
					}
					else if(源_左_输入.Is一)
					{
						if(源_左_输入.正号 != 源_右_输入.正号)
						{
							源_右_输入.正号 = false;
							的_输出 = 源_右_输入;
						}
						else		// 即：符号相等，含都是正数、都是负数2种
						{
							的_输出 = 求绝对值(源_右_输入);
						}
					}
					else if(源_右_输入.Is一)
					{
						if(源_左_输入.正号 != 源_右_输入.正号)
						{
							源_左_输入.正号 = false;
							的_输出 = 源_左_输入;
						}
						else		// 即：符号相等，含都是正数、都是负数2种
						{
							的_输出 = 求绝对值(源_左_输入);
						}
					}
					else
					{
						// 占位
						//的_输出 = null;
					}
				}
			}
			else if(运算_输入 == 算术运算.除)
			{
				// 除以：
				// 左\右		∞			−∞		1			−1		N			−N		0
				// ∞				1			−1		∞			−∞		∞			−∞		未
				// −∞			−1		1			−∞		∞			−∞		∞			未
				// 1				0			0			1			−1		泛N		泛N		±∞
				// −1			0			0			−1		1			泛N		泛N		？±∞
				// N				0			0			N			−N		泛N		泛N		±∞
				// −N			0			0			−N		N			泛N		泛N		？±∞
				// 0				0			0			0			0			0			0			未
			}
			else if(运算_输入 == 算术运算.整除)
			{
				// 整除以：
				// 左\右		∞			−∞		1			−1		N			−N		0
				// ∞				1			−1		∞			−∞		∞			−∞		未
				// −∞			−1		1			−∞		∞			−∞		∞			未
				// 1				0			0			1			−1		0			0			？±∞|未
				// −1			0			0			−1		1			0			0			？±∞|未
				// N				0			0			N			−N		泛N		泛N		？±∞|未
				// −N			0			0			−N		N			泛N		泛N		？±∞|未
				// 0				0			0			0			0			0			0			未

				// 相当于获取的都是“同侧商”，即：被除数 ÷ 商的新·商假设不能代为抵消符号，故商的符号需要与被除数一致以获得积的符号为正，即视新·商即除数为“+0”

				// 定义范围外
				// 符合数学更深层次定义，较为完美
				if(求绝对值(源_左_输入) < 求绝对值(源_右_输入))
				{
					的_输出 = 〇;
				}
				else if(源_左_输入.Is〇 | 源_右_输入.Is〇)
				{
					if(源_右_输入.Is〇)
					{
						的_输出 = 未定义;
					}
					else if(源_左_输入.Is〇)
					{
						的_输出 = 〇;
					}
					else		// 理论上不存在
					{
						// 占位
					}
				}
				else if(源_左_输入.Is无穷 | 源_右_输入.Is无穷)		// 有数是无穷的情况
				{
					if(源_左_输入.Is无穷 & 源_右_输入.Is无穷)
					{
						if(源_左_输入.正号 != 源_右_输入.正号)
						{
							的_输出 = 负一;
						}
						else		// 即：符号相等，含都是正数、都是负数2种
						{
							的_输出 = 正一;
						}
					}
					else if(源_左_输入.Is无穷)
					{
						if(源_左_输入.正号 != 源_右_输入.正号)
						{
							的_输出 = 负无穷;
						}
						else		// 即：符号相等，含都是正数、都是负数2种
						{
							的_输出 = 正无穷;
						}
					}
					else		// 除数为无穷的情况
					{
						// 占位
						//的_输出 = 〇;		// 已被前面：if(求绝对值(源_左_输入) < 求绝对值(源_右_输入))处理完了
					}
				}
				else		// 正常情况
				{
					if(源_右_输入.Is一)
					{
						if(源_右_输入.Is负数)		// × −1 = ÷ −1
						{
							的_输出 = -源_左_输入;		// 实为求相反数
						}
						else		// × 1 = ÷ 1
						{
							的_输出 = 源_左_输入;
						}
					}
					else		// 被除数绝对值为1的情况
					{
						// 占位
					}
				}
			}
			else if(运算_输入 == 算术运算.余)
			{
				// 余：
				// 左\右		∞			−∞		1		−1		N			−N		0
				// ∞				0			0			0		0			0			0			未
				// −∞			0			0			0		0			0			0			未
				// 1				1			1			0		0			1			1			未
				// −1			−1		−1		0		0			−1		−1		未
				// N				N			N			0		0			泛N		泛N		未
				// −N			−N		−N		0		0			泛N		泛N		未
				// 0				0			0			0		0			0			0			未

				// 预处理
				的_输出 = 〇;		// 余数不存在null状态

				// 定义范围外
				// 符合数学更深层次定义，较为完美
				if(求绝对值(源_左_输入) < 求绝对值(源_右_输入))
				{
					的_输出 = 源_左_输入;		// 含取巧：是什么符号返回什么符号
				}
				else if(源_右_输入.Is〇)
				{
					的_输出 = 未定义;
				}
				else if(源_左_输入.Is〇)		// 与：if(求绝对值(源_左_输入) < 求绝对值(源_右_输入))不是同一种情况，代表着〇除以任何数都是〇
				{
					// 占位
					//的_输出 = 〇;
				}
				else if(源_右_输入.Is一)
				{
					// 占位
					//的_输出 = 〇;
				}
				else if(源_左_输入.Is无穷)
				{
					// 占位
					//的_输出 = 〇;
				}
				else		// 正常的情况，即：被除数、除数都是“正常数”（非：无穷、一、〇）的情况
				{
					// 占位
				}
			}
			else		// 理论上不存在
			{
				// 占位
			}

			return 的_输出;
		}

		// 经过考虑，仍然决定使用自带的最大有符号整数类型，余下匹配则自行在调用时完成，满足工具函数的简单灵活性，而不是一种函数的多处套壳，而且容易发生问题、发生遗漏、徒增代码
		// 使用Int64因需要负数，不使用大整数因其实现依赖于此函数，故需要前置的Int64版本，预计大整数完成后，再调用除了索引，就都是大整数版的了
		// 与Math.Flooring()效果相同，但没有其底层化，仅胜在逻辑清晰
		private static UInt64 Flooring倍数化除以(UInt64 被除数_输入_输出, UInt64 除数_输入) => 被除数_输入_输出 / 除数_输入;		// 借用整除的实现即为Flooring除以的方式直接实现，故无需所谓的Flooring倍数化()
		private static UInt64 Flooring倍数化(UInt64 被除数_输入_输出, UInt64 除数_输入)
		{
			UInt64 余数 = 被除数_输入_输出 % 除数_输入;

			if(余数 != default)
			{
				被除数_输入_输出 = 被除数_输入_输出 - 余数;		// 被除数整除数倍化
			}
			else
			{
				// 占位
			}

			return 被除数_输入_输出;
		}
		// 与Math.Ceiling()效果相同，但没有其底层化，仅胜在逻辑清晰
		private static Int64 Ceiling倍数化除以(Int64 被除数_输入_输出, Int64 除数_输入) => ToInt64(Ceiling倍数化(ToUInt64(被除数_输入_输出), ToUInt64(除数_输入)) / ToUInt64(除数_输入));
		private static UInt64 Ceiling倍数化除以(UInt64 被除数_输入_输出, UInt64 除数_输入) => Ceiling倍数化(被除数_输入_输出, 除数_输入) / 除数_输入;
		private static UInt64 Ceiling倍数化(UInt64 被除数_输入_输出, UInt64 除数_输入)
		{
			UInt64 余数 = 被除数_输入_输出 % 除数_输入;

			if(余数 != default)
			{
				被除数_输入_输出 = 被除数_输入_输出 - 余数 + 除数_输入;		// 被除数整除数倍化
			}
			else
			{
				// 占位
			}

			return 被除数_输入_输出;
		}
		
		private static Int64 一索引化(Int64 〇索引_输入) => (Int64)一索引化((UInt64)〇索引_输入);
		private static UInt64 一索引化(UInt64 〇索引_输入) => 〇索引_输入 + 1;
		private static Int64 〇索引化(Int64 〇索引_输入) => (Int64)〇索引化((UInt64)〇索引_输入);
		private static UInt64 〇索引化(UInt64 一索引_输入) => 一索引_输入 - 1;
		private static UInt64 〇索引化_兜底版(UInt64 一索引_输入) => 向前_兜底版(一索引_输入);
		//
		private static Int64 向后(UInt64 原输入) => 向后(ToInt64(原输入));
		private static Int64 向后(Int64 原输入) => 原输入 + 1;
		private static Int64 向前(UInt64 原输入) => 向前(ToInt64(原输入));
		private static Int64 向前(Int64 原输入) => 原输入 - 1;
		private static UInt64 向前_兜底版(UInt64 源数_输入)
		{
			if(源数_输入 != default)
			{
				return 源数_输入 - 1;
			}
			else		// 主要用于统计后颜色值的索引转换处
			{
				return default;
			}
		}
		#endregion

		#region 移植函数
		// 移植函数
		// 4.7.2版〇值计数
		// 统计的是位（bit）
		public static Byte 高位始空白〇值计数(UInt32 源数)
		{
			Byte 位级〇值计数 = default;

			if(源数 == 0)
			{
				位级〇值计数 = 32;

				return 位级〇值计数;
			}

			if((源数 & 0xFFFF_0000) == 0)		// 0B 1111 1111 1111 1111 0000 0000 0000 0000
																// 2019.03.20，高16位掩码
			{
				位级〇值计数 += 16;
				源数 <<= 16;
			}

			if((源数 & 0xFF00_0000) == 0)		// 0B 1111 1111 0000 0000 0000 0000 0000 0000
																// 2019.03.20，高8位掩码
			{
				位级〇值计数 += 8;
				源数 <<= 8;
			}

			if((源数 & 0xF000_0000) == 0)		// 0B 1111 0000 0000 0000 0000 0000 0000 0000
																	// 2019.03.20，高4位掩码
			{
				位级〇值计数 += 4;
				源数 <<= 4;
			}

			if((源数 & 0xC000_0000) == 0)		// 0B 1100 0000 0000 0000 0000 0000 0000 0000
																	// 2019.03.20，高2位掩码
			{
				位级〇值计数 += 2;
				源数 <<= 2;
			}

			if((源数 & 0x8000_0000) == 0)		// 0B 1000 0000 0000 0000 0000 0000 0000 0000
																	// 2019.03.20，高1位掩码
			{
				位级〇值计数 += 1;
			}

			return 位级〇值计数;
		}

		public static Byte 低位始空白〇值计数_核心(UInt32 源数)
		{
			Byte 位级〇值计数 = default;

			if(源数 == 0)
			{
				位级〇值计数 = 32;		// ！需要常量化

				return 位级〇值计数;
			}

			// 2019.01.09，二分法抽查计〇
			if((源数 & 字掩码) == 0)		// 0B 0000 0000 0000 0000 1111 1111 1111 1111
														// 2019.03.20，低16位掩码
			{
				位级〇值计数 += 16;
				源数 >>= 16;
			}

			if((源数 & 字节掩码) == 0)		// 0B 0000 0000 0000 0000 0000 0000 1111 1111
															// 2019.03.20，低8位掩码
			{
				位级〇值计数 += 8;
				源数 >>= 8;
			}

			if((源数 & 四位掩码) == 0)		// 0B 0000 0000 0000 0000 0000 0000 0000 1111
															// 2019.03.20，低4位掩码
			{
				位级〇值计数 += 4;
				源数 >>= 4;
			}

			if((源数 & 双位掩码) == 0)		// 0B 0000 0000 0000 0000 0000 0000 0000 0011
															// 2019.03.20，低2位掩码
			{
				位级〇值计数 += 2;
				源数 >>= 2;
			}

			if((源数 & 位掩码) == 0)		// 0B 0000 0000 0000 0000 0000 0000 0000 0001
														// 2019.03.20，低1位掩码
			{
				位级〇值计数 += 1;
			}

			return 位级〇值计数;
		}

		public static Byte 高位始空白〇值计数(UInt64 源数)
		=>
			(源数 & 0xFFFF_FFFF_0000_0000) == default		// 0B 1111 1111 1111 1111 1111 1111 1111 1111 0000 0000 0000 0000 0000 0000 0000 0000
																						// 2019.03.20，高1位掩码
			?
				ToByte(高位始空白〇值计数((UInt32)源数) + 32)
				: 高位始空白〇值计数((UInt32)(源数 >> 双字位数));

		// Core版NumericsHelpers.GetHashCode()，3个函数合一了
		public Int32 求散列() => 求散列(this);
		public static Int32 求散列(BigInteger 源_输入)
		{
			Int32 散列 = default;

			for(Int32 索引 = ToInt32(〇索引化(源_输入.长度)); --索引 >= 0; )
			{
				散列 = CombineHash(散列, unchecked((Int32)源_输入.数值组[索引]));
			}

			return 散列;
		}
		public static UInt32 CombineHash(UInt32 源数1_输入, UInt32 源数2_输入) => ((源数1_输入 << 7) | (源数1_输入 >> 25)) ^ 源数2_输入;
		public static Int32 CombineHash(Int32 源数1_输入, Int32 源数2_输入) => unchecked((Int32)CombineHash((UInt32)源数1_输入, (UInt32)源数2_输入));

		public static Boolean Is0(UInt64 数_输入) => Is0(new UInt64[]{ 数_输入 });
		unsafe public static Boolean Is0<T>(T[] 数_输入) where T : unmanaged => Is0(转换数组<UInt64, T>(数_输入));
		public Boolean Is0() => Is0(数值组);
		public static Boolean Is0(UInt64[] 数_输入)		// 2019.06.22，理论上到了“大整数”的阶段直接和“〇”作对比就行
		{
			Boolean 结果 = default;

			if(数_输入 == default)
			{
				结果 = true;
			}
			else if(数_输入.Length == default)
			{
				结果 = true;
			}
			else
			if
			(
				数_输入.Length == 一索引化(default)
				&& 数_输入[default] == default		// 2019.06.22，尝试性的优雅写法：数_输入 == new UInt64[]{ default }不能合乎逻辑地判断
			)
			{
				结果 = true;
			}
			else
			{
				// 占位
			}

			return 结果;
		}

		public static Boolean Is0_纯粹版(UInt64[] 数_输入)		// 2019.06.22，理论上到了“大整数”的阶段直接和“〇”作对比就行
		{
			// 异常值处理
			if(IsNullOrEmpty(数_输入))
			{
				return default;
			}
			else
			{
				// 占位
			}

			Boolean 结果 = default;

			if
			(
				Trim(数_输入).Length == 一索引化(default)
				&& Trim(数_输入)[default] == default		// 2019.06.22，尝试性的优雅写法：数_输入 == new UInt64[]{ default }不能合乎逻辑地判断
			)
			{
				结果 = true;
			}
			else
			{
				// 占位
			}

			return 结果;
		}
		unsafe public static Boolean IsNullOrEmpty<T>(T[] 数_输入) where T : unmanaged		// 2019.06.22，理论上到了“大整数”的阶段直接和“〇”作对比就行
		{
			Boolean 结果 = default;

			// 不可合并，否则会出现空引用异常
			if(数_输入 == default)
			{
				结果 = true;
			}
			else if(数_输入.Length == default)		// 即：数_输入 == new T[default]
			{
				结果 = true;
			}
			else
			{
				// 占位
			}

			return 结果;
		}

		// Core版String.IsNullOrEmpty()
		// 使用≤〇而不是＝〇是因为后者会忽视1st字符的检测
		// 使用三目运算符是防止转为汇编时的被删减
		// https://github.com/dotnet/coreclr/issues/914
		//[NonVersionable]
		public static Boolean IsNullOrEmpty(String 字符串_输入) =>
			(字符串_输入 == default || 0U >= (UInt32)字符串_输入.Length)
			?
				true
				: default;

		// 尝试移植Math.Pow()失败，Framework中这是导入的；Core中则未找到定义
		//public static Double Pow(Double 底数_输入, Double 指数_输入) => throw new NotImplementedException();

		// 尝试移植Math.Log()失败，Framework中这是导入的；Core中其2参数版是单参数版的wrap，？但单参数版疑似导入
		//public static Double Log(Double 底数_输入, Double 指数_输入) => throw new NotImplementedException();

		// 4.7.2版Math.Max(Long, Long)
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]		// Core版无
		//[System.Runtime.Versioning.NonVersionable]		// Core版为[NonVersionable]
		public static Int64 Max(Int64 源数_左, Int64 源数_右) =>
			(源数_左 >= 源数_右)
			?
				源数_左
				: 源数_右;

		// 尝试移植String.cs的Length失败，Core中的定义导入的

		// 尝试移植Array.Copy()失败，Framework中这是导入的；Core中的定义是狗屁不通的自身调用，完全无有意义的实体代码
		//public static void Copy(Int64[] 源组_输入, Int64[] 的组_输入, UInt64 的容量_输入) => throw new NotImplementedException();

		// 尝试移植Array.Clear()失败，Framework中这是导入的；Core中的定义是狗屁不通的借用IList.Clear()
		//public static void Clear() => throw new NotImplementedException();

		// Core版NumericsHelpers.Abs()
		// 该函数能做到原码→原码、补码→原码，故也可称为：求原码()，相当于：求2的补码()的“1半”
		public static UInt64 求绝对值(Int64 源数_输入)
		{
			unchecked
			{
				UInt64 掩码 = (UInt64)(源数_输入 >> ToByte(向前(四字位数)));

				return ((UInt64)源数_输入 ^ 掩码) - 掩码;
			}
		}

		// Core版NumberHelpers.GetDoubleParts()
		[StructLayout(LayoutKind.Explicit)]
		public struct DoubleToUInt64转换体
		{
			[FieldOffset(0)]		// 模拟共用体
			public Double Double载体接口;
			[FieldOffset(0)]		// 模拟共用体
			public UInt64 UInt64载体接口;
		}
		public static (Int32 符号, UInt64 底数, Int32 指数) GetDoublePart(Double 双精度浮点数)
		{
			// 定义源自IEEE 754，Binary64，选用的版本是指数 ∈ [-1023, 1024]的版本
			// Double数据类型位数划分：
			// Double公式：
			// (-1)^S x 1.M（二进制） x 2^(E - 0x03FF（1024 - 1，(2^10 - 1)）)
			// Part														Bit				位数
			// Significand | Mantissa							0~51			52
			// Exponent												52~62			11
			// Sign（0 = Positive, 1 = Negative）		63				1
			// 示意结构：
			// 内存倒序
			// | 64 ╎ 63 62 61 60 59 58 57 56 55 54 53 ╎ 52 51 50 49  | 48 47 46 45 44 43 42 41 40 39 38 37 36 35 34 33 | 32 31 30 29 28 27 26 25 24 23 22 21 20 19 18 17 | 16 15 14 13 12 11 10 09 08 07 06 05 04 03 02 01 | 00
			// | (X) ╎ 					 (210)ED                        ╎ C 				   | 					 BA98 									                | 					 7654 					                                  | 					 3210 					                                   | 00
			// | 符  ╎ 					 指数位                          ╎ 					  ╵ 										                                       ╵ 					 底尾数位 					                            ╵ 										                                       | 00

			// 注：
			// integer32符号_out原取值范围：						0x00~0xFFFF FFFF（0~‭(‬21 4748 3648 - 1), -‭‬21 4748 3648~0，-2^31~(2^31 - 1)）		此处有意义值范围：		{0xFFFF FFFF（-1）, 0x0000 0001（1）}
			// integer32指数_out原取值范围：						0x00~0xFFFF FFFF（0~(‬21 4748 3648 - 1), -‭‬21 4748 3648~0，-2^31~(2^31 - 1)）		此处有意义值范围：		0x00~0x07FF（0~(‭‬2048 - 1)，0~(2^11 - 1)）
			// unsignedInteger64底数_out原取值范围：		0x00~0xFFFF FFFF FFFF FFFF（0~(‭‬1844 6744 0737 0955 1616 - 1)，0~(2^64 - 1)）		此处有意义值范围：		0x00~0x000‭F FFFF FFFF FFFF‬（0~(‭4503 5996 2737 0496 - 1)‬，0~(2^52 - 1)）
			// boolean有限_out原取值范围：						{false, true}																														此处有意义值范围：		{false, true}
			// 指数0x00、底数为〇：〇
			// 指数0x00、底数非〇：下溢
			// 指数0x7FF、底数为〇：∞
			// 指数0x7FF、底数非〇：NaN
			// 省略了小数点（.）、底数首位（1）、指数底数（2）
			// 指数的数值为：存储值 - 1023
			// 指数的特殊含义：0x000（0|-1023），singed zero、subnormal|denormal number
			// 指数的特殊含义：0x7FF（2047|1024），∞、NaN

			// 初始化
			(Int32 符号, UInt64 底数, Int32 指数) 的_输出 = default;
			DoubleToUInt64转换体 容器;
			容器.UInt64载体接口 = default;		// 共用体空间清〇
			容器.Double载体接口 = 双精度浮点数;		// 共用体赋值

			// 达成{0x01, 0x00}集合到{-1, 1}集合的简单映射，即{0x01, 0x00} → {0x02, 0x00} → {(1 - 2), (1 - 0)} → {-1, 1}
			的_输出.符号 = 1 - ((Int32)(容器.UInt64载体接口 >> 62) & 0B_0010);		// 操作数右移62位，有效数位余下2位，即右数2nd位掩码，与1相减

			// 获取底数位
			的_输出.底数 = 容器.UInt64载体接口 & 0x000F_FFFF_FFFF_FFFF;		// 64位的低52位掩码
			// 获取指数位
			的_输出.指数 = (Int32)(容器.UInt64载体接口 >> 52) & 0x07FF;		// 64位的53~63这11位掩码，故使用0x07FF而非0x0FFF，因12th不在此范围

			// 49th位为一（十六进制下14th位为一）：无影响
			// 49th位为〇（十六进制下14th位为〇）：49th位增一（十六进制下14th位增一），即增加4503 5996 2737 0496，2^52
			的_输出.底数 |= 0X_0010_0000_0000_0000;		// 底数最大容量4503 5996 2737 0496（2^52）再加一

			的_输出.指数 -= 1023 + 52;		// 0X 03FF（1023） + 0X 0034 （52）的结果，前者是转换算子，后者是指小数位被估低了52个二进制位，故还原

			return 的_输出;
		}

		// Core版BigIntegerCalculator.PowRound()
		private static UInt64 估算幂长度(UInt64 指数_输入, UInt64 底数长度_输入)
		{
			// The basic pow algorithm, but instead of squaring
			// and multiplying we just sum up the lengths.
			UInt64 幂长度_输入 = default;

			while(指数_输入 != 0)
			{
				checked
				{
					if((指数_输入 & 位掩码) == 1)
					{
						幂长度_输入 += 底数长度_输入;
					}

					if(指数_输入 != 1)
					{
						底数长度_输入 += 底数长度_输入;
					}
				}

				指数_输入 >>= 位数;
			}

			return 幂长度_输入;
		}

		// Math.Ceiling()、Math.Floor()在4.7.2版中是导入的；Core版中则是最终调用了Decimal.DecCalc.InternalRound()实现的，长达200行还用了goto，十分不适合迁移

		// Core版BitConverter.ToString()
		// Converts an array of bytes into a String.  
		public static String ToString(Byte[] 源数_输入)
		{
			if(源数_输入 == null)
			{
				throw new ArgumentNullException();
			}
			else
			{
				// 占位
			}

			return ToString(源数_输入, 0, 源数_输入.Length);
		}
		// Converts an array of bytes into a String.  
		public static String ToString(Byte[] 源数_输入, Int32 始索引_输入)
		{
			if(源数_输入 == null)
			{
				throw new ArgumentNullException();
			}
			else
			{
				// 占位
			}

			return ToString(源数_输入, 始索引_输入, 源数_输入.Length - 始索引_输入);
		}
		// Converts an array of bytes into a String.  
		public static String ToString(Byte[] 源数_输入, Int32 始索引_输入, Int32 长度_输入)
		{
			if(源数_输入 == null)
			{
				throw new ArgumentNullException();
			}
			else
			{
				// 占位
			}

			if
			(
				// 情况1
				始索引_输入 < 0
				// 情况2
				||
					始索引_输入 >= 源数_输入.Length
					&& 始索引_输入 > 0
			)
			{
				throw new ArgumentOutOfRangeException();
			}
			else
			{
				// 占位
			}

			if(长度_输入 < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			else
			{
				// 占位
			}

			if(始索引_输入 > 源数_输入.Length - 长度_输入)
			{
				throw new ArgumentNullException();
			}
			else
			{
				// 占位
			}

			if(长度_输入 == 0)
			{
				return String.Empty;
			}
			else
			{
				// 占位
			}

			if(长度_输入 > Int32.MaxValue / 3)		// Int32.MaxValue ÷ 3 ≈ 7 1582 7882 Byte ≈ 699 MB
			{
				throw new ArgumentOutOfRangeException();
			}
			else
			{
				// 占位
			}

			return String.Create
			(
				长度_输入 * 3 - 1,
				(源数_输入, 始索引_输入, 长度_输入),
				(的数组, 状态) =>
				{
					const String HexValues = "0123456789ABCDEF";

					ReadOnlySpan<Byte> 源数组 = new ReadOnlySpan<Byte>(状态.源数_输入, 状态.始索引_输入, 状态.长度_输入);

					Int32 i = 0;
					Int32 j = 0;

					Byte 数值 = 源数组[i++];
					的数组[j++] = HexValues[数值 >> 四位数];
					的数组[j++] = HexValues[数值 & 四位掩码];

					while(i < 源数组.Length)
					{
						数值 = 源数组[i++];
						的数组[j++] = '-';
						的数组[j++] = HexValues[数值 >> 四位数];
						的数组[j++] = HexValues[数值 & 四位掩码];
					}
				}
			);
		}

		// Core版BigInteger.GetDiffLength()
		// 已适应性修改
		// 一般使用版，自创
		public static Boolean 相等_核心(UInt64[] 比较值_输入, UInt64[] 被比较值_输入)
		{
			Boolean 的_输出 = default;

			if(比较值_输入 == default)
																												
			{
				的_输出 = true;
			}
			else if(比较值_输入.Length != 被比较值_输入.Length)
			{
				// 占位
				//的_输出 = default;
			}
			else
			{
				的_输出 = 相等_核心(比较值_输入, 被比较值_输入, 比较值_输入.Length);
			}

			return 的_输出;
		}

		// 原版
		public static Boolean 相等_核心(UInt64[] 比较值_输入, UInt64[] 被比较值_输入, Int32 长度_输入)
		{
			for(Int64 索引 = 〇索引化(长度_输入); 索引 >= 0; 索引--)
			{
				if(比较值_输入[索引] != 被比较值_输入[索引])
				{
					return false;
				}
				else
				{
					// 占位
				}
			}

			return true;
		}

		// ！Core版的Array.Reverse()本质是调用了Array.Reverse<T>()，后者本质则是使用指针交换首尾，依次缩减范围的算法，考虑直接改进|简化|固化而不是移植

		// Core版Buffer.BlockCopy()引入失败，无实际定义；对应的Framework 4.8中仍是COM导入的函数

		// Core版Convert.ToXXX()
		public static SByte ToSByte(Int64 源数_输入)
		{
			if
			(
				源数_输入< SByte.MinValue
				|| 源数_输入 > SByte.MaxValue
			)
			{
				throw new OverflowException("Byte (Integer 8) Overflow");
			}
			else
			{
				// 占位
			}

			return (SByte)源数_输入;
		}
		public static SByte ToSByte(UInt64 源数_输入)
		{
			if(源数_输入 > (UInt64)SByte.MaxValue)
			{
				throw new OverflowException("Byte (Integer 8) Overflow");
			}
			else
			{
				// 占位
			}

			return (SByte)源数_输入;
		}
		public static Byte ToByte(Int64 源数_输入)
		{
			if
			(
				源数_输入 > Byte.MaxValue
				|| 源数_输入 < Byte.MinValue
			)
			{
				throw new OverflowException("Byte (Integer 8) Overflow");
			}
			else
			{
				// 占位
			}

			return (Byte)源数_输入;
		}
		public static Byte ToByte(UInt64 源数_输入)
		{
			if(源数_输入 >Byte.MaxValue)
			{
				throw new OverflowException("Byte (Integer 8) Overflow");
			}
			else
			{
				// 占位
			}

			return (Byte)源数_输入;
		}
		//
		public static Int16 ToInt16(Int64 源数_输入)
		{
			if
			(
				源数_输入< Int16.MinValue
				|| 源数_输入 > Int16.MaxValue
			)
			{
				throw new OverflowException("Byte (Integer 8) Overflow");
			}
			else
			{
				// 占位
			}

			return (Int16)源数_输入;
		}
		public static Int16 ToInt16(UInt64 源数_输入)
		{
			if(源数_输入 > (UInt64)Int16.MaxValue)
			{
				throw new OverflowException("Byte (Integer 8) Overflow");
			}
			else
			{
				// 占位
			}

			return (Int16)源数_输入;
		}
		public static Char ToChar(Int64 源数_输入)
		{
			if
			(
				源数_输入 > Char.MaxValue
				|| 源数_输入 < Char.MinValue
			)
			{
				throw new OverflowException("Byte (Integer 8) Overflow");
			}
			else
			{
				// 占位
			}

			return (Char)源数_输入;
		}
		public static Char ToChar(UInt64 源数_输入)
		{
			if(源数_输入 >Char.MaxValue)
			{
				throw new OverflowException("Byte (Integer 8) Overflow");
			}
			else
			{
				// 占位
			}

			return (Char)源数_输入;
		}
		public static UInt16 ToUInt16(Int64 源数_输入)
		{
			if
			(
				源数_输入 > UInt16.MaxValue
				|| 源数_输入 < UInt16.MinValue
			)
			{
				throw new OverflowException("Byte (Integer 8) Overflow");
			}
			else
			{
				// 占位
			}

			return (UInt16)源数_输入;
		}
		public static UInt16 ToUInt16(UInt64 源数_输入)
		{
			if(源数_输入 >UInt16.MaxValue)
			{
				throw new OverflowException("Byte (Integer 8) Overflow");
			}
			else
			{
				// 占位
			}

			return (UInt16)源数_输入;
		}
		//
		public static Int32 ToInt32(Int64 源数_输入)
		{
			if
			(
				源数_输入 < Int32.MinValue
				|| 源数_输入 > Int32.MaxValue
			)
			{
				throw new OverflowException("Integer 32 Overflow");
			}
			else
			{
				// 占位
			}

			return (Int32)源数_输入;
		}
		public static Int32 ToInt32(UInt64 源数_输入)
		{
			if(源数_输入 > Int32.MaxValue)
			{
				throw new OverflowException("Integer 32 Overflow");
			}
			else
			{
				// 占位
			}

			return (Int32)源数_输入;
		}
		public static UInt32 ToUInt32(Int64 源数_输入)
		{
			if
			(
				源数_输入 < Int32.MinValue
				|| 源数_输入 > Int32.MaxValue
			)
			{
				throw new OverflowException("Integer 32 Overflow");
			}
			else
			{
				// 占位
			}

			return (UInt32)源数_输入;
		}
		public static UInt32 ToUInt32(UInt64 源数_输入)
		{
			if(源数_输入 > UInt32.MaxValue)
			{
				throw new OverflowException("Unsigned Integer 32 Overflow");
			}
			else
			{
				// 占位
			}

			return (UInt32)源数_输入;
		}
		//
		public static Int64 ToInt64(UInt64 源数_输入)
		{
			if(源数_输入 >UInt64.MaxValue)
			{
				throw new OverflowException("Integer 64 Overflow");
			}

			return (Int64)源数_输入;
		}

		public static UInt64 ToUInt64(SByte 源数_输入)
		{
			if(源数_输入 < 0)
			{
				throw new OverflowException("Unsigned Integer 64 Overflow");
			}

			return (UInt64)源数_输入;
		}
		public static UInt64 ToUInt64(Byte 源数_输入_输出) => 源数_输入_输出;		// 仅起到占位之用
		public static UInt64 ToUInt64(Int16 源数_输入)
		{
			if(源数_输入 < 0)
			{
				throw new OverflowException("Unsigned Integer 64 Overflow");
			}

			return (UInt64)源数_输入;
		}
		public static UInt64 ToUInt64(Char 源数_输入)
		{
			UInt64 的_输出 = default;

			if(源数_输入 >= 'A' && 源数_输入 <= 'Z')
			{
				的_输出 = ToUInt64(一索引化(源数_输入) - 32);		// 其实也有如下一般的写法：ToUInt64(源数_输入 - 'A' + 10)
			}
			else if(源数_输入 >= '0' && 源数_输入 <= '9')
			{
				的_输出 = ToUInt64(源数_输入 - '0');		// 其实也有如上一般的写法：ToUInt64(源数_输入 - 30)、ToUInt64(〇索引化(一索引化(源数_输入) - 32))
			}
			else		// 理论上不存在
			{
				// 占位
				throw new Exception("Invalid Digit");
			}

			return 的_输出;
		}
		public static UInt64 ToUInt64(UInt16 源数_输入_输出) => 源数_输入_输出;		// 仅起到占位之用
		public static UInt64 ToUInt64(Int32 源数_输入)
		{
			if(源数_输入 < 0)
			{
				throw new OverflowException("Unsigned Integer 64 Overflow");
			}

			return (UInt64)源数_输入;
		}
		public static UInt64 ToUInt64(UInt32 源数_输入_输出) => 源数_输入_输出;		// 仅起到占位之用
		public static UInt64 ToUInt64(Int64 源数_输入)
		{
			if(源数_输入 < 0)
			{
				throw new OverflowException("Unsigned Integer 64 Overflow");
			}

			return (UInt64)源数_输入;
		}
		#endregion
	}
}