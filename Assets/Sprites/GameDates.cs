using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

#region SkillBaseData
public enum ESkill{
	MortalStrike = 1,
	BattleRoar = 2
}

public class SkillBD{
	public int id;
	public string name;
	public string iconName;
	public int cd;
	public int cost;
	public int learnLevel;
	
	public SkillBD(JSONNode data){
		id = data["id"].AsInt;
		name = data["name"];
		iconName = data["icon"];
		cd = data["cd"].AsInt;
		cost = data["cost"].AsInt;
		learnLevel = data["learnlevel"].AsInt;
	}
}

public class MortalStrikeBD : SkillBD{
	public int engget;
	public float damageRate;
	
	public MortalStrikeBD(JSONNode data) : base(data){
		engget = data["engget"].AsInt;
		damageRate = data["damage"].AsFloat;
	}
}

public class BattleRoarBD : SkillBD{
	public float rate;
	public int dur;
	
	public BattleRoarBD(JSONNode data) : base(data){
		rate = data["rate"].AsFloat;
		dur = data["dur"].AsInt;
	}
}

#endregion

public static class GameDates{
	// key : id
	public static Dictionary<int, SkillBD> dicSkillsBD = new Dictionary<int, SkillBD>(50);
	
	public static void AddSkillBD(SkillBD bd){
		if(!dicSkillsBD.ContainsKey(bd.id)){
			dicSkillsBD.Add(bd.id, bd);
		}
	}
	
	public static SkillBD GetSkillBD(int id){
		SkillBD bd = null;
		if(dicSkillsBD.ContainsKey(id)){
			bd = dicSkillsBD[id];
		}
		return bd;
	}
	
	public static void InitSkillsBD(){
		JSONNode jdNodes = JSONNode.Parse((Resources.Load("GameData/Skills",typeof(TextAsset)) as TextAsset).ToString());
		for (int i = 0; i < jdNodes.Count; i++) {
			JSONNode jd = jdNodes[i];
			ESkill skillType = (ESkill)jd["id"].AsInt;
			SkillBD skillBd = null;
			switch (skillType) {
			case ESkill.MortalStrike:{
				// 致死打击
				skillBd = new MortalStrikeBD(jd);
			}
				break;
			case ESkill.BattleRoar:{
				// 战斗怒吼
				skillBd = new BattleRoarBD(jd);
			}
				break;
			default:
			break;
			}
			if(skillBd != null){
				AddSkillBD(skillBd);
			}
		}
	}
}
