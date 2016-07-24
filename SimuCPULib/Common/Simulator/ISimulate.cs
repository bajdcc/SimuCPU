namespace SimuCPULib.Common.Simulator
{
	/// <summary>
	/// 模拟接口
	/// </summary>
	public interface ISimulate
	{
		void Update();
		void Activate();
		void Advance();
	}
}
