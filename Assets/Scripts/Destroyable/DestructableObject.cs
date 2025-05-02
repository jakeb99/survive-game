using System;
using System.Collections;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class DestructableObject : MonoBehaviour, IDestroyable, IInteractable
{
    [SerializeField] protected Health healthObj;
    [SerializeField] private GameObject previewPrefab;
    private NavMeshModifierVolume modifierVolume;

    public Action OnDestructableDestroyed;

    private void Awake()
    {
        healthObj.OnDeath += DestroyDestructable;
        modifierVolume = GetComponentInChildren<NavMeshModifierVolume>();
    }

    public void Update()
    {
        
    }

    public virtual void DestroyDestructable()
    {
        // set game object as inactive so we can rebake navmesh, other wise destroy does not complete untill end of update
        gameObject.SetActive(false);

        OnDestructableDestroyed?.Invoke();

        // recalculate navmesh now that this obsticle is gone
        NavMeshManager.Instance.BakeNavMesh();

        Destroy(gameObject);
        healthObj.OnDeath -= DestroyDestructable;
    }

    InteractionOption[] IInteractable.GetOptions() => new[]
    {
        new InteractionOption { label = "Move" },
        new InteractionOption {label = $"Repair ({CalcRepairCost()})"},
        new InteractionOption {label = $"Scrap({CalcScrapVal()})"},
    };

    void IInteractable.OnOptionSelected(int index)
    {
        switch (index)
        {
            case 0: MoveObject(); break;
            case 1: RepairObject(); break;
            case 2: ScrapObject(); break;
        }
    }

    private void MoveObject() 
    {
        GameManager.Instance.PlacementSystem.EnterEditMode(gameObject, previewPrefab);
    }
    private void RepairObject() 
    {
        int cost = CalcRepairCost();
        if(GameManager.Instance.DecreaseScrap(cost))
        {
            Debug.Log($"repaired with cost of {cost}");
            healthObj.ResetToMaxHealth();
        }
    }
    private void ScrapObject() 
    {
        int scrapVal = CalcScrapVal();
        GameManager.Instance.IncreaseScrap(scrapVal);
        DestroyDestructable();
    }

    private int CalcScrapVal()
    {
        int cost = gameObject.GetComponent<ShopItemData>().itemCost;

        return (int)(cost * (healthObj.currentHealth / healthObj.maxHealth));
    }

    private int CalcRepairCost()
    {
        Debug.Log($"curr health {healthObj.currentHealth}");
        float amountToFix = healthObj.maxHealth - healthObj.currentHealth;
        Debug.Log($"amount to fix: {amountToFix}");
        if (amountToFix == 0) return 0;

        float costPerHP = healthObj.maxHealth / gameObject.GetComponent<ShopItemData>().itemCost;
        
        float repairCost = costPerHP * amountToFix;

        Debug.Log($"cost per hp: {costPerHP}, repair cost: {repairCost}");
        return (int) repairCost;
    }
}
