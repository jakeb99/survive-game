using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class TurretController : MonoBehaviour
{
    [Header("Turret Settings")]
    [SerializeField] Transform turretBasePivot;
    [SerializeField] Transform turretGunPivot;
    [SerializeField] string enemyTag = "Enemy";
    [SerializeField] float turretRange;
    [SerializeField] float rotationSpeed;

    [SerializeField] GameObject currentTarget;

    [Header("Shooting Settings")]
    //[SerializeField] private float fireRate;
    //[SerializeField] private float fireCooldown = 0f;
    [SerializeField] Transform weaponTip;
    [SerializeField] AudioClip gunSound;
    [SerializeField] ParticleSystem muzzleFlash;


    private AttackAbility attackAbility;
    private AudioSource audioSource;

    private void Awake()
    {
        attackAbility = GetComponent<AttackAbility>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        InvokeRepeating("UpdateCurrentTarget", 0, 0.5f);
        attackAbility.OnAttack += ShootAudio;
        attackAbility.OnAttack += MuzzleFlash;
    }

    private void Update()
    {
        if (currentTarget == null) 
        {
            attackAbility.StopAttack();
            return;
        }

        FaceEnemy();

        attackAbility.StartAttack(currentTarget);
    }

    private void ShootAudio()
    {
        if (audioSource != null)
            audioSource.PlayOneShot(gunSound);
    }

    private void MuzzleFlash()
    {
        if (muzzleFlash != null)
            muzzleFlash.Play();
    }

    void UpdateCurrentTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestEnemyDist = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(enemy.transform.position, transform.position);
            // if the enemy is in range
            if (distanceToEnemy < closestEnemyDist)
            {
                closestEnemyDist = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if ( nearestEnemy != null && closestEnemyDist <= turretRange)
        {
            currentTarget = nearestEnemy;
        } else
        {
            currentTarget = null;
        }
    }

    private void FaceEnemy()
    {
        Quaternion rotateBy90Deg = Quaternion.AngleAxis(90, Vector3.up);

        // get hdirection to face
        Vector3 baseDirVec = currentTarget.transform.position - turretBasePivot.position;
        
        baseDirVec.Normalize();
        Vector3 baseRot = baseDirVec; // only rotate on y axis
        baseRot.y =  0f; // only rotate on y axis
        Quaternion baseRotation = Quaternion.LookRotation(baseRot) * rotateBy90Deg;
        turretBasePivot.rotation = Quaternion.Slerp(turretBasePivot.rotation, baseRotation, rotationSpeed * Time.deltaTime);

        // get pitch
        Vector3 gunDir = (currentTarget.transform.position) - turretGunPivot.position;
        gunDir.y += currentTarget.transform.localScale.y / 2;       // add half its hight so we are looking at the middle of the enemy not at its feet
        gunDir.Normalize();
        Quaternion gunRotation = Quaternion.LookRotation(gunDir) * rotateBy90Deg;
        turretGunPivot.rotation = Quaternion.Slerp(turretGunPivot.rotation, gunRotation, rotationSpeed * Time.deltaTime);
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        if (currentTarget)
        {
            Gizmos.DrawLine(turretBasePivot.position, currentTarget.transform.position);
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, turretRange);
        Gizmos.DrawSphere(weaponTip.position, 0.05f);
    }

}
