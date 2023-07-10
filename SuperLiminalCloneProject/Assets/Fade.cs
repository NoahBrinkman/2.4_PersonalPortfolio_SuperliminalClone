using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [SerializeField] private float timer;
    [SerializeField] private Image img;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Color newColor = img.color;
        newColor.a = Mathf.Lerp(newColor.a, 0, timer);
        img.color = newColor;

    }
}
