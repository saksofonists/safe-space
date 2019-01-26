using UnityEngine;

public class FinishSpot : MonoBehaviour, IPlayerWatcher  {
    public FinishScreenBehaviour FinishScreen;
    private bool _once;
    
    public void Colliding(float timeDelta, PlayerBehaviour player) {
        if (_once) return;
        _once = true;

        FinishScreen.Message = "Gz";
        FinishScreen.gameObject.SetActive(true);
        
        FindObjectOfType<EntityManager>().gameObject.SetActive(false);
    }
}