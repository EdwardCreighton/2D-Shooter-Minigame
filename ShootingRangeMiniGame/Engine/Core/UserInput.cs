using System.Numerics;

namespace ShootingRangeMiniGame.Engine.Core
{
	public partial class App
	{
		public Vector2 MousePosition { get; private set; }
		public bool LeftMouseButtonPressed { get; private set; }

		protected override void OnMouseMove(MouseEventArgs e)
		{
			MousePosition = new Vector2(e.Location.X, e.Location.Y);

			base.OnMouseMove(e);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			LeftMouseButtonPressed = e.Button == MouseButtons.Left;

			base.OnMouseDown(e);
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			LeftMouseButtonPressed = e.Button != MouseButtons.Left;

			base.OnMouseUp(e);
		}
	}
}