using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 720f;
    public float jumpForce = 5f;

    private Rigidbody rb;
    private bool isGrounded;
    private bool inputEnabled = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            inputEnabled = !inputEnabled;
            Debug.Log("Ввод изменен: " + (inputEnabled ? "Активирован" : "Деактевирован"));
        }

        if (inputEnabled)
        {
            HandleInput();
        }
    }

    void HandleInput()
    {
        float move = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float rotate = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

        Move(move);
        Rotate(rotate);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    void Move(float move)
    {
        Vector3 moveDirection = transform.forward * move;
        rb.MovePosition(rb.position + moveDirection);
    }

    void Rotate(float rotate)
    {
        Quaternion turn = Quaternion.Euler(0f, rotate, 0f);
        rb.MoveRotation(rb.rotation * turn);
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnGUI()
    {
        if (!inputEnabled)
        {
            GUI.Label(new Rect(10, 10, 200, 20), "Ввод отключен");
        }
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            inputEnabled = true;
            Debug.Log("Ввод не фокусируется");
        }
    }
}