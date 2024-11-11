using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    private Animator myAnimator;

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
        StartCoroutine(DestroyExplosion());
    }

    IEnumerator DestroyExplosion()
    {
        AnimatorStateInfo explosionState = myAnimator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(explosionState.length);
        Destroy(gameObject);
    }
}
