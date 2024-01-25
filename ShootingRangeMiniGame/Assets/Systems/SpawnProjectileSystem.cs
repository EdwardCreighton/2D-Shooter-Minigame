using System.Numerics;
using Leopotam.Ecs;
using ShootingRangeMiniGame.Engine.Components;
using ShootingRangeMiniGame.Assets.Data;
using ShootingRangeMiniGame.Assets.Components;

namespace ShootingRangeMiniGame.Assets.Systems
{
	public class SpawnProjectileSystem : IEcsRunSystem
	{
		private EcsFilter<OnSpawnProjectile> _filter;
		
		private DataProvider _dataProvider;
		private EcsWorld _world;

		public void Run()
		{
			int initialCount = _filter.GetEntitiesCount();
			
			for (int i = 0; i < initialCount; i++)
			{
				ref var onSpawn = ref _filter.Get1(i);
				
				EcsEntity entity = _world.NewEntity();
				
				ref var projectile = ref entity.Get<Projectile>();
				projectile.Durability = 3;

				ref var transform = ref entity.Get<Transform>();
				transform.Position = onSpawn.Position;
				
				ref var movement = ref entity.Get<Movement>();
				movement.Direction = onSpawn.Direction;
				movement.Speed = _dataProvider.ProjectileSpeed;

				ref var mesh = ref entity.Get<Mesh>();
				mesh.FillColor = new SolidBrush(Color.Goldenrod);
				mesh.Points = new[]
				{
					new Point(-5, -5),
					new Point(5, -5),
					new Point(5, 5),
					new Point(-5, 5),
				};

				ref var collider = ref entity.Get<Collider>();
				collider.BoundingBox = new[]
				{
					new Vector2(-5f, -5f),
					new Vector2(5f, -5f),
					new Vector2(5f, 5f),
					new Vector2(-5f, 5f),
				};
				
				_filter.GetEntity(i).Del<OnSpawnProjectile>();
			}
		}
	}
}