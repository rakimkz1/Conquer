using UnityEngine;
using Zenject;

public abstract class View<TViewModel> : MonoBehaviour where TViewModel : class
{
    protected TViewModel viewModel;

    protected virtual void Start()
    {
        OnBind();
    }

    protected abstract void OnBind();

}
