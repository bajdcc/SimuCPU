using SimuCPULib.Common.Base;

namespace SimuCPULib.Common.Element
{
    /// <summary>
    /// 结点
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CommonNode<T> : NodeX<T>
        where T : Status, new()
    {
        public override void Update()
        {
            base.Update();
        }
    }
}
