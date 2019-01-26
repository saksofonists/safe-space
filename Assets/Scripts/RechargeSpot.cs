using UnityEngine;

public class RechargeSpot : MonoBehaviour, IPlayerWatcher {
    public float RechargePerSec;
    
    public void Colliding(float timeDelta, PlayerBehaviour player) {
        player.Health += RechargePerSec * timeDelta;
    }
}
