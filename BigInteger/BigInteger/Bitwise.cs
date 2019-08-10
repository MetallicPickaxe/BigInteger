using System;

namespace BigInteger
{
	public partial struct BigInteger
	{
		// 1种逆向序数对齐的Array.Copy()
		// 长度_输入是对齐的的值，不是增|减的值
		private static UInt64[] 等长化_核心(UInt64[] 数_输入, Int64 长度_输入, Boolean Is右向_输入 = default)
		{
			Int32 长度 = 数_输入.Length;
			Int64 长度差 = 长度_输入 - 长度;		// ！理论上∈ [0, +∞)
			UInt64[] 数容器 = new UInt64[长度_输入];
			Int64 容器索引 = default;		// ！称之为新·除数索引更合适
			UInt64[] 数_输出 = default;

			if(Is右向_输入 == default)		// 左向的情况
			{
				for(Int32 索引 = default; 索引 <= ToInt32(〇索引化(长度)); 索引++)		// 空出前长度差个位置
				{
					// 预处理
					容器索引 = 索引 + 长度差;

					数容器[容器索引 >= 0 ? 容器索引 : default] = 数_输入[索引];
				}
			}
			else		// 右向的情况
			{
				数_输入.CopyTo(数容器, default);
			}

			数_输出 = 数容器;

			return 数_输出;
		}

		// 对齐到当前Cell最高位
		private static UInt64[] 左对齐_核心(UInt64[] 源_输入)
		{
			UInt64 高位〇数 = 高位始空白〇值计数(源_输入[^ToInt32(一索引化(default))]);
			Int32 左移位 = ToInt32(高位〇数);

			return 左移位_核心(源_输入, 左移位);
		}

		private static UInt64[] 左移位_核心(UInt64[] 源_输入, Int32 左移位_输入)
		{
			Boolean Is左移位 = default;

			if(左移位_输入 >= 0)
			{
				Is左移位 = true;
			}
			else
			{
				// 占位
				左移位_输入 = ToInt32(求绝对值(左移位_输入));
			}

			return 移位_核心(源_输入, 左移位_输入, Is左移位);
		}

		// 左对齐_核心的Wrap
		private static UInt64[] 右移位_核心(UInt64[] 源_输入, Int32 右移位_输入) => 左移位_核心(源_输入, -右移位_输入);

