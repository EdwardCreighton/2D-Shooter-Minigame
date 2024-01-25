using ShootingRangeMiniGame.Engine.Components;

namespace ShootingRangeMiniGame.Engine.Core
{
	public partial class App
	{
		public Size ScreenSize { get; private set; }
		public List<Mesh> Meshes { get; private set; }

		private Label _countDownLabel;

		public void SetCountdownTimer(int timeLeft)
		{
			int minutes = timeLeft / 60;
			int seconds = timeLeft - minutes * 60;
			_countDownLabel.Text = $"{minutes:00}:{seconds:00}";
		}

		private void CreateWindow()
		{
			ScreenSize = new Size(800, 800);
			
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = ScreenSize;
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			MinimizeBox = false;
			Text = "Shooting Range Mini Game";
			
			_countDownLabel = new Label();
			_countDownLabel.Size = new Size(120, 60);
			_countDownLabel.Font = new Font(_countDownLabel.Font.FontFamily, 20f, FontStyle.Bold);
			_countDownLabel.TextAlign = ContentAlignment.MiddleCenter;
			_countDownLabel.Location = new Point(ScreenSize.Width / 2 - 60, 0);
			_countDownLabel.BackColor = Color.DarkGray;

			Controls.Add(_countDownLabel);
			
			CreateGraphics();
			DoubleBuffered = true;

			Meshes = new List<Mesh>();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.Clear(Color.LightGray);

			for (int i = 0; i < Meshes.Count; i++)
			{
				e.Graphics.FillPolygon(Meshes[i].FillColor, Meshes[i].Points);
			}
			
			Meshes.Clear();
			
			base.OnPaint(e);
		}
	}
}