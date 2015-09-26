using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;


public class GameMaster : MonoBehaviour {

	public static GameMaster gm;

	void Awake () {
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag ("GM").GetComponent<GameMaster>();
		}
	}

	public Transform playerPrefab;
	public Transform enemyPrefab;
	public Transform spawnPoint;
	public Transform enemySpawnPoint;
	public float spawnDelay = 2;
	public Transform spawnPrefab;

	public CameraShake cameraShake;

	void Start()
	{
		if (cameraShake == null)
		{
			Debug.LogWarning("No camera shake referenced in GameMaster");
		}
	}

	public IEnumerator _RespawnPlayer () {
		GetComponent<AudioSource>().Play ();
		yield return new WaitForSeconds (spawnDelay);

		Instantiate (playerPrefab, spawnPoint.position, spawnPoint.rotation);
		GameObject clone = Instantiate (spawnPrefab, spawnPoint.position, spawnPoint.rotation) as GameObject;
		Destroy (clone, 3f);
	}



	public IEnumerator _RespawnEnemy () {
		//GetComponent<AudioSource>().Play ();
		yield return new WaitForSeconds (spawnDelay);
		Instantiate (enemyPrefab, enemySpawnPoint.position, enemySpawnPoint.rotation);
		GameObject clone = Instantiate (enemyPrefab, enemySpawnPoint.position, enemySpawnPoint.rotation) as GameObject;
		Destroy (clone, 3f);
	}




	public static void KillPlayer (Player player) {
		Destroy (player.gameObject);
		gm.StartCoroutine(gm._RespawnPlayer());
	}

	public static void KillEnemy (Enemy enemy) {
		gm._KillEnemy(enemy);
		gm.StartCoroutine (gm._RespawnEnemy ());

	}
	public void _KillEnemy(Enemy _enemy)
	{
		GameObject _clone = Instantiate(_enemy.deathParticles, _enemy.transform.position, Quaternion.identity) as GameObject;
		Destroy(_clone, 5f);
	//	cameraShake.Shake(_enemy.shakeAmt, _enemy.shakeLength);
		Destroy(_enemy.gameObject);
	}

}
