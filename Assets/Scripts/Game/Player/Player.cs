using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private float speed = 3f;
    private bool moveVertically = true;
    private Rigidbody rb;
    private Transform parent;

    private int horizontal = 0;
    private int vertical = 0;

    void Awake()
    {
        PlayerManager pM = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        parent = pM.GetEmptyTransform();
        this.transform.parent = parent;
        if (parent.tag.Equals("Horizontal"))
            this.moveVertically = false;
    }

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
}
