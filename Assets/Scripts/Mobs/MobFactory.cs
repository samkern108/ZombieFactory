using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class MobFactory : MonoBehaviour 
{
	private float health = 1;
	private float damage = 0;
	private float speed = 1;
	private float armor = 0;
	private float plague = 0;
	private float number = 1;

	private GameObject zombie;
	public Image preview;

	public void Start() {
		zombie = ResourceLoader.LoadPrefab ("Mob");
	}
	
	public void SetHealth(float h)
	{
			health = h;
			//TODO update preview so that the healthier zombies get larger and meatier and the unhealthier zombies lose flesh until they are skeletons
	}

	public void SetDamage(float d)
	{
			damage = d;
			//TODO update preview so that the high damage zombies get better and better weapons and the lower damage zombies just get nothing or bad weapons
	}
	
	public void SetSpeed(float s)
	{
			speed = s;
			//TODO update preview so that the faster zombies become zombie dogs and the slower zombies all have lost limbs
	}
	
	public void SetArmor(float a)
	{
			armor = a;
			//TODO update preview so that the armored zombies have suits of armor and the unarmored zombies have none
	}

	public void SetPlague(float p)
	{
		plague = p;
		//TODO update preview so that the plague zombies emit disease clouds and the unplagued zombies emit none
	}

	public void SetNumber(float n)
	{
		number = n;		
	}

	//TODO add 6 slots of predefined mobs for quick spawning.
	public void Spawn()
	{
		GameObject mobGroup = new GameObject ();
		mobGroup.name = "Mob Group";

		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePosition.z = 0;
		mobGroup.transform.position = mousePosition;

		Vector3 randomPosition;

		for(int i = 0; i < number; i++) {
			randomPosition = new Vector3 (Random.Range(-number/10.0f,number/10.0f), Random.Range(-number/10.0f,number/10.0f));
			GameObject newMob = GameObject.Instantiate (zombie, mobGroup.transform, false) as GameObject;
			newMob.transform.position += randomPosition;

			newMob.GetComponent<Zombie>().SetMyStats(health, armor, plague, damage, speed);
			newMob.GetComponent<Zombie> ().Hibernate (true);
		}
		mobGroup.AddComponent <PlaceZombie>();
	}
}