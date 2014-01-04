using UnityEngine;
using System.Collections;

public class Trap : ITrigger {

	public int damage;
	
	public override void OnTrigger (GameObject gobjTarget)
	{
		Hero hero = gobjTarget.GetComponent<Hero>();
		hero.Hurted(damage);
	}
}
