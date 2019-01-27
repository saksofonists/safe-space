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
    private Animator _animator;

    private void Start() {
        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    public override void Tick(PlayerBehaviour player, bool inStealth) {
        if (AggroIndicator != null) {
            var scale = 0.0625F * AggroRange; // 64 / 1024
            AggroIndicator.localScale = new Vector3(scale, scale, 1);
        }
        if (_colliding) return;        
        
        var overlap = Physics2D.OverlapCircleAll(transform.position, AggroRange / 2f);
        if (overlap == null) return;
        
        foreach (var col in overlap) {
            if (col.CompareTag("Player") &&!inStealth) {
                var direction = (col.transform.position - transform.position).normalized;
                _body.velocity = direction * Speed;
                
                _animator.SetBool("MoveLeft", direction.x < 0);
                _animator.SetBool("MoveRight", direction.x > 1);
                _animator.SetBool("MoveUp", direction.y > 0);
                _animator.SetBool("MoveDown", direction.y < 0);
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
