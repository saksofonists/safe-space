using UnityEngine;

public class DirectionArrowBehaviour : MonoBehaviour {
    private FinishSpot _finish;
    private PlayerBehaviour _player;

    private void Start() {
        _finish = FindObjectOfType<FinishSpot>();
        _player = FindObjectOfType<PlayerBehaviour>();
    }

    private void Update() {
        var direction = _finish.transform.position - _player.transform.position;
        float angle;

        if (direction.y < 0) {
            if (direction.x > 0) {
                angle = -90 - Mathf.Rad2Deg * Mathf.Atan(-direction.y / direction.x);
            }
            else {
                angle = 90 + Mathf.Rad2Deg * Mathf.Atan(-direction.y / -direction.x);
            }
        }
        else {
            if (direction.x > 0) {
                angle = -90 + Mathf.Rad2Deg * Mathf.Atan(direction.y / direction.x);
            }
            else {
                angle = 90 - Mathf.Rad2Deg * Mathf.Atan(direction.y / -direction.x);
            }
        }

        transform.localEulerAngles = new Vector3(0f, 0f, angle);
    }
}