		private static UInt64[] 移位_核心(UInt64[] 源_输入, Int32 移位_输入, Boolean Is左移位_输入 = default)
		{
			// 预处理
			// 定义
			Int32 长度 = 源_输入.Length;
			UInt64 高位〇数 = 高位始空白〇值计数(源_输入[^ToInt32(一索引化(default))]);
			UInt64 最高位始点 = 四字位数 - 高位〇数;		// 一索引化的值
			UInt64 最高位终点 = default;		// 一索引化的值
			Int32 移位容器 = default;
			Int32 左移位 = default;
			Int32 右移位 = default;
			Boolean? Is右向 = default;
			Boolean Is进退位 = default;
			Int32 容器 = default;

			// 处理
			// 处理Is进退位
			if(Is左移位_输入)
			{
				Is进退位 = ToUInt64(移位_输入) > 高位〇数 ? true : false;		// ＞、≤
			}
			else		// Is左移位_输入 == false的情况
			{
				Is进退位 = ToUInt64(移位_输入) >= 最高位始点 ? true : false;		// ≥、＜
			}

			// 处理移位容器
			容器 = 移位容器 = 移位_输入;
			//
			if(Is进退位)
			{
				容器 = 移位容器 -= ToInt32(Is左移位_输入 ? 高位〇数 : 最高位始点);
				容器 = 求模(容器, 四字位数);
			}
			else		// 不进|退位
			{
				// 占位
				//移位容器 = 移位_输入;
			}

			// 赋值最高位终点
			if(Is进退位)
			{
				if(Is左移位_输入)
				{
					最高位终点 = ToUInt64(容器);
				}
				else		// Is左移位_输入 == false的情况
				{
					最高位终点 = ToUInt64(四字位数 - 容器);
				}
			}
			else		// 不进|退位
			{
				if(Is左移位_输入)
				{
					最高位终点 = 最高位始点 + ToUInt64(容器);
				}
				else		// Is左移位_输入 == false的情况
				{
					最高位终点 = 最高位始点 - ToUInt64(容器);
				}
			}

			// 处理Is右向
			if(最高位终点 > 最高位始点)
			{
				Is右向 = false;
			}
			else if(最高位终点 < 最高位始点)
			{
				Is右向 = true;
			}
			else		// 无移位、左|右移位终位置同始位置
			{
				// 占位
				//Is右向 = default;
			}

			// 移位数
			if(Is进退位)		// “进|退位”的情况：左移位_输入 ＞ 高位〇数
			{
				if(Is右向 == true)
				{
					右移位 = ToInt32(最高位始点 - 最高位终点);
					左移位 = 四字位数 - 右移位;
				}
				else if(Is右向 == false)
				{
					左移位 = ToInt32(最高位终点 - 最高位始点);
					右移位 = 四字位数 - 左移位;
				}
				else		// 无移位、左|右移位终位置同始位置
				{
					// 占位
				}
			}
			else		// 不足“进位”的情况：左移位_输入 ≤ 高位〇数
			{
				if(Is右向 == true)
				{
					右移位 = ToInt32(最高位始点 - 最高位终点);
					左移位 = 四字位数 - 右移位;
				}
				else if(Is右向 == false)
				{
					左移位 = ToInt32(最高位终点 - 最高位始点);
					右移位 = 四字位数 - 左移位;
				}
				else		// 无移位、左|右移位终位置同始位置
				{
					// 占位
				}
			}

			// 长度
			if(Is进退位)
			{
				// 真·右对齐借用左对齐内部的右对齐
				容器 = ToInt32(Ceiling倍数化除以(ToUInt64(移位容器), 四字位数));
				//
				if(Is左移位_输入)
				{
					长度 += 容器;
				}
				else
				{
					if(容器 >= 长度)
					{
						return new UInt64[]{ default };
					}
					else
					{
						长度 -= 容器;
					}
				}
			}
			else		// 无进|退位
			{
				// 占位
			}

			// 定义
			UInt64[] 的_输出 = new UInt64[长度];

			if		// 补充右移位的左向情况下的末端位的值，正常赋值时超出了范围，无法获得
			(
				Is左移位_输入 == default
				&& Is右向 == false
				&& 向后(长度) <= 源_输入.Length
				)
			{
				的_输出[default] = 源_输入[^ToInt32(向后(长度))] >> 右移位;
			}
			else
			{
				// 占位
			}

			UInt64[] 处理容器 = 等长化_核心(源_输入, 长度);

			// 处理
			if(Is右向 != default)		// 仅针对需要对齐的进行处理
												// 2019.06.23，≠ default比：> default道理上说得过去
			{
				for(Int32 索引 = ToInt32(〇索引化(长度)); 索引 >= 0; 索引--)
				{
					if(Is右向 == true)
					{
						的_输出[索引] |= 处理容器[索引] >> 右移位;
					}
					else if(Is右向 == false)
					{
						的_输出[索引] |= 处理容器[索引] << 左移位;
					}
					else		// 无移位、左|右移位终位置同始位置
					{
						// 占位
					}

					if(索引 >= 向后(default))
					{
						if(Is右向 == true)
						{
							的_输出[向前(索引)] = 处理容器[索引] << 左移位;		// 理论上一定足够，故不需要位于：索引 >= 向后(default)判定中
						}
						else if(Is右向 == false)
						{
							的_输出[索引] |= 处理容器[向前(索引)] >> 右移位;
						}
						else		// 无移位、左|右移位终位置同始位置
						{
							// 占位
						}
					}
					else		// 2019.06.23，视为做补〇
					{
						// 占位
					}
				}
			}
			else		// 无移位、左|右移位终位置同始位置
			{
				Array.Copy(处理容器, 的_输出, 长度);
			}

			return 的_输出;
		}
	}
}