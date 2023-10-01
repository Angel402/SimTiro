using System.Collections;
using UnityEngine;
using EnemyAI;
using Unity.Netcode;
using UnityStandardAssets.Characters.FirstPerson;

// This class is created for the example scene. There is no support for this script.
public class PlayerShooting : NetworkBehaviour
{
	public Transform shotOrigin, drawShotOrigin;
	public LayerMask shotMask;
	public WeaponMode weaponMode = WeaponMode.SEMI;
	public int RPM = 600;
	public enum WeaponMode
	{
		SEMI,
		AUTO
	}

	private LineRenderer laserLine;
	private float weaponRange = 100f;
	private float bulletDamage = 10f;
	private bool canShot;
	public bool _inputFire1;

	private AudioSource gunAudio;

	private WaitForSeconds halfShotDuration;// = new WaitForSeconds(0.06f);
	private bool _isOwner;
	
	public BulletA Bullet;
	public GameObject FirePosition;

	public AudioSource audiosource;
	public AudioClip clip;

	public ParticleSystem MuzzleFlash;

	// Start is called before the first frame update
	public override void OnNetworkSpawn()
	{
		laserLine = GetComponent<LineRenderer>();
		gunAudio = GetComponent<AudioSource>();
		canShot = true;
		float waitTime = 60f / RPM;
		halfShotDuration = new WaitForSeconds(waitTime/2);
    }

    // Update is called once per frame
    void Update()
    {
	    /*Debug.Log($"{drawShotOrigin.gameObject.transform.position} ");*/
	    if (IsOwner && Input.GetButtonDown("Fire1")) InputFire1ServerRpc();
		if(weaponMode == WeaponMode.SEMI && _inputFire1 && canShot)
		{
			Shoot();
		}
		else if(weaponMode == WeaponMode.AUTO && _inputFire1 && canShot)
		{
			Shoot();
		}
		_inputFire1 = false;
    }

    [ServerRpc]        
    private void InputFire1ServerRpc(ServerRpcParams serverRpcParams = default)
    {
	    InputFire1ClientRpc(NetworkObjectId);
    }
    
    [ClientRpc]        
    private void InputFire1ClientRpc(ulong gameObjectP)
    {
	    var networkBehaviour = GetNetworkObject(gameObjectP);
	    networkBehaviour.GetComponentInChildren<PlayerShooting>()._inputFire1 = true;
	    Debug.Log("shoot");
    }
    
	void Shoot()
	{
		
		StartCoroutine(ShotEffect());
		MuzzleFlash.Play();
		audiosource.PlayOneShot(clip);
		Vector3 hitPoint = Vector3.negativeInfinity;
		if (Physics.Raycast(shotOrigin.position, shotOrigin.forward, out RaycastHit hit, weaponRange, shotMask))
		{
			if (hit.collider)
				hitPoint = hit.point;
		}
		Shooting(hitPoint);
		
		
		/*laserLine.SetPosition(0, drawShotOrigin.position);
		Physics.SyncTransforms();
		if (Physics.Raycast(shotOrigin.position, shotOrigin.forward, out RaycastHit hit, weaponRange, shotMask))
		{
			laserLine.SetPosition(1, hit.point);

			// Call the damage behaviour of target if exists.
			if(hit.collider)
				hit.collider.SendMessageUpwards("HitCallback", new HealthManager.DamageInfo(hit.point, shotOrigin.forward, bulletDamage, hit.transform.gameObject), SendMessageOptions.DontRequireReceiver);
		}
		else
			laserLine.SetPosition(1, drawShotOrigin.position + (shotOrigin.forward * weaponRange));*/

		// Call the alert manager to notify the shot noise.
		GameObject.FindGameObjectWithTag("GameController").SendMessage("RootAlertNearby", shotOrigin.position, SendMessageOptions.DontRequireReceiver);
	}
	
	private void Shooting(Vector3 hitPoint)
	{
		var bullet = Instantiate(Bullet, FirePosition.transform.position, FirePosition.transform.rotation);
		bullet.Configure(shotOrigin.forward, bulletDamage, SendMessageOptions.DontRequireReceiver);
		if (hitPoint != Vector3.negativeInfinity) bullet.transform.LookAt(hitPoint);
	}

	private IEnumerator ShotEffect()
	{
		/*gunAudio.Play();
		// Turn on our line renderer
		laserLine.enabled = true;*/
		canShot = false;

		yield return halfShotDuration;

		// Deactivate our line renderer after waiting
		/*laserLine.enabled = false;*/

		yield return halfShotDuration;

		if (weaponMode == WeaponMode.SEMI)
		{
			yield return halfShotDuration;
			yield return halfShotDuration;
		}

		canShot = true;
	}

	// Player dead callback.
	public void PlayerDead()
	{
		canShot = false;
	}
}