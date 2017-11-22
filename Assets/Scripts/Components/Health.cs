using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Health : MonoBehaviour {

	private static float unitSpacing = .1f;

	//TODO - Can we combine these two somehow?
	private List<Attack> attackers = new List<Attack> ();
	private bool[] occupiedSlots;

	protected HealthStats stats;
	private SpriteRenderer healthBar;

	public void InitializeStats(HealthStats s)
	{
		stats = s;
		stats.health = s.maxHealth;

		float frac = 510 * (stats.health / stats.maxHealth);
		float r = Mathf.Clamp(510 - frac, 0, 255)/255;
		float g = Mathf.Clamp(frac, 0, 255)/255;

		//TODO do we want Ceil or floor?
		occupiedSlots = new bool[(int)Mathf.Ceil(GetComponent<CircleCollider2D>().bounds.size.x/unitSpacing)];

		GameObject healthBarGO = Instantiate(ResourceLoader.LoadPrefab ("HealthBar"));
		healthBarGO.transform.parent = this.transform;
		Vector3 healthBarPos = this.transform.position;
		healthBarPos.y -= .32f;
		healthBarGO.transform.position = healthBarPos;
		healthBar = healthBarGO.GetComponent <SpriteRenderer>();
		healthBar.color = new Color (r,g,0);
	}

	/*
	 * true: fatal damage
	 */
	public virtual bool TakeDamage(float damage, GameObject attacker)
	{
		if (stats.dead)
			return true;

		stats.health -= damage;
		ColorHealth ();

		GetComponentInChildren <Attack> ().AttackedBy (attacker);

		if(stats.regenAmount > 0)
			StartCoroutine ("Regenerate");

		if (stats.health <= 0) 
			Dead ();

		return stats.health <= 0;
	}

	private void Dead()
	{
		//monsters and heroes can implement a "dead" method to determine how they should handle death

		stats.dead = true;
		foreach(Attack a in attackers) 
			a.TargetDead(this.gameObject);

		attackers.Clear ();

		if (stats.respawnTimer > 0) {
			GameObject go = new GameObject ();
			go.transform.position = transform.position;
			go.AddComponent <RespawnPoint>();
			go.GetComponent <RespawnPoint>().StartRespawn(this.gameObject, stats.respawnTimer);
			this.gameObject.SetActive(false);
		}
		else {
			this.SendMessage ("Die");
		}
	}

	private void ColorHealth()
	{
		float frac = 510 * (stats.health / stats.maxHealth);
		float r = Mathf.Clamp(510 - frac, 0, 255)/255;
		float g = Mathf.Clamp(frac, 0, 255)/255;
		healthBar.color = new Color(r, g, 0);
	}

	public virtual void Revive()
	{
		//monsters and heroes can implement a "respawn" method
		//this.SendMessage ("Respawn");
		stats.dead = false;
		stats.health = stats.maxHealth;
		ColorHealth ();
	}

	public int GetPosition()
	{
		for (int i = 0; i < occupiedSlots.Length; i++) {
			if (!occupiedSlots [i]) {
				occupiedSlots [i] = true;
				CircleCollider2D col = GetComponent<CircleCollider2D>();
				Vector3 pos = col.bounds.center + 
					new Vector3(Mathf.Cos(((float)i/(float)occupiedSlots.Length) 
						* (2 * Mathf.PI)), Mathf.Sin(((float)i/(float)occupiedSlots.Length) 
							* (2 * Mathf.PI)));
				return i;
			}
		}
		return -1;
	}

	public IEnumerator Regenerate()
	{
		while (true) {
			stats.health += stats.regenAmount;
			if (stats.health > stats.maxHealth) {
				stats.health = stats.maxHealth;
				StopCoroutine ("Regenerate");
			}
			yield return new WaitForSeconds(stats.regenSeconds);
		}
	}

	//hey, we can return a bool to let the attacker know if it's allowed to attack this target,
	//or if too many already are and there's no room
	public bool Target(Attack attacker)
	{
		attackers.Add (attacker);
		return true;
	}

	public void UnTarget(Attack attacker)
	{
		//this is where FreeSlot should happen.
		attackers.Remove (attacker);
	}
}
