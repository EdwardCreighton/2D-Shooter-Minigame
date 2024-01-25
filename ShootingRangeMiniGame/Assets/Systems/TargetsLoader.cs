﻿using System.Numerics;
using Leopotam.Ecs;
using ShootingRangeMiniGame.Engine.Components;
using ShootingRangeMiniGame.Assets.Components;
using ShootingRangeMiniGame.Assets.Markers;

namespace ShootingRangeMiniGame.Assets.Systems
{
	public class TargetsLoader : IEcsInitSystem
	{
		private EcsWorld _world;
		
		public void Init()
		{
			for (int i = 0; i < 6; i++)
			{
				EcsEntity targetEntity = _world.NewEntity();
				targetEntity.Get<TargetMarker>();

				ref var transform = ref targetEntity.Get<Transform>();
				transform.Position = new Vector2(50 + i * 100, 50 + i * 100);

				ref var mesh = ref targetEntity.Get<Mesh>();
				mesh.FillColor = new SolidBrush(Color.DarkRed);
				mesh.Points = new[]
				{
					new Point(-20, -20),
					new Point(20, -20),
					new Point(20, 20),
					new Point(-20, 20),
				};

				ref var collider = ref targetEntity.Get<Collider>();
				collider.BoundingBox = new[]
				{
					new Vector2(-20, -20),
					new Vector2(20, -20),
					new Vector2(20, 20),
					new Vector2(-20, 20),
				};

				ref var movement = ref targetEntity.Get<Movement>();
				movement.Direction = new Vector2(Random.Shared.Next(-100, 100), Random.Shared.Next(-100, 100));
				movement.Direction = Vector2.Normalize(movement.Direction);
				movement.Speed = 400;
			}
		}
	}
}