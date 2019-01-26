using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour {
    public PlayerBehaviour Player;
    private Text _text;
    
    // Start is called before the first frame update
    void Start() {
        _text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update() {
        _text.text = "(debug) HP: " + Player.Health;
    }
}
