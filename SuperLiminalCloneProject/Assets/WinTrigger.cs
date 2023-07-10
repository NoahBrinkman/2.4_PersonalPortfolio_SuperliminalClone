using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent onFadeComplete;
    [SerializeField] private float timer;
    [SerializeField] private Image img;
    private bool fade = false;

    private void Update()
    {
        if (fade)
        {
            timer += Time.deltaTime;
            Color newColor = img.color;
            newColor.a = Mathf.Lerp(newColor.a, 1, timer);
            img.color = newColor;
            if (timer >= 1)
            {
                onFadeComplete?.Invoke();
                
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        fade = true;
    }
}
