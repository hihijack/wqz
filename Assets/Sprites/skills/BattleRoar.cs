using UnityEngine;
using System.Collections;

/// <summary>
/// 提升百分比护甲，持续一段时间
/// </summary>
public class BattleRoar : ISkill{
	
	public BattleRoar(){
		SetBaseData(GameDates.GetSkillBD((int)ESkill.BattleRoar));
	}
	
	public BattleRoarBD GetBaseData(){
		return base.GetBaseData() as BattleRoarBD;
	}
	
	public override void SetCaster(IActor _caster){
		caster = _caster;
	}
	
	public override IEnumerator Act ()
	{
		if(!InCD && StartCost()){
			StartCD();
			
			caster.IsSkilling = true;
			//caster.PlayAnim("Attack2HA");
			
			// 施法前摇
			//yield return new WaitForSeconds(0.5f);
			
			Buff_SuperArm bsa = caster.gameObject.AddComponent<Buff_SuperArm>();
			bsa.target = caster;
			bsa.durTime = GetBaseData().dur;
			bsa.percent = GetBaseData().rate;
			
			// 施法后摇
			//yield return new WaitForSeconds(0.5f);
			caster.IsSkilling = false;
			yield return 0;
		}
	}
}