using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public enum EGameState{
	Running,
	UIInfoing,
	UIPaging
}

public class GameView : MonoBehaviour
{
	
	public int VCInput_Axis;
	public int VCInput_Ver_Axis;
	public int VCInput_BtnA;
	public int VCInput_BtnB;
	
	public EGameState gameState;
	
	public Camera mCameraMain;
	public Camera mCamera2D;
	
	private Hero hero;
	
	public GameObject mGobjBottom;
	public GameObject mGobjTR;
	public GameObject mGobjTL;
	public GameObject mGobjC;
	public GameObject mGobjBL;
	public GameObject mGobjBR;
	
	private GameObject mGobjTargetUI;
	
	private GameObject mGobjBuffs;
	
	void Start ()
	{
		GameDates.InitSkillsBD();
		
		InitHero();
		mGobjTargetUI = Tools.GetGameObjectInChildByPathSimple(mGobjTL, "head_target");
		mGobjBuffs = Tools.GetGameObjectInChildByPathSimple(mGobjTL, "head/buffs");
	}
	
	// Update is called once per frame
	void Update ()
	{
		// keyboard controll
		/// for test.when build， close it
		#if UNITY_EDITOR||UNITY_STANDALONE_WIN
		if(Input.GetKey(KeyCode.LeftArrow)){
			VCInput_Axis = -1;
		}else if(Input.GetKey(KeyCode.RightArrow)){
			VCInput_Axis = 1;
		}else{
			VCInput_Axis = 0;
		}
		
		if(Input.GetKey(KeyCode.UpArrow)){
			VCInput_Ver_Axis = 1;
		}else if(Input.GetKey(KeyCode.DownArrow)){
			VCInput_Ver_Axis = -1;
		}else{
			VCInput_Ver_Axis = 0;
		}
		
		if(Input.GetKeyDown(KeyCode.Z)){
			VCInput_BtnA = 1;
		}else if(Input.GetKeyUp(KeyCode.Z)){
			VCInput_BtnA = 0;
		}
		
		if(Input.GetKeyDown(KeyCode.X)){
			VCInput_BtnB = 1;
		}else if(Input.GetKeyUp(KeyCode.X)){
			VCInput_BtnB = 0;
		}
		#endif
		
	}
	
	public void ShowTrargetUI(){
		if(hero.curTarget != null){
			Enermy target = hero.curTarget;
			mGobjTargetUI.SetActive(true);
			// level
			UILabel txtLevel = Tools.GetComponentInChildByPath<UILabel>(mGobjTargetUI, "level");
			txtLevel.text = "LV" + target.level;
			// hp
			SetEnermyHPUI(target);
		}
	}
	
	public void SetEnermyHPUI(Enermy enermy){
		UILabel txtHp = Tools.GetComponentInChildByPath<UILabel>(mGobjTargetUI, "hp/txt");
		txtHp.text = enermy.hpCur + "/" + enermy.hpMax;
		UISlider sliderHp = Tools.GetComponentInChildByPath<UISlider>(mGobjTargetUI, "hp");
		sliderHp.value = (float)enermy.hpCur / enermy.hpMax;
	}
	
	public void HideTargetUI(){
		GameObject gobjUITarget = Tools.GetGameObjectInChildByPathSimple(mGobjTL, "head_target");
		gobjUITarget.SetActive(false);
	}
	
