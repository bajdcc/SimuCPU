using SimuCPULib.Common.Simulator;

namespace SimuCPULib.Common.Base
{
    public class Status
    {
        public int Color { get; set; } = Constants.Initialize();

        public static T _And<T>(T a, T b)
            where T : Status, new()
        {
            a.Color &= b.Color;
            return a;
        }

        public static T _Or<T>(T a, T b)
            where T : Status, new()
        {
            a.Color |= b.Color;
            return a;
        }

        public virtual void CopyFrom(Status obj)
        {
            Color = obj.Color;
        }

        public override bool Equals(object obj)
        {
            return Color.Equals((obj as Status).Color);
        }

        public override int GetHashCode()
        {
            return Color.GetHashCode();
        }

        public override string ToString()
        {
            return Color.ToString();
        }
    }
}