using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;

namespace BigInteger
{
	// 选用结构（structure）的原因：默认传值赋值、无需照看无参构造器、位于栈空间（Stack）、自带sealed标签
	public partial struct BigInteger : IFormattable, IComparable, IComparable<BigInteger>, IEquatable<BigInteger>
	{
		#region 实现
		// 实现
		#region 接口
		// 接口的函数
		Int32 IComparable.CompareTo(Object 被比数_输入)
		{
			BigInteger  被比数 = default;

			被比数 = (BigInteger)被比数_输入;

			Int32 的_输出 = default;

			if(this > 被比数)
			{
				的_输出 = 1;
			}
			else if(this < 被比数)
			{
				的_输出 = -1;
			}
			else
			{
				// 占位
				//的_输出 = default;
			}

			return 的_输出;
		}

		Int32 IComparable<BigInteger>.CompareTo(BigInteger 被比数_输入)
		{
			Int32 的_输出 = default;

			if(this > 被比数_输入)
			{
				的_输出 = 1;
			}
			else if(this < 被比数_输入)
			{
				的_输出 = -1;
			}
			else
			{
				// 占位
				//的_输出 = default;
			}

			return 的_输出;
		}
		Boolean IEquatable<BigInteger>.Equals(BigInteger 被比数_输入) => 相等于(this, 被比数_输入);

		String IFormattable.ToString(String 格式, IFormatProvider 格式源) => throw new NotImplementedException();
		#endregion

		#region 一般
		// 函数
		public override String ToString() => 转字符串();
		public override Boolean Equals(Object 被比数_输入)
		{
			// 异常情况处理
			if(被比数_输入 is BigInteger == false)
			{
				return false;
			}
			else
			{
				// 占位
			}

			return 相等于(this, (BigInteger)被比数_输入);
		}
		public override Int32 GetHashCode()
		{
			// ！未考虑异常情况

			UInt64 散列 = default;

			if(Is未定义 | Is无穷)
			{
				// 占位
				//散列 = default;
			}
			else
			{
				for(Int32 索引 = ToInt32(长度); --索引 >= 0; )
				{
					散列 = ((散列 << 14) | (散列 >> 50)) ^ 数值组[索引];		// ！混淆用的位数硬编码待处理|优化，原值是7、25，这里直接 × 2处理后使用
				}
			}

			return ToInt32(散列);
		}
		#endregion
		#endregion

		#region 枚举
		// 枚举
		public enum 进制 : Byte
		{
			二进制 = 2,
			十六进制 = 16,
			泛二进制 = 二进制 | 十六进制,		// ！需要参考C++的“联合枚举量”进行设计

			十进制 = 10,
			二十进制 = 20,
			泛十进制 = 十进制 | 二十进制
		}

		public enum 分隔符 : Byte
		{
			无,		// 空串|空字符
			空格,		// 指：Space，U+0020
			撇号,		// 指：Apostrophe，U+0027
			逗号,		// 指：Comma，U+002C
			连词符,		// 指：Hyphen-Minus，U+002D
			点号,		// 指：Full Stop，U+002E
			下划线		// 指：Low Line，U+005F
		}

		public enum 符号 : SByte
		{
			负 = -1,		// 代表false
			〇 = default,		// 代表null
			正 = 1,		// 代表true
			正负 = 2,		// 代表±
			默认		// ？对应数值是多少
		}

		public enum 位运算 : Byte
		{
			与,
			或,
			异或,
			同或,
			非
		}

		public enum 算术运算 : Byte
		{
			加,
			减,
			乘,
			除,		// 实为“除以”
			整除,		// 实为“整除以”
			余,		// 用于“同余”|Mod运算、含余数除法
			乘方,
			余方,		// ModPow()的个人翻译：求同余+乘方之省
			开方,
			对数
		}
		#endregion

