using UnityEngine;
using System.Collections;
using System.Reflection;

public class Hero : IActor
{
	
	public float speed = 10.0f;
	
	private GameObject g_gobjCurStepOn;
	
	private Vector2 moveDir = Vector2.zero;
	
	int axisH = 0;
	int axisV = 0;
	int btnA = 0;
	int btnB = 0;
	
	private int g_Clock_Times = 0;
	
	Transform tf;
	
	CharacterController cc;
	
	
	public int expCur;
	public int expMax;
	public int cash;
	public int score;
	
	public Enermy curTarget; // 当前攻击目标
	
	// 人物属性
	public int eng; //能量
	private int engMax = 10;
	
	
	private ESkill _skillIdA;
	private ESkill _skillIDB;
	private ISkill _skillA;
	private ISkill _skillB;
	
	public ESkill SkillIDA{
		get{
			return _skillIdA;
		}
		set{
			_skillIdA = value;
			_skillA = CreateSkillById(_skillIdA);
		}
	}
	
	public ESkill SkillIDB{
		get{
			return _skillIDB;
		}
		set{
			_skillIDB = value;	
			_skillB = CreateSkillById(_skillIDB);
		}
	}
	
	public ISkill SkillA{
		get{
			return _skillA;
		}
	}
	
	public ISkill SkillB{
		get{
			return _skillB;
		}
	}
	
	public bool isInBattleState = false;
	
	public bool isDead = false;
	
	// buff start
	// buff end
	
	void Start(){
		base.Start();
		
		isHero = true;
		
		cc = GetComponent<CharacterController>();
		actor_type = EActorType.Hero;
		state = new HeroActorState_Idle(this);
		tf = transform;
	}
	
	void Update(){
		btnA = gameView.VCInput_BtnA;
		btnB = gameView.VCInput_BtnB;
		axisH = gameView.VCInput_Axis;
		moveDir.x = 0f;
		this.state.DoUpdata();
		gameView.VCInput_BtnA = 0;
		gameView.VCInput_BtnB = 0;
	}
	
	void OnGUI(){
		
	}
	
	#region FSM
	public override void OnEnterIdle ()
	{
	}
	
	public override void DoUpdateIdle ()
	{
		updataState(new IActorAction(EFSMAction.HERO_RUN));
	}
	
	public override void DoUpdateRun ()
	{
		
		Vector3 move = new Vector3(axisH * speed * Time.deltaTime, 0f, speed * Time.deltaTime);
		cc.Move(move);
		
		Vector3 pos = tf.position;
		
		if(pos.x > 3){
			pos.x = 3;
		}
		if(pos.x < -3){
			pos.x = -3;
		}
		tf.position = pos;
		
		if(btnA > 0 && SkillA != null){
			SkillA.SetCaster(this);
			SkillA.SetTarget(curTarget);
			StartCoroutine(SkillA.Act());
		}
		else if(btnB > 0 && SkillB != null){
			SkillB.SetCaster(this);
			SkillB.SetTarget(curTarget);
			StartCoroutine(SkillB.Act());
		}
	}
	
	
	
	public override void OnEnterRun (){
		PlayAnim("Run");
	}
	
	public override void OnEnterHeroAttack ()
	{
		StartCoroutine(CoStartAttackCurTarget());
	}
	
	public override void DoUpdateHeroAttack ()
	{
		if(btnA > 0 && SkillA != null){
			SkillA.SetCaster(this);
			SkillA.SetTarget(curTarget);
			StartCoroutine(SkillA.Act());
		}
		else if(btnB > 0 && SkillB != null){
			SkillB.SetCaster(this);
			SkillB.SetTarget(curTarget);
			StartCoroutine(SkillB.Act());
		}
	}
	#endregion
	
	
	public void SetFace(bool isRigth){
		
	}
	
	public bool IsHitSomeThing(){
		return false;
	}
	
	#region Game Mehtods
	public ISkill CreateSkillById(ESkill skillId){
		ISkill r = null;
		switch (skillId) {
		case ESkill.MortalStrike:{
//			r = new MortalStrike();
			r = gameObject.AddComponent<MortalStrike>();
		}
			break;
		case ESkill.BattleRoar:{
//			r = new BattleRoar();
			r = gameObject.AddComponent<BattleRoar>();
		}
			break;
		default:
		break;
		}
		return r;
	}
	#endregion
	
	#region InteractiveEventHandle
	#endregion
	
	void OnControllerColliderHit(ControllerColliderHit hit) {
		if(hit.moveDirection.y < 0 && Mathf.Abs(hit.moveDirection.x) < 0.3f ){
			g_gobjCurStepOn = hit.gameObject;
		}
		
		// killed when hit enermy
		if(hit.collider.CompareTag("enermy")){
			isInBattleState = true;
			Enermy enermy = hit.collider.gameObject.GetComponent<Enermy>();
			curTarget = enermy;
			updataState(new IActorAction(EFSMAction.HERO_ATTACK));
			gameView.ShowTrargetUI();
			
			enermy.curTarget = this;
			enermy.updataState(new IActorAction(EFSMAction.NPC_ATTACK));
		}
	}
	
	void Killed(){
		
	}
	
	
	// 不停自动攻击目标，直到目标死亡
	IEnumerator CoStartAttackCurTarget(){
		while(!curTarget.isDead){
			if(IsSkilling){
				yield return 1;
				continue;
			}
			PlayAnim("Attack2HA");
			// 攻击前摇
			yield return new WaitForSeconds(atkAnimTimeBefore);
			
			if(IsSkilling){
				yield return 1;
				continue;
			}
			
			if(isDead){
				break;
			}
			
			// 攻击伤害
			DamageTarget(GetAtk(), curTarget);
			
			if(curTarget.hpCur <= 0){
				curTarget.isDead = true;
				curTarget.updataState(EFSMAction.NPC_DIE);
				updataState(EFSMAction.HERO_RUN);
			}
			gameView.SetEnermyHPUI(curTarget);
		
			// 攻击后摇
			yield return new WaitForSeconds(atkAnimTimeAfter);
			
			if(IsSkilling){
				yield return 1;
				continue;
			}
			
			// 攻击间隔
			yield return new WaitForSeconds(atkTimeInterval);
		}
	}	 
	
	void OnTriggerEnter(Collider other) {
		ITrigger it = other.GetComponent<ITrigger>();
		if(it != null){
			it.OnTrigger(gameObject);
		}
	}
	
	public void RecoverHp(int hp){
		hpCur += hp;
		if(hpCur > hpMax){
			hpCur = hpMax;
		}
		gameView.UpdateUIHeroHp(this);
		gameView.UIShowHeal(hp);
	}
	
	public void AddEng(int engAdd){
		eng += engAdd;
		if(eng > engMax){
			eng = engMax;
		}
		gameView.UpdateUIHeroEng(this);
	}
	
	public void ReduceEng(int engReduce){
		eng -= engReduce;
		if(eng < 0){
			eng = 0;
		}
		gameView.UpdateUIHeroEng(this);
	}
	
	#region buff
	#endregion
	
}
