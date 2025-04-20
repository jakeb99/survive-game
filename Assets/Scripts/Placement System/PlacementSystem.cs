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

    private GameObject previewObject = null;
    private bool inPlacementMode = false;
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
        //if (Input.GetKeyDown(KeyCode.B))
        //{
        //    EnterPlacementMode();
        //}
        //else if (Input.GetKeyDown(KeyCode.X))
        //{
        //    ExitPlacementMode();
        //}
        if (Input.GetMouseButtonDown(0))
        {
            PlaceObject();
        }

    }

    private void PlaceObject()
    {
        if (!inPlacementMode || !validPlacementPos) return;

        Instantiate(placeableObjectPrefab, currentPlacementPostition, Quaternion.identity);
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

        inPlacementMode = true;
        previewObject = Instantiate(previewObjectPrefab, currentPlacementPostition, Quaternion.identity);

    }     
    
    private void ExitPlacementMode()
    {
        Destroy(previewObject);
        previewObject = null;
        inPlacementMode = false;
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
