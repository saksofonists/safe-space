using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour {
	public Vector2 Speed;
	private Rigidbody2D _body;
	public Animator Animator;
	public float MaxHealth;

	private float _health;
	public float Health {
		get { return _health; }
		set {
			if (value < 0) {
				_health = 0;
				Die();
			}
			else if (value > MaxHealth) {
				_health = MaxHealth;
			}
			else {
				_health = value;
			}
		}
	}

	private void Die() {
		Debug.Log("F");
		SceneManager.LoadScene("Scenes/Menu");
	}

	private Dictionary<IPlayerWatcher, float> _enterTimes = new Dictionary<IPlayerWatcher, float>();

	private void Start() {
		_body = GetComponent<Rigidbody2D>();
		_health = MaxHealth;
	}

	private void Update() {
		var xdir = 0;
		var ydir = 0;

		if (Input.GetKey(KeyCode.A)) xdir--;
		if (Input.GetKey(KeyCode.D)) xdir++;
		if (Input.GetKey(KeyCode.W)) ydir++;
		if (Input.GetKey(KeyCode.S)) ydir--;

		if (Animator != null) {
			Animator.SetBool("MoveLeft", xdir < 0);
			Animator.SetBool("MoveRight", xdir > 0);
			Animator.SetBool("MoveUp", ydir > 0);
			Animator.SetBool("MoveDown", ydir < 0);
		}

		_body.velocity = Speed * new Vector2(xdir, ydir);
	}

	private void OnCollisionEnter2D(Collision2D other) {
		var watcher = other.gameObject.GetComponent<IPlayerWatcher>();
		if (watcher == null) return;
		_enterTimes[watcher] = Time.fixedTime;
	}

	private void OnCollisionStay2D(Collision2D other) {
		var watcher = other.gameObject.GetComponent<IPlayerWatcher>();
		if (watcher == null) return;
		float lastTime;
		if (!_enterTimes.TryGetValue(watcher, out lastTime)) return;
		var current = Time.fixedTime;
		watcher.Colliding(current - lastTime, this);
		_enterTimes[watcher] = current;
	}

	private void OnCollisionExit2D(Collision2D other) {
		var watcher = other.gameObject.GetComponent<IPlayerWatcher>();
		if (watcher == null) return;
		if (!_enterTimes.ContainsKey(watcher)) return;
		_enterTimes.Remove(watcher);
	}
}
