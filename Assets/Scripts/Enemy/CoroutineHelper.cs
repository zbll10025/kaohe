using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHelper : MonoBehaviour
{
    public void InvokeAgainrush(BeeBossRush state, float delay)
    {
        StartCoroutine(DoInvokeAgainrush(state, delay));
    }

    private IEnumerator DoInvokeAgainrush(BeeBossRush state, float delay)
    {
        yield return new WaitForSeconds(delay);
        state.Againrush();
    }
}
