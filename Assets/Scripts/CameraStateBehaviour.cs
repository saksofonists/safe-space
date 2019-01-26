using Cinemachine;
using UnityEngine;

public class CameraStateBehaviour : MonoBehaviour {
	private PlayerBehaviour _player;
	private CinemachineVirtualCamera _cinemachine;
	public float MaxFrequency;
	public float MaxAmplitude;

	private void Start() {
		_player = FindObjectOfType<PlayerBehaviour>();
		_cinemachine = FindObjectOfType<CinemachineVirtualCamera>();
	}

	private void Update() {
		var percentage = 1 - _player.Health / _player.MaxHealth;
		_cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain =
			1 + MaxAmplitude * percentage;
		_cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain =
			MaxFrequency * percentage;
	}
}
