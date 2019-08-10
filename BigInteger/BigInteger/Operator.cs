using System;

namespace BigInteger
{
	public partial struct BigInteger
	{
		#region 运算符
		// 运算符
		// 类型转换
		// 目前考虑转入|转出都是包装且虚增代码量，暂不设立
		//
		// 真·运算符
		// 原BigInteger位逻辑运算逻辑：负数以补码形式进行计算；正数则是原码执行，后续仍是原码
		public static BigInteger operator &(BigInteger 源数_左_输入, BigInteger 源数_右_输入) => 与_补码负数(源数_左_输入, 源数_右_输入);
		public static BigInteger 与_补码负数(BigInteger 源数_左_输入, BigInteger 源数_右_输入) => 与(源数_左_输入, 源数_右_输入, true);		// 即常见的通行版
		public static BigInteger 与_原码负数(BigInteger 源数_左_输入, BigInteger 源数_右_输入) => 与(源数_左_输入, 源数_右_输入);

		// Is补码_输入即Is数值化符号_输入
		public static BigInteger 与(BigInteger 源数_左_输入, BigInteger 源数_右_输入, Boolean Is数值化符号_输入 = default)
		{
			// 特殊情况处理
			if(源数_左_输入.Is无穷)
			{
				return 源数_左_输入;
			}
			else if(源数_右_输入.Is无穷)
			{
				return 源数_右_输入;
			}
			else
			{
				// 占位
			}

			BigInteger 的_输出 = default;
			Boolean 符号容器_左 = !(源数_左_输入.正号 == false) || Is数值化符号_输入 == default;		// 当是数值化符号的时候，再讨论是否是负数
			Boolean 符号容器_右 = !(源数_右_输入.正号 == false) || Is数值化符号_输入 == default;		// 当是数值化符号的时候，再讨论是否是负数
			Boolean 符号容器 = default;
			Int64 长度 = Max(ToInt64(源数_左_输入.长度), ToInt64(源数_右_输入.长度));
			UInt64[] 数容器_左 = 等长化_核心(源数_左_输入.数值组, 长度, true);
			UInt64[] 数容器_右 = 等长化_核心(源数_右_输入.数值组, 长度, true);

			// 符号
			的_输出.正号 = 符号逻辑运算(源数_左_输入.正号, 源数_右_输入.正号, 位运算.与);
			符号容器 = !(的_输出.正号 == false) || Is数值化符号_输入 == default;		// 当是数值化符号的时候，再讨论是否是负数
																															// 整体相当于1个3→2

			// 数值
			// 预处理
			数容器_左 = 符号容器_左 ? 数容器_左 : 求2的补码_核心(数容器_左);
			数容器_右 = 符号容器_右 ? 数容器_右 : 求2的补码_核心(数容器_右);
			//
			的_输出.数值组 = 与_核心(数容器_左, 数容器_右);
			的_输出.数值组 = 符号容器 ? 的_输出.数值组 : 求2的补码_核心(的_输出.数值组);
			的_输出.数值组 = Trim(的_输出.数值组);

			// 终处理
			// 处理特殊值产生的〇的正号
			if(Is0(的_输出.数值组))
			{
				的_输出.正号 = default;
			}
			else
			{
				// 占位
			}

			return 的_输出;
		}

		private static UInt64[] 与_核心(UInt64[] 源数_左_输入, UInt64[] 源数_右_输入)
		{
			Int64 长度 = Max(源数_左_输入.Length, 源数_右_输入.Length);
			UInt64[] 的_输出 = new UInt64[长度];

			for(Int32 索引 = default; 索引 <= ToInt32(〇索引化(长度)); 索引++)
			{
				的_输出[索引] = 源数_左_输入[索引] & 源数_右_输入[索引];
			}

			return 的_输出;
		}

