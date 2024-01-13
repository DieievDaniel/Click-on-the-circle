using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAnimation : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void ApearAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Show");
        }
    }
    public void DissapearAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Hide");
        }
    }

}
