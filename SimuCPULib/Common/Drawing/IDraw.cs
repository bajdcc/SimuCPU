﻿using SimuCPULib.Common.Simulator;
using System.Drawing;

namespace SimuCPULib.Common.Drawing
{
    /// <summary>
    /// 交互接口
    /// </summary>
    public interface IDraw
	{
		/// <summary>
		/// 绘制
		/// </summary>
		/// <param name="bound">绘制区域</param>
		void Draw(Rectangle bound);
		/// <summary>
		/// 绘制前的事件（计算区域）
		/// </summary>
		/// <param name="bound">提供的区域</param>
		void Prepare(Rectangle bound);
		/// <summary>
		/// 处理事件
		/// </summary>
		/// <param name="type">事件类型</param>
		/// <param name="obj">参数</param>
		/// <returns></returns>
		int Handle(HandleType type, object obj);
	}
}
