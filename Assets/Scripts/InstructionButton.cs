using UnityEngine;
using UnityEngine.UI;

public class InstructionButton : MonoBehaviour {
    public GameObject NextScreen;

    private void Start() {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick() {
        NextScreen.SetActive(true);
        gameObject.SetActive(false);
    }
}