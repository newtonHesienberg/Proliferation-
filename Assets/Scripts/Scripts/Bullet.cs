using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8f;
    public float lifeTime = 1f;
    void Start()
    {

       Invoke("DestoyProjectile", lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void DestoyProjectile()
    {
        Destroy(gameObject);
    }
}
