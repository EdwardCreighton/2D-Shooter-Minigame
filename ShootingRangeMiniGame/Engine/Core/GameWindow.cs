namespace ShootingRangeMiniGame.Engine.Core
{
	public partial class App
	{
		public Size ScreenSize { get; private set; }
		
		private void CreateWindow()
		{
			ScreenSize = new Size(800, 800);
			
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = ScreenSize;
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			MinimizeBox = false;
			Text = "Shooting Range Mini Game";

			CreateGraphics();
			
			DoubleBuffered = true;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.Clear(Color.LightBlue);
			
			base.OnPaint(e);
		}
	}
}