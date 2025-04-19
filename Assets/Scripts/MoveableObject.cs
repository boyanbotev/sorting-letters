using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    public bool isDragging = false;
    [SerializeField] float speed = 30f;
    Vector2 offset;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Move(Vector2 mousePos)
    {
        var offsetTarget = new Vector2(mousePos.x - offset.x, mousePos.y - offset.y);
        var dir = ((Vector3)offsetTarget - transform.position) * speed;
        rb.velocity = dir;
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