		#region 成员
		#region 常量
		// 常量
		// 掩码
		private const UInt64 四字掩码 = 0X_FFFF_FFFF_FFFF_FFFF;
		private const UInt32 双字掩码 = 0X_FFFF_FFFF;		// 0B 1111 1111 1111 1111 1111 1111 1111 1111
																						// ！2019.06.19，可由0X 1 0000 0000 0000 0000 0000 0000 0000 0000的〇索引化()得到，0X 1 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000可由1 << 32得到，32可由常数定义
		private const UInt16 字掩码 = 0X_FFFF;		// 0B 1111 1111 1111 1111
																			// ！2019.06.19，可由0X 1 0000 0000 0000 0000的〇索引化()得到，0X 1 0000 0000 0000 0000可由1 << 16得到，16可由常数定义
		private const Byte 字节掩码 = 0B_1111_1111;		// 0B 1111 1111
																					// ！2019.06.19，可由0X 1 0000 0000的〇索引化()得到，0X 1 0000 0000可由1 << 8得到，8可由常数定义
		private const Byte 四位掩码 = 0B_1111;		// 0B 1111
																			// ！2019.06.19，可由0X 1 0000的〇索引化()得到，0X 1 0000可由1 << 4得到，4可由常数定义
		private const Byte 双位掩码 = 0B_11;		// 0B 11
																		// ！2019.06.19，可由0X 100的〇索引化()得到，0X 100可由1 << 2得到，2可由常数定义
		private const Byte 位掩码 = 0B_01;		// 0B 1
																	// ！2019.06.19，可由0X 10的〇索引化()得到，0X 10可由1 << 1得到，1可由常数定义
		//
		// 移位用位数
		private const Byte 四字位数 = 64;
		private const Byte 双字位数 = 32;
		private const Byte 字位数 = 16;
		private const Byte 字节位数 = 8;
		private const Byte 四位数 = 4;
		private const Byte 双位数 = 2;
		private const Byte 位数 = 1;
		//
		// 十六进制 → 二进制位数
		private const Byte 十六进率 = 4;
		#endregion

		#region 属性
		// 属性
		// 供便捷判断之用
		public static BigInteger 未定义 => new BigInteger(@$"Undefined");		// 超域情况的处理
		public static BigInteger 正无穷 => new BigInteger(@$"+∞");		// 写作正无穷更为对称
		public static BigInteger 负无穷 => new BigInteger(@$"−∞");
		public static BigInteger 〇 => new BigInteger(default(UInt64));		// 默认都是UInt64
																											// 合并了正〇、负〇
		public static BigInteger 正一 => new BigInteger(+一索引化(default));		// 写作正一为对称
		public static BigInteger 负一 => new BigInteger(-一索引化(default));		// 也可以以new 大整数(-正一)，但那样就有了依赖性

		// 真·成员
		// 有且仅有2种情况
		private Boolean 定义 => Is无穷 | (数值组 != default);		// Boolean型成员之用
																							// 既然是只读，又有Is未定义，故直接private
		// 有且仅有2种情况
		public Boolean 无穷		// Boolean型成员之用
		{
			get;
			set;
		}
		public Boolean? 正号		// 本身Boolean一共2种值，但会出现〇的符号取舍问题，为了和数学定义更加统一，故用Boolean?
		{
			get;
			set;
		}
		public UInt64[] 数值组
		{
			get;
			set;
		}
		public UInt64 长度 => 求非〇值Cell长(数值组);		// 最大长度为Integer.Ceiling(log(2^64)) × 2^32 ≈ 20 × 2^32 = 858 9934 5920，∈ (UInt32.MaxValue, UInt64.MaxValue)
																					// 长度 ∈ [0, +∞)

		// UInt64足够，因二进制下总位数为：(2^32 − 1) × 64 < 2^64 − 1
		public UInt64 数位数 => 求位数(数值组);

