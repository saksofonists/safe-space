using UnityEngine;

public class LockdownEntity : Entity, IPlayerWatcher {
    public Vector2 Speed;
    public float LockdownTime;

    private Rigidbody2D _body;
    private MovementSpeedBehaviour _movement;

    private float _lockDownLeft = float.PositiveInfinity;

    private void Start() {
        _movement = FindObjectOfType<MovementSpeedBehaviour>();
        _body = GetComponent<Rigidbody2D>();
    }

    public override void Tick(PlayerBehaviour player) {
        if (float.IsPositiveInfinity(_lockDownLeft)) {
            var direction = (player.transform.position - transform.position).normalized;
            _body.velocity = direction * Speed;
        }
        else if (float.IsNegativeInfinity(_lockDownLeft)) {
            var direction = (player.transform.position - transform.position).normalized;
            _body.velocity = -direction * Speed;
        }
        else {
            _body.velocity = Vector2.zero;
            _lockDownLeft -= Time.deltaTime;
            if (_lockDownLeft < 0) {
                _movement.Stoppers--;
                _lockDownLeft = float.NegativeInfinity;
            }
        }
    }

    public void Colliding(float timeDelta, PlayerBehaviour player) {
        if (float.IsPositiveInfinity(_lockDownLeft)) {
            _lockDownLeft = LockdownTime;
            _movement.Stoppers++;
        }
    }

    public void StopColliding(PlayerBehaviour player) {
        
    }
}
