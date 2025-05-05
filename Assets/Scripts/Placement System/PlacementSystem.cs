using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlacementSystem : MonoBehaviour, IDataPersistence
{
    [SerializeField] private Camera sceneCamera;
    [SerializeField] private Vector3 currentPlacementPostition;
    [SerializeField] private LayerMask placeableLayerMask;

    [SerializeField] private GameObject placeableObjectPrefab;
    [SerializeField] private GameObject previewObjectPrefab;
    [SerializeField] private GameObject oldObject;

    [SerializeField] private Material previewMat;
    [SerializeField] private Color validColour;
    [SerializeField] private Color invalidColour;

    [SerializeField] private List<GameObject> placedObjects;
    [SerializeField] private GameObject woodPrefab;
    [SerializeField] private GameObject sandPrefab;
    [SerializeField] private GameObject concretePrefab;
    [SerializeField] private GameObject turretPrefab;
    [SerializeField] private GameObject barbWirePrefab;

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

        GameObject placedObject = Instantiate(placeableObjectPrefab, currentPlacementPostition, placeableObjectPrefab.transform.rotation);

        placedObjects.Add(placedObject);

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
        if (oldObject != null) 
        { 
            RemovePlacedObject(oldObject);
            Destroy(oldObject); 
        }
        previewObject = null;
        inPlacementMode = false;
        GameManager.Instance.UIManager.ShowShopUI();
    }

    public void EnterEditMode(GameObject objectToMove, GameObject objectPreview)
    {
        placeableObjectPrefab = objectToMove;
        oldObject = objectToMove;
        previewObjectPrefab = objectPreview;
        
        EnterPlacementMode();
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

    /// <summary>
    /// remove the object from the placed objects list
    /// </summary>
    public void RemovePlacedObject(GameObject obj)
    {
        placedObjects.Remove(obj);
    }

    void IDataPersistence.LoadGameData(GameData data)
    {
        foreach (PlaceableObjectSerializableData serializableData in data.PlacedObjects)
        {
            PlaceSavedObject(serializableData);
        }
    }

    private void PlaceSavedObject(PlaceableObjectSerializableData data)
    {
        Debug.Log("Placing saved object");
        GameObject prefab = null;
        switch (data.type)
        {
            case PlaceableType.WOODEN_BARRIER:
                prefab = woodPrefab; break;
            case PlaceableType.SANDBAG_BARRIER:
                prefab = sandPrefab; break;
            case PlaceableType.CONCRETE_BARRIER:
                prefab = concretePrefab; break;
            case PlaceableType.TURRET:
                prefab = turretPrefab; break;
            case PlaceableType.BARBED_WIRE:
                prefab = barbWirePrefab; break;
        }

        Vector3 position = new Vector3();
        position.x = data.Position[0];
        position.y = data.Position[1];
        position.z = data.Position[2];

        GameObject placedObject = Instantiate(prefab, position, prefab.transform.rotation);
        placedObjects.Add(placedObject);
        placedObject.GetComponent<Health>().SetCurrentHealth(data.CurrentHealth);
    }

    void IDataPersistence.SaveGameData(ref GameData data)
    {
        List<PlaceableObjectSerializableData> serializableDatas = 
            new List<PlaceableObjectSerializableData>();

        foreach (GameObject obj in this.placedObjects)
        {
            PlaceableObjectSerializableData newData = new PlaceableObjectSerializableData();

            serializableDatas.Add(newData.SetData(obj));
        }

        data.PlacedObjects = serializableDatas;
    }
}