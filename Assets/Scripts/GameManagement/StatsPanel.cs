using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatsPanel : MonoBehaviour {

	public Image icon;
	public Text entityName, damage, attackSpeed, health, moveSpeed;
	private EntityStats stats;

	public static StatsPanel self;

	public void Start()
	{
		self = this;
		ClosePanel ();
	}

	public void ShowPanel(EntityStats s)
	{
		stats = s;
		icon.sprite = ResourceLoader.LoadPortrait (s.uid);
		UpdateUnitStats ();
		this.gameObject.SetActive (true);
	}

	public void ClosePanel()
	{
		this.gameObject.SetActive (false);
	}

	private void UpdateUnitStats()
	{
		entityName.text = stats.name;
		damage.text = ""+stats.attackStats.damage;
		attackSpeed.text = ""+stats.attackStats.attackSpeed;
		health.text = ""+stats.healthStats.health;
		moveSpeed.text = ""+stats.movementStats.moveSpeed;
	}

	public void Update()
	{
		UpdateUnitStats ();
	}
}
