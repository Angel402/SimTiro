using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleA : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject FirePosition;

    public AudioSource audiosource;
    public AudioClip clip;

    public ParticleSystem MuzzleFlash;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            MuzzleFlash.Play();
            audiosource.PlayOneShot(clip);
            Shooting();
        }
    }
    public void Shooting()
    {
        var bala = Instantiate(Bullet, FirePosition.transform.position, FirePosition.transform.rotation).GetComponent<BulletA>();
        bala.Configure(FirePosition.transform.forward, 10, SendMessageOptions.DontRequireReceiver);
    }
}
