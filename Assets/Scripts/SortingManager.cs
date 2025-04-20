using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SortingManager : MonoBehaviour
{
    public static event System.Action OnCompleted;
    [SerializeField] Transform letters;
    [SerializeField] public Transform leftArea;
    [SerializeField] public Transform rightArea;
    [SerializeField] public string leftLetter;
    [SerializeField] public string rightLetter;

    private void Start()
    {
        InvokeRepeating("CheckIfWon", 0.5f, 0.5f);
    }

    void CheckIfWon()
    {
        if (AreLettersWithinArea(leftArea, leftLetter) && AreLettersWithinArea(rightArea, rightLetter))
        {
            OnCompleted?.Invoke();
        }
    }

    bool AreLettersWithinArea(Transform area, string letter)
    {
        bool areAllLettersInArea = true;
        foreach (Transform child in letters)
        {
            var text = child.GetComponentInChildren<TextMeshPro>();

            if (text && Clean(text.text) == Clean(letter) && !IsWithinBounds(child, area))
            {
                areAllLettersInArea = false;
            }
        }
        return areAllLettersInArea;
    }

    bool IsWithinBounds(Transform child, Transform area)
    {
        var boxCollider = area.GetComponent<BoxCollider2D>();

        if (boxCollider) return boxCollider.bounds.Contains(child.position);
        return false;
    }

    string Clean(string str)
    {
        return str.Replace(" ", "").Replace("\n", "").Replace("\r", "").Replace("\t", "");
    }
}
