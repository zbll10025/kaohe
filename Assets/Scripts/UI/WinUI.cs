using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WinUI : MonoBehaviour
{
    public Character b1;
    public Character b2;
    public GameObject Winui;
    private void Update()
    {
        if (b1.currentHealth <= 0 && b2.currentHealth <= 0)
        {
            Time.timeScale = 0f;
            Winui.SetActive(true);
        }
    }
    }