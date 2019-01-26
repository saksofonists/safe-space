using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour {
    public PlayerInputBehaviour Player;
    public List<Entity> Entities;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update() {
        foreach (var entity in Entities) {
            entity.Tick(Player);
        }
    }
}