using Leopotam.Ecs;
using ShootingRangeMiniGame.Engine.Core;
using ShootingRangeMiniGame.Assets.Data;
using ShootingRangeMiniGame.Assets.Components;
using ShootingRangeMiniGame.Assets.Markers;

namespace ShootingRangeMiniGame.Assets.Systems
{
	public class GameProgressSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsFilter<Weapon, PlayerMarker> _playerWeaponFilter;
		private EcsFilter<Projectile> _projectilesFilter;

		private DataProvider _dataProvider;
		private App _app;

		private float _timeLeft;

		public void Init()
		{
			_timeLeft = _dataProvider.Time;
		}

		public void Run()
		{
			_timeLeft -= _app.UpdateDeltaTime;
			_app.SetCountdownTimer((int)Math.Ceiling(_timeLeft));

			if (_timeLeft <= 0)
			{
				_app.RestartGame();
				return;
			}

			if (_playerWeaponFilter.Get1(0).BulletsLeft == 0 && _projectilesFilter.GetEntitiesCount() == 0)
			{
				_app.RestartGame();
				return;
			}
		}
	}
}