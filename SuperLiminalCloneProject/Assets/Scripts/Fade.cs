using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// I simple alpha fade that starts when the scene gets started (used for a scene transition for example).
/// </summary>
public class Fade : MonoBehaviour
{
    [SerializeField] private float _timer;
    [SerializeField] private Image _img;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        Color newColor = _img.color;
        newColor.a = Mathf.Lerp(newColor.a, 0, _timer);
        _img.color = newColor;

    }
}
