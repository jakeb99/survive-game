using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class UIContextMenu : MonoBehaviour
{
    public GameObject buttonPrefab;         // Prefab with Button + Text
    public Transform buttonContainer;       // VerticalLayoutGroup parent
    public CanvasGroup canvasGroup;         // For show/hide
    private Action<int> onOptionSelected;

    public void Show(InteractionOption[] options, Vector2 screenPosition, Action<int> callback)
    {
        ClearButtons();
        onOptionSelected = callback;

        for (int i = 0; i < options.Length; i++)
        {
            int index = i; // capture local copy for closure
            GameObject buttonObj = Instantiate(buttonPrefab, buttonContainer);
            var button = buttonObj.GetComponent<Button>();
            var text = buttonObj.GetComponentInChildren<TMP_Text>();
            text.text = options[i].label;

            button.onClick.AddListener(() => OnOptionClicked(index));
        }

        transform.position = screenPosition;
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void Hide()
    {
        ClearButtons();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void ClearButtons()
    {
        foreach (Transform child in buttonContainer)
            Destroy(child.gameObject);
    }

    private void OnOptionClicked(int index)
    {
        onOptionSelected?.Invoke(index);
        Hide();
    }
}
