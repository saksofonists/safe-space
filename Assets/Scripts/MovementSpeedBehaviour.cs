using UnityEngine;

public class MovementSpeedBehaviour : MonoBehaviour{
    public Vector2 DefaultSpeed;
    public Vector2 SprintSpeed;
    public float SprintDuration;
    public float SprintCooldown;
    public int Stoppers;

    private float _lastSprint = -123123;
    private PlayerBehaviour _player;
    private Rigidbody2D _playerBody;

    public bool CanSprint {
        private set;
        get;
    }

    private void Start() {
        _player = FindObjectOfType<PlayerBehaviour>();
        _playerBody = _player.GetComponent<Rigidbody2D>();
    }

    private void Update() {
        var sinceLastStart = Time.time - _lastSprint;
        
        if (Stoppers > 0) {
            _playerBody.bodyType = RigidbodyType2D.Static;
            _player.Speed = Vector2.zero;
            return;
        }

        _playerBody.bodyType = RigidbodyType2D.Dynamic;

        CanSprint = sinceLastStart > SprintDuration + SprintCooldown;

        if (sinceLastStart < SprintDuration) {
            // in sprint
            _player.Speed = SprintSpeed;
        }
        else if (sinceLastStart > SprintDuration + SprintCooldown && Input.GetKey(KeyCode.LeftShift)) {
            _lastSprint = Time.time;
            _player.Speed = SprintSpeed;
        }
        else {
            _player.Speed = DefaultSpeed;
        }
    }
}
