# Введение

---

Это тестовое задание на позицию C# Developer в Thera Interactive.
Требовалось создать на ЯП C# игру "Тир" без использования готовых игровых движков.

Для разработки архитектуры геймплея был выбран легковесный ecs-фреймворк LeoEcs.
В качестве окна с рендером игры используется Windows Forms.

# Процесс разработки

---

На разработку было затрачено примерно 64 часа, 40 из которых ушли на исследования и эксперименты с Windows Forms,
Так как до этих пор мне не доводилось работать с графикой вне игровых движков.
Остальное время - разработка архитектуры, движка, геймплея и тестирования.

У меня давно есть идея собрать свой игровой движок для создания 2D игр, особенностью которого будет интегрированный Ecs фреймворк.
Данное тестовое задание стало для меня своеобразной пробой пера, поэтому я полностью погрузился в разработку и каждая сфера, часть ее была для меня интересной.

# Комментарии по архитектуре

---

С самого начала я решил, что буду разрабатывать геймплейную часть в парадигме ecs, так как она гораздо больше, чем ООП подходит для игровой логики.
Хоть ООП и более интуитивный для большинства программистов, это все же Enterprise подход к разработке.
В играх чаще всего геймплей формируется за счет комбинаций: есть объекты, а на них лежат компоненты, и в зависимости от наличия тех или иных компонентов, объект может выполнять то или иное действие.

Правильный фреймворк не только предоставляет удобный инструментарий, но еще и является оптимизированным по памяти и скорости.

Свой проект я условно разделил на две части: Assets и Engine (почему условно, будет понятно в разделе [сложности](#Cложности)). Engine содержит скрипты, формирующие фундамент игры. Assets - вся геймплейная логика.

Благодаря ecs в классе `App` можно легко записать в нужном порядке системы, которые должны отработать в одном игровом цикле

```csharp
// В конструкторе класса создаем среду Ecs 
private void CreateEcsInfrastructure()
{
	_world = new EcsWorld();

	_systemsRoot = new EcsSystems(_world);
	_gameplaySystems = new EcsSystems(_world);
	_gameplayLoader.AssignSystems(_gameplaySystems);
	
	_systemsRoot
		.Add(_gameplaySystems)
		.Add(new PhysicsSystem())
		.Add(new RenderSystem())
		.Inject(this)
		.Init();
}

// Таймером на ивенте Tick вызываем метод обработки
private void GameLoopTick(object? sender, EventArgs e)
{
	if (!_running)
		return;
	
	_systemsRoot.Run();
	CheckGameResult();

	UpdateGameProgress();

	Invalidate();
}
```

Весь геймплей записывается в `_gameplaySystems` через `IGameplaySystemsLoader.AssignSystems(EcsSystems)`, который реализован в директории Assets

```csharp
namespace ShootingRangeMiniGame.Assets
{
	public class ShootingRangeMiniGameLoader : IGameplaySystemsLoader
	{
		// ...
		
		public void AssignSystems(EcsSystems gameplaySystems)
		{
			gameplaySystems
				.Add(new PlayerLoader())
				.Add(new TargetsLoader())

				.Add(new PlayerRotationSystem())
				.Add(new PlayerShootSystem())
				.Add(new SpawnProjectileSystem())

				.Add(new TargetsOnCollisionResolver())
				.Add(new ProjectilesOnCollisionResolver())
				.Add(new GameProgressSystem())

				.Add(new MovementSystem())

				.Inject(_dataProvider);
		}
	}
}
```

Также сразу видно, какая логика в каком порядке обрабатывается.

# Сложности

---

В разработке была лишь одна сложность - Windows Forms не подходят для игры в том виде, каком я ее себе представлял.
Поэтому пришлось выкручиваться.

Не было возможности создать `UISystem` наподобие `RenderSystem` - из компонентов сформировать UI и управлять им из геймплея.
Весь UI пришлось хардкодить в "движке", а управлять из геймплея заполняя структуру `GameProgressData`

```csharp
namespace ShootingRangeMiniGame.Engine.Core
{
	public struct GameProgressData
	{
		public bool GameResult;

		public int InitialTargetsCount;
		public int InitialTime;
		public int InitialBulletsCount;
		
		public float TimeLeft;
		public int TargetsLeft;
		public int FreeProjectiles;

		public int BulletsLeft;
		public bool WeaponReady;
	}
}
```

> **Вывод**
> 
>Windows Forms не подходит как фреймворк рендера для игрового generic движка.
>В своем будущем движке использовать другой фреймворк.

Еще я не понял, почему не работает графика в отдельном `PictureBox`. Я могу рисовать и видеть объекты только на графике основной формы.
Все это привело к необходимости создавать ряд костыльных решений, чтобы сделать приятный UI.