		private static Boolean? 符号逻辑运算(Boolean? 源正号_左_输入, Boolean? 源正号_右_输入, 位运算 运算符_输入)
		{
			// 运算逻辑表
			// 与：
			// 			负		〇		正
			// 负		负		〇		正
			// 〇		〇		〇		〇
			// 正		正		〇		正
			// 或：
			// 			负		〇		正
			// 负		负		负		负
			// 〇		负		〇		正
			// 正		负		正		正
			// 异或：
			// 			负		〇		正
			// 负		正		负		负
			// 〇		负		〇		正
			// 正		负		正		正
			// 同或：
			// 			负		〇		正
			// 负		负		正		正
			// 〇		正		负		负
			// 正		正		负		负
			// 非：
			// 负		正
			// 〇		〇
			// 正		负

			Boolean? 的_输出 = default;
			Boolean? 的_容器 = default;
			Boolean 左_容器 = default;
			Boolean 右_容器 = default;


			// 处理〇的符号
			if(源正号_左_输入 == default || 源正号_右_输入 == default)
			{
				if(运算符_输入 == 位运算.与)
				{
					// 占位
					//的_容器 = default;
				}
				else if(运算符_输入 == 位运算.或)
				{
					if(源正号_左_输入 == default && 源正号_右_输入 == default)
					{
						// 占位
						//的_容器 = default;
					}
					else
					{
						if(源正号_左_输入 == true || 源正号_右_输入 == true)
						{
							的_容器 = false;
						}
						else		// 源正号_左_输入 == false || 源正号_右_输入 == false的情况
						{
							的_容器 = true;
						}
					}
				}
				else if(运算符_输入 == 位运算.异或)
				{
					if(源正号_左_输入 == default && 源正号_右_输入 == default)
					{
						// 占位
						//的_容器 = default;
					}
					else
					{
						if(源正号_左_输入 == true || 源正号_右_输入 == true)
						{
							的_容器 = false;
						}
						else		// 源正号_左_输入 == false || 源正号_右_输入 == false的情况
						{
							的_容器 = true;
						}
					}
				}
				else if(运算符_输入 == 位运算.同或)		// 同或的〇全是特殊值情况，不存在规整的规则
				{
					if(源正号_左_输入 == false || 源正号_右_输入 == false)
					{
						的_容器 = false;
					}
					else		// 任意1者为正数、〇的情况
					{
						的_容器 = true;
					}
				}
				else		// 理论上应该不存在
							// 因为位运算.非是单目运算符
				{
					// 占位
				}
			}
			else		// 普通数值 + 无穷的情况：有正|负号的情况
			{
				左_容器 = !源正号_左_输入.Value;		// 2种相反的逻辑转换
				右_容器 = !源正号_右_输入.Value;		// 2种相反的逻辑转换

				if(运算符_输入 == 位运算.与)
				{
					的_容器 = 左_容器 && 右_容器;
				}
				else if(运算符_输入 == 位运算.或)
				{
					的_容器 = 左_容器 || 右_容器;
				}
				else if(运算符_输入 == 位运算.异或)
				{
					的_容器 = (左_容器 != 右_容器) ? true : false;
				}
				else if(运算符_输入 == 位运算.同或)
				{
					的_容器 = (左_容器 == 右_容器) ? true : false;
				}
				else		// 理论上应该不存在
							// 因为位运算.非是单目运算符
				{
					// 占位
				}
			}

			// 〇不变，余下颠
			if(的_容器 == default)
			{
				// 占位
				//的_输出 = default;
			}
			else
			{
				的_输出 = !的_容器.Value;
			}

			return 的_输出;
		}

		public static BigInteger operator |(BigInteger 源数_左_输入, BigInteger 源数_右_输入) => 或(源数_左_输入, 源数_右_输入);
		public static BigInteger 或_补码负数(BigInteger 源数_左_输入, BigInteger 源数_右_输入) => 或(源数_左_输入, 源数_右_输入, true);		// 即常见的通行版
		public static BigInteger 或_原码负数(BigInteger 源数_左_输入, BigInteger 源数_右_输入) => 或(源数_左_输入, 源数_右_输入);

