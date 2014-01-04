using UnityEngine;
using System.Collections;

public class IActor : MonoBehaviour{
	public int id;
	public EActorType actor_type;
	public IActorState state;
	public IActorAction action;
	
	public int hpCur;
	public int hpMax;
	public int level;
	public int atk;
	public int arm;
	
	int _atkExtra;
	public int AtkExtra{
		set{
			_atkExtra = value;
		}
		get{
			return _atkExtra;
		}
	}
	
	int _armExtra;
	public int ArmExtra{
		set{
			_armExtra = value;
		}
		
		get{
			return _armExtra;
		}
	}
	
	
	public float atkAnimTimeBefore; // 攻击前摇时间
	public float atkAnimTimeAfter; //  攻击后摇时间
	public float atkTimeInterval; // 攻击间隔时间
	public int deadlyStrike;	// 致命一击几率 x%
	protected float deadlyStrikeDamage = 2f; //致命一击倍率
	
	public string actorName;
	
	protected bool _isSkilling = false;
	
	public GameView gameView;
	
	public bool isHero;
	
	public bool IsSkilling{
		get{
			return _isSkilling;
		}
		set{
			_isSkilling = value;
		}
	}
	
	public void Start(){
		gameView = GameObject.Find("CPU").GetComponent<GameView>();
	}
	
	/// <summary>
	/// Sets the position. lower left
	/// </summary>
	/// <param name='x'>
	/// X.
	/// </param>
	/// <param name='y'>
	/// Y.
	/// </param>

    public void updataState(IActorAction action) {
        if(action.actiontype != EFSMAction.NONE){
//            Debug.Log("updataState - " + this.state + " by action:" + action.actiontype);//########
            IActorState asCur = this.state;
            IActorState asNext = asCur.toNextState(action.actiontype);
            if (asNext != null)
            {
                this.state = asNext;
				this.action = action;
                this.state.OnEnter();
            }
        }
    }
	
	public void updataState(EFSMAction eAction){
		IActorAction action = new IActorAction(eAction);
		updataState(action);
	}

    public bool IsInState(System.Type type) {
        return state.GetType() == type;
    }
	
	public int GetAtk(){
		return atk + _atkExtra;
	}
	
	public int GetArm(){
		return arm + _armExtra;
	}
	
	
	public int GetDamgerToOther(int damageOri, IActor other){
		int damage = 0;
		int atk = damageOri;
		int armOther = other.GetArm();
		
		if(armOther >= 0){
			int damgeOffset = (int)( atk * (armOther * 0.06 / ( armOther * 0.06 + 1 )));
			damage = atk - damgeOffset;
		}else{
			int N = Mathf.Abs(armOther);
			float damgeAddPercent = 2 - Mathf.Pow(0.94f, N);
			damage = (int)(atk * damgeAddPercent);
		}
		return damage;
	}
	
	
	public void DamageTarget(int damageOri, IActor target){
		int damage = GetDamgerToOther(damageOri,target);
		
		// 致命一击
		if(Tools.IsHitOdds(deadlyStrike)){
			damage = (int)(damage * deadlyStrikeDamage);
		}
		
		target.hpCur -= damage;
		if(target.hpCur <= 0){
			target.hpCur = 0;
		}
		if(isHero){
			gameView.SetEnermyHPUI(target as Enermy);
			gameView.UIShowDamage(damage);
		}else{
			gameView.UpdateUIHeroHp(target as Hero);
			gameView.UIShowHurt(damage);
		}
	}
	
	
	public void Hurted(int damageOri){
		int damage = GetDamgerToOther(damageOri,this);
		
		hpCur -= damage;
		if(hpCur <= 0){
			hpCur = 0;
		}
		if(isHero){
			gameView.UpdateUIHeroHp(this as Hero);
			gameView.UIShowHurt(damage);
		}
	}
	
	public Vector3 GetPos(){
		return gameObject.transform.position;
	}
	
	public void PlayAnim(string animname){
		Animation anim = Tools.GetComponentInChildByPath<Animation>(gameObject, "model");
		anim.CrossFade(animname);
	}
	
	#region Hero
	public virtual void OnEnterIdle(){}
	public virtual void DoUpdateIdle() {}
	public virtual void DoUpdateRun(){}
	public virtual void OnEnterRun(){}
	public virtual void OnEnterHeroAttack(){}
	public virtual void DoUpdateHeroAttack(){}
	public virtual void OnEnterHeroDead(){}
	public virtual void DoUpdateHeroDead(){}
	#endregion
	
	#region NPC
	public virtual void OnEnterNPCIdle(){}
	public virtual void DoUpdateNPCIdle() {}
	public virtual void OnEnterNPCAttack(){}
	public virtual void DoUpdateNPCAttack(){}
	public virtual void OnEnterNPCDead(){}
	public virtual void DoUpdateNPCDead(){}
	#endregion
	
	#region Skill
	#endregion
}
