using UnityEngine;
using Zenject;

public class ProjectInstiler : MonoInstaller
{
    public ResourceManager resourceManager;
    public AudioManager audioManager;
    public override void InstallBindings()
    {
        GameObject resource = Container.InstantiatePrefab(resourceManager);
        GameObject audio = Container.InstantiatePrefab(audioManager);
        Container.Bind<ResourceManager>().FromInstance(resource.GetComponent<ResourceManager>()).AsSingle().NonLazy();
        Container.Bind<AudioManager>().FromInstance(audio.GetComponent<AudioManager>()).AsSingle().NonLazy() ;
    }
}