		// Is补码_输入即Is数值化符号_输入
		public static BigInteger 或(BigInteger 源数_左_输入, BigInteger 源数_右_输入, Boolean Is数值化符号_输入 = default)
		{
			// 特殊情况处理
			if(源数_左_输入.Is无穷)
			{
				return 源数_左_输入;
			}
			else if(源数_右_输入.Is无穷)
			{
				return 源数_右_输入;
			}
			else
			{
				// 占位
			}

			BigInteger 的_输出 = default;
			Boolean 容器_左 = !(源数_左_输入.正号 == false) || Is数值化符号_输入 == default;		// 当是数值化符号的时候，再讨论是否是负数
			Boolean 容器_右 = !(源数_右_输入.正号 == false) || Is数值化符号_输入 == default;		// 当是数值化符号的时候，再讨论是否是负数
			Boolean 符号容器 = default;
			Int64 长度 = Max(ToInt64(源数_左_输入.长度), ToInt64(源数_右_输入.长度));
			UInt64[] 数容器_左 = 等长化_核心(源数_左_输入.数值组, 长度, true);
			UInt64[] 数容器_右 = 等长化_核心(源数_右_输入.数值组, 长度, true);

			// 符号
			的_输出.正号 = 符号逻辑运算(源数_左_输入.正号, 源数_右_输入.正号, 位运算.或);
			符号容器 = !(的_输出.正号 == false) || Is数值化符号_输入 == default;		// 当是数值化符号的时候，再讨论是否是负数

			// 数值
			// 预处理
			数容器_左 = 容器_左 ? 数容器_左 : 求2的补码_核心(数容器_左);
			数容器_右 = 容器_右 ? 数容器_右 : 求2的补码_核心(数容器_右);
			//
			的_输出.数值组 = 或_核心(数容器_左, 数容器_右);
			的_输出.数值组 = 符号容器 ? 的_输出.数值组 : 求2的补码_核心(的_输出.数值组);
			的_输出.数值组 = Trim(的_输出.数值组);

			// 终处理
			// 处理特殊值产生的〇的正号
			if(Is0(的_输出.数值组))
			{
				的_输出.正号 = default;
			}
			else
			{
				// 占位
			}

			return 的_输出;
		}

		private static UInt64[] 或_核心(UInt64[] 源数_左_输入, UInt64[] 源数_右_输入)
		{
			Int64 长度 = Max(源数_左_输入.Length, 源数_右_输入.Length);
			UInt64[] 的_输出 = new UInt64[长度];

			for(Int32 索引 = default; 索引 <= ToInt32(〇索引化(长度)); 索引++)
			{
				的_输出[索引] = 源数_左_输入[索引] | 源数_右_输入[索引];
			}

			return 的_输出;
		}

		public static BigInteger operator ^(BigInteger 源数_左_输入, BigInteger 源数_右_输入) => 异或(源数_左_输入, 源数_右_输入);
		public static BigInteger 异或_补码负数(BigInteger 源数_左_输入, BigInteger 源数_右_输入) => 异或(源数_左_输入, 源数_右_输入, true);		// 即常见的通行版
		public static BigInteger 异或_原码负数(BigInteger 源数_左_输入, BigInteger 源数_右_输入) => 异或(源数_左_输入, 源数_右_输入);

		// Is补码_输入即Is数值化符号_输入
		public static BigInteger 异或(BigInteger 源数_左_输入, BigInteger 源数_右_输入, Boolean Is数值化符号_输入 = default)
		{
			// 特殊情况处理
			if(源数_左_输入.Is无穷)
			{
				return 源数_左_输入;
			}
			else if(源数_右_输入.Is无穷)
			{
				return 源数_右_输入;
			}
			else
			{
				// 占位
			}

			BigInteger 的_输出 = default;
			Boolean 容器_左 = !(源数_左_输入.正号 == false) || Is数值化符号_输入 == default;		// 当是数值化符号的时候，再讨论是否是负数
			Boolean 容器_右 = !(源数_右_输入.正号 == false) || Is数值化符号_输入 == default;		// 当是数值化符号的时候，再讨论是否是负数
			Boolean 符号容器 = default;
			Int64 长度 = Max(ToInt64(源数_左_输入.长度), ToInt64(源数_右_输入.长度));
			UInt64[] 数容器_左 = 等长化_核心(源数_左_输入.数值组, 长度, true);
			UInt64[] 数容器_右 = 等长化_核心(源数_右_输入.数值组, 长度, true);

			// 符号
			的_输出.正号 = 符号逻辑运算(源数_左_输入.正号, 源数_右_输入.正号, 位运算.异或);
			符号容器 = !(的_输出.正号 == false) || Is数值化符号_输入 == default;		// 当是数值化符号的时候，再讨论是否是负数

			// 数值
			// 预处理
			数容器_左 = 容器_左 ? 数容器_左 : 求2的补码_核心(数容器_左);
			数容器_右 = 容器_右 ? 数容器_右 : 求2的补码_核心(数容器_右);
			//
			的_输出.数值组 = 异或_核心(数容器_左, 数容器_右);
			的_输出.数值组 = 符号容器 ? 的_输出.数值组 : 求2的补码_核心(的_输出.数值组);
			的_输出.数值组 = Trim(的_输出.数值组);

			// 终处理
			// 处理特殊值产生的〇的正号
			if(Is0(的_输出.数值组))
			{
				的_输出.正号 = default;
			}
			else
			{
				// 占位
			}

			return 的_输出;
		}

