using Leopotam.Ecs;
using ShootingRangeMiniGame.Engine.Components;
using ShootingRangeMiniGame.Engine.Core;

namespace ShootingRangeMiniGame.Engine.Systems
{
	public class RenderSystem : IEcsRunSystem
	{
		private App _app;
		private EcsFilter<Mesh> _filter;

		public void Run()
		{
			for (int i = 0; i < _filter.GetEntitiesCount(); i++)
			{
				var mesh = new Mesh(_filter.Get1(i));
				ref var transform = ref _filter.GetEntity(i).Get<Transform>();
				
				for (int j = 0; j < mesh.Points.Length; j++)
				{
					mesh.Points[j].X += (int)Math.Round(transform.Position.X);
					mesh.Points[j].Y += (int)Math.Round(transform.Position.Y);
				}
				
				_app.Meshes.Add(mesh);
			}
		}
	}
}