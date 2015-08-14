NGUI-Alert
by Orlyapps -> www.orlyapps.de

If you have any problems, please send a mail to -> info@orlyapps.de

How do I install?
Just import the plugin

Requirements
 - NGUI >= 3.5 required
 - The samples build for 2D GUI (2DBoxCollider), if you need 3D support please edit the templates and change the BoxCollider

How to use?
Please take a look at the Demo.cs Script.
One line creation:
	--> UIAlert.create("Hello World", "Hi World ....").Show();	

Templates?
You find predefined templates in NGUI-Alert/Templates/Resources

Add Items?
You can add every GameObject to an UIAlert
	--> UIAlert.create("Hello World", "Hi World ....").Add(myCustomGameObject);
Or you can use Templates (Prefabs)
	--> UIAlert.create("Hello World", "Hi World ....").Add("UIAlert-Button-Template");
	
Features
 - Just a single line of code
 - Full customizable
 - Works with every GameObject and Prefab
 - Autoresize added GameObjects
 - Auto focus hierarchy
 - Predefined templates (Button, Input, Horizontal, Vertical)

Contains (Assets/NGUI-Alert)
Demo.unity (Demo scene)
Scripts/Demo.cs (Sample script)
Scripts/UIAlert.cs (Main component script)
Sprites (Sample nine-patch sprites for box and input + sample atlas)
Templates/Resources (predefined prefabs, horizontal, vertical, button, input)

Hierarchy
UIAlert creates one UIPanel directly under the UIRoot (search for typeof(UIRoot)).
All UIAlerts are inserted under the UIAlert-Panel.
UIRoot -> UIAlert-Panel -> UIAlert1
						-> UIAlert2

Please rate us on the Asset Store!