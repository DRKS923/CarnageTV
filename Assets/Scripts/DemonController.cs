using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Animator))]
public class DemonController : MonoBehaviour
{
    public int hitpoints;
    private Animator myAnimator;
    private AudioSource hitSound;
    private AudioClip deathSound;

    void Start()
    {
        myAnimator = GetComponentInChildren<Animator>();
        hitSound = GetComponent<AudioSource>();
        deathSound = Resources.Load<AudioClip>("Audio/DemonWail");
    }

    void Update()
    {
        
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            hitpoints--;
            if(hitpoints <= 0)
            {
                hitSound.clip = deathSound;
                StartCoroutine(KillDemon());
            }
            hitSound.Play();
        }
    }

    IEnumerator KillDemon()
    {
        GetComponent<Collider2D>().enabled = false;
        GameController.instance.SlayDemon();
        myAnimator.SetTrigger("KillDemon");
        AnimatorStateInfo deathAnimState = myAnimator.GetCurrentAnimatorStateInfo(0);

        yield return new WaitForSeconds(deathAnimState.length);

        Destroy(gameObject);
    }
}
