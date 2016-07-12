using SimuCPULib.Common.Base;

namespace SimuCPULib.Common.Element
{
    /// <summary>
    /// 结点基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Node<T> : Mutable<T>
        where T : Status, new()
    {
        public Node()
        {
            base.Activate();
        }

        public override void Advance()
        {
            Update();
        }
    }
}
