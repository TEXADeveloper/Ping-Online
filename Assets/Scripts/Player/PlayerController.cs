using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public int ID;
    [Header("General")]
    [SerializeField] private float speed = 3f;
    [SerializeField] private bool moveVertically = true;
    [SerializeField] private TMP_Text scoreText;
    private Rigidbody rb;

    private int horizontal = 0;
    private int vertical = 0;
    int score = 0;

    public void ReceiveInput(float value)
    { 
        if (moveVertically)
            vertical = (int) value;
        else
            horizontal = (int) value;
    }

    void Start()
    { 
        rb = this.GetComponent<Rigidbody>();
        RigidbodyConstraints constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        if (moveVertically)
            constraints = constraints | RigidbodyConstraints.FreezePositionX;
        else
            constraints = constraints | RigidbodyConstraints.FreezePositionZ;
        rb.constraints =  constraints;
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(speed * horizontal, 0, speed * vertical);
    }

    public void Score()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
