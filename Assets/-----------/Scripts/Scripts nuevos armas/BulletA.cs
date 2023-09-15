using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletA : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject hit;
    public GameObject Fire;
    void Start()
    {
        rb.AddForce(transform.forward *25000);
        GameObject A = Instantiate(Fire, this.transform.position, Quaternion.identity);
        Destroy(A, 2);
        

    }

    private void OnCollisionEnter(Collision other)
    {
        GameObject B = Instantiate(hit, this.transform.position, Quaternion.identity);
        Destroy(B, 2);
        Destroy(this.gameObject);
    }
}
