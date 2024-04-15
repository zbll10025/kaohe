using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(menuName = "Event/VoidEventSO")]
public class voidEventSO : ScriptableObject
{
    public UnityAction OnEventRised;
    public void RaiseEvent()
    {
        OnEventRised?.Invoke();
    }
}

