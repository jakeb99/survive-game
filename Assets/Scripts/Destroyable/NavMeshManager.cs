using Unity.AI.Navigation;
using UnityEngine;

[RequireComponent(typeof(NavMeshSurface))]
public class NavMeshManager : MonoBehaviour
{
    private NavMeshSurface Surface;

    private static NavMeshManager instance;
    public static NavMeshManager Instance
    {
        get
        {
            return instance;
        }

        private set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"Multiple NavMeshManagers in the scene! Destroying {name}!");
            Destroy(gameObject);
            return;
        }

        Surface = GetComponent<NavMeshSurface>();
        Instance = this;
    }

    public void BakeNavMesh()
    {
        Surface.BuildNavMesh();
    }
}
