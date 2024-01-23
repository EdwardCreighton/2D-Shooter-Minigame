using Leopotam.Ecs;

namespace ShootingRangeMiniGame.Engine.Core
{
	public sealed partial class App : Form
	{
		private System.Windows.Forms.Timer _timer;
		private EcsWorld _world;
		private EcsSystems _systemsRoot;

		public App()
		{
			CreateWindow();
			CreateTimer();
			CreateEcsInfrastructure();
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

		private void CreateEcsInfrastructure()
		{
			_world = new EcsWorld();

			_systemsRoot = new EcsSystems(_world);
			_systemsRoot.Init();
		}

		private void GameLoopTick()
		{
			_systemsRoot.Run();

			Invalidate();
		}
	}
}