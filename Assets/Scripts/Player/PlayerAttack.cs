using UnityEngine;

public class PlayerAttack : MonoBehaviour // zmienic na player attack
{
    // Shooting attributes
    public GameObject LeftProjectile, RightProjectile;
    public Transform LeftHandFirePoint, RightHandFirePoint;
    public float projectileSpeed = 30f;
    public float fireRate = 4f;

    private Camera mainCamera;
    private Vector3 attackDestination;
    private float timeToFire;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // When the game is paused disallow shooting
        if (PauseMenu.isPaused == false)
        {
            // Wait until set time passes after the last shot
            if (Time.time >= timeToFire)
            {
                if (Input.GetButton("Fire1"))
                {
                    Attack(isRight: false);
                    timeToFire = Time.time + 1 / fireRate;
                }
                else if (Input.GetButton("Fire2"))
                {
                    Attack(isRight: true);
                    timeToFire = Time.time + 1 / fireRate;
                }
            }
        }
    }
    private void Attack(bool isRight)
    {
        HandsAnimationController.instance.Attack(isRight);

        // Choose proper fire point and projectile based on isRight variable
        Transform firePoint = isRight ? RightHandFirePoint : LeftHandFirePoint;
        GameObject projectile = isRight ? RightProjectile : LeftProjectile;

        // Create a ray from player's camera through the center of view
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        // Set ray destination 10 units from the camera
        attackDestination = ray.GetPoint(10);
        InstantiateProjectile(projectile, firePoint);
    }

    private void InstantiateProjectile(GameObject projectile, Transform firePoint)
    {
        GameObject projectileObject = Instantiate(projectile, firePoint.position, Quaternion.identity);

        // Set projectile velocity towards destination
        projectileObject.GetComponent<Rigidbody>().velocity = (attackDestination - firePoint.position).normalized * projectileSpeed;
        
        // Destroy projectile after 3 seconds
        Destroy(projectileObject, 3);
    }
}

