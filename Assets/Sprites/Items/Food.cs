using UnityEngine;
using System.Collections;

/// <summary>
/// 可以恢复生命的道具
/// 恢复生命
/// 碰到后消失
/// </summary>
public class Food : ITrigger {

	public int hpRecover;
	
	public override void OnTrigger (GameObject gobjTarget)
	{
		Hero hero = gobjTarget.GetComponent<Hero>();
		hero.RecoverHp(hpRecover);
		DestroyObject(gameObject);
	}
}
