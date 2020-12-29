using UnityEngine;

[CreateAssetMenu(fileName = "Replenishment", menuName = "Item/Replenishment")]
public class Replenishment : ScriptableObject
{
    public string officialname;
    public replenishType type;
    //the amount of replenishment per use in percent of the maximum capacity
    public float amount;
    //the amount of charge available
    public int charge;
}

public enum replenishType
{
    Health,
    Ammo,
    Gadget
}
