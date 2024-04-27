using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public Grid Grid;
    public override void InstallBindings()
    {
        Container.Bind<GameManager>().AsSingle().NonLazy();
        Container.Bind<GridManager>().AsSingle().NonLazy();
        Container.BindFactory<Grid, Grid.GridFactory>().FromComponentInNewPrefab(Grid);
    }
}