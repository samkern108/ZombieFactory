﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Town : MonoBehaviour {

	public GameObject villagerPrefab;
	private static List<Villager> villagers = new List<Villager>();
	private static int population = 20;

	void Start () {
		villagerPrefab = ResourceLoader.LoadPrefab ("Villager");
		for (int i = 0; i < population; i++) {
			BuildVillager ();
		}
	}

	private void BuildVillager()
	{
		GameObject villager = Instantiate (villagerPrefab);
		villager.transform.position = transform.position + new Vector3 (Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f), 0);
		villager.GetComponent <Villager> ().InitializeEntity ();

		/*
		GameObject attack; 
		if(stats.attackStats.projectile)
			attack = Instantiate (rangedAttack);
		else
			attack = Instantiate (meleeAttack);

		attack.tag = "Friendly Radius";
		attack.transform.position = h.transform.position;
		attack.transform.parent = h.transform;
		h.GetComponent<Entity>().InitializeEntity (stats);
		h.GetComponent<SFX> ().SetSuffixes (stats.sfxNum);
		*/
	}
}