		private UInt64 求位数(BigInteger 源_输入, 进制 进制_输入 = 进制.二进制)
		{
			if(源_输入.Is未定义)
			{
				throw new Exception("Definition Error");
			}
			else
			{
				// 占位
			}
			//
			if(源_输入.Is无穷)
			{
				throw new OverflowException();
			}
			else
			{
				// 占位
			}

			UInt64 位数_输出 = default;
			UInt64 最高Cell容器 = 源_输入.数值组[^ToInt32(一索引化(default))];
			UInt64 长度 = 源_输入.长度;

			if(进制_输入 == 进制.十进制)
			{
				位数_输出 = ToUInt64(求绝对值(源_输入).ToString().Length);		// 求绝对值为了正数化，正数化为了String长度中不会出现符号的占用
			}
			else if((进制_输入 == 进制.二进制) | (进制_输入 == 进制.十六进制))
			{
				位数_输出 += 长度 * 四字位数;
				位数_输出 -= 高位始空白〇值计数(最高Cell容器);

				if(进制_输入 == 进制.十六进制)
				{
					位数_输出 = Ceiling倍数化除以(位数_输出, 十六进率);
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

			return 位数_输出;
		}

		//
		// 判断之用
		public Boolean Is未定义 => this == 未定义;
		public Boolean Is无穷 => this == 正无穷 || this == 负无穷;
		public Boolean Is〇 => Is0_纯粹版(数值组);
		public Boolean Is一 => this == 正一 || this == 负一;
		//
		public Boolean Is奇数 => !Is偶数_核心();
		public Boolean Is偶数 => Is偶数_核心();
		//
		public Boolean Is正数 => Is正数_核心();
		public Boolean Is真正数 => Is正数_核心(default);
		public Boolean Is负数 => !Is正数_核心();		// 负数即真·负数，绝对不含〇
		#endregion
		#endregion

		#region 构造器
		// 构造器
		// ∈ [−求和(i = 1 ~ (2^32 − 1), (2^64 − 1) × 10^i), 求和(i = 1 ~ (2^32 − 1), (2^64 − 1) × 10^i)]

		// 2019.06.22，！考虑为BigInteger设置构造器
		// 2种思路：
		// 1.设立欺骗性namespace，骗取BigInteger的sign、bits，转为public直接转UInt64[]
		// 2.使用BigInteger和大整数共认的类型：ToByteArray()（8分之1的容量）、ToString()（十六进制版为16分之1的容量）、获取ToUint32[]（2分之1的容量）

		public BigInteger(SByte 源数_输入)
		{
			// 预处理
			this = default;

			// 后续处理
			this = 初始化(源数_输入);
		}
		public BigInteger(Byte 源数_输入)
		{
			// 预处理
			this = default;

			// 后续处理
			this = 初始化(源数_输入);
		}

		public BigInteger(Int16 源数_输入)
		{
			// 预处理
			this = default;

			// 后续处理
			this = 初始化(源数_输入);
		}
		public BigInteger(UInt16 源数_输入)
		{
			// 预处理
			this = default;

			// 后续处理
			this = 初始化(源数_输入);
		}

		public BigInteger(Int32 源数_输入)
		{
			// 预处理
			this = default;

			// 后续处理
			this = 初始化(源数_输入);
		}
		public BigInteger(UInt32 源数_输入)
		{
			// 预处理
			this = default;

			// 后续处理
			this = 初始化(源数_输入);
		}

		// ∈ [−2^63, 2^63 − 1]
		// 内含：负数补码、正数原码，首位作数值符号用
		public BigInteger(Int64 源数_输入)
		{
			// 预处理
			this = default;

			// 后续处理
			this = 初始化(源数_输入);
		}

		// ∈ [0, 2^64 − 1]
		public BigInteger(UInt64 源数_输入)
		{
			// 预处理
			this = default;

			// 后续处理
			this = 初始化(源数_输入);
		}
		//
		// 即Decimal128版
		public BigInteger(Decimal 源数_输入)
		{
			// 预处理
			this = default;

			this = 初始化(源数_输入);
		}
		// 兼含有Single|Float输入版
		public BigInteger(Double 源数_输入)
		{
			// 预处理
			this = default;

			this = 初始化(源数_输入);
		}
		//
		public BigInteger(符号 正号_输入, SByte[] 源数_输入, Boolean Is右向_输入 = default)
		{
			// 预处理
			this = default;

			// 后续处理
			this = 初始化(源数_输入, Is右向_输入, 正号_输入);
		}
		// 补码专版
		public BigInteger(SByte[] 源数_输入, Boolean Is右向_输入 = default)
		{
			// 预处理
			this = default;		// ！仍无法省略

			this = 初始化(源数_输入, Is右向_输入);
		}
		public BigInteger(符号 正号_输入, Byte[] 源数_输入, Boolean Is右向_输入 = default)
		{
			// 预处理
			this = default;

			// 后续处理
			this = 初始化(源数_输入, Is右向_输入, 正号_输入);
		}
		public BigInteger(Byte[] 源数_输入, Boolean Is右向_输入 = default)
		{
			// 预处理
			this = default;

			this = 初始化(源数_输入, Is右向_输入);
		}
		//
		public BigInteger(符号 正号_输入, Int16[] 源数_输入, Boolean Is右向_输入 = default)
		{
			// 预处理
			this = default;

			// 后续处理
			this = 初始化(源数_输入, Is右向_输入, 正号_输入);
		}
		// 补码专版
		public BigInteger(Int16[] 源数_输入, Boolean Is右向_输入 = default)
		{
			// 预处理
			this = default;

			this = 初始化(源数_输入, Is右向_输入);
		}
		public BigInteger(符号 正号_输入, UInt16[] 源数_输入, Boolean Is右向_输入 = default)
		{
			// 预处理
			this = default;

			// 后续处理
			this = 初始化(源数_输入, Is右向_输入, 正号_输入);
		}
		// 补码专版
		public BigInteger(UInt16[] 源数_输入, Boolean Is右向_输入 = default)
		{
			// 预处理
			this = default;

			this = 初始化(源数_输入, Is右向_输入);
		}
		//
		public BigInteger(符号 正号_输入, Int32[] 源数_输入, Boolean Is右向_输入 = default)
		{
			// 预处理
			this = default;

			// 后续处理
			this = 初始化(源数_输入, Is右向_输入, 正号_输入);
		}
		// 补码专版
		public BigInteger(Int32[] 源数_输入, Boolean Is右向_输入 = default)
		{
			// 预处理
			this = default;

			this = 初始化(源数_输入, Is右向_输入);
		}
		public BigInteger(符号 正号_输入, UInt32[] 源数_输入, Boolean Is右向_输入 = default)
		{
			// 预处理
			this = default;

			// 后续处理
			this = 初始化(源数_输入, Is右向_输入, 正号_输入);
		}
		// 补码专版
		public BigInteger(UInt32[] 源数_输入, Boolean Is右向_输入 = default)
		{
			// 预处理
			this = default;

			this = 初始化(源数_输入, Is右向_输入);
		}
		//
		public BigInteger(符号 正号_输入, Int64[] 源数_输入, Boolean Is右向_输入 = default)
		{
			// 预处理
			this = default;

			this = 初始化(源数_输入, Is右向_输入, 正号_输入);
		}
		// 补码专版
		public BigInteger(Int64[] 源数_输入, Boolean Is右向_输入 = default)
		{
			// 预处理
			this = default;

			this = 初始化(源数_输入, Is右向_输入);
		}
		// 原码、额外符号、可右向|大端
		public BigInteger(符号 正号_输入, UInt64[] 源数_输入, Boolean Is右向_输入 = default)
		{
			// 预处理
			this = default;

			this = 初始化(源数_输入, Is右向_输入, 正号_输入, false);
		}
		// 补码专版
		public BigInteger(UInt64[] 源数_输入, Boolean Is右向_输入 = default)
		{
			// 预处理
			this = default;

			this = 初始化(源数_输入, Is右向_输入);
		}
		//
		public BigInteger(Char 源数_输入, Boolean Is字符_输入 = true)
		{
			// 预处理
			this = default;

			// 后续处理
			this = 初始化(源数_输入, Is字符_输入);
		}
		//
		public BigInteger(符号 符号_输入, Char[] 源数_输入, Boolean Is右向_输入 = default, 分隔符 分隔符_输入 = 分隔符.空格, 进制 进制_输入 = 进制.十进制)
		{
			// 预处理
			this = default;

			this = 初始化(源数_输入, Is右向_输入, 进制_输入, 分隔符_输入, 符号_输入);
		}
		// 锁定为：数值化符号、类·二进制、补码
		public BigInteger(Char[] 类二进制数_输入, 进制 进制_输入, Boolean Is右向_输入 = default, 分隔符 分隔符_输入 = 分隔符.空格)
		{
			// 预处理
			this = default;

			this = 初始化(类二进制数_输入, Is右向_输入, 进制_输入, 分隔符_输入, 符号.默认, true);
		}

		// 需要添加Hover文本提示需要自备符号在String中
		public BigInteger(Char[] 含符号数_输入, Boolean Is右向_输入 = default, 分隔符 分隔符_输入 = 分隔符.空格, 进制 进制_输入 = 进制.十进制)
		{
			// 预处理
			this = default;

			this = 初始化(含符号数_输入, Is右向_输入, 进制_输入, 分隔符_输入);
		}
		//
		// ∵ String的容量是Char[]的容量，共有(2^32 − 1)位，除去符号位还有(2^32 − 2)位；而大整数的容量是(求对数(10, 2^64 − 1) × (2^32 − 1))，后者>>前者
		// ∴ ∈[0, 2^32 − 1)]
		public BigInteger(符号 符号_输入, String 源数_输入, Boolean Is右向_输入 = default, 分隔符 分隔符_输入 = 分隔符.空格, 进制 进制_输入 = 进制.十进制)
		{
			// 预处理
			this = default;

			this = 初始化(源数_输入, Is右向_输入, 进制_输入, 分隔符_输入, 符号_输入);
		}
		// 锁定为：数值化符号、类·二进制、补码
		public BigInteger(String 类二进制数_输入, 进制 进制_输入, Boolean Is右向_输入 = default, 分隔符 分隔符_输入 = 分隔符.空格)
		{
			// 预处理
			this = default;

			this = 初始化(类二进制数_输入, Is右向_输入, 进制_输入, 分隔符_输入, 符号.默认, true);
		}
		// 需要添加Hover文本提示需要自备符号在String中
		public BigInteger(String 含符号数_输入, Boolean Is右向_输入 = default, 分隔符 分隔符_输入 = 分隔符.空格, 进制 进制_输入 = 进制.十进制)
		{
			// 预处理
			this = default;

			this = 初始化(含符号数_输入, Is右向_输入, 进制_输入, 分隔符_输入);
		}
		#endregion

		private Boolean Is偶数_核心() => (数值组[default] & 0B_0001) == default;

		private Boolean Is正数_核心(Boolean Is包含〇_输入 = true)
		{
			Boolean 的_输出 = default;

			if(Is包含〇_输入)
			{
				的_输出 = 正号 != false;
			}
			else
			{
				的_输出 = 正号 == true;
			}

			return 的_输出;
		}
	}
}