using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Health healthobj;

    private void Awake()
    {
        healthobj = GetComponent<Health>();
        if (healthobj)
        {
            healthobj.OnDeath += handleDeath;
        }

    }

    private void handleDeath()
    {

        healthobj.OnDeath -= handleDeath;
    }
}
