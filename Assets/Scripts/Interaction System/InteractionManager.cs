using UnityEngine;
using UnityEngine.EventSystems;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private UIContextMenu contextMenu;
    private IInteractable currentInteractableTarget;
    public bool isEnabled = false;

    private void Update()
    {
        if (!enabled) return;

        if (EventSystem.current.IsPointerOverGameObject()) return;

        Ray ray = GameManager.Instance.SceneCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 200f, interactableLayer) && Input.GetMouseButton(1))
        {
            var interactable = hit.collider.GetComponentInParent<IInteractable>();

            Debug.Log(interactable);
            if (interactable != null && interactable != currentInteractableTarget)
            {
                currentInteractableTarget = interactable;
                
                var screenPos = Input.mousePosition;
                var options = currentInteractableTarget.GetOptions();

                contextMenu.Show(options, screenPos, index =>
                {
                    currentInteractableTarget.OnOptionSelected(index);
                    contextMenu.Hide();
                });
            }
        } else if (!EventSystem.current.IsPointerOverGameObject())
        {
            contextMenu.Hide();
            currentInteractableTarget = null;
        }
    }

    public void OnUIOptionClicked(int index)
    {
        currentInteractableTarget?.OnOptionSelected(index);
    }

}
