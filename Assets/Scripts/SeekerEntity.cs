using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SeekerEntity : Entity, IPlayerWatcher {
    public Vector2 Speed;
    public float Dps;
    
    private Rigidbody2D _body;

    private float _lastTick;

    private void Start() {
        _body = GetComponent<Rigidbody2D>();
    }

    public override void Tick(PlayerBehaviour player) {
        var direction = (player.transform.position - transform.position).normalized;
        _body.velocity = direction * Speed;
    }

    public void Colliding(float timeDelta, PlayerBehaviour player) {
        player.Health -= Dps * timeDelta;
    }
}
