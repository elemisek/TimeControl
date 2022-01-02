using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject Impact;

    private void OnCollisionEnter(Collision collision)
    {
        // Dont allow player to shoot themselves
        if (collision.gameObject.tag != "Player")
        {
            GameObject impact = Instantiate(Impact, collision.contacts[0].point, Quaternion.identity);

            Destroy(gameObject);
            // Destroy impact after 1 second
            Destroy(impact, 1);
        }

    }
}
