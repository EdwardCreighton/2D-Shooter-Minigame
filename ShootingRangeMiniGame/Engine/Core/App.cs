using Leopotam.Ecs;
using ShootingRangeMiniGame.Engine.Systems;

namespace ShootingRangeMiniGame.Engine.Core
{
	public sealed partial class App : Form
	{
		private const int TickIntervalMilliseconds = 10;
		
		public float UpdateDeltaTime { get; private set; }
		public ref GameProgressData GameProgressData => ref _gameProgressData;
		
		private System.Windows.Forms.Timer _timer;

		private GameProgressData _gameProgressData;
		private bool _running;
		
		private EcsWorld _world;
		private EcsSystems _systemsRoot;
		private EcsSystems _gameplaySystems;
		private readonly IGameplaySystemsLoader _gameplayLoader;

		public App(IGameplaySystemsLoader gameplaySystemsLoader)
		{
			_gameplayLoader = gameplaySystemsLoader;
			
			CreateWindow();
			CreateTimer();
			CreateEcsInfrastructure();
		}

		protected override void Dispose(bool disposing)
		{
			DeleteEcsInfrastructure();
			DeleteTimer();
			DeleteWindow();
			
			base.Dispose(disposing);
		}

		protected override void OnLoad(EventArgs e)
		{
			_endOfGameUIPanel.Visible = false;
			_running = true;
			
			_timer.Start();

			base.OnLoad(e);
		}

		private void CreateTimer()
		{
			_timer = new();
			_timer.Interval = TickIntervalMilliseconds;
			_timer.Tick += GameLoopTick;

			UpdateDeltaTime = _timer.Interval / 1000f;
		}

		private void DeleteTimer()
		{
			_timer.Stop();
			_timer.Tick -= GameLoopTick;
			_timer.Dispose();
			_timer = null;
		}

		private void CreateEcsInfrastructure()
		{
			_world = new EcsWorld();

			_systemsRoot = new EcsSystems(_world);
			_gameplaySystems = new EcsSystems(_world);
			_gameplayLoader.AssignSystems(_gameplaySystems);
			
			_systemsRoot
				.Add(_gameplaySystems)
				.Add(new PhysicsSystem())
				.Add(new RenderSystem())
				.Inject(this)
				.Init();
		}

		private void DeleteEcsInfrastructure()
		{
			_systemsRoot?.Destroy();
			_systemsRoot = null;
			_gameplaySystems = null;
			_world?.Destroy();
			_world = null;
		}

		private void GameLoopTick(object? sender, EventArgs e)
		{
			if (!_running)
				return;
			
			_systemsRoot.Run();
			CheckGameResult();

			UpdateGameProgress();

			Invalidate();
		}

		private void EndGame()
		{
			_gameUIPanel.Visible = false;
			_endOfGameUIPanel.Visible = true;
			_running = false;
			
			UpdateEndOfGameStatus();
		}

		private void RestartGame()
		{
			DeleteTimer();
			DeleteEcsInfrastructure();

			CreateTimer();
			CreateEcsInfrastructure();
			
			_gameUIPanel.Visible = true;
			_endOfGameUIPanel.Visible = false;
			_running = true;
			
			_timer.Start();
		}

		private void CheckGameResult()
		{
			if (_gameProgressData.TargetsLeft == 0)
			{
				_gameProgressData.GameResult = true;
				EndGame();
			}
			else if (_gameProgressData.TimeLeft <= 0)
			{
				_gameProgressData.GameResult = false;
				EndGame();
			}
			else if (_gameProgressData.BulletsLeft == 0 && _gameProgressData.FreeProjectiles == 0)
			{
				_gameProgressData.GameResult = false;
				EndGame();
			}
		}
	}
}