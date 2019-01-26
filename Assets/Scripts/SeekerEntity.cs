using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SeekerEntity : Entity {
    public Vector2 Speed;
    public float Dps;
    
    private Rigidbody2D _body;
    private PlayerBehaviour _player;

    private float _lastTick;

    private void Start() {
        _body = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<PlayerBehaviour>();
    }

    public override void Tick(PlayerBehaviour player) {
        var direction = (player.transform.position - transform.position).normalized;
        _body.velocity = direction * Speed;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (!other.gameObject.CompareTag("Player")) return;
        var player = other.gameObject.GetComponent<PlayerBehaviour>();
        if (player == null) return;
        
        _lastTick = Time.fixedTime;
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (float.IsNegativeInfinity(_lastTick)) return;

        var delta = Time.fixedTime - _lastTick;
        _player.Health -= Dps * delta;
        _lastTick = Time.fixedTime;
    }

    private void OnCollisionExit2D(Collision2D other) {
        _lastTick = float.NegativeInfinity;
    }
}

//public class RechargeSpot : MonoBehaviour {
//    public Collider2D _collider;
//
//    private void OnCollisionStay2D(Collision2D other) {
//        throw new System.NotImplementedException();
//    }
//}
