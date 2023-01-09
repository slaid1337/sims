using UnityEngine;
using UnityEngine.Events;

public class ActivityPoint : MonoBehaviour
{
    private bool _isBusy;
    public bool IsBusyForRegistration;

    public UnityEvent<bool> OnFreePoint = new UnityEvent<bool>();

    public void TakePoint()
    {
        _isBusy = true;
    }

    public void FreePoint()
    {
        _isBusy = false;
        IsBusyForRegistration = false;
        OnFreePoint.Invoke(false);
    }

    public bool IsBusy()
    {
        return _isBusy;
    }
}