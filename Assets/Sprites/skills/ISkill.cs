using UnityEngine;
using System.Collections;

public class ISkill : MonoBehaviour{
	public IActor caster;
	
	private SkillBD baseData;
	
	public virtual IEnumerator Act(){yield return 0;}
	
	protected GameObject _gobjIcon;
	public GameObject GobjIcon{
		set{
			_gobjIcon = value;
		}
		get{
			return _gobjIcon;
		}
	}
	
	protected bool _inCD;
	public bool InCD{
		get{
			return _inCD;
		}
		
		set{
			_inCD = value;
		}
	}
	
	
	public void SetBaseData(SkillBD bd){
		baseData = bd;
	}
	
	public SkillBD GetBaseData(){
		return baseData;
	}
	
	public virtual void SetCaster(IActor caster){}
	public virtual void SetTarget(IActor target){}
	
	protected void StartCD(){
		InCD = true;
		StartCoroutine(CoCDTime());
	}
	
	IEnumerator CoCDTime(){
		yield return new WaitForSeconds(baseData.cd);
		InCD = false;
	}
	
	protected bool StartCost(){
		bool r = false;
		int cost = baseData.cost;
		if(caster.isHero){
			Hero hero = caster as Hero;
			if(hero.eng >= cost){
				r = true;
				hero.ReduceEng(cost);
			}
		}else{
			r = true;
		}
		return r;
	}
	
}
