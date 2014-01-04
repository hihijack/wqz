using UnityEngine;
using System.Collections;

public enum ArmamentType{
	Helm,
	Shoulder,
	Breastplate,
	HandGuard,
	LegGuard
}

public class AvtarTest : MonoBehaviour {

	public Material body;
	
	public Texture2D t2dChestUpper;
	public Texture2D t2dCheckLower;
	public Texture2D t2dArmUpper;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.A)){	
			
		}
	}
	
	void ChangeArmament(){
		
	}
}
