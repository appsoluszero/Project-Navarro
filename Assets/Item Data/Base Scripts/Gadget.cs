using UnityEngine;

[CreateAssetMenu(fileName = "Tool", menuName = "Item/Tool")]
public class Gadget : ScriptableObject
{
    public string officialname;
    public gadgetType type;
    //the amount of charge available to use -- for turret, the total amount of ammo left
    public float charge;
    [Header("Turret Specific Parameter (Required for turret)")]
    public weaponType turretType;
    public float damage;
    public int pelletnumber;
    public float range;
    public float degreeCover;
    [Header("For automatic/non-automatic")]
    //shot per minute
    public float firerate;
    [Header("For burst")]
    public float timeBetweenShot;
    public float timeBeforeNextBurst;
}

public enum gadgetType
{
    Turret,
    Glowstick
}
