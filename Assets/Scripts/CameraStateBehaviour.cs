using Cinemachine;
using UnityEngine;
using Wilberforce.FinalVignette;

public class CameraStateBehaviour : MonoBehaviour {
    private PlayerBehaviour _player;
    private CinemachineVirtualCamera _cinemachine;
    public float MaxFrequency;
    public float MaxAmplitude;
    private FinalVignetteCommandBuffer _vignette;

    private void Start() {
        _player = FindObjectOfType<PlayerBehaviour>();
        _cinemachine = FindObjectOfType<CinemachineVirtualCamera>();
        _vignette = FindObjectOfType<FinalVignetteCommandBuffer>();
    }

    private void Update() {
        var percentageLeft = Mathf.Clamp01(_player.Health / _player.MaxHealth);
        var percentageLost = 1 - percentageLeft;
        _cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain =
            1 + MaxAmplitude * percentageLost;
        _cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain =
            MaxFrequency * percentageLost;

        _vignette.VignetteInnerValueDistance = 0;
        _vignette.VignetteOuterValueDistance = 0.4f + 1.6f * percentageLeft;
    }
}