		private static UInt64[] 异或_核心(UInt64[] 源数_左_输入, UInt64[] 源数_右_输入)
		{
			Int64 长度 = Max(源数_左_输入.Length, 源数_右_输入.Length);
			UInt64[] 的_输出 = new UInt64[长度];

			for(Int32 索引 = default; 索引 <= ToInt32(〇索引化(长度)); 索引++)
			{
				的_输出[索引] = 源数_左_输入[索引] ^ 源数_右_输入[索引];
			}

			return 的_输出;
		}
		public static BigInteger 同或_补码负数(BigInteger 源数_左_输入, BigInteger 源数_右_输入) => 同或(源数_左_输入, 源数_右_输入, true);		// 即常见的通行版
		public static BigInteger 同或_原码负数(BigInteger 源数_左_输入, BigInteger 源数_右_输入) => 同或(源数_左_输入, 源数_右_输入);

		// Is补码_输入即Is数值化符号_输入
		private static BigInteger 同或(BigInteger 源数_左_输入, BigInteger 源数_右_输入, Boolean Is数值化符号_输入 = default)
		{
			// 特殊情况处理
			if(源数_左_输入.Is无穷)
			{
				return 源数_左_输入;
			}
			else if(源数_右_输入.Is无穷)
			{
				return 源数_右_输入;
			}
			else
			{
				// 占位
			}

			BigInteger 的_输出 = default;
			Boolean 容器_左 = !(源数_左_输入.正号 == false) || Is数值化符号_输入 == default;		// 当是数值化符号的时候，再讨论是否是负数
			Boolean 容器_右 = !(源数_右_输入.正号 == false) || Is数值化符号_输入 == default;		// 当是数值化符号的时候，再讨论是否是负数
			Boolean 符号容器 = default;
			Int64 长度 = Max(ToInt64(源数_左_输入.长度), ToInt64(源数_右_输入.长度));
			UInt64[] 数容器_左 = 等长化_核心(源数_左_输入.数值组, 长度, true);
			UInt64[] 数容器_右 = 等长化_核心(源数_右_输入.数值组, 长度, true);

			// 符号
			的_输出.正号 = 符号逻辑运算(源数_左_输入.正号, 源数_右_输入.正号, 位运算.同或);
			符号容器 = !(的_输出.正号 == false) || Is数值化符号_输入 == default;		// 当是数值化符号的时候，再讨论是否是负数

			// 数值
			// 预处理
			数容器_左 = 容器_左 ? 数容器_左 : 求2的补码_核心(数容器_左);
			数容器_右 = 容器_右 ? 数容器_右 : 求2的补码_核心(数容器_右);
			//
			的_输出.数值组 = 同或_核心(数容器_左, 数容器_右);
			的_输出.数值组 = 符号容器 ? 的_输出.数值组 : 求2的补码_核心(的_输出.数值组);
			的_输出.数值组 = Trim(的_输出.数值组);

			// 终处理
			// 处理特殊值产生的〇的正号
			if(Is0(的_输出.数值组))
			{
				的_输出.正号 = default;
			}
			else
			{
				// 占位
			}

			return 的_输出;
		}
		// 视为按位取反
		public static BigInteger operator ~(BigInteger 源数_输入_输出) => 按位取反(源数_输入_输出, true);
		public static BigInteger 按位取反_自定版(BigInteger 源数_输入_输出) => 按位取反(源数_输入_输出);
		//
		// 实际的范围是64 × (2^32 − 1)，UInt64[2]能全容下，但只允许Int32的值作输入
		public static BigInteger operator <<(BigInteger 源数_输入, Int32 移位数_输入) => 左移位(源数_输入, 移位数_输入);

		// 实际的范围是64 × (2^32 − 1)，UInt64[2]能全容下，但只允许Int32的值作输入
		public static BigInteger operator >>(BigInteger 源数_输入, Int32 移位数_输入) => 右移位(源数_输入, 移位数_输入);

