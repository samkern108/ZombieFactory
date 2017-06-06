using UnityEngine;
using System.Collections;

public class SelectionManager : MonoBehaviour {

	public static GameObject selected;
	public static GameObject selectGlow;

	public static GameObject selectGlowPrefab;

	public void Update()
	{
		if (Input.GetMouseButtonDown (0))
		{
			Vector3 clickPos = CameraController._camera.ScreenToWorldPoint (Input.mousePosition);
			RaycastHit2D[] hits = Physics2D.RaycastAll (clickPos, Vector3.back, 100.0f);
			foreach (RaycastHit2D h in hits) {
				if (!h.collider.isTrigger && h.transform.tag.Contains("Selectable")) {
					ChangeSelected (h.transform.gameObject);
				}
			}
		}
	}

	public void Start()
	{
		selectGlowPrefab = ResourceLoader.LoadGameObject ("SelectGlow");
	}

	public static void ChangeSelected(GameObject clicked) {

		Attack attack;

		if(selected != null) {
			if(selected.tag.Contains("Hero"))
				selected.GetComponent <Direct>().enabled = false;
			attack = selected.GetComponentInChildren<Attack> ();
			if (attack)
				attack.HideRadius ();
		}

		selected = clicked;
		if(clicked.tag.Contains("Hero")) {
			clicked.GetComponent<SFX> ().PlayClip (clicked.name, sfxlib.select);
			clicked.GetComponent <Direct>().enabled = true;
		}
		Vector3 pos = new Vector3 (0, 0, 1);

		if(!selectGlow)
			selectGlow = Instantiate (selectGlowPrefab);

		selectGlow.transform.parent = clicked.transform;
		selectGlow.transform.localPosition = pos;

		attack = clicked.GetComponentInChildren<Attack> ();

		if (attack)
			attack.ShowRadius ();

		UIManager.OpenStatsPanel (clicked.GetComponent<Entity>().stats);
	}
}
