using UnityEngine;
using System.Collections;

public class Direct : MonoBehaviour {

	public static GameObject directFlag;
	private GameObject flag;

	void Update () {
		if (!GameManager.paused && Input.GetMouseButtonDown (0)) {
			Vector3 clickPos = CameraController._camera.ScreenToWorldPoint (Input.mousePosition);
			BoxCollider2D col = GetComponentInChildren <BoxCollider2D>();
			if (!col.OverlapPoint (clickPos)) {
				if (flag)
					Destroy (flag);
				flag = Instantiate (directFlag) as GameObject;
				clickPos.z = this.transform.position.z;
				flag.transform.position = clickPos;
				flag.GetComponent<DirectFlag> ().assignedHero = this.gameObject;
				GetComponentInParent<Movement> ().SetGoalAndMove (flag.transform, true);
				GetComponent<SFX> ().PlayClip (this.name, sfxlib.move);
			}
			else
				GetComponentInParent<Movement>().StopMoving (true);
		}
	}
}
