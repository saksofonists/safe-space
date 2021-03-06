using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishScreenBehaviour : MonoBehaviour {
    public string Message;

    public Text Title;
    public Button Retry;
    public Button MainMenu;
    public Button Quit;

    // Use this for initialization
    void Start () {
        Retry.onClick.AddListener(OnRetry);
        MainMenu.onClick.AddListener(OnMainMenu);
        Quit.onClick.AddListener(OnQuit);
    }

    private void OnEnable() {
        Title.text = Message;
    }

    private void OnRetry() {
        SceneManager.LoadScene("Scenes/Gameplay");
    }

    private void OnMainMenu() {
        SceneManager.LoadScene("Scenes/Menu");
    }

    private void OnQuit() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }
}
