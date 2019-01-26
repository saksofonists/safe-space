using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SeekerEntity : Entity {
    private Rigidbody2D _body;
    public Vector2 Speed;

    private void Start() {
        _body = GetComponent<Rigidbody2D>();
    }

    public override void Tick(PlayerInputBehaviour player) {
        var direction = (player.transform.position - transform.position).normalized;
        _body.velocity = direction * Speed;
    }
}
