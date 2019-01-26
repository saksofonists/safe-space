using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SeekerEntity : Entity, IPlayerWatcher {
    public Vector2 Speed;
    public float Dps;
    public float AggroRange;
    public Transform AggroIndicator;

    private Rigidbody2D _body;
    private bool _colliding;

    private float _lastTick;

    private void Start() {
        _body = GetComponent<Rigidbody2D>();
    }

    public override void Tick(PlayerBehaviour player) {
        if (AggroIndicator != null) {
            AggroIndicator.localScale = new Vector3(AggroRange, AggroRange, 1);
        }
        if (_colliding) return;        
        
        var overlap = Physics2D.OverlapCircleAll(transform.position, AggroRange / 2f);
        if (overlap == null) return;
        
        foreach (var col in overlap) {
            if (col.CompareTag("Player")) {
                var direction = (col.transform.position - transform.position).normalized;
                _body.velocity = direction * Speed;
            }
        }
    }

    public void Colliding(float timeDelta, PlayerBehaviour player) {
        player.Health -= Dps * timeDelta;
        _colliding = true;
        _body.bodyType = RigidbodyType2D.Static;
    }

    public void StopColliding(PlayerBehaviour player) {
        _colliding = false;
        _body.bodyType = RigidbodyType2D.Dynamic;
    }
}
