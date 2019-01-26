using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {
	public GameObject DeathScreen;
	public Vector2 Speed;
	private Rigidbody2D _body;
	public Animator Animator;
	public float MaxHealth;

	private float _health;
	public float Health {
		get { return _health; }
		set {
			_health = value;
			if (IsDead) {
				_health = 0;
				Die();
			}
			else if (_health > MaxHealth) {
				_health = MaxHealth;
			}
		}
	}

	public bool IsDead {
		get { return Health <= 0.01f; }
	}

	private void Die() {
		DeathScreen.SetActive(true);
	}

	private Dictionary<IPlayerWatcher, float> _enterTimes = new Dictionary<IPlayerWatcher, float>();

	private void Start() {
		_body = GetComponent<Rigidbody2D>();
		_health = MaxHealth;
	}

	private void Update() {
		if (IsDead) return;
		
		var xdir = 0;
		var ydir = 0;

		if (Input.GetKey(KeyCode.A)) xdir--;
		if (Input.GetKey(KeyCode.D)) xdir++;
		if (Input.GetKey(KeyCode.W)) ydir++;
		if (Input.GetKey(KeyCode.S)) ydir--;

		_body.velocity = Speed * new Vector2(xdir, ydir);

		if (Animator != null) {
			Animator.SetBool("MoveLeft", xdir < 0);
			Animator.SetBool("MoveRight", xdir > 0);
			Animator.SetBool("MoveUp", ydir > 0);
			Animator.SetBool("MoveDown", ydir < 0);
		}

		var copy = _enterTimes.Keys.ToList();

		foreach (var collision in copy) {
			var current = Time.time;
			var lastTime = _enterTimes[collision];
			collision.Colliding(current - lastTime, this);
			_enterTimes[collision] = current;
		}
	}

	private void OnCollisionEnter2D(Collision2D other) {
		var watcher = other.gameObject.GetComponent<IPlayerWatcher>();
		if (watcher == null) return;
		_enterTimes[watcher] = Time.time;
	}

	private void OnCollisionExit2D(Collision2D other) {
		var watcher = other.gameObject.GetComponent<IPlayerWatcher>();
		if (watcher == null) return;
		if (!_enterTimes.ContainsKey(watcher)) return;
		_enterTimes.Remove(watcher);
	}

	private void OnTriggerEnter2D(Collider2D other) {
		var watcher = other.gameObject.GetComponent<IPlayerWatcher>();
		if (watcher == null) return;
		_enterTimes[watcher] = Time.time;
	}

	private void OnTriggerExit2D(Collider2D other) {
		var watcher = other.gameObject.GetComponent<IPlayerWatcher>();
		if (watcher == null) return;
		if (!_enterTimes.ContainsKey(watcher)) return;
		_enterTimes.Remove(watcher);
	}
}
