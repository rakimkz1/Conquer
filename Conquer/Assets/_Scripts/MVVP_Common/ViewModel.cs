using System;
using UniRx;
using UnityEngine;
using Zenject;

public abstract class ViewModel<TModel> : IInitializable, IDisposable where TModel : Model
{
    protected TModel model;
    protected CompositeDisposable disposables = new CompositeDisposable();

    // you need to initialize it by Container.BindInterfacesAndSelfTo<TViewModel>().AsSingle();
    [Inject]
    public void Construct(TModel model)
    {
        this.model = model;
    }

    public virtual void Initialize()
    {
        OnInitialize();
    }

    protected virtual void OnInitialize() { }

    public virtual void Dispose()
    {
        disposables.Dispose();
    }
}
