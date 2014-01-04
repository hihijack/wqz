using UnityEngine;
using System.Collections;

/// <summary>
/// 提升百分比护甲，持续一段时间
/// </summary>
public class Buff_SuperArm : IBaseBuff{
	public float percent;
	
	void Start () {
		buffTxuName = "skill_battle_roar";
		
		OnAdd();
		
		base.Start();
	}
	
	public override void OnAdd ()
	{
		base.OnAdd();
		target.ArmExtra += (int)(percent * target.arm);
	}
	
	public override void OnRemove ()
	{
		base.OnRemove();
		target.ArmExtra -= (int)(percent * target.arm);
	}
	
}
