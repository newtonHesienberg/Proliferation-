using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePos; 


    public void InstantiateFireBall()
    {
        GameObject ballInstance = Instantiate(bulletPrefab, firePos.position, Quaternion.identity);
        ballInstance.transform.position = firePos.position;
        ballInstance.transform.forward = firePos.forward;
    }
}
