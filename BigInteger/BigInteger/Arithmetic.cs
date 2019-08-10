using System;

namespace BigInteger
{
	public partial struct BigInteger
	{
		#region 算术运算
		// 算术运算
		#region 求对数
		// 求对数
		// 也可以叫Log2()
		public BigInteger LB() => 求以2为底的对数(this);
		public static BigInteger 求以2为底的对数(BigInteger 幂_输入)
		{
			if(幂_输入.Is未定义)
			{
				return 未定义;		// 测试方面认为返回Double.NaN，对应大整数.未定义
			}
			else
			{
				// 占位
			}

			if(幂_输入.Is无穷)
			{
				return 幂_输入;		// 无穷求对数后还是无穷
											// 测试方面认为返回Double.NaN，对应大整数.未定义
			}
			else
			{
				// 占位
			}

			if(幂_输入.Is〇)
			{
				return 未定义;
			}
			else
			{
				// 占位
			}

			if(幂_输入.Is负数)
			{
				return 未定义;		// ？或者是抛出异常
			}
			else
			{
				// 占位
			}

			return new BigInteger(〇索引化(ToInt64(幂_输入.数位数)));
		}
		public Double LB_Double版() => 求以2为底的对数_Double版(this);
		// 出处：https://math.stackexchange.com/questions/820094/what-is-the-best-way-to-calculate-log-without-a-calculator/1181355
		// 算法：
		// 1.计算真数到 < 2，得：真数·准小数部分
		// 2.计算真数·准小数部分^2，得：真数·运算用·小数部分
		// 3.计算真数·运算用·小数部分 ÷ 2 ≥ 2：是：当前小数位置1；否：当前小数位置0		// 这是二进制的特例，实际为 ÷ 基数^n，即最大可承受的幂，而二进制仅有2^0 = 1、2^1 = 2，故选用得也是2，与基数同
		// 4.精度是否达到需求：是：退出；否：小数位后移，执行步骤2
		public static Double 求以2为底的对数_Double版(BigInteger 幂_输入)
		{
			if(幂_输入.Is未定义)
			{
				return Double.NaN;		// 测试方面认为返回Double.NaN
			}
			else
			{
				// 占位
			}

			if(幂_输入.Is无穷)
			{
				return 幂_输入.Is正数 ? Double.PositiveInfinity : Double.NegativeInfinity;		// 无穷求对数后还是无穷
																																		// 测试方面认为返回Double.NaN
			}
			else
			{
				// 占位
			}

			if(幂_输入.Is〇)
			{
				return Double.NaN;		// ？或者是抛出异常
			}
			else
			{
				// 占位
			}

			if(幂_输入.Is负数)
			{
				return Double.NaN;		// ？或者是抛出异常
			}
			else
			{
				// 占位
			}

			return (Double)幂_输入.LB() + 求以2为底的对数_小数部分(幂_输入);
		}
		public BigInteger LN() => 求自然对数(this);
		public static BigInteger 求自然对数(BigInteger 幂_输入) => new BigInteger(幂_输入.LB_Double版() / 1.4426950408889634073599246810019D);		// 这是Log2(e)的值，可惜e没有Double的常量，？好像有Decimal的
		public Double LN_Double版() => 求自然对数_Double版(this);
		public static Double 求自然对数_Double版(BigInteger 幂_输入) => 幂_输入.LB_Double版() / 1.4426950408889634073599246810019D;			// 这是LB(e)的值
		// 也可以叫Log10()
		public BigInteger LG() => 求以10为底的对数(this);
		public static BigInteger 求以10为底的对数(BigInteger 幂_输入) => 幂_输入.Log(10);
		public Double LG_Double版() => 求以10为底的对数_Double版(this);
		public static Double 求以10为底的对数_Double版(BigInteger 幂_输入) => 幂_输入.Log_Double版(10);

		// 负数不可为底数、真数；0不可为底数、真数；1不可为底数。实际实现是由除法限制的，此时LB(1)的结果为0作为了除数，会导致输出未定义；余下则直接特例化处理
		// 求对数：		// ！待验证
		// 真\底			∞			−∞		1			−1		N			−N		0
		// ∞				∞			未			未			未			∞			未			未
		// −∞			未			未			未			未			未			未			未
		// 1				0			未			未			未			0			未			未
		// −1			未			未			未			未			未			未			未
		// N				0			未			未			未			泛N		未			未
		// −N			未			未			未			未			未			未			未
		// 0				未			未			未			未			未			未			未
		public BigInteger Log(BigInteger 底数_输入) => 求对数(this, 底数_输入);
		public static BigInteger 求对数(BigInteger 幂_输入, BigInteger 底数_输入) => new BigInteger(幂_输入.LB_Double版() / 底数_输入.LB_Double版());

		public Double Log_Double版(BigInteger 底数_输入) => 求对数_Double版(this, 底数_输入);
		public static Double 求对数_Double版(BigInteger 幂_输入, BigInteger 底数_输入) => 幂_输入.LB_Double版() / 底数_输入.LB_Double版();
		#endregion

		// ModPow()，即：a≡b mod n
		public static BigInteger 求幂余(BigInteger 底数_输入, BigInteger 指数_输入, BigInteger 除数_输入)
		{
			BigInteger 的_输出 = default;
			BigInteger 容器 = default;

			底数_输入.赋值(ref 容器);

			if(求余数(容器, 除数_输入) == 〇)		// 无余数
			{
				的_输出 = 正一;

				// 与商的符号一致
				的_输出.正号 = (容器 / 除数_输入).正号;
			}
			else
			{
				容器 = 求余数(容器, 除数_输入);
			}

			的_输出 = 求余数(求幂(容器, 指数_输入), 除数_输入);

			return 的_输出;
		}

