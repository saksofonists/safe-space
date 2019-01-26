using UnityEngine;

public class HpPickup : MonoBehaviour, IPlayerWatcher {
    public float Health;
    private bool _ensureOnceJustInCase;
    
    public void Colliding(float timeDelta, PlayerBehaviour player) {
        if (player.Health >= player.MaxHealth - 0.01f || _ensureOnceJustInCase) return;
        _ensureOnceJustInCase = true;

        player.Health += Health;
        Destroy(gameObject);
    }

    public void StopColliding(PlayerBehaviour player) {
        
    }
}
