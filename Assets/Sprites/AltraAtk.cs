using UnityEngine;
using System.Collections;

public class AltraAtk : ITrigger {

	public float percent;
	public float durTime;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public override void OnTrigger (GameObject gobjTarget)
	{
		Hero hero = gobjTarget.GetComponent<Hero>();
		Buff_SuperAtk bsa = gobjTarget.AddComponent<Buff_SuperAtk>();
		bsa.target = hero;
		bsa.durTime = durTime;
		bsa.percent = percent;
		GameView gameView = GameManager.FindCPU();
		gameView.UIShowTip("攻击力提升" + (percent * 100) + "%!");
	}
}
