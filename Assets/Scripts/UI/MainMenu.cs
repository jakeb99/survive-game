using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private UIDocument doc;
    [SerializeField] private DataPersistenceManager saveSystem;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buttonHoverClip;
    [SerializeField] private AudioClip buttonClickedClip;

    private Button playButton;
    private Button exitButton;

    private void Awake()
    {
        playButton = doc.rootVisualElement.Q<Button>("PlayBtn");
        playButton.clicked += PlayButtonOnClicked;

        exitButton = doc.rootVisualElement.Q<Button>("ExitBtn");
        exitButton.clicked += ExitButtonOnClicked;

        Button[] buttons = {playButton, exitButton};

        foreach (var button in buttons)
        {
            // onHover sound
            button.RegisterCallback<PointerEnterEvent>(e =>
            {
                PlaySound(buttonHoverClip);
            });

            button.RegisterCallback<ClickEvent>(e =>
            {
                PlaySound(buttonClickedClip);
            });
        }
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

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
