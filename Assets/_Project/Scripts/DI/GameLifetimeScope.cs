using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private GroundSpawnerService _groundSpawnerData;
    [SerializeField] private GameObject _farmTilePrefab;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
        {
            entryPoints.Add<GroundSpawnerPresenter>();
        });

        builder.RegisterInstance(_groundSpawnerData).AsSelf();

        builder.Register<FarmTileFactory>(Lifetime.Singleton)
               .WithParameter("tilePrefab", _farmTilePrefab);
    }
}
