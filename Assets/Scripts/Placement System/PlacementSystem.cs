using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private Camera sceneCamera;
    [SerializeField] private Vector3 currentPlacementPostition;
    [SerializeField] private LayerMask placeableLayerMask;

    [SerializeField] private GameObject placeableObjectPrefab;
    [SerializeField] private GameObject previewObjectPrefab;

    [SerializeField] private Material previewMat;
    [SerializeField] private Color validColour;
    [SerializeField] private Color invalidColour;

    public bool inPlacementMode { get; private set; } = false;
    private GameObject previewObject = null;
    private bool validPlacementPos = true;

    private void Update()
    {
        UpdateInput();

        if (inPlacementMode)
        {
            GetSelectedGroundPosition();
            previewObject.transform.position = currentPlacementPostition;

            if (CanPlaceObject())
            {
                SetValidPreview();
            } else
            {
                SetInvalidPreview();
            }
        }
    }

    private void UpdateInput()
    {

        if (Input.GetMouseButtonDown(0))
        {
            PlaceObject();
        }
        if ( Input.GetMouseButtonDown(1) && inPlacementMode)
        {
            ExitPlacementMode();
        }

    }

    private void PlaceObject()
    {
        if (!inPlacementMode || !validPlacementPos) return;

        Instantiate(placeableObjectPrefab, currentPlacementPostition, placeableObjectPrefab.transform.rotation);
        ExitPlacementMode();
    }

    private bool CanPlaceObject()
    {
        if (previewObject == null) return false;

        return previewObject.GetComponent<PreviewObjectValidChecker>().IsValid;
    }

    private void SetValidPreview()
    {
        previewMat.color = validColour;
        validPlacementPos = true;
    }

    private void SetInvalidPreview()
    {
        previewMat.color = invalidColour;
        validPlacementPos = false;
    }

    public void EnterPlacementMode()
    {
        if (inPlacementMode) return;
        GameManager.Instance.UIManager.HideShopUI();
        inPlacementMode = true;
        previewObject = Instantiate(previewObjectPrefab, currentPlacementPostition, previewObjectPrefab.transform.rotation);

    }     
    
    private void ExitPlacementMode()
    {
        Destroy(previewObject);
        previewObject = null;
        inPlacementMode = false;
        GameManager.Instance.UIManager.ShowShopUI();
    }

    private void GetSelectedGroundPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = sceneCamera.nearClipPlane;   // so we cannot select objects not rendered by cam

        Ray placementRay = sceneCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(placementRay, out hit, 100f, placeableLayerMask))
        {
            currentPlacementPostition = hit.point;
        }
    }

    public void SetPlaceableObjectPrefab(GameObject obj)
    {
        placeableObjectPrefab = obj;
    }

    public void SetPreviewObjectPrefab(GameObject obj)
    {
        previewObjectPrefab = obj;
    }
}
