using UnityEngine;
using System.Collections;

[System.Serializable]
public class EntityStats
{
	public string uid { get; set; }
	public string name { get; set; }
	public int experienceGiven { get; set; }

	public MovementStats movementStats { get; set; }
	public AttackStats attackStats { get; set; }
	public HealthStats healthStats { get; set; }
	public HeroStats heroStats { get; set; }
	public int[] sfxNum { get; set; }
}

[System.Serializable]
public class MovementStats
{
	public float moveSpeed { get; set; }
	public bool moving = false;
}

[System.Serializable]
public class AttackStats
{
	public float damage { get; set; }
	public float spread { get; set; }
	public float attackSpeed { get; set; }
	public bool aggressive { get; set; }
	public bool attackWhileMoving { get; set; }
	public bool projectile { get; set; }
	public float range { get; set; }
}

[System.Serializable]
public class HealthStats
{
	public float health;
	public float maxHealth { get; set; }
	public float respawnTimer { get; set; }
	public bool dead = false;
	public float regenSeconds { get; set; }
	public float regenAmount { get; set; }
}

[System.Serializable]
public class HeroStats
{
	public int level { get; set; }
	public float experience { get; set; }
	public float boost { get; set; } //what was this supposed to be? ... :|
}

[System.Serializable]
public class Level
{
	public string uid { get; set; }
	public string name { get; set; }
	public string[] heroes { get; set; }
	public int lives { get; set; }
	public SpawnerStats[] spawners { get; set; }
}

[System.Serializable]
public class GameState
{
	public string currentlevel { get; set; }
}

[System.Serializable]
public class SpawnerStats
{
	public float spawnRate { get; set; }
	public int spawnCount { get; set; }
	public string enemy { get; set; }
}