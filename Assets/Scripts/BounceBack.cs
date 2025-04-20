using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BounceBack : MonoBehaviour
{
    [SerializeField] float bounceBackSpeed = 1f;
    SortingManager sortingManager;
    TextMeshPro text;
    string letter;
    string leftLetter;
    string rightLetter;
    BoxCollider2D leftArea;
    BoxCollider2D rightArea;
    MoveableObject moveableObject;
    bool isBouncingBack = false;

    private void Start()
    {
        sortingManager = FindObjectOfType<SortingManager>();
        leftLetter = Clean(sortingManager.leftLetter);
        rightLetter = Clean(sortingManager.rightLetter);
        leftArea = sortingManager.leftArea.GetComponent<BoxCollider2D>();
        rightArea = sortingManager.rightArea.GetComponent<BoxCollider2D>();

        text = GetComponentInChildren<TextMeshPro>();
        letter = Clean(text.text);

        moveableObject = GetComponent<MoveableObject>();
    }

    private void FixedUpdate()
    {
        if (leftArea.bounds.Contains(transform.position) && letter != leftLetter)
        {
            MoveBack();
        }
        else if (rightArea.bounds.Contains(transform.position) && letter != rightLetter)
        {
            MoveBack();
        } 
        else
        {
            isBouncingBack = false;
        }
    }

    void MoveBack()
    {
        if (isBouncingBack) moveableObject.MoveBySpeed(Vector2.zero, bounceBackSpeed);
        else StartCoroutine(BounceBackRoutine());
    }

    string Clean(string str)
    {
        return str.Replace(" ", "").Replace("\n", "").Replace("\r", "").Replace("\t", "");
    }

    IEnumerator BounceBackRoutine()
    {
        yield return new WaitForSeconds(0.3f);
        isBouncingBack = true;
    }

}
