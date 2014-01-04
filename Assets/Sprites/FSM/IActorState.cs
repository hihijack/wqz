using UnityEngine;
using System.Collections;

public class IActorState{
    public IActor actor;
    public float time;
    public IActorState() {}
    public virtual IActorState toNextState(EFSMAction action) { return null; }
    public virtual void OnEnter() { }
    public virtual void DoUpdata() { }
}

#region Hero State
public class HeroActorState_Idle : IActorState{
	public HeroActorState_Idle(IActor actor){
		this.actor = actor;
	}
	
	public override IActorState toNextState (EFSMAction action)
	{
		IActorState result = null;
		if(action == EFSMAction.HERO_RUN){
			result = new HeroActorState_Run(actor);
		}
		return result;
	}
	
	public override void DoUpdata ()
	{
		actor.DoUpdateIdle();
	}
	
	public override void OnEnter ()
	{
		actor.OnEnterIdle();
	}
}

public class HeroActorState_Run : IActorState{
	public HeroActorState_Run(IActor actor){
		this.actor = actor;
	}
	
	public override IActorState toNextState (EFSMAction action)
	{
		IActorState result = null;
		if(action == EFSMAction.HERO_IDLE){
			result = new HeroActorState_Idle(actor);
		}else if(action == EFSMAction.HERO_ATTACK){
			result = new HeroActorState_Attack(actor);
		}
		return result;
	}
	
	public override void DoUpdata ()
	{
		actor.DoUpdateRun();
	}
	
	public override void OnEnter ()
	{
		actor.OnEnterRun();
	}
}

public class HeroActorState_Attack : IActorState{
	public HeroActorState_Attack(IActor actor){
		this.actor = actor;
	}
	
	public override IActorState toNextState (EFSMAction action)
	{
		IActorState result = null;
		if(action == EFSMAction.HERO_IDLE){
			result = new HeroActorState_Idle(actor);
		}else if(action == EFSMAction.HERO_DIE){
			result = new HeroActorState_Dead(actor);
		}else if(action == EFSMAction.HERO_RUN){
			result = new HeroActorState_Run(actor);
		}
		return result;
	}
	
	public override void DoUpdata ()
	{
		actor.DoUpdateHeroAttack();
	}
	
	public override void OnEnter ()
	{
		actor.OnEnterHeroAttack();
	}
}

public class HeroActorState_Dead : IActorState{
	public HeroActorState_Dead(IActor actor){
		this.actor = actor;
	}
	
	public override IActorState toNextState (EFSMAction action)
	{
		IActorState result = null;
		return result;
	}
	
	public override void OnEnter ()
	{
		actor.OnEnterHeroDead();
	}
	
	public override void DoUpdata ()
	{
		actor.DoUpdateHeroDead();
	}
}
#endregion

#region NPC state
public class NPCActorState_Idle : IActorState{
	public NPCActorState_Idle(IActor actor){
		this.actor = actor;
	}
	
	public override IActorState toNextState (EFSMAction action)
	{
		IActorState result = null;
		if(action == EFSMAction.NPC_ATTACK){
			result = new NPCActorState_Attack(actor);
		}
		return result;
	}
	
	public override void DoUpdata ()
	{
		actor.DoUpdateNPCIdle();
	}
	
	public override void OnEnter ()
	{
		actor.OnEnterNPCIdle();
	}
}

public class NPCActorState_Attack : IActorState{
	public NPCActorState_Attack(IActor actor){
		this.actor = actor;
	}
	
	public override IActorState toNextState (EFSMAction action)
	{
		IActorState result = null;
		if(action == EFSMAction.NPC_DIE){
			result = new NPCActorState_Dead(actor);
		}
		return result;
	}
	
	public override void DoUpdata ()
	{
		actor.DoUpdateNPCAttack();
	}
	
	public override void OnEnter ()
	{
		actor.OnEnterNPCAttack();
	}
}

public class NPCActorState_Dead : IActorState{
	public NPCActorState_Dead(IActor actor){
		this.actor = actor;
	}
	
	public override IActorState toNextState (EFSMAction action)
	{
		IActorState result = null;
		
		return result;
	}
	
	public override void OnEnter ()
	{
		actor.OnEnterNPCDead();
	}
	
	public override void DoUpdata ()
	{
		actor.DoUpdateHeroDead();
	}
}
#endregion

