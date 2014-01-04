using UnityEngine;
using System.Collections;

public class Enermy : IActor {

	public bool isDead = false;
	
	public Hero curTarget;
	
	void Start () {
		
		base.Start();
		
		isHero = false;
		
		state = new NPCActorState_Idle(this);
		
		StartCoroutine(CoUpdateState());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	IEnumerator CoUpdateState(){
		while(true){
			this.state.DoUpdata();
			yield return new WaitForSeconds(0.5f);
		}
	}
	
	public override void OnEnterNPCIdle ()
	{
		
	}
	
	public override void DoUpdateNPCIdle ()
	{
		
	}
	
	public override void OnEnterNPCAttack ()
	{
		StartCoroutine(CoStartAttackCurTarget());
	}
	
	public override void DoUpdateNPCAttack ()
	{
		
	}
	
	public override void OnEnterNPCDead ()
	{
		isDead = true;
		DestroyObject(gameObject);
		gameView.HideTargetUI();
		gameView.strGUITip +=  "消灭了" + actorName  + "\n";
	}
	
	public override void DoUpdateNPCDead ()
	{
		
	}
	
		// 不停自动攻击目标，直到目标死亡
	IEnumerator CoStartAttackCurTarget(){
		while(!curTarget.isDead){
			// 攻击前摇
			yield return new WaitForSeconds(this.atkAnimTimeBefore);
			
			if(isDead){
				break;
			}
			
			// 攻击伤害
			DamageTarget(GetAtk(), curTarget);
			
			if(curTarget.hpCur <= 0){
				curTarget.hpCur = 0;
				curTarget.isDead = true;
			}
			
			gameView.UpdateUIHeroHp(curTarget);
			
			// 攻击后摇
			yield return new WaitForSeconds(atkAnimTimeAfter);
			
			// 攻击间隔
			yield return new WaitForSeconds(atkTimeInterval);
		}
	}
}
