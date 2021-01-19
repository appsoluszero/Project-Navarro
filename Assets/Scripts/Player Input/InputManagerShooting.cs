using System;
using UnityEngine;
using UnityEngine.Events;

public class InputManagerShooting : MonoBehaviour
{
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private KeyCode shootWeapon;
    [SerializeField] private KeyCode reloadWeapon;
    public event EventHandler OnShoot;
    public event EventHandler OnReloading;

    void Update()
    {
        ShootingEvent();
        ReloadingEvent();
    }

    void ShootingEvent()
    {
        if(inventory.playerWeapon.checkType() == weaponType.Automatic || inventory.playerWeapon.checkType() == weaponType.Burst)
        {
            if(Input.GetKey(shootWeapon))
            {
                OnShoot?.Invoke(this, EventArgs.Empty);
            }
        }
        else if(inventory.playerWeapon.checkType() == weaponType.NonAutomatic)
        {
            if(Input.GetKeyDown(shootWeapon))
            {
                OnShoot?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    void ReloadingEvent()
    {
        if(Input.GetKeyDown(reloadWeapon))
        {
            OnReloading?.Invoke(this, EventArgs.Empty);
        }
    }
}
