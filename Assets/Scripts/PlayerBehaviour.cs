using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {
	public FinishScreenBehaviour FinishScreen;
	public Vector2 Speed;
	private Rigidbody2D _body;
	public Animator Animator;
	public float MaxHealth;
    public AudioSource clip;

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
		Animator.SetBool("Dead", true);
		StartCoroutine(DieForReal());
	}

	private IEnumerator DieForReal() {
		yield return new WaitForSeconds(2f);
		FinishScreen.Message = "F";
		FinishScreen.gameObject.SetActive(true);	}

	private Dictionary<IPlayerWatcher, float> _enterTimes = new Dictionary<IPlayerWatcher, float>();

	private void Start() {
		_body = GetComponent<Rigidbody2D>();
		_health = MaxHealth;
        clip.Play();
	}

	private void Update() {
		var xdir = 0;
		var ydir = 0;

        if (Input.GetKey(KeyCode.A)) xdir--;
        if (Input.GetKey(KeyCode.D)) xdir++;
		if (Input.GetKey(KeyCode.W)) ydir++;
        if (Input.GetKey(KeyCode.S)) ydir--;

        if (Speed != Vector2.zero) {
	        _body.velocity = Speed * new Vector2(xdir, ydir);
        }

        if (xdir != 0 || ydir != 0) clip.UnPause();
        else clip.Pause();

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
		watcher.StopColliding(this);
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
		watcher.StopColliding(this);
		_enterTimes.Remove(watcher);
	}
}
