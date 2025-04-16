using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    MoveableObject clickedObject;
    Vector3 lastPos = Vector2.zero;

    private void Awake()
    {
        InvokeRepeating("SetLastPos", 0.1f, 0.1f);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleMouseDown();
        }

        if (Input.GetMouseButton(0))
        {
            HandleMouseDrag();
        }

        if (Input.GetMouseButtonUp(0))
        {
            HandleMouseUp();
        }
    }

    void HandleMouseDown()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lastPos = mousePos;

        var directHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (directHit.collider)
        {
            clickedObject = directHit.collider.GetComponent<MoveableObject>();
        }
        else
        {
            clickedObject = null;
        }
    }

    void HandleMouseDrag()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;


        if (clickedObject)
        {
            //clickedObject.transform.position = mousePos;
        }

        var rayHits = Physics2D.CircleCastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), 1f, Vector2.zero);

        foreach (var hit in rayHits)
        {
            if (hit.collider /*&& hit != clickedObject*/)
            {
                var obj = hit.collider.GetComponent<MoveableObject>();
                if (obj)
                {
                    var dir = (mousePos - lastPos) * 3f;
                    //obj.SetKinematic(true);
                    obj.Move(dir);
                }
            }
        }
    }

    private void SetLastPos()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x != lastPos.x || mousePos.y != lastPos.y)
        {
            lastPos = Vector2.Lerp(lastPos, mousePos, 0.1f);
        }
    }

    void HandleMouseUp()
    {
        clickedObject = null;
    }
}