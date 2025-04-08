using Unity.AI.Navigation.Editor;
using UnityEngine;

public class DestructableObject : MonoBehaviour, IDestroyable
{
    [SerializeField] private Health healthObj; 
    private void Start()
    {
        healthObj.OnDeath += Destroy;
    }

    public void Destroy()
    {
        // set game object as inactive so we can rebake navmesh, other wise destroy does not complete untill end of update
        gameObject.SetActive(false);

        // recalculate navmesh now that this obsticle is gone
        NavMeshManager.Instance.BakeNavMesh();

        Destroy(gameObject);
    }
}
