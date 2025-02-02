using UnityEngine;

public class HookSystem : MonoBehaviour
{
    public GameObject hookPrefab; // Prefab-ul pentru hook
    public float hookSpeed = 10f;
    public float hookCooldown = 2f;

    private TargetingSystem targetingSystem;
    private bool canShoot = true;

    private void Start()
    {
        targetingSystem = GetComponent<TargetingSystem>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canShoot)
        {
            ShootHook();
        }
    }

    private void ShootHook()
    {
        GameObject target = targetingSystem.GetCurrentTarget();
        if (target != null)
        {
            canShoot = false;
            GameObject hook = Instantiate(hookPrefab, transform.position, Quaternion.identity);
            StartCoroutine(MoveHook(hook, target.transform.position));
        }
    }

    private System.Collections.IEnumerator MoveHook(GameObject hook, Vector3 targetPosition)
    {
        // Calculează direcția de mișcare a hook-ului
        Vector3 direction = (targetPosition - hook.transform.position).normalized;

        // Rotește hook-ul către direcția țintei
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        hook.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        while (Vector3.Distance(hook.transform.position, targetPosition) > 0.1f)
        {
            hook.transform.position = Vector3.MoveTowards(hook.transform.position, targetPosition, hookSpeed * Time.deltaTime);
            yield return null;
        }

        Destroy(hook);

        // Capturarea peștelui
        GameObject target = targetingSystem.GetCurrentTarget();
        if (target != null)
        {
            Destroy(target);
        }

        // Cooldown
        yield return new WaitForSeconds(hookCooldown);
        canShoot = true;
    }
}
