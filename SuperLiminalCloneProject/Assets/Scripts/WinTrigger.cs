using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// Final trigger to 'beat' the level.
/// </summary>
public class WinTrigger : MonoBehaviour
{
    [FormerlySerializedAs("onFadeComplete")] [SerializeField] private UnityEvent _onFadeComplete;
    [FormerlySerializedAs("timer")] [SerializeField] private float _timer;
    [FormerlySerializedAs("img")] [SerializeField] private Image _img;
    private bool _fade = false;
    
    /// <summary>
    /// When fade is true, start a timer to fade to white.
    /// When this is complete, fire off an event to 'end' the level or do other things depensing on what you'd want.
    /// </summary>
    private void Update()
    {
        if (_fade)
        {
            _timer += Time.deltaTime;
            Color newColor = _img.color;
            newColor.a = Mathf.Lerp(newColor.a, 1, _timer);
            _img.color = newColor;
            if (_timer >= 1)
            {
                _onFadeComplete?.Invoke();
                
            }
        }
    }
    
    /// <summary>
    /// Upon entering the trigger start the fade to white sequence
    /// </summary>
    /// <param name="other">Should always be the player</param>
    private void OnTriggerEnter(Collider other)
    {
        _fade = true;
    }
}
