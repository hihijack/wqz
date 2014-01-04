using UnityEngine;
using System.Collections;
using System.ComponentModel;
using System;

public enum ECommandAI{
	COMMAND_NONE,
	COMMAND_TARGET_PLAYER,
	COMMAND_IDLE,
	COMMAND_ATTACK_PLAYER
}

public enum EActorState{
	STATE_MOVE,
	STATE_IDLE,
	STATE_ATTACK,
    STATE_ATTACKINTERVAL
}

public enum EFSMAction {
    NONE,
	NPC_ATTACK,
	NPC_DIE,
	HERO_IDLE,
	HERO_RUN,
	HERO_ATTACK,
	HERO_DIE
}

public enum ERuneStoneType{
	NONE,
	JUMP
}

public enum EActorType{
	Enermy,
	Hero
}