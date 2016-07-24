namespace SimuCPULib.UI.Drawing
{
	public interface IGraphicsElement
	{
		IGraphicsElementFactory GetFactory();
		IGraphicsRenderer GetRenderer();
	}
}
