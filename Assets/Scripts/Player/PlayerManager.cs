using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Restart level if player is hit by an enemy bullet
        if (collision.gameObject.tag == "Bullet")
        {
            LevelManager.RestartLevel();
        }
    }

    private void Update()
    {
        // Restart level if player falls out of the map
        if (transform.position.y < -10)
        {
            LevelManager.RestartLevel();
        }
    }
}
