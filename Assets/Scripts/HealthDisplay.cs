using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour {
    private PlayerBehaviour _player;
    private Text _text;

    // Start is called before the first frame update
    void Start() {
        _player = FindObjectOfType<PlayerBehaviour>();
        _text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update() {
        _text.text = "(debug) HP: " + _player.Health;
    }
}
