using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {
	public Vector2 Speed;
	private Rigidbody2D _body;
	public float Health;

	private void Start() {
		_body = GetComponent<Rigidbody2D>();
	}

	private void Update() {
		var xdir = 0;
		var ydir = 0;

		if (Input.GetKey(KeyCode.A)) xdir--;
		if (Input.GetKey(KeyCode.D)) xdir++;
		if (Input.GetKey(KeyCode.W)) ydir++;
		if (Input.GetKey(KeyCode.S)) ydir--;

		_body.velocity = Speed * new Vector2(xdir, ydir);
	}
}