		// 即乘方
		// 效率应该不高，考虑移植Core版的PowCore()，里面针对自乘、平方做了优化，被称为“平方求幂”的快速求幂算法。最新版已经弃用该方案，转而重新设计了乘法的调用方式，但都是标准乘法了
		public BigInteger 求幂(BigInteger 指数_输入) => 求幂(this, 指数_输入);
		public static BigInteger 求幂(BigInteger 底数_输入, BigInteger 指数_输入)
		{
			BigInteger 的_输出 = default;

			if(指数_输入.Is负数)
			{
				(的_输出, _) = 除以(正一, 求幂_核心(底数_输入, 求绝对值(指数_输入)));
			}
			else
			{
				的_输出 = 求幂_核心(底数_输入, 指数_输入);
			}

			return 的_输出;
		}

		// 截至Core 3.0 Preview 3，Math中的Pow()仍是COM导入的
		// 而追根下去，是C++写成的SSCLI中实现了该函数，继续溯源下去，是宏中定义了函数，并且实际是指定了JIT直接使用机器码|汇编模拟FPU进行直接计算，即无实际的函数体：https://stackoverflow.com/questions/8870442/how-is-math-pow-implemented-in-net-framework
		// Win 10计算器中的RatPack对开方的计算思路是直接使小数化的指数±0.5然后分别计算其幂再求Floor()最后以之处理为结果
		// 64位X86的CPU实现中，有支持SSE2的版本
		// GLibStdC++中以汇编实现的部分核心：fintrz%.x %1,%0 : =f(temp)（integer-valued float）: f(y)，还有部分是：±exp(y * log(±x))
		// exp()：fetox%.x %1,%0 : =f(value) : f(x)
		// log()：flogn%.x %1,%0 : =f(value) : f(x)
		// ？GLibC中的实现则是最终走了特殊的乘方
		// 自行设计：从平方数求起，逐层递减。？是否存在效率问题未测试、未论证
		// Newton法（首2项近似）：
		// https://en.wikipedia.org/wiki/Nth_root_algorithm
		// https://www.geeksforgeeks.org/n-th-root-number
		// https://rosettacode.org/wiki/Nth_root#C.23
		// 移位|按位求根（Shifting N-th Root Algorithm）：https://en.wikipedia.org/wiki/Shifting_nth_root_algorithm
		public BigInteger 开方(UInt64 指数_输入 = 2) => 开方(this, 指数_输入);
		public static BigInteger 开方(BigInteger 底数_输入, UInt64 指数_输入 = 2) => new BigInteger(开方_Double版(底数_输入, 指数_输入));
		public Double 开方_Double版(UInt64 指数_输入 = 2) => 开方_Double版(this, 指数_输入);
		public static Double 开方_Double版(BigInteger 底数_输入, UInt64 指数_输入 = 2)
		{
			// 源头：快速近似法，可认为由Newton-Raphson方法特化
			// 来源：https://en.wikipedia.org/wiki/Nth_root_algorithm
			// 公式：x_(k + 1) = 1 / n · ((n − 1) · x_k + A / (x_k^(n − 1)))
			// 初始的x_0一般是猜想的

			Double 容器 = default;
			Double 的_输出 = (Double)底数_输入 / 指数_输入;		// + 1D;		// 当x_0 = 1时，x_1 = 1 / n × ((n − 1) × 1 + A / (1^(n − 1))) = 1 / n × ((n − 1) + A) ≈ A / n + 1
			Double 差 = default;
			Double 精度阈值 = Double.Epsilon;
			Double 底数 = (Double)底数_输入;

			do
			{
				容器 = 的_输出;
				的_输出 = (向前(指数_输入) * 容器 + 底数 / 求幂(容器, ToUInt64(向前(指数_输入)))) / 指数_输入;

				// 终处理
				差 = 求绝对值(的_输出 - 容器);
			}
			while(差 > 精度阈值);

			return 的_输出;
		}
		public Decimal 开方_Decimal版(UInt64 指数_输入 = 2) => 开方_Decimal版(this, 指数_输入);		// ！需要常量化
		public static Decimal 开方_Decimal版(BigInteger 底数_输入, UInt64 指数_输入 = 2)
		{
			// 源头：快速近似法，可认为由Newton-Raphson方法特化
			// 来源：https://en.wikipedia.org/wiki/Nth_root_algorithm
			// 公式：x_(k + 1) = 1 / n · ((n − 1) · x_k + A / (x_k^(n − 1)))
			// 初始的x_0一般是猜想的

			Decimal 容器 = default;
			Decimal 的_输出 = (Decimal)底数_输入 / 指数_输入;// + 1D;		// 当x_0 = 1时，x_1 = 1 / n × ((n − 1) × 1 + A / (1^(n − 1))) = 1 / n × ((n − 1) + A) ≈ A / n + 1
			Decimal 差 = default;
			Decimal 精度阈值 = 0.0000_0000_0000_0000_0000_0000_001M;		// 28th位飘摆不定，故选27th
			Decimal 底数 = (Decimal)底数_输入;

			do
			{
				容器 = 的_输出;
				的_输出 = (向前(指数_输入) * 容器 + 底数 / 求幂(容器, ToUInt64(向前(指数_输入)))) / 指数_输入;

				// 终处理
				差 = 求绝对值(的_输出 - 容器);
			}
			while(差 > 精度阈值);

			return 的_输出;
		}
		

		// Wrap
		public BigInteger 求绝对值() => 求绝对值(this);
		public static BigInteger 求绝对值(BigInteger 源数_输入)
		{
			if(源数_输入.Is未定义 == false)
			{
				// 占位
			}
			else
			{
				throw new Exception("Syntax Error");
			}

			if(源数_输入.正号 == false)		// 负数、负无穷
			{
				源数_输入.正号 = true;
			}
			else		// 〇、正数、（正）无穷
			{
				// 占位
			}

			return 源数_输入;
		}
		private static Double 求绝对值(Double 源_输入) => Double.IsNegative(源_输入) ? -源_输入 : 源_输入;
		private static Decimal 求绝对值(Decimal 源_输入) => 源_输入 < 0 ? -源_输入 : 源_输入;
		private static Double 求幂(Double 底数_输入, UInt64 指数_输入)
		{
			Double 的_输出 = 1D;

			for(UInt64 索引 = default; 索引 <= 〇索引化(指数_输入); 索引++)
			{
				的_输出 *= 底数_输入;
			}

			return 的_输出;
		}
		private static Decimal 求幂(Decimal 底数_输入, UInt64 指数_输入)
		{
			Decimal 的_输出 = 1M;

			for(UInt64 索引 = default; 索引 <= 〇索引化(指数_输入); 索引++)
			{
				的_输出 *= 底数_输入;
			}

			return 的_输出;
		}

