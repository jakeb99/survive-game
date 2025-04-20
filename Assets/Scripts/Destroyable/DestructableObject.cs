using System;
using Unity.AI.Navigation;
using UnityEngine;

public class DestructableObject : MonoBehaviour, IDestroyable
{
    [SerializeField] protected Health healthObj;
    private NavMeshModifierVolume modifierVolume;

    public Action OnDestructableDestroyed;

    private void Awake()
    {
        healthObj.OnDeath += Destroy;
        modifierVolume = GetComponentInChildren<NavMeshModifierVolume>();
    }

    public void Update()
    {
        
    }

    public virtual void Destroy()
    {
        // set game object as inactive so we can rebake navmesh, other wise destroy does not complete untill end of update
        gameObject.SetActive(false);

        OnDestructableDestroyed?.Invoke();

        // recalculate navmesh now that this obsticle is gone
        NavMeshManager.Instance.BakeNavMesh();

        Destroy(gameObject);
        healthObj.OnDeath -= Destroy;
    }
}
