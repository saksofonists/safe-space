using UnityEngine;

public class LockdownEntity : Entity, IPlayerWatcher {
    public Vector2 Speed;
    public float LockdownTime;
    public float AggroRange;
    public Transform AggroIndicator;

    private Rigidbody2D _body;
    private MovementSpeedBehaviour _movement;

    private float _lockDownLeft = float.PositiveInfinity;

    private void Start() {
        _movement = FindObjectOfType<MovementSpeedBehaviour>();
        _body = GetComponent<Rigidbody2D>();
    }

    public override void Tick(PlayerBehaviour player, bool inStealth) {
        if (float.IsPositiveInfinity(_lockDownLeft)) {
            if (AggroIndicator != null) {
                var scale = 0.0625F * AggroRange; // 64 / 1024
                AggroIndicator.localScale = new Vector3(scale, scale, 1);
            }
            
            var overlap = Physics2D.OverlapCircleAll(transform.position, AggroRange / 2f);
            if (overlap == null) return;
        
            foreach (var col in overlap) {
                if (col.CompareTag("Player") &&!inStealth) {
                    var direction = (col.transform.position - transform.position).normalized;
                    _body.velocity = direction * Speed;
                }
            }
        }
        else if (float.IsNegativeInfinity(_lockDownLeft)) {
            if (AggroIndicator != null) {
                AggroIndicator.gameObject.SetActive(false);
            }
            var direction = (player.transform.position - transform.position).normalized;
            _body.velocity = -direction * Speed;
        }
        else {
            if (AggroIndicator != null) {
                AggroIndicator.gameObject.SetActive(false);
            }
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
