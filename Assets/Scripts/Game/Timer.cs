using TMPro;
using System;
using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
    public static event Action Throw;
    [SerializeField] private int seconds;
    [SerializeField] private TMP_Text text;

    void OnEnable()
    {
        StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    {
        for (int i = seconds ; i > 0 ; i --)
        {
            text.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        Throw?.Invoke();
        this.gameObject.SetActive(false);
    }
}
