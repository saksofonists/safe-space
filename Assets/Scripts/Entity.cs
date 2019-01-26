using UnityEngine;

public abstract class Entity : MonoBehaviour {
    public abstract void Tick(PlayerInputBehaviour player);
}