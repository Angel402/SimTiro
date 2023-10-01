using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletA : MonoBehaviour
{
    public Rigidbody rb;

    private Vector3 _direction;

    private float _bulletDamage;

    private SendMessageOptions _messageOptions;
    /*public GameObject hit;
    public GameObject Fire;*/

    void Start()
    {
        rb.AddForce(transform.forward *25000);/*
        GameObject A = Instantiate(Fire, this.transform.position, Quaternion.identity);
        Destroy(A, 2);*/
        

    }

    private void OnCollisionEnter(Collision other)
    {
        other.collider.SendMessageUpwards("HitCallback",
            new HealthManager.DamageInfo(other.GetContact(0).point, _direction, _bulletDamage,
                other.transform.gameObject), SendMessageOptions.DontRequireReceiver);
        var impactEffect = ImpactEffectManager.Instance.GetImpactEffect(other.gameObject);
        if (impactEffect == null) return;
        GameObject B = Instantiate(impactEffect, transform.position, Quaternion.identity);
        B.transform.LookAt(other.GetContact(0).point + other.GetContact(0).normal);
        Destroy(B, 2);
        Destroy(gameObject);
    }

    public void Configure(Vector3 direction, float bulletDamage, SendMessageOptions messageOptions)
    {
        _direction = direction;
        _bulletDamage = bulletDamage;
        _messageOptions = messageOptions;
    }
}