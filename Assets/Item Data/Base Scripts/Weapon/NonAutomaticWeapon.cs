using UnityEngine;

[CreateAssetMenu(fileName = "Non-Automatic Weapon", menuName = "Item/Weapon/Non-Automatic")]
public class NonAutomaticWeapon : WeaponBase
{
    public override weaponType checkType()
    {
        return weaponType.NonAutomatic;
    }
}
