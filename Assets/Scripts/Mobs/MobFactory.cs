using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class MobFactory : MonoBehaviour 
{
	private float health = 0;
	private float damage = 0;
	private float speed = 0;
	private float armor = 0;
	private float plague = 0;
	private float number = 0;

	private GameObject zombie;
	public Transform mobManager;
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
	//TODO bad college code is bad. The mobgroup script and placezombie script should be the same.
	public void Spawn()
	{
		GameObject mobGroup = new GameObject ();
		mobGroup.name = "Mob Group";
		mobGroup.AddComponent <PlaceZombie>();
		mobGroup.GetComponent <PlaceZombie>().Init(speed);
		mobGroup.transform.parent = mobManager;

		for(int i = 0; i < number; i++) {
			GameObject newMob = GameObject.Instantiate (zombie) as GameObject;
			newMob.transform.position += new Vector3 (Random.Range(-number/10.0f,number/10.0f), Random.Range(-number/10.0f,number/10.0f));
			newMob.GetComponent<Zombie>().SetMyStats(health, armor, plague, damage, speed);
			newMob.transform.parent = mobGroup.transform;
		}
	}
}