using Zenject;

public class ProjectInstaller : MonoInstaller
{
    #region Fields
    public Grid Grid;
    #endregion
    public override void InstallBindings()
    {
        Container.Bind<GridTable>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GameManager>().AsSingle();
        Container.Bind<GridManager>().AsSingle();
        Container.BindFactory<Grid, Grid.GridFactory>().FromComponentInNewPrefab(Grid);
    }
}