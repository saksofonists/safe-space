﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SeekerEntity : Entity, IPlayerWatcher {
    public Vector2 Speed;
    public float Dps;

    private Rigidbody2D _body;
    private bool _colliding;

    private float _lastTick;

    private void Start() {
        _body = GetComponent<Rigidbody2D>();
        Debug.Log("Start");
    }

    public override void Tick(PlayerBehaviour player) {
        if (_colliding) return;
        var direction = (player.transform.position - transform.position).normalized;
        _body.velocity = direction * Speed;
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