		#region 四则运算
		// 四则运算
		public static BigInteger 加(BigInteger 加数_左_输入, BigInteger 加数_右_输入)
		{
			// 预处理
			BigInteger? 和容器 = 异常值计算(加数_左_输入, 加数_右_输入, 算术运算.加);
			//
			if(和容器 != null)
			{
				return 和容器.Value;
			}
			else
			{
				// 占位
			}

			Boolean? 正号_左 = 加数_左_输入.正号;
			Boolean? 正号_右 = 加数_右_输入.正号;
			BigInteger 和 = default;		// 只需要指针，后续提供对象

			// 赋值
			// 数值、符号
			if(正号_左 == 正号_右)
			{
				和.数值组 = 加_核心(加数_左_输入.数值组, 加数_右_输入.数值组);
				和.数值组 = Trim(和.数值组);
				和.正号 = 正号_左;
			}
			else		// 异号，算作减法
			{
				if(正号_左 == true)
				{
					加数_右_输入.正号 = true;		// 既异号，左又为正，则右为负
					和 = 加数_左_输入 - 加数_右_输入;
				}
				else
				{
					加数_左_输入.正号 = true;		// 既异号，右又为正，则左为负
					和 = 加数_右_输入 - 加数_左_输入;
				}
			}

			// 无穷不需要赋值，因一般运算无法呈现此态

			return 和;
		}

		public static BigInteger 减(BigInteger 被减数_输入, BigInteger 减数_输入)
		{
			// 预处理
			BigInteger? 差容器 = 异常值计算(被减数_输入, 减数_输入, 算术运算.减);
			if(差容器 != null)
			{
				return 差容器.Value;
			}
			else
			{
				// 占位
			}

			Boolean? 正号_被减数 = 被减数_输入.正号;
			Boolean? 正号_减数 = 减数_输入.正号;
			BigInteger 差 = default;		// 只需要指针，后续提供对象

			// 都是负数且减数大的情况，其实可以等价为：−(|减数| − |被减数|)，但没省什么事

			// 赋值
			// 数值、符号
			if(正号_被减数 == 正号_减数)		// 同号的情况
																// 被减数		减数		差		情况
																// +				+			+		大 − 小
																// +				+			〇		相等
																// +				+			−		小 − 大
																// −				−			−		|被减数| > |减数|，即值上的大 − 小
																// −				−			〇		相等
																// −				−			+		|被减数| < |减数|，即值上的小 − 大
			{
				if(求绝对值(被减数_输入) > 求绝对值(减数_输入))
				{
					差.数值组 = 减_核心(被减数_输入.数值组, 减数_输入.数值组);

					差.正号 = 正号_被减数;
				}
				else if(求绝对值(被减数_输入) < 求绝对值(减数_输入))
				{
					差.数值组 = 减_核心(减数_输入.数值组, 被减数_输入.数值组);

					差.正号 = !正号_被减数;		// "‘小’ − ‘大’"，符号相异
				}
				else		// 相等的情况
							// 值相等，减完为〇
				{
					差 = 〇;
				}

				差.数值组 = Trim(差.数值组);
			}
			else		// 异号，算作加法
						// 包含了以下4种pattern
						// 符号						数值						差
						// 被减数		减数		被减数		减数		符号		数值
						// +				−			大				小			传+		转加
						// +				−			小				大			传+		转加
						// −				+			大				小			传−		转加
						// −				+			小				大			传−		转加
			{
				减数_输入.正号 = !减数_输入.正号;		// Boolean类型的“求逆”
				差 = 被减数_输入 + 减数_输入;
			}

			// 无穷不需要赋值，因一般运算无法呈现此态

			return 差;
		}

		public static BigInteger 乘(BigInteger 因数_左_输入, BigInteger 因数_右_输入)		// ！万级Cell效率衰减严重！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
		{
			// 预处理
			BigInteger? 积容器 = 异常值计算(因数_左_输入, 因数_右_输入, 算术运算.乘);
			if(积容器 != null)		// ？是否可用default
			{
				return 积容器.Value;
			}
			else
			{
				// 占位
			}

			BigInteger 积 = default;		// 仅有空壳，需要后续的赋值

			// 赋值
			// 处理符号
			// 因数		因数		积
			// +			+			+
			// +			−			−
			// −			+			−
			// −			−			+
			积.正号 = !(因数_左_输入.正号 ^ 因数_右_输入.正号);		// 即同或

			// 因为是整数乘法，所以不会出现乘分数等于除以整数的转移

			积.数值组 = 乘_核心(因数_左_输入.数值组, 因数_右_输入.数值组);
			积.数值组 = Trim(积.数值组);

			// 无穷不需要赋值，因一般运算无法呈现此态

			return 积;
		}

		// 整数的除法，故实为整除
		public static (BigInteger 商, BigInteger 余数) 除以(BigInteger 被除数_输入, BigInteger 除数_输入)
		{
			// 预处理
			BigInteger? 商容器 = 异常值计算(被除数_输入, 除数_输入, 算术运算.整除);
			//
			if(商容器 != null)
			{
				return (商容器.Value, 异常值计算(被除数_输入, 除数_输入, 算术运算.余).Value);
			}
			else
			{
				// 占位
			}

			(BigInteger 商, BigInteger 余数) 结果 = default;		// 仅有空壳，需要后续的赋值

			// 赋值
			// 处理符号
			// ！余数的符号逻辑遵从“广义”的定义：被除数 = 除数 × 商 + 余数，即：被除数、余数同号
			// 被除数		除数		商		余数
			// +				+			+		+
			// +				−			−		+
			// −				+			−		−
			// −				−			+		−
			结果.商.正号 = !(被除数_输入.正号 ^ 除数_输入.正号);		// 即同或
			结果.余数.正号 = 被除数_输入.正号;

			// 因为是整数除法，所以不会出现除以分数等于乘整数的转移

			(结果.商.数值组, 结果.余数.数值组) = 除以_核心(被除数_输入.数值组, 除数_输入.数值组);
			结果.商.数值组 = Trim(结果.商.数值组);

			结果.余数.数值组 = Trim(结果.余数.数值组);

			// 无穷不需要赋值，因一般运算无法呈现此态

			return 结果;
		}
		//
		public static BigInteger 求余数(BigInteger 被除数_输入, BigInteger 除数_输入)
		{
			(_, BigInteger 余数) = 除以(被除数_输入, 除数_输入);

			return 余数;
		}
		#endregion
		
