using UnityEngine;
public class PlayerTransformMovement : MonoBehaviour, IMovementMethod
{
    //Moving a character without regards to physics
    [Header("Player Properties")]
    [SerializeField] private float playerSpeed;
    [HideInInspector] public Vector3 inputVector;
    void Awake()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }
    void Update()
    {
        transform.position += inputVector * playerSpeed * Time.deltaTime;
    }
    public void SetMovement(Vector3 vec)
    {
        inputVector = vec;
    }
}
