using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Properties")]
    [SerializeField] private float playerSpeed;
    private InputManager playerInput;
    private Rigidbody2D rb;
    void Awake()
    {
        playerInput = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        rb.velocity = playerInput.inputVector * playerSpeed;
    }
}