		public BigInteger 求2的补码(Boolean IsLegacy_输入 = default) => 求2的补码(this, IsLegacy_输入);
		// 既涵盖了求补码()，又涵盖了求原码()
		// 定义法：二进制表示的整数原码按位取反后加1即为其补码；二进制表示的整数补码按位取反后加1即为其原码。其中〇、正数的原码 = 补码；负数的原码 = 求2的补码(补码)|补码 = 求2的补码(原码)，即负数的原码、补码不相同，但互为2的补码操作的逆，类似相反数的概念
		// 直接视补码操作为纯粹的数值操作，不涉及符号；但保留兼容的Legacy逻辑
		public static BigInteger 求2的补码(BigInteger 源数_输入, Boolean IsLegacy_输入 = default)
		{
			BigInteger 的_输出 = 源数_输入;		// ！防止后续操作遗漏赋值

			if(源数_输入.Is未定义 == false)
			{
				的_输出.数值组 = 求2的补码_核心(源数_输入.数值组);
				//
				if(IsLegacy_输入 == true)
				{
					if(源数_输入.正号 != default)
					{
						的_输出.正号 = !源数_输入.正号;
					}
					else
					{
						// 占位
					}
				}
				else
				{
					// 占位
					//的_输出.正号 = 源数_输入.正号;
				}
			}
			else
			{
				// 占位
			}

			return 的_输出;
		}
		public static BigInteger 求相反数(BigInteger 源数_输入_输出)
		{
			if(源数_输入_输出 != 〇)		// 正常的数字、无穷的情况
			{
				源数_输入_输出.正号 = !源数_输入_输出.正号;
			}
			else
			{
				// 〇的情况是自身等于自身，故无需操作
			}

			return 源数_输入_输出;
		}
		#endregion

		#region 算术运算-核心
		// 算术运算
		public static Double 求以2为底的对数_小数部分(BigInteger 幂_输入)
		{
			Double 的_输出 = default;
			Double 判断容器 = default;
			BigInteger 容器 = 右移位(幂_输入, ToInt32(幂_输入.数位数) - 53);		// 实际做成了“移位到”版本
																														// 1举2得地实现了自动转向的移位：超过53位的，需要右移(幂.数位数 - 53)；不足53位的，需要右移(幂.数位数 - 53)，由于是负数，即左移(53 - 幂.数位数)
			Double 运算容器 = (Double)容器 / Math.Pow(2, 53 - 1);
																										// ！考虑本地化

			for(Int32 索引 = ToInt32(一索引化(default)); 索引 <= 52; 索引++)		// 实际非〇起数值最多53个，但判定困难
			{
				运算容器 *= 运算容器;		// 模拟平方

				if(运算容器 >= 2)		// ∈ [2, 4)。∈ [1, 2)的数值再平方也上不到4，！理论上
				{
					的_输出 += Math.Pow(2, -索引);

					运算容器 /= 2;

					if((的_输出 > 判断容器) == false)		// 相当于没增长→赋值超出范围，继续加也无效，便退出
																			// 都是正数
					{
						break;
					}
					else
					{
						// 占位
					}

					// 终处理
					判断容器 = 的_输出;
				}
				else		// ∈ [1, 2)
				{
					// 占位
					//的_输出 += Math.Pow(0, -索引);
				}
			}

			return 的_输出;
		}
		private static UInt64[] 加_核心(UInt64 加数_左_输入, UInt64 加数_右_输入) => 加_核心(new UInt64[]{ 加数_左_输入 }, 加数_右_输入);
		private static UInt64[] 加_核心(UInt64 加数_左_输入, UInt64[] 加数_右_输入) => 加_核心(加数_右_输入, 加数_左_输入);
		private static UInt64[] 加_核心(UInt64[] 加数_左_输入, UInt64 加数_右_输入) => 加_核心(加数_左_输入, new UInt64[]{ 加数_右_输入 });
		private static UInt64[] 加_核心(UInt64[] 加数_左_输入, UInt64[] 加数_右_输入)
		{
			// 数组变量初始化
			UInt64 进位数 = default;
			UInt64 长度 = ToUInt64(Max(加数_左_输入.LongLength, 加数_右_输入.LongLength));
			UInt64[] 和 = new UInt64[长度];

			// 取齐长度，保证循环索引一致可用
			UInt64[] 加数_左 = new UInt64[长度];
			UInt64[] 加数_右 = new UInt64[长度];
			Array.Copy(加数_左_输入, 加数_左, 加数_左_输入.LongLength);
			Array.Copy(加数_右_输入, 加数_右, 加数_右_输入.LongLength);

			for(Int64 索引 = default; 索引 <= 〇索引化(ToInt64(长度)); 索引++)
			{
				(进位数, 和[索引]) = 加_核心_单层(加数_左[索引], 加数_右[索引], 进位数);

				// 最高双字时还有进位
				if
				(
					索引 == 〇索引化(ToInt64(长度))
					&& 进位数 != 0
				)
				{
					// Resize()
					长度++;
					UInt64[] 新和 = new UInt64[ToInt64(长度)];
					Array.Copy(和, 新和, 〇索引化(ToInt64(长度)));
					和 = 新和;

					(进位数, 和[〇索引化(ToInt64(长度))]) = 加_核心_单层(和[〇索引化(ToInt64(长度))], 0, 进位数);

					break;		// 既能在本层操作中继续使用长度，又能不干扰循环的既定退出
				}
				else
				{
					// 占位
				}
			}

			return 和;
		}

