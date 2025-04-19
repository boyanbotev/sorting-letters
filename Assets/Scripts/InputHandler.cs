using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
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

        var rayHits = Physics2D.CircleCastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), 1f, Vector2.zero);

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

        // get all objects who are not in rayhits and call their OnDragEnd
        MoveableObject[] rayHitObjects = rayHits.Select(hit => hit.collider.GetComponent<MoveableObject>()).ToArray();
        var unDraggedItems = objects.Except(rayHitObjects);
        foreach (var hit in unDraggedItems)
        {
            hit.OnDragEnd();
        }
    }

    void HandleMouseUp()
    {
        foreach (var obj in objects)
        {
            obj.OnDragEnd();
        }
    }
}