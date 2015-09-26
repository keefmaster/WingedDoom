using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
//putin huilo
	[System.Serializable]
	public class PlayerStats {
		public int Health = 100;
		public int numberofLives = 3;
	}

	public PlayerStats playerStats = new PlayerStats();

	public int fallBoundary = -20;

	void Update () {
		if (transform.position.y <= fallBoundary)
			DamagePlayer (9999999);
	}

	public void DamagePlayer (int damage) {
		playerStats.Health -= damage;
		if (playerStats.Health <= 0) {
			GameMaster.KillPlayer(this);
		}
	}

}
