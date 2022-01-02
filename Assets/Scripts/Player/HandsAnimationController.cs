using UnityEngine;

public class HandsAnimationController : MonoBehaviour
{
    #region Singleton

    public static HandsAnimationController instance;

    private void Awake()
    {
        instance = this;
        HandsAnimator = GetComponent<Animator>();
    }

    #endregion

    private Animator HandsAnimator;

    public void Attack(bool isRight)
    {
        if (isRight)
        {
            HandsAnimator.SetTrigger("RightHandAnimation");
        }
        else
        {
            HandsAnimator.SetTrigger("LeftHandAnimation");
        }

        HandsAnimator.SetTrigger("Idle");
    }

}
