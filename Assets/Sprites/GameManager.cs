using UnityEngine;
using System.Collections;

public static class GameManager{
	public static int cash;
	public static int score;
	
	public static GameView FindCPU(){
		return UnityEngine.GameObject.FindGameObjectWithTag("CPU").GetComponent<GameView>();
	}
}
