using UnityEngine;
using System.Collections;

public class DirectFlag : MonoBehaviour {

	public GameObject assignedHero;

	public void OnTriggerEnter2D(Collider2D obj)
	{
		if (obj.gameObject == assignedHero) {
			Invoke ("DestroyFlag", 3f);
		}
	}

	public void DestroyFlag()
	{
		Destroy (this.gameObject);
	}
}
