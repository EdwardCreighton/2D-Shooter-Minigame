using System.Numerics;

namespace ShootingRangeMiniGame.Engine.Core
{
	public partial class App
	{
		public Vector2 MousePosition { get; private set; }
		public bool LeftMouseButtonPressed { get; private set; }

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			MousePosition = new Vector2(e.Location.X, e.Location.Y);

			LeftMouseButtonPressed = e.Button == MouseButtons.Left;
		}
	}
}