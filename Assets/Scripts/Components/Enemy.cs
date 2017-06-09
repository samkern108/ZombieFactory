using UnityEngine;
using System.Collections;

public class Enemy : Entity {

	// All of this is left over from my old tower defense, which I stole it from. :)
	public int enemyNum;
	public static int enemyAliveCounter = 0;
	public static int enemyKillCounter = 0;
	public static int enemyFailCounter = 0;

	void Awake () {
		enemyNum = enemyAliveCounter;
		enemyAliveCounter++;
		//WavePanel.self.SetEnemiesAlive (enemyAliveCounter, enemyKillCounter);
	}

	public override void Die()
	{
		enemyKillCounter++;
		DeathLogistics ();
		base.Die ();
	}

	public void EnterGoal()
	{
		enemyFailCounter++;
		//WavePanel.self.SetLivesRemainingText (GameManager.lives - enemyFailCounter);
		DeathLogistics ();
	}

	private void DeathLogistics()
	{
		enemyAliveCounter--;
		//WavePanel.self.SetEnemiesAlive (enemyAliveCounter, enemyKillCounter);

		if (enemyFailCounter >= GameManager.lives || enemyAliveCounter == 0) {
			GameManager.GameOver ();
		}
	}
}