		public static BigInteger operator +(BigInteger 源数_输入) => 源数_输入;
		// 视为符号|整体求反
		public static BigInteger operator -(BigInteger 源数_输入_输出) => 求相反数(源数_输入_输出);
		//
		public static BigInteger operator ++(BigInteger 源数_输入) => 源数_输入 + 正一;
		public static BigInteger operator --(BigInteger 源数_输入) => 源数_输入 - 正一;
		//
		//
		// 加法pattern表
		// 符号					和
		// 加数		加数		符号		数值
		// +			+			传+		加
		// +			−			从大		转减
		// −			+			从大		转减
		// −			−			传−		加
		// 加法的实现：
		// 1. 从头使用位逻辑运算构建进位加法器
		// 2. 使用既成的无符号64位整型的加法 √
		public static BigInteger operator +(BigInteger 加数_左_输入, BigInteger 加数_右_输入) => 加(加数_左_输入, 加数_右_输入);
		// 减法pattern表
		// 符号						数值						差
		// 被减数		减数		被减数		减数		符号		数值
		// +				+			大				小			传+		直减
		// +				+			小				大			造−		反减
		// +				−			大				小			传+		转加
		// +				−			小				大			传+		转加
		// −				+			大				小			传−		转加
		// −				+			小				大			传−		转加
		// −				−			大				小			传−		直减
		// −				−			小				大			造+		反减
		// 减法的实现：
		// 1. 从头使用位逻辑运算构建进位减法器
		// 2. 使用既成的无符号64位整型的减法 √（部分可化为加法的转给了加法）
		// 3. 使用加法的逆运算，但需要涉及模的大小及设计
		public static BigInteger operator -(BigInteger 被减数_输入, BigInteger 减数_输入) => 减(被减数_输入, 减数_输入);
		// 乘法的实现：
		// 1. 从头使用位逻辑运算构建进位乘法器
		// 2. 使用既成的无符号64位整型的乘法 √
		// 3. 使用乘法的数学定义：加法的累加，主要有迭代、递归2种
		public static BigInteger operator *(BigInteger 因数_左_输入, BigInteger 因数_右_输入) => 乘(因数_左_输入, 因数_右_输入);
		// 除法的实现：
		// 1. 从头使用位逻辑运算构建进位除法器
		// 2. 使用既成的无符号64位整型的除法
		// 3. 使用乘法的逆运算，但需要涉及模的大小及设计
		// 4. 使用除法的数学定义：减法的累减，主要有迭代、递归2种
		// 5. 使用试乘|试除
		// 除法分情况处理的主要原因是：资源占用大、计算慢，故能简化就简化处理
		public static BigInteger operator /(BigInteger 被除数_输入, BigInteger 除数_输入)
		{
			(BigInteger 商, _)  = 除以(被除数_输入, 除数_输入);

			return 商;
		}
		//
		// 考虑和除法合用同样的运算内核
		public static BigInteger operator %(BigInteger 源数_左_输入, BigInteger 源数_右_输入) => 求余数(源数_左_输入, 源数_右_输入);
		// 比较的核心功能之一
		public static Boolean operator >(BigInteger 源数_左_输入, BigInteger 源数_右_输入) => 大于(源数_左_输入, 源数_右_输入);
		public static Boolean operator <(BigInteger 源数_左_输入, BigInteger 源数_右_输入) => 小于(源数_左_输入, 源数_右_输入);		// 相当于小于号是交换了左右的大于号
		// 以≥即≮的思路实现的
		public static Boolean operator >=(BigInteger 源数_左_输入, BigInteger 源数_右_输入) => 大于等于(源数_左_输入, 源数_右_输入);
		// 以≤即≯的思路实现的
		public static Boolean operator <=(BigInteger 源数_左_输入, BigInteger 源数_右_输入) => 小于等于(源数_左_输入, 源数_右_输入);
		public static Boolean operator ==(BigInteger 源数_左_输入, BigInteger 源数_右_输入) => 相等于(源数_左_输入, 源数_右_输入);
		public static Boolean operator !=(BigInteger 源数_左_输入, BigInteger 源数_右_输入) => 不等于(源数_左_输入, 源数_右_输入);

		#region 类型转换
		// 类型转换
		#region 可隐形类型转换|赋值等号，“=”
		// 可隐形类型转换|赋值等号，“=”

