using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] float dragRadius = 0.7f;
    [SerializeField] float dragDistance = 0.7f;
    [SerializeField] BoxCollider2D bounds;

    List<MoveableObject> objects = new List<MoveableObject>();

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            HandleMouseDrag();
        }

        if (Input.GetMouseButtonUp(0))
        {
            HandleMouseUp();
        }
    }

    void HandleMouseDrag()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        if (!IsWithinBounds(mousePos))
        {
            //HandleMouseUp();
            return;
        }

        var rayHits = Physics2D.CircleCastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), dragRadius, Vector2.zero, dragDistance, LayerMask.GetMask("Letter"));

        foreach (var hit in rayHits)
        {
            if (hit.collider)
            {
                var obj = hit.collider.GetComponent<MoveableObject>();
                if (obj)
                {
                    if (!obj.isDragging)
                    {
                        objects.Add(obj);
                        obj.OnDragStart(mousePos);
                    }

                    obj.Move(mousePos);
                }
            }
        }

        var rayHitObjects = rayHits.Select(hit => hit.collider.GetComponent<MoveableObject>());
        var unDraggedItems = objects.Except(rayHitObjects.ToArray());
        foreach (var hit in unDraggedItems)
        {
            hit.OnDragEnd();
        }

        objects = rayHitObjects.ToList();
    }

    void HandleMouseUp()
    {
        foreach (var obj in objects)
        {
            obj.OnDragEnd();
        }

        objects.Clear();
    }

    bool IsWithinBounds(Vector2 pos)
    {
        return bounds.bounds.Contains(pos);
    }
}
// TODO:
// wierd stopping after coming out of range sometimes bug