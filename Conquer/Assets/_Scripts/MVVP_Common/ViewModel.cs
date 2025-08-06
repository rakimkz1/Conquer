using System;
using UniRx;
using UnityEngine;
using Zenject;

public abstract class ViewModel<TModel> : IDisposable where TModel : Model
{
    public TModel model;
    protected CompositeDisposable disposables = new CompositeDisposable();

    [Inject]
    public ViewModel(TModel model) 
    {
        this.model = model;
        OnInitialize();
    }

    protected virtual void OnInitialize() { }

    public virtual void Dispose()
    {
        disposables.Dispose();
    }
}
