using Leopotam.Ecs;

namespace ShootingRangeMiniGame.Engine.Core
{
	public sealed partial class App : Form
	{
		private System.Windows.Forms.Timer _timer;
		private EcsWorld _world;
		private EcsSystems _systemsRoot;
		private EcsSystems _gameplaySystems;

		public App(GameplaySystemsLoader gameplaySystemsLoader)
		{
			CreateWindow();
			CreateTimer();
			CreateEcsInfrastructure(gameplaySystemsLoader);
		}

		protected override void Dispose(bool disposing)
		{
			_timer.Dispose();
			
			_systemsRoot?.Destroy();
			_systemsRoot = null;
			_world?.Destroy();
			_world = null;
			
			base.Dispose(disposing);
		}

		protected override void OnLoad(EventArgs e)
		{
			_timer.Start();

			base.OnLoad(e);
		}

		private void CreateTimer()
		{
			_timer = new();
			_timer.Interval = 20;
			_timer.Tick += (_, _) => GameLoopTick();
		}

		private void CreateEcsInfrastructure(GameplaySystemsLoader gameplaySystemsLoader)
		{
			_world = new EcsWorld();

			_systemsRoot = new EcsSystems(_world);
			_gameplaySystems = new EcsSystems(_world);
			gameplaySystemsLoader.AssignSystems(_gameplaySystems);
			
			_systemsRoot
				.Add(_gameplaySystems)
				.Init();
		}

		private void GameLoopTick()
		{
			_systemsRoot.Run();

			Invalidate();
		}
	}
}