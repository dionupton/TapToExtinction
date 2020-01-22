using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
public class SaveLoad : MonoBehaviour {
	public CloudHandler CHandler;
	public shopHandler SHandler;
	public Image savebutton;
	public Image loadbutton;
	public TouchStuff TStuff;
	private bool Overwrite;
	public GameObject oWrite;
	private bool Chosen = false;
	// Use this for initialization
	void Start () {
	
	}
	public void SavedButton(){
		savebutton.GetComponent<Animation> ().Play ("Saved");


	}
	public void LoadButton(){

		loadbutton.GetComponent<Animation> ().Play ("Loaded");

	}

	public void PerformSave(){
		float[] buttonslist = SHandler.returnButtons ();
		BinaryFormatter binary = new BinaryFormatter ();
		FileStream fStream = File.Create (Application.persistentDataPath + "/19882609.dru");
		KillCount saver = new KillCount ();
		saver.s_cps = CHandler.cps;
		saver.s_Count = CHandler.totalClouds;
		saver.s_CValue = CHandler.clickValue;
		saver.b_values = buttonslist;
		saver.s_clicks = TStuff.returnScore ();
		binary.Serialize (fStream, saver);
		fStream.Close ();
		CloseUI ();

	}

	public void CloseUI(){
		oWrite.GetComponent <CanvasGroup>().alpha = 0;
		oWrite.GetComponent <CanvasGroup>().blocksRaycasts = false;
		oWrite.GetComponent <CanvasGroup>().interactable = false;

	}
	public void Save(){
		if (File.Exists (Application.persistentDataPath + "/19882609.dru")) {

			oWrite.GetComponent <CanvasGroup>().alpha = 1;
			oWrite.GetComponent <CanvasGroup>().blocksRaycasts = true;
			oWrite.GetComponent <CanvasGroup>().interactable = true;

		} else {
			PerformSave();
		}

	}

	public void Load()
	{
		if (File.Exists (Application.persistentDataPath + "/19882609.dru")) 
		{
			BinaryFormatter binary = new BinaryFormatter();
			FileStream fStream = File.Open (Application.persistentDataPath + "/19882609.dru", FileMode.Open);
			KillCount saver = (KillCount)binary.Deserialize (fStream);
			fStream.Close ();
			CHandler.cps = saver.s_cps ;
			CHandler.totalClouds = saver.s_Count ; 
			CHandler.clickValue =saver.s_CValue;
			SHandler.ButtonLoad(saver.b_values);
			TStuff.AcceptScore (saver.s_clicks);
			//etc.
			SHandler.UpdateItems ();
		}

	}
	
}
[Serializable]
class KillCount
{
	public int s_clicks;
	public float s_cps;
	public float s_Count; 
	public float s_CValue;
	public float[] b_values;

}