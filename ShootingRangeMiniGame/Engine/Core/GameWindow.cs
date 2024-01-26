using ShootingRangeMiniGame.Engine.Components;

namespace ShootingRangeMiniGame.Engine.Core
{
	public partial class App
	{
		private const string FontFamilyName = "Segoe UI";
		
		public Size ScreenSize { get; private set; }
		public List<Mesh> Meshes { get; private set; }
		
		private int _uiAreaWidth;
		private FontFamily _defaultFontFamily;
		
		private Panel _gameUIPanel;
		private Label _countDownLabel;
		private Label _targetsStatusLabel;
		private Label _bulletsStatusLabel;
		private Label _weaponStatusLabel;

		private Panel _endOfGameUIPanel;
		private Label _endOfGameLabel;

		private void CreateWindow()
		{
			_defaultFontFamily = new FontFamily(FontFamilyName);
			ScreenSize = new Size(800, 800);
			_uiAreaWidth = 400;

			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = ScreenSize with { Width = ScreenSize.Width + _uiAreaWidth };
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			MinimizeBox = false;
			Text = "Shooting Range Mini Game";
			
			CreateGraphics();
			DoubleBuffered = true;

			CreateGameUI();
			CreateEndOfGameUI();

			Meshes = new List<Mesh>();
		}

		private void DeleteWindow()
		{
			Meshes.Clear();
			Meshes = null;
			
			_defaultFontFamily.Dispose();
			_defaultFontFamily = null;
		}

		private void CreateGameUI()
		{
			_gameUIPanel = new Panel();
			_gameUIPanel.Size = new Size(_uiAreaWidth, 0);
			_gameUIPanel.Dock = DockStyle.Right;
			_gameUIPanel.BackColor = Color.DarkGray;
			_gameUIPanel.Padding = new Padding(10);
			Controls.Add(_gameUIPanel);
			
			Font boldFont = new Font(_defaultFontFamily, 20, FontStyle.Bold);
			Font boldFontSmall = new Font(_defaultFontFamily, 12, FontStyle.Bold);

			GroupBox groupBox = new GroupBox();
			groupBox.Size = new Size(0, 200);
			groupBox.Font = boldFontSmall;
			groupBox.Text = "Weapon Status";
			groupBox.Dock = DockStyle.Top;
			groupBox.Padding = new Padding(20);
			groupBox.BackColor = _gameUIPanel.BackColor;
			_gameUIPanel.Controls.Add(groupBox);

			_weaponStatusLabel = new Label();
			_weaponStatusLabel.Dock = DockStyle.Fill;
			_weaponStatusLabel.Font = boldFont;
			_weaponStatusLabel.TextAlign = ContentAlignment.MiddleCenter;
			_weaponStatusLabel.BackColor = _gameUIPanel.BackColor;
			groupBox.Controls.Add(_weaponStatusLabel);

			_bulletsStatusLabel = new Label();
			_bulletsStatusLabel.Size = new Size(0, 40);
			_bulletsStatusLabel.Dock = DockStyle.Top;
			_bulletsStatusLabel.Font = boldFont;
			_bulletsStatusLabel.TextAlign = ContentAlignment.TopLeft;
			_bulletsStatusLabel.BackColor = _gameUIPanel.BackColor;
			groupBox.Controls.Add(_bulletsStatusLabel);

			_targetsStatusLabel = new Label();
			_targetsStatusLabel.Size = new Size(0, 60);
			_targetsStatusLabel.Dock = DockStyle.Top;
			_targetsStatusLabel.Font = boldFont;
			_targetsStatusLabel.TextAlign = ContentAlignment.TopLeft;
			_targetsStatusLabel.BackColor = _gameUIPanel.BackColor;
			_gameUIPanel.Controls.Add(_targetsStatusLabel);

			_countDownLabel = new Label();
			_countDownLabel.Size = new Size(0, 60);
			_countDownLabel.Dock = DockStyle.Top;
			_countDownLabel.Font = boldFont;
			_countDownLabel.TextAlign = ContentAlignment.TopLeft;
			_countDownLabel.BackColor = _gameUIPanel.BackColor;
			_gameUIPanel.Controls.Add(_countDownLabel);
		}

		private void CreateEndOfGameUI()
		{
			_endOfGameUIPanel = new Panel();
			_endOfGameUIPanel.Size = new Size(_uiAreaWidth, 0);
			_endOfGameUIPanel.Dock = DockStyle.Right;
			_endOfGameUIPanel.BackColor = Color.DarkGray;
			_endOfGameUIPanel.Padding = new Padding(10);
			Controls.Add(_endOfGameUIPanel);
			
			Font boldFont = new Font(_defaultFontFamily, 15, FontStyle.Bold);

			Button restartButton = new Button();
			restartButton.Size = new Size(160, 60);
			restartButton.Location = new Point(120, 300);
			restartButton.Font = boldFont;
			restartButton.Text = "Restart";
			restartButton.Click += OnRestartButtonPressed;
			_endOfGameUIPanel.Controls.Add(restartButton);

			_endOfGameLabel = new Label();
			_endOfGameLabel.Size = new Size(0, 200);
			_endOfGameLabel.Dock = DockStyle.Top;
			_endOfGameLabel.Font = boldFont;
			_endOfGameLabel.TextAlign = ContentAlignment.TopLeft;
			_endOfGameLabel.BackColor = _endOfGameUIPanel.BackColor;
			_endOfGameUIPanel.Controls.Add(_endOfGameLabel);
		}

		private void OnRestartButtonPressed(object? sender, EventArgs e)
		{
			RestartGame();
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

		private void UpdateGameProgress()
		{
			int timeLeft = (int)Math.Ceiling(_gameProgressData.TimeLeft);
			int minutes = timeLeft / 60;
			int seconds = timeLeft - minutes * 60;
			_countDownLabel.Text = $"Time left - {minutes:00}:{seconds:00}";
			
			_targetsStatusLabel.Text = $"Targets left - {_gameProgressData.TargetsLeft}";
			
			if (_gameProgressData.WeaponReady)
			{
				_weaponStatusLabel.Text = "READY";
				_weaponStatusLabel.ForeColor = Color.Green;
			}
			else
			{
				_weaponStatusLabel.Text = "RELOADING...";
				_weaponStatusLabel.ForeColor = Color.DarkRed;
			}

			_bulletsStatusLabel.Text = $"Bullets left - {_gameProgressData.BulletsLeft}";
		}

		private void UpdateEndOfGameStatus()
		{
			string result = _gameProgressData.GameResult ? "WIN" : "LOSE";
			int destroyedTargets = _gameProgressData.InitialTargetsCount - _gameProgressData.TargetsLeft;
			int firedBullets = _gameProgressData.InitialBulletsCount - _gameProgressData.BulletsLeft;
			int time = _gameProgressData.InitialTime - (int)Math.Floor(_gameProgressData.TimeLeft);
			int minutes = time / 60;
			int seconds = time - minutes * 60;

			_endOfGameLabel.Text = $"YOU {result}!\n\n" +
			                       $"Time: {minutes:00}:{seconds:00}\n" +
			                       $"Targets destroyed: {destroyedTargets} / {_gameProgressData.InitialTargetsCount}\n" +
			                       $"Bullets fired: {firedBullets} / {_gameProgressData.InitialBulletsCount}";
		}
	}
}