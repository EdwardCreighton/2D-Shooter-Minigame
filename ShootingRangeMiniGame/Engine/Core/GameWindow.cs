using System.Numerics;
using ShootingRangeMiniGame.Engine.Components;

namespace ShootingRangeMiniGame.Engine.Core
{
	public partial class App
	{
		public Vector2 MousePosition { get; private set; }
		public Size ScreenSize { get; private set; }
		public List<Mesh> Meshes { get; private set; }

		private void CreateWindow()
		{
			ScreenSize = new Size(800, 800);
			
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = ScreenSize;
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			MinimizeBox = false;
			Text = "Shooting Range Mini Game";
			MouseMove += OnMouseMove;

			CreateGraphics();
			
			DoubleBuffered = true;

			Meshes = new List<Mesh>();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.Clear(Color.White);

			for (int i = 0; i < Meshes.Count; i++)
			{
				e.Graphics.FillPolygon(Meshes[i].FillColor, Meshes[i].Points);
			}
			
			Meshes.Clear();
			
			base.OnPaint(e);
		}

		private void OnMouseMove(object? sender, MouseEventArgs e)
		{
			MousePosition = new Vector2(e.Location.X, e.Location.Y);
		}
	}
}