using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnableObj : MonoBehaviour
{
    public UnityEvent events;

    private void OnEnable()
    {
        events.Invoke();
    }
}
