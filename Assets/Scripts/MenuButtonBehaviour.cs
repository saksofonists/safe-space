using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtonBehaviour : MonoBehaviour {

    public Button NewGame;
    public Button Instructions;
    public Button Quit;

	// Use this for initialization
	void Start () {
		NewGame.onClick.AddListener(OnNewGame);
		Instructions.onClick.AddListener(OnInstructions);
		Quit.onClick.AddListener(OnQuit);
	}

	private void OnQuit() {
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}

	private void OnInstructions() {
		SceneManager.LoadScene("Scenes/Instructions");
	}
	
	private void OnNewGame() {
		SceneManager.LoadScene("Scenes/Gameplay");
	}
}