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

		public App(IGameplaySystemsLoader gameplaySystemsLoader)
		{
			CreateWindow();
			CreateTimer();
			CreateEcsInfrastructure(gameplaySystemsLoader);
		}

		protected override void Dispose(bool disposing)
		{
			_timer.Stop();
			_timer.Dispose();
			_timer = null;
			
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
			_timer.Interval = 10;
			_timer.Tick += (_, _) => GameLoopTick();

			UpdateDeltaTime = _timer.Interval / 1000f;
		}

		private void CreateEcsInfrastructure(IGameplaySystemsLoader gameplaySystemsLoader)
		{
			_world = new EcsWorld();

			_systemsRoot = new EcsSystems(_world);
			_gameplaySystems = new EcsSystems(_world);
			gameplaySystemsLoader.AssignSystems(_gameplaySystems);
			
			_systemsRoot
				.Add(_gameplaySystems)
				.Add(new PhysicsSystem())
				.Add(new RenderSystem())
				.Inject(this)
				.Init();
		}

		private void GameLoopTick()
		{
			_systemsRoot.Run();

			Invalidate();
		}
	}
}