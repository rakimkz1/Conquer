using UniRx;
using UnityEngine;

public abstract class ViewModel<T> where T : Model
{
    protected T model;
    protected CompositeDisposable disposables = new CompositeDisposable();

    public ViewModel(T model)
    {
        this.model = model;
        OnInitialize();
    }

    // Переопределяется в дочерних ViewModel
    protected virtual void OnInitialize() { }

    public virtual void Dispose()
    {
        disposables.Dispose();
    }
}
