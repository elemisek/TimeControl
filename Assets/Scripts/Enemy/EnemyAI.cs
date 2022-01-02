using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // Attribute stating which player projectile
    // (left or right) will kill particular enemy
    public bool isRight = true;

    // Shooting
    public GameObject projectile;
    public Transform firePoint;
    public float projectileSpeed = 15f;
    public float fireRate = 1f;

    // Movement ranges
    public float sightRange = 10f;
    public float melleRange = 3f;

    // Components
    private Transform target;
    private NavMeshAgent agent;
    private EnemyAnimationController animationController;

    // Temp variables
    private float timeToFire;
    private float distance;
    private bool isAlive = true;

    private void Start()
    {
        // Components assignment
        agent = GetComponent<NavMeshAgent>();
        animationController = GetComponent<EnemyAnimationController>();
        target = FollowPlayer.instance.player.transform;
    }

    private void Update()
    {
        // Enemy is not performing any actions when dead except for death animation
        if (isAlive)
        {
            // Get distance from enemy to player
            distance = Vector3.Distance(transform.position, target.position);

            // Face player even if out of sight range to make it more creepy
            FaceTarget();

            if (distance <= sightRange)
            {
                // When in sight range approach the player
                FollowTarget();

                if (distance < melleRange)
                {
                    // When in melee range kill the player
                    LevelManager.RestartLevel();
                }
                else if (Time.time >= timeToFire)
                {
                    // Attack with set cooldown 
                    Attack();
                    timeToFire = Time.time + 1 / fireRate;
                }
            }
            else
            {
                // When player outside of range change animation to idle and stop
                Stop();
            }
        }
        else if (animationController.isDeathAnimationOver())
        {
            // Wait for death animation to end and destroy the enemy
            DestroyEnemy();
        }
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;

        // Rotate the enemy 90 degrees because Blender models are imported rotated 
        direction = Quaternion.AngleAxis(-90, Vector3.up) * direction;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = lookRotation;
    }

    private void FollowTarget()
    {
        // Set enemy destination and play walking and shooting animation
        agent.SetDestination(target.position);
        agent.isStopped = false;
        animationController.Walk();
    }

    private void Attack()
    {
        // Create a ray from the enemy's hand to the player
        Ray ray = new Ray(firePoint.position, target.position - firePoint.position);
        RaycastHit hit;

        // Shoot only if player can be hit
        if (Physics.Raycast(ray, out hit, sightRange) && hit.transform.tag == "Player")
        {
            InstantiateProjectile(hit.point);
        }
    }

    private void InstantiateProjectile(Vector3 destination)
    {
        GameObject projectileObject = Instantiate(projectile, firePoint.position, Quaternion.identity);
        // Set projectile velocity towards destination
        projectileObject.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * projectileSpeed;
        // Destroy projectile after 3 seconds
        Destroy(projectileObject, 3);
    }

    private void Stop()
    {
        agent.isStopped = true;
        animationController.Idle();
    }

    private void DestroyEnemy()
    {
        // Decrement number of enemies left on current level
        LevelManager.instance.enemiesCount--;

        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // When enemy killed with proper attack kill it
        if (isRight && collision.gameObject.tag == "RightBullet")
        {
            Die();
        }
        else if (!isRight && collision.gameObject.tag == "LeftBullet")
        {
            Die();
        }
    }

    private void Die()
    {
        // Stop the enemy from doing anything except from playing death animation
        isAlive = false;
        agent.isStopped = true;
        animationController.Death();
    }
}
