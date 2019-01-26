using UnityEngine;

public class SprintBehaviour : MonoBehaviour{
    public Vector2 DefaultSpeed;
    public Vector2 SprintSpeed;
    public float SprintDuration;
    public float SprintCooldown;

    private float _lastSprint = float.NegativeInfinity;
    private PlayerBehaviour _player;

    private void Start() {
        _player = FindObjectOfType<PlayerBehaviour>();
    }

    private void Update() {
        var sinceLastStart = Time.time - _lastSprint;

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
