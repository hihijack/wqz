using UnityEngine;
using System.Collections;

public class MortalStrike : ISkill{
	
	
	private IActor target;
	
	public MortalStrike(){
		SetBaseData(GameDates.GetSkillBD((int)ESkill.MortalStrike));
	}
	
	public override void SetCaster(IActor _caster){
		caster = _caster;
	}
	
	public override void SetTarget(IActor _target){
		target = _target;
	}
	
	public MortalStrikeBD GetBaseData(){
		return base.GetBaseData() as MortalStrikeBD;
	}
	
	public override IEnumerator Act ()
	{
		if(!InCD && target != null && StartCost()){
			StartCD();
			
			Hero hero = caster as Hero;
			hero.IsSkilling = true;
			hero.PlayAnim("Attack2HA");
			
			// 施法前摇
			yield return new WaitForSeconds(0.5f);
			
			hero.AddEng(GetBaseData().engget);
			int damage = (int)(hero.GetAtk() * GetBaseData().damageRate);
			hero.DamageTarget(damage, target);
			
			// 施法后摇
			yield return new WaitForSeconds(0.5f);
			
			hero.IsSkilling = false;
		}
	}
}