	void InitHero(){
		GameObject gobjHero = GameObject.Find("hero");
		hero = gobjHero.GetComponent<Hero>();
		hero.expCur = 0;
		hero.expMax = 100;
		hero.cash = 0;
		hero.score = 0;
		hero.eng = 0;
		hero.SkillIDA = ESkill.MortalStrike;
		hero.SkillIDB = ESkill.BattleRoar;
		hero.deadlyStrike = 50;
		// level
		UILabel txtLevl = Tools.GetComponentInChildByPath<UILabel>(mGobjTL, "head/level");
		txtLevl.text = "LV" + hero.level;
		// hp
		UpdateUIHeroHp(hero);
		// exp
		UISlider sliderExp = Tools.GetComponentInChildByPath<UISlider>(mGobjBottom, "exp/progbar");
		sliderExp.sliderValue = (float)hero.expCur / hero.expMax;
		// cash
		UILabel txtCash = Tools.GetComponentInChildByPath<UILabel>(mGobjTR, "cash/txt");
		txtCash.text = hero.cash.ToString();
		// score
		UILabel txtScore = Tools.GetComponentInChildByPath<UILabel>(mGobjTR, "score/txt");
		txtScore.text = hero.score.ToString();
		// eng
		UpdateUIHeroEng(hero);
		
		// skill
		UpdateUIHeroSkillIcon(hero);
	}
	
	public string strGUITip = "";
	void OnGUI(){
		//GUI.TextArea(new Rect(300, 100, 300, 100), strGUITip);
	}
	
	public void UpdateUIHeroHp(Hero hero){
		UILabel txtHp = Tools.GetComponentInChildByPath<UILabel>(mGobjTL, "head/hp/txt");
		txtHp.text = hero.hpCur + "/" + hero.hpMax;
		UISlider sliderHp = Tools.GetComponentInChildByPath<UISlider>(mGobjTL, "head/hp");
		sliderHp.value = (float)hero.hpCur / hero.hpMax;
	}
	
	public void UpdateUIHeroEng(Hero hero){
		UILabel txtEng = Tools.GetComponentInChildByPath<UILabel>(mGobjTL, "head/eng/txt_num");
		txtEng.text = hero.eng.ToString();
	}
	
	public void UpdateUIHeroSkillIcon(Hero hero){
		string iconNameA = "skill_none";
		string iconNameB = "skill_none";
		
		ISkill skillA = hero.SkillA;
		ISkill skillB = hero.SkillB;
		
		if(skillA != null){
			iconNameA = skillA.GetBaseData().iconName;
		}
		if(skillB != null){
			iconNameB = skillB.GetBaseData().iconName;
		}
		
		GameObject gobjIconA = Tools.GetGameObjectInChildByPathSimple(mGobjBR, "btn_A");
		UISprite iconSkillA = Tools.GetComponentInChildByPath<UISprite>(mGobjBR, "btn_A/Background");
		iconSkillA.spriteName = iconNameA;
		skillA.GobjIcon = gobjIconA;
		
		GameObject gobjIconB = Tools.GetGameObjectInChildByPathSimple(mGobjBR, "btn_B");
		UISprite iconSkillB = Tools.GetComponentInChildByPath<UISprite>(mGobjBR, "btn_B/Background");
		iconSkillB.spriteName = iconNameB;
		skillB.GobjIcon = gobjIconB;
	}
	
	//×××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××××
	
	public void OnBtnPress(string btnname, bool isDown){
		if("btn_down".Equals(btnname)){
			if(isDown){
				VCInput_Ver_Axis = -1;
			}else{
				VCInput_Ver_Axis = 0;
			}
		}
		if("btn_up".Equals(btnname)){
			if(isDown){
				VCInput_Ver_Axis = 1;
			}else{
				VCInput_Ver_Axis = 0;
			}
		}
		if("btn_left".Equals(btnname)){
			if(isDown){
				VCInput_Axis = -1;
			}else{
				VCInput_Axis = 0;
			}
		}
		if("btn_right".Equals(btnname)){
			if(isDown){
				VCInput_Axis = 1;
			}else{
				VCInput_Axis = 0;
			}
		}
		
		if("btn_A".Equals(btnname)){
			if(isDown){
				VCInput_BtnA = 1;
			}else{
				VCInput_BtnA = 0;
			}
		}
		if("btn_B".Equals(btnname)){
			if(isDown){
				VCInput_BtnB = 1;
			}else{
				VCInput_BtnB = 0;
			}
		}
	}
	
	public bool IsInGameState(EGameState gameState){
		return this.gameState == gameState;
	}
	
