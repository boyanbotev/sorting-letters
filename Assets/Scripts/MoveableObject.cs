using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    Rigidbody2D rb;
    public bool isDragging = false;
    public Vector2 offset;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Move(Vector2 mousePos)
    {
        var offsetTarget = new Vector2(mousePos.x - offset.x, mousePos.y - offset.y);
        var dir = ((Vector3)offsetTarget - transform.position) * 30f;
        rb.velocity = dir;
    }

    public void SetKinematic(bool kinematic)
    {
        rb.isKinematic = kinematic;
    }

    public void OnDragStart(Vector2 mousePos)
    {
        isDragging = true;
        offset = mousePos - (Vector2)transform.position;
    }

    public void OnDragEnd() {
        isDragging = false;
        offset = Vector2.zero;
    }
}
