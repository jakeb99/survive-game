using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBarSprite;
    [SerializeField] private Health health;
    [SerializeField] private Canvas healthCanvas;
    [SerializeField] private float maxShowTimeAfterUpdate;
    [SerializeField] private Transform sceneCam;
    private float currentTime;
    bool showCanvas;

    private void Update()
    {
        RotateToFaceSceneCamera();
        if (showCanvas && healthCanvas.enabled)
        {
            // start the timer
            if (currentTime > maxShowTimeAfterUpdate)
            {
                currentTime = 0;
                healthCanvas.enabled = false;
                showCanvas = false;
            }
            else
            {
                currentTime += Time.deltaTime;
            }
        }
    }

    private void Start()
    {
        sceneCam = GameManager.Instance.SceneCamera.transform;

        health = GetComponentInParent<Health>();
        healthCanvas.enabled = false;

        if (health == null)
        {
            Debug.Log("No Health obect in parent of healthbar");
        } else
        {
            health.OnIncrementHealth += UpdateHealthBar;
            health.OnDecrementHealth += UpdateHealthBar;
        }

        // rotate the healthbar to always face the camera
        RotateToFaceSceneCamera();
    }

    public void UpdateHealthBar()
    {
        if (!healthCanvas.enabled) 
        { 
            healthCanvas.enabled = true;
            showCanvas = true;
        }

        healthBarSprite.fillAmount = health.currentHealth / health.maxHealth;
    }

    private void RotateToFaceSceneCamera()
    {
        Vector3 dirVec = sceneCam.transform.position - gameObject.transform.position;

        dirVec.Normalize();
        //dirVec.y = 0;  // sp we only rotate along the y axis

        gameObject.transform.rotation = Quaternion.LookRotation(dirVec);
    }
}