		private static (UInt64 进位数, UInt64 和) 加_核心_单层(UInt64 加数_左_输入, UInt64 加数_右_输入, UInt64 进位数_输入 = default)
		{
			(UInt64 进位数, UInt64 和) 的_输出 = default;
			UInt64 和数值_容器 = default;

			// 防止2个无符号64位整数相加后进位产生溢出且无法获得其溢出的部分，因无更大的类型承载其“瞬时结果”
			// 官方代码种使用无符号32位整数，此时可以用64位来承接“瞬时结果”，不排除其选用32位的目的就在于此，即损失较小的功能达到更高的目的
			// 经估算，2个(2^64 − 1)（0X FFFF FFFF FFFF FFFF）的和，即0X 1 FFFF FFFF FFFF FFFE，即最大的进位 ∈ [0, 1]
			// 低32位
			和数值_容器 = (加数_左_输入 & 双字掩码) + (加数_右_输入 & 双字掩码) + (进位数_输入 & 双字掩码);
			//
			的_输出.和 = 和数值_容器 & 双字掩码;
			//
			进位数_输入 >>= 双字位数;
			进位数_输入 += 和数值_容器 >> 双字位数;		// 2019.06.21，！考虑增设ToUInt16()，合理应用范围

			// 高32位
			和数值_容器 = (加数_左_输入 >> 双字位数) + (加数_右_输入 >> 双字位数) + 进位数_输入;
			//
			的_输出.和 |= (和数值_容器 & 双字掩码) << 双字位数;
			//
			的_输出.进位数 = 和数值_容器 >> 双字位数;

			return 的_输出;
	}
		
		private static UInt64[] 减_核心(UInt64[] 被减数_输入, UInt64[] 减数_输入)
		{
			UInt64 借位数 = default;
			UInt64 长度 = ToUInt64(Max(被减数_输入.LongLength, 减数_输入.LongLength));		// UInt64→Int64→UInt64
			UInt64[] 差 = new UInt64[长度];

			// 取齐长度，保证循环索引一致可用
			UInt64[] 被减数 = new UInt64[长度];
			UInt64[] 减数 = new UInt64[长度];
			Array.Copy(被减数_输入, 被减数, 被减数_输入.LongLength);
			Array.Copy(减数_输入, 减数, 减数_输入.LongLength);

			for(Int64 索引 = default; 索引 <= 〇索引化(ToInt64(长度)); 索引++)
			{
				(借位数, 差[索引]) = 减_核心_单层(被减数[索引], 减数[索引], 借位数);

				// 由于保证了绝对值上被减数 ≥ 减数，故不会出现最高位仍有借位的情况
			}

			// 借位数最高位不会产生借位且一定会被刷新为〇，故无需还原默认值，即使每刷为默认值，后面也用不上了，所以不需要还原默认值

			return 差;
		}
		private static (UInt64 借位数, UInt64 差) 减_核心_单层(UInt64 被减数_输入, UInt64 减数_输入, UInt64 借位数_输入)
		{
			// 巧用了模数的减法模式，即UInt64相减，不足时自动以A + UInt64.MaxValue − B作为结果（正数），正好可视为借位数。若以Int64解读（即强制转换为Int64）之，则为对应的负数

			(UInt64 借位数, UInt64 差) 的_输出 = default;
			UInt64 差数值_容器 = default;

			// 低32位
			差数值_容器 = (被减数_输入 & 双字掩码) - (减数_输入 & 双字掩码) - (借位数_输入 & 双字掩码);
			//
			的_输出.差 = 差数值_容器 & 双字掩码;
			//
			借位数_输入 >>= 双字位数;
			借位数_输入 += ToByte(求绝对值((Int32)(差数值_容器 >> 双字位数)));		// 使用Convert.ToInt64()的话，超出Int64.MaxValue的部分会报错
			借位数_输入 += 的_输出.借位数;

			// 高32位
			差数值_容器 = (被减数_输入 >> 双字位数) - (减数_输入 >> 双字位数) - 借位数_输入;
			//
			的_输出.差 |= (差数值_容器 & 双字掩码) << 双字位数;
			//
			的_输出.借位数 = ToByte(求绝对值((Int32)(差数值_容器 >> 双字位数)));		// 使用Convert.ToInt64()的话，超出Int64.MaxValue的部分会报错

			return 的_输出;
		}

