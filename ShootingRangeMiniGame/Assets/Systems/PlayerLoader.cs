using System.Numerics;
using Leopotam.Ecs;
using ShootingRangeMiniGame.Engine.Core;
using ShootingRangeMiniGame.Engine.Components;
using ShootingRangeMiniGame.Assets.Data;
using ShootingRangeMiniGame.Assets.Components;
using ShootingRangeMiniGame.Assets.Markers;

namespace ShootingRangeMiniGame.Assets.Systems
{
	public class PlayerLoader : IEcsInitSystem
	{
		private DataProvider _dataProvider;
		private App _app;
		private EcsWorld _world;
		
		public void Init()
		{
			EcsEntity entity = _world.NewEntity();

			entity.Get<PlayerMarker>();

			ref var transform = ref entity.Get<Transform>();
			transform.Position = new Vector2(_app.ScreenSize.Width / 2f, _app.ScreenSize.Height - 40);

			ref var mesh = ref entity.Get<Mesh>();
			mesh.FillColor = _dataProvider.PlayerFillColor;
			mesh.Points = _dataProvider.PlayerMesh;

			ref var weapon = ref entity.Get<Weapon>();
			weapon.ReloadDuration = 1f;
			weapon.ReloadElapsed = 0f;
			weapon.BulletsLeft = _dataProvider.BulletsCount;
		}
	}
}