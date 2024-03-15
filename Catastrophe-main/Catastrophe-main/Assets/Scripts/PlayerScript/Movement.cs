using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]


public class Movement : MonoBehaviour
{
    Rigidbody2D rgb2d;

    [HideInInspector]
    public Vector3 movementVector;

    Animate animate;

    [HideInInspector]
    public float lastHorizontalVector;

    [HideInInspector]
    public float lastVerticalVector;

    [SerializeField] public float speed = 3f;

    // Start is called before the first frame update
    private void Awake()
    {
        rgb2d = GetComponent<Rigidbody2D>();
        movementVector = new Vector3();
        animate = GetComponent<Animate>();
    }

    private void Start()
    {
        lastHorizontalVector = -1f;
        lastVerticalVector = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = Input.GetAxisRaw("Vertical");
        

        animate.horizontal = movementVector.x;

        if (movementVector.x != 0)
            lastHorizontalVector = movementVector.x;
        if (movementVector.y != 0)
            lastVerticalVector = movementVector.y;

        rgb2d.velocity = movementVector * speed;
    }
}
