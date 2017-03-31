using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class InterfaceManager : MonoBehaviour {

	public ClickOption makeOnClick = ClickOption.Cloud; 
	public int maxClouds = 6; 

	private GameObject cloud; 
	private int numClouds; 

	List<GameObject> clouds; 

	// Use this for initialization
	void Start () {
		cloud = (GameObject)Resources.Load ("Prefabs/Cloud");

		this.transform.FindChild ("DeleteSelector").GetComponent<Button> ().onClick.AddListener (SetDelete);  
		this.transform.FindChild ("CloudSelector").GetComponent<Button> ().onClick.AddListener (SetCloud); 
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0) && Input.mousePosition.x > 100)
		{
			if(makeOnClick == ClickOption.Cloud && numClouds < maxClouds)
			{
				GameObject newCloud = Instantiate (cloud); 
				newCloud.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
				numClouds++; 
			}
			if(makeOnClick == ClickOption.Delete)
			{
				//RaycastHit hit;
				//Physics.Raycast (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, -1), Vector3.forward, out hit);
				RaycastHit2D hit = Physics2D.Raycast((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.up, 0.1f); 
				if (hit.collider != null) {
					Destroy (hit.collider.gameObject); 
					Debug.Log ("DESTROOOOOOYED"); 
				} else
					Debug.Log ("AHH"); 
			}
		}
	}

	public void SetCloud()
	{
		makeOnClick = ClickOption.Cloud; 
		Debug.Log ("Cloud has been set"); 
	}

	public void SetDelete()
	{
		makeOnClick = ClickOption.Delete; 
		Debug.Log ("Delete has been set"); 
	}

}

public enum ClickOption
{
	Cloud, 
	Delete
};