using System;
using Unity.AI.Navigation;
using UnityEngine;

public class DestructableObject : MonoBehaviour, IDestroyable, IInteractable
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

    InteractionOption[] IInteractable.GetOptions() => new[]
    {
        new InteractionOption { label = "Move" },
        new InteractionOption {label = "Repair"},
        new InteractionOption {label = "Scrap"},
        new InteractionOption {label = "Upgrade"},
    };

    void IInteractable.OnOptionSelected(int index)
    {
        switch (index)
        {
            case 0: MoveObject(); break;
            case 1: RepairObject(); break;
            case 2: ScrapObject(); break;
            case 3: UpgradeObject(); break;
        }
    }

    private void MoveObject() { }
    private void RepairObject() { }
    private void ScrapObject() { }
    private void UpgradeObject() { }
}
