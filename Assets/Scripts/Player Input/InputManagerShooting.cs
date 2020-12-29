using System;
using UnityEngine;
using UnityEngine.Events;

public class InputManagerShooting : MonoBehaviour
{
    [SerializeField] private PlayerInventory inventoryData;
    [SerializeField] private KeyCode shootWeapon;
    [SerializeField] private KeyCode reloadWeapon;
    public event EventHandler<OnShootArgs> OnShoot;
    public class OnShootArgs : EventArgs {
        public float damage;
        public float effectiverange;
        public float actualrange;
        public float accuracy;
        public int pellet;
    }
    public event EventHandler<OnReloadingArgs> OnReloading;
    public class OnReloadingArgs : EventArgs {
        public int magsize;
    }

    void Update()
    {
        ShootingEvent();
        ReloadingEvent();
    }

    void ShootingEvent()
    {
        if(inventoryData.leftinmagazine > 0)
        {
            if(inventoryData.playerWeapon.type == weaponType.Automatic)
            {
                if(Input.GetKey(shootWeapon))
                {
                    OnShoot?.Invoke(this, new OnShootArgs {
                        damage = inventoryData.playerWeapon.damage,
                        effectiverange = inventoryData.playerWeapon.effectiverange,
                        actualrange = inventoryData.playerWeapon.actualrange,
                        accuracy = inventoryData.playerWeapon.accuracy,
                        pellet = inventoryData.playerWeapon.pelletnumber
                    });
                }
            }
            else if(inventoryData.playerWeapon.type == weaponType.nonAutomatic)
            {
                if(Input.GetKeyDown(shootWeapon))
                {
                    OnShoot?.Invoke(this, new OnShootArgs {
                        damage = inventoryData.playerWeapon.damage,
                        effectiverange = inventoryData.playerWeapon.effectiverange,
                        actualrange = inventoryData.playerWeapon.actualrange,
                        accuracy = inventoryData.playerWeapon.accuracy,
                        pellet = inventoryData.playerWeapon.pelletnumber
                    });
                }
            }
        }
    }

    void ReloadingEvent()
    {
        if(inventoryData.leftinmagazine < inventoryData.playerWeapon.magazinesize && inventoryData.totalammo > 0)
        {
            if(Input.GetKeyDown(reloadWeapon))
            {
                OnReloading?.Invoke(this, new OnReloadingArgs {
                    magsize = inventoryData.playerWeapon.magazinesize
                });
            }
        }
    }
}