		// 和笔算的乘法竖式相同逻辑，En文称之为Grammar-School
		private static UInt64[] 乘_核心(UInt64 因数_左_输入, UInt64 因数_右_输入) => 乘_核心(new UInt64[]{ 因数_左_输入 }, 因数_右_输入);
		private static UInt64[] 乘_核心(UInt64 因数_左_输入, UInt64[] 因数_右_输入) => 乘_核心(因数_右_输入, 因数_左_输入);
		private static UInt64[] 乘_核心(UInt64[] 因数_左_输入, UInt64 因数_右_输入) => 乘_核心(因数_左_输入, new UInt64[]{ 因数_右_输入 });
		private static UInt64[] 乘_核心(UInt64[] 因数_左_输入, UInt64[] 因数_右_输入)
		{
			UInt64 进位数 = default;		// 每64位的进位不会超过1个64位，故该容量足够
			UInt64 长度 = ToUInt64(因数_左_输入.LongLength + 因数_右_输入.LongLength);			// 积.长度 = 和(因数.长度) + Flooring(对数(进制位数, 和(因数.长度)))，故以：和(因数.长度)为起始

			UInt64[] 积 = new UInt64[长度];
			UInt64 进位数_接收 = default;		// 专供加法接收用
			UInt64 容器 = default;
			Int32 索引_右 = default;

			// 处理数值
			for(Int32 索引_左 = default; 索引_左 <= 〇索引化(因数_左_输入.LongLength); 索引_左++)
			{
				for(索引_右 = default; 索引_右 <= 〇索引化(因数_右_输入.LongLength); 索引_右++)
				{
					(进位数, 容器) = 乘_核心_单层(因数_左_输入[索引_左], 因数_右_输入[索引_右], 进位数);
					(进位数_接收, 积[索引_左 + 索引_右]) = 加_核心_单层(积[索引_左 + 索引_右], 容器);

					// 进位数合并
					进位数 += 进位数_接收;
					进位数_接收 = default;		// 仅供接收用，但调用处会先使用其值，故设为default防止污染结果
				}

				// 处理最高四字的进位
				if(进位数 != default)
				{
					// 各步积求和运算的最高四字进位
					for(Int32 索引 = ToInt32(向后(〇索引化(因数_右_输入.LongLength) + 索引_左)); 索引 <= 〇索引化(ToInt64(长度)); 索引++)		// 移植的逻辑，强调都是〇索引的值、内总外Current
					{
						(进位数, 积[索引]) = 加_核心_单层(积[索引], default, 进位数);

						if(进位数 == default)
						{
							break;
						}
						else
						{
							// 占位
						}
					}

					// 最终最高四字进位
					// Cell不足，重新分配并赋值进位
					// 2019.06.20，！理论上不会出现
					if(进位数 != default)
					{
						// Resize()
						长度++;
						// 重映射积的内容，实际相当于积的数组长度增1
						UInt64[] 新积 = new UInt64[ToInt64(长度)];
						Array.Copy(积, 新积, 向前(ToInt64(长度)));
						积 = 新积;

						(进位数, 积[〇索引化(ToInt64(长度))]) = 加_核心_单层(积[〇索引化(ToInt64(长度))], default, 进位数);
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

				// 以上各操作保证内循环开始前进位数为default，故不重设default
			}

			return 积;
		}
		private static (UInt64 进位数, UInt64 积) 乘_核心_单层(UInt64 因数_左_输入, UInt64 因数_右_输入, UInt64 进位数_输入 = default)
		{
			// 乘法的逻辑：
			// 设：1个正整数由以下结构组成：(高 × 基 + 低)
			// 则：2个正整数的乘法由以下结构构成：(左高 × 基 + 左低) × (右高 × 基 + 右低)
			// 即：左高 × 右高 × 基^2 + (左高 × 右低 + 左低 × 右高) × 基 + 左低 × 右低
			// 此处：高指高32位，低指低32位，基指2^32

			// 64位空间可以容纳2个32位无符号整数的乘积 + 进位
			// 2^64 − 1 = 0X FFFF FFFF FFFF FFFF FFFF FFFF FFFF FFFF > (2^32 − 1) × (2^32 − 1) + ((2^32 − 1) × (2^32 − 1) >> 32) = 0X 0000 0000 0000 0000 FFFF FFFE FFFF FFFE
			// 64位空间可以容纳2个64位无符号整数的乘积及累计进位的和的进位
			// 2^64 − 1 = 0X FFFF FFFF FFFF FFFF FFFF FFFF FFFF FFFF > 0X FFFF FFFF FFFF FFFF 0000 0000 0000 0000 > (2^64 − 1) × (2^64 − 1) + ((2^64 − 1) × (2^64 − 1) >> 64) = 0X FFFF FFFF FFFF FFFE FFFF FFFF FFFF FFFE
			// 64位无符号整型.Max，32位无符号整型.Max，32位的进位.Max
			// X进制下其每数位最大容量是x^2 − x − 1

			(UInt64 进位数, UInt64 积) 的_输出 = default;
			UInt64 积数值_容器 = default;

			// 低32位
			// 左低 × 右低 + 进位数，走低
			(的_输出.进位数, 积数值_容器) = 加_核心_单层((因数_左_输入 & 双字掩码) * (因数_右_输入 & 双字掩码), 进位数_输入);
			的_输出.积 = 积数值_容器 & 双字掩码;
			//
			// 进位数赋值：
			// 64th~33rd位进位，96th~65th位进位在前面已经他获取
			进位数_输入 = 积数值_容器 >> 双字位数;
			进位数_输入 += 的_输出.进位数 << 双字位数;

			// 高32位
			// 左高 × 右低 + 左低 × 右高 + 进位数，走高
			(的_输出.进位数, 积数值_容器) = 加_核心_单层((因数_左_输入 >> 双字位数) * (因数_右_输入 & 双字掩码), (因数_左_输入 & 双字掩码) * (因数_右_输入 >> 双字位数), 进位数_输入);
			//
			// 积的高32位赋值
			的_输出.积 |= (积数值_容器 & 双字掩码) << 双字位数;
			//
			// 进位数赋值：
			// 96th~65th位进位，128th~97th位进位在前面已经他获取
			// 预处理
			进位数_输入 = 积数值_容器 >> 双字位数;		// 相当于：(积数值_容器 & 高64位掩码) >> 双字位数，即提取高64位值

			进位数_输入 += 的_输出.进位数 << 双字位数;

			// 下1次循环的64位
			// 进位数赋值：左高 × 右高，走下1次循环
			//的_输出.进位数 += (因数_左_输入 >> 双字位数) * (因数_右_输入 >> 双字位数);		// 2019.06.22，理论上不会出现进位
			(的_输出.进位数, 积数值_容器) = 加_核心_单层((因数_左_输入 >> 双字位数) * (因数_右_输入 >> 双字位数), 进位数_输入);

			// 终处理
			if(的_输出.进位数 == default)
			{
				的_输出.进位数 = 积数值_容器;
			}
			else
			{
				throw new OverflowException();
			}

			return 的_输出;
		}
		
		private static (UInt64[] 商, UInt64[] 余数) 除以_核心(UInt64[] 被除数_输入, UInt64 除数_输入) => 除以_核心(被除数_输入, new UInt64[] { 除数_输入 });
		private static (UInt64[] 商, UInt64[] 余数) 除以_核心(UInt64[] 被除数_输入, UInt64[] 除数_输入)
		{
			// “‘取小’对齐”，默认的方式是以除数对齐：高位始空白〇值计数(除数_输入[〇索引化(被除数_输入.LongLength)]);
			Byte 除数高位〇数 = 高位始空白〇值计数(除数_输入[^ToInt32(一索引化(default))]);
			Byte 左移位 = 除数高位〇数;
			Byte 右移位 = (Byte)(四字位数 - 除数高位〇数);
			UInt64[] 运算用被除数及余数容器 = 左移位_核心(被除数_输入, 左移位);
			UInt64 近似除数 = 左移位_核心(除数_输入, 左移位)[^ToInt32(一索引化(default))];
			Boolean Is操作数相等 = 相等_核心(被除数_输入, 除数_输入);

			Boolean Is商够大 = 比_大_左对齐(被除数_输入, 除数_输入);		// 以：!(除数 > 被除数)代表：被除数 ≥ 除数
																											// “=”部分用于解决被除数、除数相等但商位数不正确的问题
			//
			Is商够大 |= (运算用被除数及余数容器[^ToInt32(一索引化(default))] < 近似除数) ? true : false;		// 首位不足，需要退位的情况

			// 预处理
			UInt64[] 近似除数容器 = new UInt64[除数_输入.Length];
			近似除数容器[^ToInt32(一索引化(default))] = 近似除数;
			//
			Boolean Is除数一致 = 相等_核心(左对齐_核心(除数_输入), 近似除数容器);		// 要么大于，要么等于

			Int32 长度差 = 运算用被除数及余数容器.Length - 除数_输入.Length;		// 被除数因除数而左移位，可能会进位，故以运算用值长度为准；而此左移位是以除数为准的，故长度不会变
			UInt64 商长度 =
				ToUInt64(长度差)
				+ (Is商够大 ? ToUInt64(一索引化(default)) : default)
				+ (Is操作数相等 ? ToUInt64(一索引化(default)) : default);		// 根据：和(因数.长度) ≤ 积.长度，故：被除数.长度 − 除数.长度 ≥ 商.长度
																												// 2019.06.24，Core版的商长度是：向后(长度差)
																												// default不能参与加减运算，故用0，考虑以default代之
			Int32 转商索引差 = 运算用被除数及余数容器.Length - ToInt32(商长度);
			(UInt64[] 商, UInt64[] 余数) 结果 = (new UInt64[商长度], default);
			UInt64[] 商容器 = new UInt64[商长度];
			UInt64 运算用被除数 =default;
			UInt64 近似商 = default;
			UInt64[] 被除数容器 = default;
			UInt64 余位数容器 = default;

			// 获取被除数近似值
			// 实际该索引与商长度等长，但起止点的值与之相差了1个出书长度
			// 终止值为：被除数索引 >= 向后(〇索引化(除数_输入.Length))的省略，后者其值与除数_输入.Length相等，但含义不同
			for(Int32 被除数索引 = ToInt32(〇索引化(运算用被除数及余数容器.Length)); 被除数索引 - 转商索引差 >= 0; 被除数索引--)		// 原版的索引 ∈ [0, 商.长度]
			{
				// 预处理
				运算用被除数 = 运算用被除数及余数容器[被除数索引];

				// 计算近似商
				(近似商, _) = 除以_核心_单层(运算用被除数, 近似除数, 余位数容器);

				// 粗削
				商容器[被除数索引 - 转商索引差] = 近似商;
				//
				被除数容器 = 乘_核心(除数_输入, 商容器);
				被除数容器 = 左移位_核心(被除数容器, 左移位);		// 即：被除数容器 << 左移位

				// 商过大
				while		// 即：被除数_输入 ≤ 乘_核心(除数_输入, 商)都算Is商过大
				(
					比_大_核心(Trim(被除数容器), Trim(运算用被除数及余数容器))
					|
					(
						相等_核心(Trim(被除数容器), Trim(运算用被除数及余数容器))
						& (Is操作数相等 == false)
					)
				)
				{
					// 特别情况
					if		// 近似除数就是除数的所有有效值的情况（除数 ∈ (0, UInt64.MaxValue)、除数仅最高UInt64有非〇数字）下，被除数容器值与被除数一致的话不算商过大
					(
						相等_核心(Trim(运算用被除数及余数容器), Trim(被除数容器))
						&& Is除数一致
					)
					{
						break;
					}

					近似商--;

					// 终处理
					商容器[被除数索引 - 转商索引差] = 近似商;		// Last操作相当于直接得到了商的值
					被除数容器 = 乘_核心(除数_输入, 商容器);
					被除数容器 = 左移位_核心(被除数容器, 左移位);		// 即：被除数容器 << 左移位
				}

				运算用被除数及余数容器 = 减_核心(运算用被除数及余数容器, 被除数容器);		// 即：运算用被除数及余数容器--

				余位数容器 = 运算用被除数及余数容器[被除数索引];

				结果.商[被除数索引 - 转商索引差] = 商容器[被除数索引 - 转商索引差];

				近似商 = default;
				Array.Clear(商容器, default, 商容器.Length);
			}

			结果.余数 = 右移位_核心(运算用被除数及余数容器, 左移位);

			return 结果;
		}
		public static (UInt64 商, UInt64 余数) 除以_核心_单层(UInt64 被除数_输入, UInt64 除数_输入, UInt64 余位数_输入 = default)
		{
			(UInt64 商, UInt64 余数) 结果 = default;
			UInt64 被除数_容器 = 余位数_输入;
			UInt64 余数容器 = default;
			UInt64 余位数容器 = default;

			for(Int32 索引 = ToInt32(〇索引化(四字位数 / 十六进率)); 索引 >= 0; 索引--)		// 此处不可使用default，因其未对“>”重载
			{
				// 预处理
				结果.商 <<= 十六进率;		// 首次执行无效化了
				余位数容器 = 被除数_容器 >> ToInt32(向前(四字位数 / 十六进率) * 十六进率);
				被除数_容器 <<= 十六进率;
				被除数_容器 |= (被除数_输入 >> (索引 * 十六进率)) & 四位掩码;
				余数容器 = default;

				while(余位数容器 != default)
				{
					// 预处理
					余数容器 = default;

					结果.商 += UInt64.MaxValue / 除数_输入;		// 2019.06.24，商 ∈ [1, 2^64 − 1)
																							// 2019.06.25，理论上不会出现进位
					//
					余数容器 += UInt64.MaxValue % 除数_输入;		// 2019.06.26，理论上不会出现进位

					// 终处理
					余位数容器--;		// 借位为1
					余数容器++;		// 借位为1，实用为UInt64.MaxValue，故还需要加1
					//
					(余数容器, 被除数_容器) = 加_核心_单层(被除数_容器, default, 余数容器);
					余位数容器 += 余数容器;
				}

				结果.商 += 被除数_容器 / 除数_输入;

				// 终处理
				余数容器 += 被除数_容器 % 除数_输入;		// 2019.06.26，理论上不会出现进位
				被除数_容器 = 余数容器;
			}

			// 获取本次余数|下次余位数
			余位数容器 = default;
			//
			(余位数容器, 被除数_容器) = 乘_核心_单层(除数_输入, 结果.商, 余位数容器);
			// 相当于被除数的高64位
			余位数容器 = 余位数_输入 - 余位数容器;
			(余位数容器, 结果.余数) = 减_核心_单层(被除数_输入, 被除数_容器, 余位数容器);

			return 结果;
		}

		public static BigInteger 求幂_核心(BigInteger 底数_输入, BigInteger 指数_输入)
		{
			BigInteger 幂 = 正一;
			UInt64 指数 = default;
			Int64 总位数 = ToInt64(指数_输入.数位数);

			// 特殊情况
			// 不符合当前定义的
			// 负指数、小数指数
			//
			// 可简便运算的
			// 0^0 = 1
			if((底数_输入 == 〇) & (指数_输入 == 〇))
			{
				幂 = 正一;

				return 幂;
			}
			else
			{
				// 占位
			}

			// x^0 = 1
			if(指数_输入 == 〇)
			{
				幂 = 正一;

				return 幂;
			}
			else
			{
				// 占位
			}

			// x^1 = x
			if(指数_输入 == 正一)
			{
				幂 = 底数_输入;		// 此处由于是结构（structure）类型，故为值传递

				return 幂;
			}
			else
			{
				// 占位
			}

			// 1^x = 1
			if(底数_输入 == 正一)
			{
				幂 = 正一;

				return 幂;
			}
			else
			{
				// 占位
			}

			// 0^x = 0
			if(底数_输入 == 〇)
			{
				幂 = 〇;

				return 幂;
			}
			else
			{
				// 占位
			}

			// (−1)^x = −1，x是奇数；(−1)^x = 1，x是偶数
			if(底数_输入 == 负一)
			{
				幂 = 负一;

				return 幂;
			}
			else
			{
				// 占位
			}

			for(Int64 索引 = 总位数; 索引 > 0; 索引--)
			{
				指数 = 指数_输入.IndexOf(索引);

				if(指数 != default)		// 奇次幂
				{
					幂 *= 底数_输入;
				}
				else
				{
					// 占位
				}

				if(索引 != 一索引化(default))		// 下面的右移位操作的实际执行逻辑
																// 末位的情况
				{
					幂 *= 幂;		// 模拟得平方
				}
				else
				{
					// 占位
				}
			}

			return 幂;
		}

		private static Int64 最高Cell最高位序号(BigInteger 源_输入) => 四字位数 - 高位始空白〇值计数(源_输入.数值组[^ToInt32(一索引化(default))]);

		// Int64足够，因二进制下总位数为：(2^32 − 1) × 64 < 2^(64 − 1) − 1，且需要正负数2种情况，故使用Int64而不是UInt64
		// 正数为左起|高位起计数索引；负数为右起|低位起计数索引
		public UInt64 IndexOf(Int64 索引_输入, Boolean Is〇索引化_输入 = default) =>IndexOf(this, ToInt32(索引_输入), Is〇索引化_输入);
		private static UInt64 IndexOf(BigInteger 源_输入, Int32 索引_输入, Boolean Is〇索引化_输入 = default)
		{
			Int32 索引 = Is〇索引化_输入 ? ToInt32(一索引化(索引_输入)) : 索引_输入;
			UInt64 的_输出 = default;
			Int64 总位数 = ToInt64(源_输入.数位数);
			UInt64 容器 = default;
			Boolean Is倒序 = 索引 < 0;

			索引 = ToInt32(求绝对值(索引));

			if(ToInt32(求绝对值(索引)) > 总位数)
			{
				throw new OverflowException();
			}
			else
			{
				// 占位
			}

			容器 = Flooring倍数化除以(ToUInt64(索引), 四字位数);

			索引 %= 四字位数;
			//
			if(Is倒序)
			{
				索引 = 四字位数 - 索引;

				的_输出 = 源_输入.数值组[^ToInt32(一索引化(容器))];
			}
			else		// 顺序的情况
			{
				的_输出 = 源_输入.数值组[容器];
			}
			//
			的_输出 &= ToUInt64(一索引化(default) << ToInt32(〇索引化(索引)));
			的_输出 >>= ToInt32(〇索引化(索引));

			return 的_输出;
		}

		// 〇的补码同一般的逻辑，也会“舍”成〇
		private static UInt64[] 求2的补码_核心(UInt64[] 源组_输入_输出)
		{
			UInt64[] 加数 = new UInt64[]{ ToUInt64(一索引化(default)) };		// 数组版的1
			Int64 长度 = 源组_输入_输出.LongLength;
			UInt64[] 容器 = new UInt64[长度];

			源组_输入_输出 = 按位取反_核心(源组_输入_输出);

			源组_输入_输出 = 加_核心(源组_输入_输出, 加数);
			Array.Copy(源组_输入_输出, 容器, 长度);		// 消除了进位的数值

			return 源组_输入_输出;
		}
		#endregion
	}
}