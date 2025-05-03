using UnityEngine;

public class PlaceableObjectSerializableData
{
    public float[] Position;
    public float CurrentHealth;
    public PlaceableType type;

    public PlaceableObjectSerializableData SetData(GameObject obj)
    {
        Transform t = obj.transform;
        Health health = obj.GetComponent<Health>();
        ShopItemData itemData = obj.GetComponent<ShopItemData>();

        return new PlaceableObjectSerializableData
        {
            Position = new float[] {t.position.x, t.position.y, t.position.z},
            CurrentHealth = health.currentHealth,
            type = itemData.type,
        };
    }

}
