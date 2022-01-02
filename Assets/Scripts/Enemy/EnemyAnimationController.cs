using UnityEngine;
using System.Collections;

public class EnemyAnimationController : MonoBehaviour
{
    private Animator EnemyAnimator;

    private void Start() => EnemyAnimator = GetComponent<Animator>();
    public void Idle()
    {
        if (EnemyAnimator)
        {
            EnemyAnimator.SetBool("Walk", false);
        }
    }

    public void Walk()
    {
        if (EnemyAnimator)
        {
            EnemyAnimator.SetBool("Walk", true);
        }
    }

    public void Death()
    {
        if (EnemyAnimator)
        {
            EnemyAnimator.SetTrigger("Death");
        }
    }

    public bool isDeathAnimationOver()
    {
        // When enemy animator is already destroyed we can assume that the death animation is over
        if (EnemyAnimator)
        {
            return EnemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("EnemyDead");
        }
        return true;
    }
}
