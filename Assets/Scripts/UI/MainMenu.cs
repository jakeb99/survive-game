using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private UIDocument doc;
    [SerializeField] private DataPersistenceManager saveSystem;

    private Button playButton;
    private Button exitButton;

    private void Awake()
    {
        playButton = doc.rootVisualElement.Q<Button>("PlayBtn");
        playButton.clicked += PlayButtonOnClicked;

        exitButton = doc.rootVisualElement.Q<Button>("ExitBtn");
        exitButton.clicked += ExitButtonOnClicked;
    }

    private void PlayButtonOnClicked()
    {
        SceneManager.LoadScene(1);
        saveSystem.StartGame();
    }

    private void ExitButtonOnClicked()
    {
        Application.Quit();
    }
}
