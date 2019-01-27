using UnityEngine;
using UnityEngine.UI;

public class StealthStatusBehaviour : MonoBehaviour {
    public Sprite Available;
    public Sprite Unavailable;
    
    private Image _image;
    private EntityManager _entities;

    private void Start() {
        _entities = FindObjectOfType<EntityManager>();
        _image = GetComponent<Image>();
    }
    
    private void Update() {
        _image.sprite = _entities.CanStealth ? Available : Unavailable;
        var color = Color.white;
        color.a = _entities.CanStealth ? 1 : 0f;
        _image.color = color;
    }
}