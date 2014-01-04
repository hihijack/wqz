using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public static class Tools{

	public static GameObject LoadResourcesGameObject(string path){
		//Debug.Log("LoadResourceGameObject:"+path);
		UnityEngine.Object obj = Resources.Load(path);
		if(obj == null) return null;
		return path != null ? UnityEngine.Object.Instantiate(obj) as GameObject : null;
	}
	
	
	public static GameObject LoadResourcesPrefab(string path){
		return Resources.Load(path) as GameObject;
	}		
	public static GameObject LoadResourcesGameObject(string path, GameObject gobjParent, float x, float y, float z){
		GameObject gobj = null;
		gobj = LoadResourcesGameObject(path);
		if (gobj != null) {
			gobj.transform.parent = gobjParent.transform;
			gobj.transform.localPosition = new Vector3(x, y, z);
		}
		return gobj;
	}
	
	public static GameObject LoadResourcesGameObject(string path, GameObject gobjParent){
		GameObject gobj = null;
		gobj = LoadResourcesGameObject(path);
		if (gobj != null) {
			gobj.transform.parent = gobjParent.transform;
		}
		return gobj;
	}
	
//	public static string Md5Sum(string strToEncrypt)
//	{
//		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
//		byte[] bytes = ue.GetBytes(strToEncrypt);
//	 
//		// encrypt bytes
//		System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
//		byte[] hashBytes = md5.ComputeHash(bytes);
//	 
//		// Convert the encrypted bytes back to a string (base 16)
//		string hashString = "";
//	 
//		for (int i = 0; i < hashBytes.Length; i++)
//		{
//			hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
//		}
//	 
//		return hashString.PadLeft(32, '0');
//	}
	
	public static string GetGUID(){
       System.Guid myGUID = System.Guid.NewGuid();
	   //Debug.Log("myGUID:" + myGUID.ToString("N"));

       return myGUID.ToString("N");
    }
	
	public static string[] StringSplit(string txt,string character){
		return Regex.Split(txt,character,RegexOptions.IgnoreCase);
	}
	
	public static string FormatTime(int seconds){
		string strTime;
		if(seconds < 0){
			strTime = "00:00:00";
		}else{
			int intH = seconds / 3600;
			string strH = intH < 10 ? "0" + intH.ToString() : intH.ToString();
			int intM = (seconds % 3600) / 60;
			string strM = intM < 10 ? "0" + intM.ToString() : intM.ToString();
			int intS = seconds % 3600 % 60;
			string strS = intS < 10 ? "0" + intS.ToString() : intS.ToString();
			strTime = strH + ":" + strM + ":" + strS;
		}
		return strTime;
	}

	
	public static int MathfRound(float num){
		return (int)Mathf.Floor(num + 0.5f);
	}
	
	public static int CashToDiamon(int cash){
		int rate = 10;
		return (int)Mathf.Ceil(cash * 1.0f/ rate);
	}
	
	public static int GetMax(int[] nums){
		int r = 0;
		if (nums == null || nums.Length == 0){
			return r;
		}
		r = nums[0];
		foreach (int item in nums) {
			if (item > r) {
				r = item;
			}
		}
		return r;
	}
	
	public static T GetComponentInChildByPath<T> (GameObject gobjParent, string path) where T : Component{
		Component r = null;
		if (gobjParent == null){
			return null;
		}
//		Transform tf = GetTransformInChildByPath(gobjParent, path);
		Transform tf = GetTransformInChildByPathSimple(gobjParent, path);
		if (tf != null){
			r = tf.gameObject.GetComponent<T>();
		}else{
			Debug.Log("!!!Can't get transform by path: " + path + " in GetComponentInChildByPath");
		}
		return r as T;
	}
	
	// path , split by "/"
	public static Transform GetTransformInChildByPath(GameObject gobjParent, string path){
		Transform r = null;
		if (gobjParent == null){
			return r;
		}
		string[] strArr = path.Split('/');
		r = gobjParent.transform;
		foreach (string strPathItem in strArr) {
			if (r == null) {
				continue;
			}
			r = r.FindChild(strPathItem);
		}
		if (r == null) {
			Debug.Log("!!!Can't get transform by path : " + path +". in Tools.GetTransformInChildByPath");
		}
		return r;
	}
	
	public static Transform GetTransformInChildByPathSimple(GameObject gobjParent, string path){
		Transform r = null;
		if(gobjParent != null){
			r = gobjParent.transform.FindChild(path);
		}
		return r;
	}
	
	public static GameObject GetGameObjectInChildByPathSimple(GameObject gobjParent, string path){
		GameObject gobj = null;
		Transform tf = GetTransformInChildByPathSimple(gobjParent, path);
		if(tf != null){
			gobj = tf.gameObject;
		}
		return gobj;
	}
	
	public static int GetIntLength(int i){
		int length = 0;
		length = i.ToString().Length;
		return length;
	}
	
	public static void ChangeLayersRecursively(Transform trans, string name)
	{
		try {
			trans.gameObject.layer = LayerMask.NameToLayer(name);
			foreach (Transform child in trans)
		    {
		        child.gameObject.layer = LayerMask.NameToLayer(name);
		        ChangeLayersRecursively(child, name);
		    }
		} catch (Exception ex) {
			Debug.Log(ex.ToString());	
		}
	}
	
	public static void ChangeLayersRecursively(GameObject gobj, string name){
		try {
			
			gobj.layer = LayerMask.NameToLayer(name);
			foreach (Transform child in gobj.transform)
		    {
		        child.gameObject.layer = LayerMask.NameToLayer(name);
		        ChangeLayersRecursively(child, name);
		    }
		} catch (Exception ex) {
			Debug.Log(ex.ToString());	
		}
	}
	
	public static int FindIntValInArr(int[] arr, int ivalue){
		int r = -1;
		for (int i = 0; i < arr.Length; i++) {
			if (arr[i] == ivalue) {
				r = i;
				break;
			}
		}
		return r;
	}
	
	public static string FloatToPercent(float fVal){
		string per = "";
		per = Mathf.Round(fVal * 100).ToString() + "%";
		return per;
	}
	
	public static Hashtable Hash(params object[] args){
		Hashtable hashTable = new Hashtable(args.Length/2);
		if (args.Length %2 != 0) {
			return null;
		}else{
			int i = 0;
			while(i < args.Length - 1) {
				hashTable.Add(args[i], args[i+1]);
				i += 2;
			}
			return hashTable;
		}
	}
	
	public static void SetGameObjMaterial(GameObject gobj, string materialName){
		Material changMaterial = UnityEngine.Object.Instantiate(Resources.Load("Materials/"+ materialName)) as Material;
		if(changMaterial != null){
			gobj.renderer.material = changMaterial;
		}else{
			Debug.LogError("can't find material:" + materialName);
		}
	}
	
	public static GameObject AddNguiChild(GameObject gobjParent, string path){
		GameObject gobj = Resources.Load(path) as GameObject;
		return NGUITools.AddChild(gobjParent, gobj);
	}
	
	public static Vector3 WorldPos2NguiPos(Vector3 posWorld, Camera cameraWorld, Camera cameraNgui){
		Vector3 screenPos = cameraWorld.WorldToViewportPoint(posWorld);
		Vector3 posNgui = cameraNgui.ViewportToWorldPoint(screenPos);
		return posNgui;
	}
	
	/// <summary>
	/// 是否命中几率
	/// </summary>
	/// <returns>
	/// <c>true</c> if this instance is hit odds the specified odds; otherwise, <c>false</c>.
	/// </returns>
	/// <param name='odds'>
	/// If set to <c>true</c> odds.
	/// </param>
	public static bool IsHitOdds(int odds){
		bool r = false;
		if(odds >= 0 && odds <= 100){
			System.Random ran = new System.Random();
			int ranVal = ran.Next(1, 101);
			r = (odds >= ranVal);
		}else{
			Debug.LogError("error");
		}
		return r;
	}
}

public class BtnAction{
	public string btn_head{ get; set ;}
	public int state{ get; set ;}
	public string request{ get; set ;}
}