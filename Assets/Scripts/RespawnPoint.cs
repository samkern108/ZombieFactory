using UnityEngine;
using System.Collections;

public class RespawnPoint : MonoBehaviour {

	private Vector3 respawnPosition;
	private float timer;

	public void StartRespawn(GameObject go, float timer)
	{
		this.timer = timer;
		StartCoroutine ("RespawnCount", go);
	}

	private IEnumerator RespawnCount(GameObject go) 
	{
		yield return new WaitForSeconds (timer);
		go.SetActive (true);
		go.GetComponent <Health>().Revive();
		StopCoroutine ("RespawnCount");
	}
}
