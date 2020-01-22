using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class shopHandler : MonoBehaviour {
	public CloudHandler CHandler;
	public SaveLoad SLoad;
	[System.Serializable]
	public class Item{
		public string name;
		public Sprite image;
		public float cost;
		public float cps;
		public float quantity;
		public bool UpClick;
		public Button.ButtonClickedEvent btnClicked;
		public bool FinalUnlock;
	}

	public GameObject[] buttonItems;
	public Item[] shopItems;
	int counter;
	public GameObject button;
	public TouchStuff tStuff;


	public float rCost(){
		return 1;
	}
	// Use this for initialization
	void Start () {
		buttonItems = new GameObject[shopItems.Length];
		counter = 0;
		foreach (Item i in shopItems) {

			GameObject btn = (GameObject)Instantiate (button);

			buttonItems[counter]=btn;
			itemScript scp = btn.GetComponent<itemScript>();
			scp.ClickValue = i.UpClick;
			scp.FinalUnlock = i.FinalUnlock;
			scp.name.text = i.name;
			scp.icon.sprite = i.image;
			if (i.cost > 1000){
				if (i.cost > 1000000){
					if(i.cost > 1000000000){
						if(i.cost > 1000000000000){
							scp.cost.text =  (i.cost/1000000000000).ToString("F2")+" T" ;
						}else{
							scp.cost.text =  (i.cost/1000000000).ToString("F2") +" B" ;}
					}else{
						scp.cost.text =  (i.cost/1000000).ToString("F2") +" M" ;}
				}else{
					scp.cost.text =  (i.cost/1000).ToString("F2") +" K" ;}
			}else{
				scp.cost.text = i.cost.ToString ("F2")  ;}

			if (i.UpClick == false){
				if (i.cps > 1000){
					if (i.cps > 1000000){
						if(i.cps > 1000000000){
							if(i.cps > 1000000000000){
								scp.cps.text = "+"+ (i.cps/1000000000000).ToString("F2") +" T" + " k/s";
							}else{
								scp.cps.text = "+"+ (i.cps/1000000000).ToString("F2") +" B" + " k/s";}
						}else{
							scp.cps.text = "+"+ (i.cps/1000000).ToString("F2") +" M" + " k/s";}
					}else{
						scp.cps.text = "+"+ (i.cps/1000).ToString("F2") +" K" + " k/s";}
				}else{
					scp.cps.text = "+"+ i.cps.ToString ("F2") + " k/s";}
			}
			else{
				if (i.cps > 1000){
					if (i.cps > 1000000){
						if(i.cps > 1000000000){
							if(i.cps > 1000000000000){
								scp.cps.text = "+"+ (i.cps/1000000000000).ToString("F2")+" T" ;
							}else{
								scp.cps.text = "+"+ (i.cps/1000000000).ToString("F2") +" B" ;}
						}else{
							scp.cps.text = "+"+ (i.cps/1000000).ToString("F2") +" M" ;}
					}else{
						scp.cps.text = "+"+ (i.cps/1000).ToString("F2") +" K" ;}
				}else{
					scp.cps.text = "+"+ i.cps.ToString ("F2")  ;}
			}
			scp.Quantity.text = i.quantity.ToString();

			Item thisItem = i;

			scp.thisButton.onClick.AddListener( () => Purchase (thisItem) );

			btn.transform.SetParent (this.transform, false);
			counter++;
		}

	}

	public float[] returnButtons(){
		float[] buttonstats = new float[shopItems.Length*3];
		counter = 0;
		foreach (Item i in shopItems) {
			buttonstats[counter] = i.cost;
			counter++;
			buttonstats[counter] = i.cps;
			counter++;
			buttonstats[counter] = i.quantity;
			counter++;
		}return buttonstats;

	}
	public void ButtonLoad(float[] variables){
		counter = 0;
		foreach (Item i in shopItems){
			i.cost = variables[counter];
			counter++;
			i.cps = variables[counter];
			counter++;
			i.quantity = variables[counter];
			counter++;
		}
		
	}
	void Purchase(Item bought){
		if (bought.FinalUnlock == true) {

			Social.ReportProgress ("CgkI99TKipAJEAIQBQ", 100.0f,(bool success) => {
					
				});
		}
		if (bought.cost > CHandler.totalClouds) {

		} else {

			Social.ReportProgress ("CgkI99TKipAJEAIQAg", 100.0f,(bool success) => {
					
				});

			bought.quantity++;
			if (bought.UpClick == false) {
				CHandler.cps = CHandler.cps + bought.cps;
			} else {
				CHandler.clickValue = CHandler.clickValue + bought.cps;
			}
			tStuff.PlaySound();
			CHandler.totalClouds = CHandler.totalClouds - bought.cost; //here deduct price
			bought.cost = bought.cost * 2.5f;
			if(bought.UpClick == true){
				bought.cps = bought.cps * 1.8f;
			}else{
				bought.cps = bought.cps * 1.5f;}
			UpdateItems ();
		}
	}

	
	public void UpdateItems(){
		counter = 0;
		foreach (Item i in shopItems) {
	
			itemScript scp = buttonItems[counter].GetComponent<itemScript>();
			if (i.UpClick == false){
				if (i.cps > 1000){
					if (i.cps > 1000000){
						if(i.cps > 1000000000){
							if(i.cps > 1000000000000){
								scp.cps.text = "+"+ (i.cps/1000000000000).ToString("F2") +" T" + " k/s";
							}else{
								scp.cps.text = "+"+ (i.cps/1000000000).ToString("F2") +" B" + " k/s";}
						}else{
							scp.cps.text = "+"+ (i.cps/1000000).ToString("F2") +" M" + " k/s";}
					}else{
						scp.cps.text = "+"+ (i.cps/1000).ToString("F2") +" K" + " k/s";}
				}else{
					scp.cps.text = "+"+ i.cps.ToString ("F2") + " k/s";}
			}
			else{
				if (i.cps > 1000){
					if (i.cps > 1000000){
						if(i.cps > 1000000000){
							if(i.cps > 1000000000000){
								scp.cps.text = "+"+ (i.cps/1000000000000).ToString("F2")+" T" ;
							}else{
								scp.cps.text = "+"+ (i.cps/1000000000).ToString("F2") +" B" ;}
						}else{
							scp.cps.text = "+"+ (i.cps/1000000).ToString("F2") +" M" ;}
					}else{
						scp.cps.text = "+"+ (i.cps/1000).ToString("F2") +" K" ;}
				}else{
					scp.cps.text = "+"+ i.cps.ToString ("F2")  ;}
			}
			scp.name.text = i.name;
			scp.icon.sprite = i.image;
			if (i.cost > 1000){
				if (i.cost > 1000000){
					if(i.cost > 1000000000){
						if(i.cost > 1000000000000){
							scp.cost.text =  (i.cost/1000000000000).ToString("F2")+" T" ;
						}else{
							scp.cost.text =  (i.cost/1000000000).ToString("F2") +" B" ;}
					}else{
						scp.cost.text =  (i.cost/1000000).ToString("F2") +" M" ;}
				}else{
					scp.cost.text =  (i.cost/1000).ToString("F2") +" K" ;}
			}else{
				scp.cost.text = i.cost.ToString ("F2")  ;}

			if(i.quantity > 1000){
				scp.Quantity.text = (i.quantity/1000).ToString("F0") + " K" ;
			}else{
				scp.Quantity.text = i.quantity.ToString();
			}



			counter++;
		}


	}
	
	// Update is called once per frame
	void Update () {
		counter = 0;
		foreach (Item i in shopItems) {
			if(i.cost > CHandler.totalClouds){
				buttonItems[counter].GetComponent<Button>().interactable = false;
				counter++;
			}else{
				buttonItems[counter].GetComponent<Button>().interactable = true;
				counter++;
			}

		}
}
}