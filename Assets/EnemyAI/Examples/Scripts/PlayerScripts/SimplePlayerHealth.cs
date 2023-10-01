using System.Collections;
using System.Linq;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

// This class is created for the example scene. There is no support for this script.

public class SimplePlayerHealth : HealthManager
{
	public float health = 100f;

	public Transform canvas;
	public GameObject hurtPrefab;
	public float decayFactor = 0.8f;

	private HurtHUD hurtUI;

	private void Awake()
	{
		AudioListener.pause = false;
		hurtUI = this.gameObject.AddComponent<HurtHUD>();
		hurtUI.Setup(canvas, hurtPrefab, decayFactor, this.transform);
	}



	public override void TakeDamageMessageSender(Vector3 location, Vector3 direction, float damage, GameObject bodyPart,
		GameObject origin)
	{
		var healthManager = bodyPart.GetComponentInParent<HealthManager>();
		var bodyPartId = 0;
		for (int i = 0; i < healthManager.bodyParts.Count; i++)
		{
			if (healthManager.bodyParts[i] == bodyPart)
			{
				bodyPartId = i;
			}
		}

		TakeDamageClientRPC(location, direction, damage, healthManager.NetworkBehaviourId, bodyPartId,
			origin.GetComponent<NetworkBehaviour>().NetworkBehaviourId);
	}

	/*[ServerRpc]
	private void TakeDamageServerRpc(Vector3 location, Vector3 direction, float damage, ushort receiverId,
		int bodyPartId, ushort originId)
	{
		TakeDamageClientRPC(location, direction, damage, receiverId, bodyPartId, originId);
	}*/

	[ClientRpc]
	private void TakeDamageClientRPC(Vector3 location, Vector3 direction, float damage, ushort receiverId, int bodyPartId, ushort originId)
	{
		var receiver = GetNetworkBehaviour(receiverId);
		var receiverHealthManager = receiver.GetComponent<HealthManager>();
		var origin = GetNetworkBehaviour(originId);
		var bodyPart = receiverHealthManager.bodyParts[bodyPartId];
		receiverHealthManager.TakeDamage(location, direction, damage, bodyPart, origin.gameObject);
	}

	public override void TakeDamage(Vector3 location, Vector3 direction, float damage, GameObject bodyPart = null,
		GameObject origin = null)
	{
		health -= damage;

		if (hurtPrefab && canvas)
			hurtUI.DrawHurtUI(origin.transform, origin.GetHashCode());
	}
	
	public void OnGUI()
	{
		if (health > 0f)
		{
			GUIStyle textStyle = new GUIStyle
			{
				fontSize = 50
			};
			textStyle.normal.textColor = Color.white;
			GUI.Label(new Rect(0, Screen.height - 60, 30, 30), health.ToString(), textStyle);
		}
		else if (!dead)
		{
			dead = true;
			StartCoroutine("ReloadScene");
		}
	}

	private IEnumerator ReloadScene()
	{
		SendMessage("PlayerDead", SendMessageOptions.DontRequireReceiver);
		yield return new WaitForSeconds(0.5f);
		canvas.gameObject.SetActive(false);
		AudioListener.pause = true;
		/*Camera.main.clearFlags = CameraClearFlags.SolidColor;
		Camera.main.backgroundColor = Color.black;
		Camera.main.cullingMask = LayerMask.GetMask();*/

		yield return new WaitForSeconds(1);

		dead = false;
		health = 100;
		canvas.gameObject.SetActive(true);
		AudioListener.pause = false;
		
	}
}
