using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public GameObject follower;
    public GameObject bar;
    private void Update()
    {
        Vector2 hpbox = Camera.main.WorldToScreenPoint(follower.transform.position);
        bar.GetComponent<RectTransform>().position = hpbox;
    }
}
