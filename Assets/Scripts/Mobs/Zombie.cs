using UnityEngine;
using System.Collections;

public class Zombie : MonoBehaviour {

		private float health;	//low health are skeletons, high health are demon lords
		private float armor;	//high armor actually have armor, low armor have nothing
		private float plague;	//high plague emit clouds, low plague have nothing
		private float attack;	//high attack have swords, low attack have nothing
		private float speed;	//high speed are demon dogs, low speed are missing limbs
	
		public void SetMyStats(float h, float a, float p, float at, float s)
		{
				health = h;
				armor = a;
				plague = p;
				attack = at;
				speed = s;
		}

		
}
