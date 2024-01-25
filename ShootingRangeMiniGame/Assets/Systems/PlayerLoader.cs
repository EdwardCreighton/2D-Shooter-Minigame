﻿using System.Numerics;
using Leopotam.Ecs;
using ShootingRangeMiniGame.Engine.Core;
using ShootingRangeMiniGame.Engine.Components;
using ShootingRangeMiniGame.Assets.Markers;

namespace ShootingRangeMiniGame.Assets.Systems
{
	public class PlayerLoader : IEcsRunSystem
	{
		private App _app;
		private EcsWorld _world;
		
		public void Run()
		{
			EcsEntity entity = _world.NewEntity();

			entity.Get<PlayerMarker>();

			ref var transform = ref entity.Get<Transform>();
			transform.Position = new Vector2(_app.ScreenSize.Width / 2f, _app.ScreenSize.Height - 40);

			ref var mesh = ref entity.Get<Mesh>();
			mesh.FillColor = Brushes.DarkRed;
			mesh.Points = new[]
			{
				new Point(-15, -15),
				new Point(15, -15),
				new Point(15, 15),
				new Point(7, 15),
				new Point(7, 55),
				new Point(-7, 55),
				new Point(-7, 15),
				new Point(-15, 15),
			};
		}
	}
}