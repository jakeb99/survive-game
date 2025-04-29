using UnityEngine;

public interface IInteractable
{
    InteractionOption[] GetOptions();
    void OnOptionSelected(int index);   // handle slected option
}

[System.Serializable]
public struct InteractionOption
{
    public string label;
}
