using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notifications : MonoBehaviour {

	public static Notifications self;

	public void Awake() {
		self = this;
	}

	public void SendGameEndNotification(bool victory) {
		BroadcastMessage ("N_GameEnd", victory);
	}

	public void SendPauseNotification(bool pause) {
		BroadcastMessage ("N_Pause",pause);
	}

	public void SendRestartNotification() {
		BroadcastMessage ("N_Restart");
	}

	public void SendLoadLevelNotification(int level) {
		BroadcastMessage ("N_LevelLoaded", level);
	}
}
