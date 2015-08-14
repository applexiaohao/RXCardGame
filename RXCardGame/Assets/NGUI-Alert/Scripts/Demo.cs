using UnityEngine;
using System.Collections;

public class Demo : MonoBehaviour {
	public string title = "Commander Cool";
	public string message = "Are you ready for the coolest guy in the app store? He is like Batman! But he can‘t fly.... He is like Superman! But he can die... Anyways… He is pretty much the greatest guy to walk the earth. Commander Cool.";
	UIAlert formular;
	
	void OnGUI() {
		if (GUI.Button(new Rect(10, 10, 150, 50), "Horizontal 1 Button")) Show(1);
		if (GUI.Button(new Rect(170, 10, 150, 50), "Horizontal 2 Button")) Show(2);
		if (GUI.Button(new Rect(330, 10, 150, 50), "Horizontal 3 Button")) Show(3);

		if (GUI.Button(new Rect(490, 10, 150, 50), "Vertical 1 Button")) Show(1, UIAlert.Layout.VERTICAL);
		if (GUI.Button(new Rect(650, 10, 150, 50), "Vertical 2 Button")) Show(2, UIAlert.Layout.VERTICAL);
		if (GUI.Button(new Rect(810, 10, 150, 50), "Vertical 3 Button")) Show(3, UIAlert.Layout.VERTICAL);

		if (GUI.Button(new Rect(10, 70, 150, 50), "Vertical Formular")) Formular();
		if (GUI.Button(new Rect(170, 70, 150, 50), "Two Alerts")) Two();
		if (GUI.Button(new Rect(330, 70, 150, 50), "Focus")) Focus();
		if (GUI.Button(new Rect(490, 70, 150, 50), "No Buttons")) NoButtons();
		if (GUI.Button(new Rect(650, 70, 150, 50), "Close All")) UIAlert.CloseAll(true);
	}

	public void NoButtons() {
		UIAlert.CloseAll(true);
		UIAlert.create("Alert 1", this.message).Show();
	}

	public void Focus() {
		UIAlert.CloseAll(true);
		UIAlert alert1 = UIAlert.create("Alert 1", this.message);
		alert1.padding = new Vector2(10.0f, 10.0f);
		alert1.Add<UIButton>("UIAlert-Button-Template").onClick.Add(new EventDelegate(() => {
			alert1.Close(true);
		}));
		alert1.Show(new Vector2(-200.0f, 0.0f));
		
		UIAlert alert2 = UIAlert.create("Focused Alert", this.message);
		alert2.padding = new Vector2(10.0f, 10.0f);
		
		alert2.Add<UIButton>("UIAlert-Button-Template").onClick.Add(new EventDelegate(() => {
			alert2.Close(true);
		}));

		alert2.Add<UIButton>("UIAlert-Button-Template").onClick.Add(new EventDelegate(() => {
			alert2.Close(true);
		}));
		
		alert2.Show(new Vector2(200.0f, 0.0f));
	}
	
	public void Two() {
		UIAlert.CloseAll(true);
		UIAlert alert1 = UIAlert.create("Alert 1", this.message);
		alert1.padding = new Vector2(10.0f, 10.0f);
		alert1.Add<UIButton>("UIAlert-Button-Template").onClick.Add(new EventDelegate(() => {
			alert1.Close(true);
		}));
		alert1.Show();

		UIAlert alert2 = UIAlert.create("Alert 2", this.message);
		alert2.padding = new Vector2(10.0f, 10.0f);

		alert2.Add<UIButton>("UIAlert-Button-Template").onClick.Add(new EventDelegate(() => {
			alert2.Close(true);
		}));
		alert2.Add<UIButton>("UIAlert-Button-Template").onClick.Add(new EventDelegate(() => {
			alert2.Close(true);
		}));

		alert2.Show();
	}

	public void Formular() {
		UIAlert.CloseAll(true);
		UIAlert alert = UIAlert.create(this.title, this.message, UIAlert.Layout.VERTICAL);
		alert.padding = new Vector2(10.0f, 10.0f);

		for(int i=0; i<3; i++) {
			alert.Add<UIInput>("UIAlert-Input-Template");
		}

		alert.Add<UIButton>("UIAlert-Button-Template").onClick.Add(new EventDelegate(() => {
			alert.Close(true);
		}));
		
		alert.Show ();
	}

	public void Show(int buttons, UIAlert.Layout layout = UIAlert.Layout.HORIZONTAL) {
		UIAlert.CloseAll(true);
		UIAlert alert = UIAlert.create(this.title, this.message, layout);
		alert.padding = new Vector2(10.0f, 10.0f);

		for(int i=0; i<buttons; i++) {
			alert.Add<UIButton>("UIAlert-Button-Template").onClick.Add(new EventDelegate(() => {
				alert.Close(true);
			}));
		}

		alert.Show ();
	}
}
