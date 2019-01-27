using UnityEngine;
using UnityEngine.UI;

public class SprintStatusBehaviour : MonoBehaviour {
    public Sprite Available;
    public Sprite Unavailable;
    
    private Image _image;
    private MovementSpeedBehaviour _player;

    private void Start() {
        _player = FindObjectOfType<MovementSpeedBehaviour>();
        _image = GetComponent<Image>();
    }
    
    private void Update() {
        _image.sprite = _player.CanSprint ? Available : Unavailable;
        var color = Color.white;
        color.a = _player.CanSprint ? 1 : 0.2f;
        _image.color = color;
    }
}
