using UnityEngine;
public interface IPhysicsObject
{
    void recieveForce(Vector2 dir, Vector2 hitPos);
}