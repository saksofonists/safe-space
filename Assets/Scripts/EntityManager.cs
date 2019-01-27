using System.Collections.Generic;
using UnityEngine;
using Wilberforce.FinalVignette;

public class EntityManager : MonoBehaviour {
    private PlayerBehaviour _player;
    public List<Entity> Entities;

    public float StealthDuration = 3;
    public float StealthCooldown = 10f;
    private float _lastStealth = -123123;
    private FinalVignetteCommandBuffer _vignette;

    public bool CanStealth {
        private set;
        get;
    }

    public bool InStealth;
    
    // Start is called before the first frame update
    void Start() {
        _player = FindObjectOfType<PlayerBehaviour>();
        _vignette = FindObjectOfType<FinalVignetteCommandBuffer>();
    }

    void Update() {
        var sinceLastStart = Time.time - _lastStealth;
        
        CanStealth = sinceLastStart > StealthDuration + StealthCooldown;

        if (sinceLastStart < StealthDuration) {
            // in sprint
            InStealth = true;
        }
        else if (sinceLastStart > StealthDuration + StealthCooldown && Input.GetKey(KeyCode.E)) {
            _lastStealth = Time.time;
            InStealth = true;
        }
        else {
            InStealth = false;
        }

        if (InStealth) {
            _vignette.VignetteInnerSaturation = 0f;
            _vignette.VignetteOuterSaturation = 0f;
        }
        else {
            _vignette.VignetteInnerSaturation = 1f;
            _vignette.VignetteOuterSaturation = 1f;
        }
        
        foreach (var entity in Entities) {
            if (!entity.gameObject.activeSelf) continue;
            entity.Tick(_player, InStealth);
        }
    }
}