	public void UIShowDamage(int damage){
		GameObject gobjTxt = Tools.AddNguiChild(mGobjC, IPath.UI + "txt_damage");
		gobjTxt.transform.localScale = new Vector3(1.5f, 1.5f, 0f);
		UILabel txt = gobjTxt.GetComponent<UILabel>();
		txt.text = damage.ToString();
		Vector3 posOri = Tools.WorldPos2NguiPos(hero.GetPos(), mCameraMain, mCamera2D);
		gobjTxt.transform.position = posOri;
		
		TweenPosition tp = gobjTxt.GetComponent<TweenPosition>();
		tp.onFinished.Add(new EventDelegate(gobjTxt.GetComponent<UIDestroyOnEnd>(), "OnAnimEnd"));
		float x = gobjTxt.transform.localPosition.x;
		tp.from.x = x;
		tp.to.x = x;
	}
	
	public void UIShowHurt(int damage){
		GameObject gobjTxt = Tools.AddNguiChild(mGobjC, IPath.UI + "txt_hurt");
		gobjTxt.transform.localScale = new Vector3(1.5f, 1.5f, 0f);
		UILabel txt = gobjTxt.GetComponent<UILabel>();
		txt.text = "-" + damage.ToString();
		Vector3 posOri = Tools.WorldPos2NguiPos(hero.GetPos(), mCameraMain, mCamera2D);
		gobjTxt.transform.position = posOri;
		
		TweenPosition tp = gobjTxt.GetComponent<TweenPosition>();
		tp.onFinished.Add(new EventDelegate(gobjTxt.GetComponent<UIDestroyOnEnd>(), "OnAnimEnd"));
		float x = gobjTxt.transform.localPosition.x;
		tp.from.x = x;
		tp.to.x = x;
	}
	
	public void UIShowHeal(int heal){
		GameObject gobjTxt = Tools.AddNguiChild(mGobjC, IPath.UI + "txt_heal");
		gobjTxt.transform.localScale = new Vector3(1.5f, 1.5f, 0f);
		UILabel txt = gobjTxt.GetComponent<UILabel>();
		txt.text = "+" + heal.ToString();
		Vector3 posOri = Tools.WorldPos2NguiPos(hero.GetPos(), mCameraMain, mCamera2D);
		gobjTxt.transform.position = posOri;
		
		TweenPosition tp = gobjTxt.GetComponent<TweenPosition>();
		tp.onFinished.Add(new EventDelegate(gobjTxt.GetComponent<UIDestroyOnEnd>(), "OnAnimEnd"));
		float x = gobjTxt.transform.localPosition.x;
		tp.from.x = x;
		tp.to.x = x;
	}
	
	public void UIShowTip(string strTxt){
		GameObject gobjTxt = Tools.AddNguiChild(mGobjC, IPath.UI + "txt_tip");
		UILabel txt = gobjTxt.GetComponent<UILabel>();
		txt.text = strTxt;
		
		TweenScale ts = gobjTxt.GetComponent<TweenScale>();
		UIDestroyOnEnd udoe = gobjTxt.GetComponent<UIDestroyOnEnd>();
		ts.onFinished.Add(new EventDelegate(udoe, "OnAnimEnd"));
	}
	
	public void UIAddBuff(IBaseBuff buff){
		GameObject gobjIcon = Tools.AddNguiChild(mGobjBuffs, IPath.UI + "buff_icon");
		UISprite icon = gobjIcon.GetComponent<UISprite>();
		icon.spriteName = buff.GetTxtName();
		buff.SetGobjIcon(gobjIcon);
		UIGrid ug = mGobjBuffs.GetComponent<UIGrid>();
		ug.Reposition();
	}
	
	public void UIRemoveBuff(IBaseBuff buff){
		if(buff.GetIconGobj() != null){
			DestroyObject(buff.GetIconGobj());
			UIGrid ug = mGobjBuffs.GetComponent<UIGrid>();
			ug.Reposition();
		}
	}
}