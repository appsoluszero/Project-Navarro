using System;
using UnityEngine;
using UnityEngine.Events;

public class InputManagerAction : MonoBehaviour
{
    [SerializeField] private KeyCode shootWeapon;
    [SerializeField] private UnityEvent OnShoot;

    void Update()
    {
        if(Input.GetKeyDown(shootWeapon))
        {
            OnShoot?.Invoke();
        }
    }
}
