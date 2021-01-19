using UnityEngine;

[CreateAssetMenu(fileName = "Automatic Weapon", menuName = "Item/Weapon/Automatic")]
public class AutomaticWeapon : WeaponBase
{
    public override weaponType checkType()
    {
        return weaponType.Automatic;
    }
}
