using UnityEngine;

public class TrophyCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // When hit with either attack destroy the trophy
        if (collision.gameObject.tag == "RightBullet" || collision.gameObject.tag == "LeftBullet")
        {
            LevelManager.instance.enemiesCount--;
            Destroy(gameObject);
        }
    }
}
