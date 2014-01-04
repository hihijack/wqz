using UnityEngine;
using System.Collections;

public class IBaseBuff : MonoBehaviour {

	public IActor target;
	public float durTime;
	
	protected string buffTxuName;
	protected GameObject gobjIcon;
	
	public virtual void Start(){
		StartCoroutine(CoTime());
	}
	
	public virtual void OnAdd(){
		GameManager.FindCPU().UIAddBuff(this);
	}
	public virtual void OnRemove(){
		GameManager.FindCPU().UIRemoveBuff(this);
	}
	
	public virtual void DoPerSecond(){}
	
	public string GetTxtName(){
		return buffTxuName;
	}
	
	public void SetGobjIcon(GameObject gobjIcon){
		this.gobjIcon = gobjIcon;
	}
	
	public GameObject GetIconGobj(){
		return gobjIcon;
	}
	
	IEnumerator CoTime(){
		while(true){
			if(durTime > 0){
				yield return new WaitForSeconds(1f);
				durTime --;
				if(durTime == 0){
					OnRemove();
					break;
				}else{
					DoPerSecond();
				}
			}else{
				break;
			}
		}
	}
}
