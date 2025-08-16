using Level;
using UnityEngine;
using Zenject;

public class RoadMapInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<SaveManager>().AsSingle();
    }
}