using Leopotam.Ecs;
using ShootingRangeMiniGame.Engine.Core;
using ShootingRangeMiniGame.Engine.Components;

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
					int x = mesh.Points[j].X;
					int y = mesh.Points[j].Y;
					float angleRad = transform.Rotation * MathE.Deg2Rad;
					float cos = (float)Math.Cos(angleRad);
					float sin = (float)Math.Sin(angleRad);

					mesh.Points[j].X = (int)Math.Round(cos * x - sin * y + transform.Position.X);
					mesh.Points[j].Y = (int)Math.Round(sin * x + cos * y + transform.Position.Y);
				}
				
				_app.Meshes.Add(mesh);
			}
		}
	}
}