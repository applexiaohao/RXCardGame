using UnityEngine;
using System.Collections;

public class UIAlert : MonoBehaviour {
	static string panelName = "UIAlert-Panel";

	public UILabel titleLabel;
	public UILabel messageLabel;
	public UITable table;

	public enum Layout {
		HORIZONTAL,
		VERTICAL
	};
	public Layout layout;
	
	Vector2 lastScreenSize;
	public string title;
	public string message;
	int startDepth = 100;
	UIPanel panel;
	int width, height;
	public Vector2 padding = new Vector2(0,0);

	public static void CloseAll(bool destroyOnClose = false) {
		GameObject panel = GameObject.Find(UIAlert.panelName);
		if(panel != null) {
			UIAlert[] alerts = panel.GetComponentsInChildren<UIAlert>();
			for(int i=0; i<alerts.Length; i++) {
				alerts[i].Close(destroyOnClose);
			}
		} else {
			//Debug.Log ("! NGUI-Alert Warning cannot find panel");
		}
	}
	
	public static UIAlert create(string title, string message = "", Layout layout = Layout.HORIZONTAL, int width = 400, int height = 300, string template = null) {
		string alertTemplate = "UIAlert-Horizontal-Template";
		if(layout == Layout.VERTICAL) {
			alertTemplate = "UIAlert-Vertical-Template";
		}
		if(template != null) {
			alertTemplate = template;
		} 

		UIRoot root = GameObject.FindObjectOfType(typeof(UIRoot)) as UIRoot;
		if(root != null) {
			UIPanel panel = CreatePanel(root);
			UIAlert alert = UIAlert.Instantiate(alertTemplate, panel);
			alert.GetComponent<UISprite>().alpha = 0.0f;
			alert.title = title;
			alert.message = message;
			alert.layout = layout;
			alert.panel = panel;
			alert.width = width;
			alert.height = height;
			
			alert.Hide();
			alert.Block();
			
			return alert;
		} else {
			Debug.Log("UIAlert Warning! UIRoot script not found");
		}
		return null;	
	}
	
	public T Add<T> (string template) where T : Component {
		GameObject prefab = Resources.Load(template) as GameObject;
		if(prefab == null) {
			Debug.Log ("! NGUI-Alert Warning cannot load: "+template);
		} else {
			return this.Add<T>(prefab);
		}
		return null;
	}
	
	public T Add<T> (GameObject go) where T : Component {
		GameObject instance = NGUITools.AddChild(this.table.gameObject, go) as GameObject;
		return instance.GetComponent<T>();
	}

	public void Preload() {
		this.GetComponent<UIWidget>().alpha = 0.0f;
		NGUITools.SetActive(this.gameObject, true);
	}

	public void Block() {
		this.GetComponent<BoxCollider2D>().size = new Vector3(4096, 4096);
	}

	public void Hide() {
		NGUITools.SetActive(this.gameObject, false);
	}

	private static UIAlert Instantiate(string alertTemplate, UIPanel panel) {
		GameObject alertTemplatePrefab = Resources.Load(alertTemplate) as GameObject;
		GameObject alertInstance = NGUITools.AddChild(panel.gameObject, alertTemplatePrefab) as GameObject;
		return alertInstance.GetComponent<UIAlert>();
	}

	private static UIPanel CreatePanel(UIRoot root) {
		GameObject panelInstance = GameObject.Find(UIAlert.panelName);
		if(panelInstance == null) {
			GameObject panelPrefab = Resources.Load("UIAlert-Panel-Template") as GameObject;
			panelInstance = NGUITools.AddChild(root.gameObject, panelPrefab) as GameObject;
			panelInstance.name = UIAlert.panelName;
		}
		return panelInstance.GetComponent<UIPanel>();
	}

	public void SetAnchors() {
		UIWidget widget = this.GetComponent<UIWidget>();
		widget.SetAnchor(this.panel.gameObject);
	}

	public void Resize() {
		this.GetComponent<UIWidget>().width = this.width;
		this.GetComponent<UIWidget>().height = this.height;
		int paddingX = (int)(this.padding.x);
		//int paddingY = (int)(this.padding.y);

		UIWidget container = NGUITools.FindInParents<UIWidget>(this.table.gameObject);

		this.table.transform.localPosition = new Vector2(
			(container.width / 2 *-1),
			this.table.transform.localPosition.y
		);

		int count = this.table.GetChildList().Count;
		if(count > 0) {
			Vector2 screenSize = new Vector2(container.width, container.height);
			if(screenSize != lastScreenSize) {
				lastScreenSize = screenSize;
				if(this.layout == Layout.HORIZONTAL) {
					int gridSize = (container.width - ((paddingX*2) * (count))) / count;

					this.table.GetChildList().ForEach((Transform child) => {
						UIWidget widget = child.GetComponent<UIWidget>();
						widget.width = gridSize;
					});
				} else {
					//int gridSizeH = (container.height - ((paddingY*2) * (count))) / count;
					this.table.GetChildList().ForEach((Transform child) => {
						UIWidget widget = child.GetComponent<UIWidget>();
						//widget.height = gridSizeH;

						widget.leftAnchor.target = container.transform;
						widget.leftAnchor.absolute = paddingX;
						
						widget.rightAnchor.target = container.transform;
						widget.rightAnchor.absolute = paddingX * -1;
					});
				}
			}
		}

	}
	
	public void Start() {
		if(this.titleLabel != null)  this.titleLabel.text = this.title;
		if(this.messageLabel != null) this.messageLabel.text = this.message;
	}

	public void Close(bool destroyOnClose = false) {
		NGUITools.SetActive(this.gameObject, false);
		if(destroyOnClose) {
			Destroy (this.gameObject);
		}
	}

	public void Reorder() {
		int depth = this.startDepth + (this.panel.GetComponentsInChildren<UIAlert>().Length * 50);
		this.GetComponent<UISprite>().depth = depth;
		UIWidget[] widgets = this.gameObject.GetComponentsInChildren<UIWidget>(true);
		for (int i = 0, imax = widgets.Length; i < imax; ++i) {
			UIWidget w = widgets[i];
			w.depth = depth+i;
		}
	}

	public void Show(Vector2 position = default(Vector2)) {
		this.table.padding = this.padding;

		if(position != Vector2.zero) {
			this.transform.localPosition = position;
		}

		NGUITools.SetActive(this.gameObject, true);

		this.Resize();
		this.Reorder();
	}
}