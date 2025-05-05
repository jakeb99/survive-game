using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    [SerializeField] TurretController turretController;
    [SerializeField] float zPlane = 20f;
    [SerializeField] LayerMask mainMenuPlane;


    private void Update()
    {
        //Vector3 mousPos = Input.mousePosition;

        //mousPos.z = 10f;

        //Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousPos);
        //turretController.SetCurrentTarget(worldMousePos);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, mainMenuPlane)) // use layer mask if needed
        {
            Vector3 targetPoint = hit.point;
            turretController.SetCurrentTarget(targetPoint);
        }

    }
}
