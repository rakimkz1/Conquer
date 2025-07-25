using Assets._Scripts.MainHUB;
using Assets._Scripts.Managers;
using System;
using UnityEngine;
using Zenject;

public class HUB_GameContext : MonoInstaller
{
    [SerializeField] private ManaManager manaManager;
    [SerializeField] private Extractor_Model extractor_model;
    [SerializeField] private Extractor_View extractor_view;
    public override void InstallBindings()
    {
        Container.Bind<ManaManager>().FromInstance(manaManager).AsSingle();
        Container.Bind<Extractor_Model>().FromInstance(extractor_model).AsSingle();
        Container.Bind<Extractor_View>().FromInstance(extractor_view).AsSingle();
        Container.BindInterfacesAndSelfTo<Extractor_ViewModel>().AsSingle();
    }
}
