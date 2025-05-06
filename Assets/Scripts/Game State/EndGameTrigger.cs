using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    [SerializeField] private string enemyTag;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyTag))
        {
            Debug.Log("Game Over!");
            // go to game over state
        }
    }
}
