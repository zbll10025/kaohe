using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(menuName ="Event/PlayerSkillEventSo")]
public class PlayerSkillEventSo:ScriptableObject
{
    public UnityAction <PlyerController>OnEventRaised;
    
    public void RiseEvent(PlyerController plyerController)
    {
        OnEventRaised?.Invoke(plyerController);
    }

}
