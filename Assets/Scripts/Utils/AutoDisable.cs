using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDisable : MonoBehaviour
{
    [SerializeField] private float duration = 3f;
    private void OnEnable()
    {
        Invoke("DisableSelf", duration);
    }

    private void DisableSelf()
    { 
        gameObject.SetActive(false);
    }
}
