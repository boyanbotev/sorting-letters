using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    Rigidbody2D rb;
    float maxVelocity = 17f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Move(Vector2 dir)
    {
        Debug.Log("Moving object" + dir.x + dir.y);
        rb.velocity = dir;

        if (rb.velocity.magnitude > maxVelocity)
        {
         rb.velocity = rb.velocity.normalized * maxVelocity;
        }
        //rb.MovePosition(pos);
    }

    public void SetKinematic(bool kinematic)
    {
        rb.isKinematic = kinematic;
    }
}
