using System.Numerics;
using Leopotam.Ecs;
using ShootingRangeMiniGame.Engine.Core;
using ShootingRangeMiniGame.Engine.Components;
using ShootingRangeMiniGame.Assets.Data;
using ShootingRangeMiniGame.Assets.Components;
using ShootingRangeMiniGame.Assets.Markers;

namespace ShootingRangeMiniGame.Assets.Systems
{
	public class TargetsLoader : IEcsInitSystem
	{
		private DataProvider _dataProvider;
		private App _app;
		private EcsWorld _world;
		
		public void Init()
		{
			for (int i = 0; i < _dataProvider.TargetsCount; i++)
			{
				EcsEntity targetEntity = _world.NewEntity();
				targetEntity.Get<TargetMarker>();

				ref var transform = ref targetEntity.Get<Transform>();
				float xPosBase = 50f + i * 100f;
				float yPosBase = 50f + (float)Math.Floor(xPosBase / _app.ScreenSize.Width) * 100f;
				xPosBase %= _app.ScreenSize.Width;
				transform.Position = new Vector2(xPosBase + Random.Shared.Next(-20, 20), yPosBase + Random.Shared.Next(-20, 20));

				ref var mesh = ref targetEntity.Get<Mesh>();
				mesh.FillColor = _dataProvider.TargetFillColor;
				mesh.Points = _dataProvider.TargetMesh;

				ref var collider = ref targetEntity.Get<Collider>();
				collider.BoundingBox = _dataProvider.TargetBoundingBox;

				ref var movement = ref targetEntity.Get<Movement>();
				movement.Direction = new Vector2(Random.Shared.Next(-100, 100), Random.Shared.Next(-100, 100));
				movement.Direction = Vector2.Normalize(movement.Direction);
				movement.Speed = Random.Shared.Next(150, 400);
			}
		}
	}
}