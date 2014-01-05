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
	private Vector3 offset;
	// Use this for initialization
	void Start () {
		gameView = GameObject.Find("CPU").GetComponent<GameView>();
		offset = transform.position - target.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		pos.z = target.transform.position.z + offset.z;
		pos.x = target.transform.position.x + offset.x;
		transform.position = pos;
	}
}
