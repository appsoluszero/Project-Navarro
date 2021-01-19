using UnityEngine;

[CreateAssetMenu(fileName = "Replenishment", menuName = "Item/Replenishment")]
public class ReplenishmentBase : ScriptableObject
{
    public string officialname;
    public float replenishamount;
    public virtual void Replenish()
    {
        Debug.Log("Generic Replenishment!");
    }
}
