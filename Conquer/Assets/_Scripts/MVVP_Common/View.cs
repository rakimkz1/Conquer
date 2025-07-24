using UnityEngine;

public abstract class View<T> : MonoBehaviour where T : class
{
    protected T viewModel;

    // Инициализация ViewModel
    public virtual void BindViewModel(T viewModel)
    {
        this.viewModel = viewModel;
        OnBind();
    }

    // Подписки и связывание UI
    protected abstract void OnBind();
}
