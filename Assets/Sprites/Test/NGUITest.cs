using UnityEngine;
using System.Collections;

public class NGUITest : MonoBehaviour {

	public GameObject gobjGrid;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			TestNGUI();
		}
	}
	
	public void TestNGUI(){
		GameObject gobjIcon = Tools.AddNguiChild(gobjGrid, IPath.UI + "buff_icon");
//		UISprite icon = gobjIcon.GetComponent<UISprite>();
//		icon.spriteName = "skill_battle_roar";
//		UIGrid ug = gobjGrid.GetComponent<UIGrid>();
//		ug.Reposition();
	}
}
