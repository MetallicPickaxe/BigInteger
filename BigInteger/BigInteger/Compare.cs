namespace BigInteger
{
	public partial struct BigInteger
	{
		// Max()
		public static BigInteger 求大(BigInteger 源数_左_输入, BigInteger 源数_右_输入)
		{
			if(大于等于(源数_左_输入, 源数_右_输入))		// 包含等于，与一般事实实现的表现逻辑一致
			{
				return 源数_左_输入;
			}
			else
			{
				return 源数_右_输入;
			}
		}
		//
		// Min()
		public static BigInteger 求小(BigInteger 源数_左_输入, BigInteger 源数_右_输入)
		{
			if(小于等于(源数_左_输入, 源数_右_输入))		// 包含等于，与一般事实实现的表现逻辑一致
			{
				return 源数_左_输入;
			}
			else
			{
				return 源数_右_输入;
			}
		}
	}
}