		// T→大整数，即固化版本、特定调用形式化的构造器
		public static implicit operator BigInteger(SByte 源数_输入) => 初始化(源数_输入);
		public static implicit operator BigInteger(Byte 源数_输入) => 初始化(源数_输入);
		public static implicit operator BigInteger(Int16 源数_输入) => 初始化(源数_输入);
		public static implicit operator BigInteger(UInt16 源数_输入) => 初始化(源数_输入);
		public static implicit operator BigInteger(Int32 源数_输入) => 初始化(源数_输入);
		public static implicit operator BigInteger(UInt32 源数_输入) => 初始化(源数_输入);
		public static implicit operator BigInteger(Int64 源数_输入) => 初始化(源数_输入);
		// 兼含有UInt32、UInt16、Char（部分）、Byte输入版
		public static implicit operator BigInteger(UInt64 源数_输入) => 初始化(源数_输入);
		//
		public static implicit operator BigInteger(SByte[] 源数_输入) => 初始化(源数_输入);
		public static implicit operator BigInteger(Byte[] 源数_输入) => 初始化(源数_输入);
		public static implicit operator BigInteger(Int16[] 源数_输入) => 初始化(源数_输入);
		public static implicit operator BigInteger(UInt16[] 源数_输入) => 初始化(源数_输入);
		public static implicit operator BigInteger(Int32[] 源数_输入) => 初始化(源数_输入);
		public static implicit operator BigInteger(UInt32[] 源数_输入) => 初始化(源数_输入);
		public static implicit operator BigInteger(Int64[] 源数_输入) => 初始化(源数_输入);
		public static implicit operator BigInteger(UInt64[] 源数_输入) => 初始化(源数_输入);
		//
		public static implicit operator BigInteger(Char[] 含符号数_输入) => 初始化(含符号数_输入, default, 进制.十进制, 分隔符.空格);
		public static implicit operator BigInteger(String 含符号数_输入) => 初始化(含符号数_输入, default, 进制.十进制, 分隔符.空格);
		#endregion

		#region 强制类型转换
		// 强制类型转换
		// T→大整数
		public static explicit operator BigInteger(Single 源数_输入) => 初始化(源数_输入);
		public static explicit operator BigInteger(Double 源数_输入) => 初始化(源数_输入);
		public static explicit operator BigInteger(Decimal 源数_输入) => 初始化(源数_输入);

		// 大整数→T
		public static explicit operator SByte(BigInteger 源数_输入) => 转SByte(源数_输入);
		public static explicit operator Byte(BigInteger 源数_输入) => 转Byte(源数_输入);
		public static explicit operator Int16(BigInteger 源数_输入) => 转Int16(源数_输入);
		public static explicit operator UInt16(BigInteger 源数_输入) => 转UInt16(源数_输入);
		public static explicit operator Int32(BigInteger 源数_输入) => 转Int32(源数_输入);
		public static explicit operator UInt32(BigInteger 源数_输入) => 转UInt32(源数_输入);
		public static explicit operator Int64(BigInteger 源数_输入) => 转Int64(源数_输入);
		public static explicit operator UInt64(BigInteger 源数_输入) => 转UInt64(源数_输入);
		//
		public static explicit operator Single(BigInteger 源数_输入) => (Single)转Double(源数_输入);
		public static explicit operator Double(BigInteger 源数_输入) => 转Double(源数_输入);
		public static explicit operator Decimal(BigInteger 源数_输入) => 转Decimal(源数_输入);
		//
		// 默认不启用is大端、启用is补码
		public static explicit operator SByte[](BigInteger 源数_输入) => 源数_输入.转数组<SByte>();
		public static explicit operator Byte[](BigInteger 源数_输入) => 源数_输入.转数组<Byte>();
		public static explicit operator Int16[](BigInteger 源数_输入) => 源数_输入.转数组<Int16>();
		public static explicit operator UInt16[](BigInteger 源数_输入) => 源数_输入.转数组<UInt16>();
		public static explicit operator Int32[](BigInteger 源数_输入) => 源数_输入.转数组<Int32>();
		public static explicit operator UInt32[](BigInteger 源数_输入) => 源数_输入.转数组<UInt32>();
		public static explicit operator Int64[](BigInteger 源数_输入) => 源数_输入.转数组<Int64>();
		public static explicit operator UInt64[](BigInteger 源数_输入) => 源数_输入.转数组<UInt64>();

		public static explicit operator Char(BigInteger 源数_输入) => 转Char(源数_输入);
		//
		public static explicit operator Char[](BigInteger 源数_输入) => 转字符组(源数_输入);
		//
		public static explicit operator String(BigInteger 源数_输入) => 转字符串(源数_输入);
		#endregion
		#endregion
		#endregion
	}
}