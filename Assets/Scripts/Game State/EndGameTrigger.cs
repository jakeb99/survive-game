using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    [SerializeField] private string enemyTag;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyTag))
        {
            GameManager.Instance.ChangeState(new GameEndState(GameManager.Instance));
        }
    }
}
