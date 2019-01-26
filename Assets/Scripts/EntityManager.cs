using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour {
    private PlayerBehaviour _player;
    public List<Entity> Entities;
    
    // Start is called before the first frame update
    void Start() {
        _player = FindObjectOfType<PlayerBehaviour>();
    }

    void Update() {
        foreach (var entity in Entities) {
            entity.Tick(_player);
        }
    }
}
