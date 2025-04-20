using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static event System.Action OnComplete;
    [SerializeField] int time = 50;
    TextMeshPro text;
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        text.text = time.ToString();
        StartCoroutine(TimerRoutine());
    }

    IEnumerator TimerRoutine()
    {
        yield return new WaitForSeconds(1);
        time--;
        text.text = time.ToString();

        if (time > 0)
        {
            StartCoroutine(TimerRoutine());
        }
        else
        {
            OnComplete?.Invoke();
        }
    }

}
