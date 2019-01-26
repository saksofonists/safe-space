using UnityEngine;

public class FinishSpot : MonoBehaviour, IPlayerWatcher  {
    public FinishScreenBehaviour FinishScreen;
    private bool _once;
    public AudioSource vAudio;

    public void Colliding(float timeDelta, PlayerBehaviour player) {
        if (_once) return;
        _once = true;

        vAudio.Play();
        FinishScreen.Message = "Gz";
        FinishScreen.gameObject.SetActive(true);

        FindObjectOfType<EntityManager>().gameObject.SetActive(false);
    }

    public void StopColliding(PlayerBehaviour player) {
       
    }
}
