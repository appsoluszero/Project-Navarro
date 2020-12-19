using UnityEngine;
public class RigidbodyMovement : MonoBehaviour, IMovementMethod
{
    //Moving a character with regards to physics
    [Header("Player Properties")]
    [SerializeField] private float playerSpeed;
    [HideInInspector] public Vector3 inputVector;
    private Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        rb.velocity = inputVector * playerSpeed;
    }
    public void SetMovement(Vector3 vec)
    {
        inputVector = vec;
    }
}
