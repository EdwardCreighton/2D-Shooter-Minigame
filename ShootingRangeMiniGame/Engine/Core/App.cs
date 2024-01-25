using Leopotam.Ecs;
using ShootingRangeMiniGame.Engine.Systems;

namespace ShootingRangeMiniGame.Engine.Core
{
	public sealed partial class App : Form
	{
		public float UpdateDeltaTime { get; private set; }
		
		private System.Windows.Forms.Timer _timer;
		
		private EcsWorld _world;
		private EcsSystems _systemsRoot;
		private EcsSystems _gameplaySystems;
		private IGameplaySystemsLoader _gameplayLoader;

		public App(IGameplaySystemsLoader gameplaySystemsLoader)
		{
			_gameplayLoader = gameplaySystemsLoader;
			
			CreateWindow();
			CreateTimer();
			CreateEcsInfrastructure();
		}

		protected override void Dispose(bool disposing)
		{
			DeleteTimer();
			DeleteEcsInfrastructure();
			
			base.Dispose(disposing);
		}

		protected override void OnLoad(EventArgs e)
		{
			_timer.Start();

			base.OnLoad(e);
		}

		public void RestartGame()
		{
			DeleteTimer();
			DeleteEcsInfrastructure();

			CreateTimer();
			CreateEcsInfrastructure();
			
			_timer.Start();
		}

		private void CreateTimer()
		{
			_timer = new();
			_timer.Interval = 10;
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
			_world?.Destroy();
			_world = null;
		}

		private void GameLoopTick(object? sender, EventArgs e)
		{
			_systemsRoot.Run();

			Invalidate();
		}
	}
}