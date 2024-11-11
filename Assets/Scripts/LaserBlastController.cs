using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBlastController : MonoBehaviour
{
    public float bulletSpeed;
    public float bulletLife;
    Vector3 moveVector;
    public GameObject explosionPrefab;

    void Start()
    {
        moveVector = Vector3.up * bulletSpeed * Time.fixedDeltaTime;
        StartCoroutine(DestroyBlast());
    }

    void FixedUpdate()
    {
        transform.Translate(moveVector);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    IEnumerator DestroyBlast()
    {
        yield return new WaitForSeconds(bulletLife);
        Destroy(gameObject);
    }

}
