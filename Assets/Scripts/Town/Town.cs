using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Town : MonoBehaviour {

	public GameObject villagerPrefab;
	private static List<Villager> villagers = new List<Villager>();
	private static int population = 10;

	void Start () {
		villagerPrefab = ResourceLoader.LoadPrefab ("Villager");
		for (int i = 0; i < population; i++) {
			BuildVillager ();
		}
	}
	
	void Update () {
	
	}

	private void BuildVillager()
	{
		GameObject villager = Instantiate (villagerPrefab);
		villager.transform.position = transform.position + new Vector3 (Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f), 0);


		/*GameObject h = Instantiate (hero);
		h.transform.position = heroSpawnPoint.transform.position + new Vector3(Mathf.Cos(((float)i/(float)heroes.Length) * (2 * Mathf.PI)), Mathf.Sin(((float)i/(float)heroes.Length) * (2 * Mathf.PI)));
		EntityStats stats = IOManager.LoadEntityStats (heroes [i]);

		h.name = stats.uid;

		GameObject attack; 
		if(stats.attackStats.projectile)
			attack = Instantiate (rangedAttack);
		else
			attack = Instantiate (meleeAttack);

		attack.tag = "Friendly Radius";
		attack.transform.position = h.transform.position;
		attack.transform.parent = h.transform;
		h.GetComponent<Entity>().InitializeEntity (stats);
		h.GetComponent<SFX> ().SetSuffixes (stats.sfxNum);*/
	}
}