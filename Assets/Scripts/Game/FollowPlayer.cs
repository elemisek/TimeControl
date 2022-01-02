using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    #region Singleton

    public static FollowPlayer instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    // Allow for player object to be available for other scripts
    public GameObject player;
}
