using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] Transform turretBasePivot;
    [SerializeField] Transform turretGunPivot;
    [SerializeField] Transform weaponTip;

    [SerializeField] GameObject currentTarget;
    [SerializeField] float colliderRadius;

    private AttackAbility attackAbility;

    private void Awake()
    {
        attackAbility = GetComponent<AttackAbility>();
    }

    private void Start()
    {
        CapsuleCollider capCollider = GetComponent<CapsuleCollider>();
        capCollider.height = 10f;
        capCollider.radius = colliderRadius;
        capCollider.center = new Vector3(-colliderRadius, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("turret: enemy trigger entered");
        }
    }


}
