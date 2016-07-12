using System.Drawing;
using SimuCPULib.Common.Base;
using SimuCPULib.Common.Drawing;

namespace SimuCPULib.Common.Graph
{
    /// <summary>
    ///     对事件参数的包装
    /// </summary>
    public class MarkableArgs
    {
        public Point Pt { get; set; }
        public Markable Id { get; set; }
        public IDraw Draw { get; set; }
    }
}