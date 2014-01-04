using UnityEngine;
using System.Collections;

public enum EViewType{
	A,
	B
}

public class CameraControll : MonoBehaviour {

	public GameObject target;
	GameView gameView;
	private float upSpeed = 5.0f;
	private float uplimitOffset = 4.0f;
	private float offsetZ;
	// Use this for initialization
	void Start () {
		gameView = GameObject.Find("CPU").GetComponent<GameView>();
		offsetZ = transform.position.z - target.transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		pos.z = target.transform.position.z + offsetZ;
		transform.position = pos;
	}
}
