using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SeekerEntity : Entity {
    private Rigidbody2D _body;
    public Vector2 Speed;
    public float Damage;

    private void Start() {
        _body = GetComponent<Rigidbody2D>();
    }

    public override void Tick(PlayerBehaviour player) {
        var direction = (player.transform.position - transform.position).normalized;
        _body.velocity = direction * Speed;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (!other.gameObject.CompareTag("Player")) return;
        var player = other.gameObject.GetComponent<PlayerBehaviour>();
        if (player == null) return;

        player.Health -= Damage;
    }
}
