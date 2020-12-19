using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Transform flashLight;
    void Update()
    {
        Vector3 LookDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        LookDirection.z = 0f;
        float angle = -CalculateAngle(LookDirection);
        Vector3 rot = new Vector3(0f, 0f, angle);
        flashLight.rotation = Quaternion.Euler(rot); 
    }

    float CalculateAngle(Vector3 LookDir)
    {
        return Mathf.Atan2(LookDir.x, LookDir.y) * Mathf.Rad2Deg;
    }
}
