using UnityEngine;

public class PlayerInputBehaviour : MonoBehaviour {

	public Vector2 Speed;

	private void Update () {
		var xdir = 0;
		var ydir = 0;
		if (Input.GetKey(KeyCode.A)) xdir--;
		if (Input.GetKey(KeyCode.D)) xdir++;
		if (Input.GetKey(KeyCode.W)) ydir++;
		if (Input.GetKey(KeyCode.S)) ydir--;

		transform.localPosition += (Vector3) (Speed * new Vector2(xdir, ydir) * Time.deltaTime);
	}
}
