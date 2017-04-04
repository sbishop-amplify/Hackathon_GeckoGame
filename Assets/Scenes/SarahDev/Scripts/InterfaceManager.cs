using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class InterfaceManager : MonoBehaviour {

	public ClickOption makeOnClick = ClickOption.Cloud; 
	public int maxClouds = 6; 

	public GameObject cloud;
	private GameObject current_cloud;	// The cloud that is currently being placed

	private bool noMoreClouds = false; // Keep track of the text in the clouds button

	List<GameObject> cloud_list; 


	private Environment grid;
	// Use this for initialization
	void Start () {

		cloud = (GameObject)Resources.Load ("Prefabs/Cloud");	
		cloud_list = new List<GameObject>();	

		this.transform.FindChild ("Panel/DeleteSelector").GetComponent<Button> ().onClick.AddListener (SetDelete);  
		this.transform.FindChild ("Panel/CloudSelector").GetComponent<Button> ().onClick.AddListener (SetCloud); 
		grid = GameObject.Find ("Environment").GetComponent<Environment> ();
	}

	// Update is called once per frame
	void Update () {
		// Checks if mouse is pressed
		if(Input.GetMouseButton(0) && Input.mousePosition.x > 100)
		{
			// cloud_list.Count reaplaced numClouds because it was not needed
			if(makeOnClick == ClickOption.Cloud && cloud_list.Count < maxClouds)
			{
				// Checks if there is a cloud already being created
				if (current_cloud == null) {
					current_cloud = Instantiate (cloud); 
					Debug.Log ("Created Cloud");
				}

				// Gets the sprite render and cloud bounds of current_cloud
				SpriteRenderer cloudRenderer = current_cloud.GetComponent<SpriteRenderer> ();
				Vector2 mouse_position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
				current_cloud.transform.position = mouse_position;
				Bounds cloudBounds = current_cloud.GetComponent<BoxCollider2D> ().bounds;

				cloudRenderer.color = new Color (0, 1, 0, .5f);
				foreach(GameObject otherCloud in cloud_list){
					// Gets the bounds of each cloud in the list
					Bounds otherCloudBounds = otherCloud.GetComponent<BoxCollider2D> ().bounds;
					if (cloudBounds.Intersects(otherCloudBounds)) {
						// Sets color to red if in contact
						cloudRenderer.color = new Color (1, 0, 0, .5f);
						break;
					}
				}
			}
		}
		else if(cloud_list.Count >= maxClouds && !noMoreClouds)
		{
			this.transform.FindChild ("Panel/CloudSelector").FindChild ("Text").GetComponent<Text> ().text = "No more clouds!"; 
			noMoreClouds = true; 
		}

		// Checks when the mouse is released
		if (Input.GetMouseButtonUp (0)) {
			if (makeOnClick == ClickOption.Cloud && current_cloud != null) {
				SpriteRenderer cloudRenderer = current_cloud.GetComponent<SpriteRenderer> ();

				if (cloudRenderer.color == new Color (0, 1, 0, .5f)) {
					// Adds the current cloud to the list and increments the counter
					cloudRenderer.color = new Color (1, 1, 1, 1f);
					cloud_list.Add (current_cloud);
					CloudClass cloud_temp = current_cloud.GetComponent<CloudClass> ();
					Debug.Log(cloud_temp.TempChange);
					grid.UpdateGridTemps (current_cloud, cloud_temp.TempChange);
					//numClouds++; 
					Debug.Log ("Cloud was placed");
				}  else if (cloudRenderer.color == new Color (1, 0, 0, .5f)) {
					// Else it destroys the cloud
					Destroy (current_cloud);
					Debug.Log ("Was not valid placement");
				}
				current_cloud = null;
			}

			// Deletes cloud on button release
			if(makeOnClick == ClickOption.Delete)
			{
				//RaycastHit hit;
				//Physics.Raycast (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, -1), Vector3.forward, out hit);
				RaycastHit2D hit = Physics2D.Raycast((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.up, 0.1f); 
				if (hit.collider != null) {
					// Removes the object from the list and destroys it
					GameObject deleted_cloud = hit.collider.gameObject;
					CloudClass cloud_temp = deleted_cloud.GetComponent<CloudClass> ();

					cloud_list.Remove (deleted_cloud);	
					grid.UpdateGridTemps (deleted_cloud, -cloud_temp.TempChange);
					Destroy (hit.collider.gameObject); 
					Debug.Log ("DESTROOOOOOYED"); 
				}  

				if(noMoreClouds)
				{
					this.transform.FindChild ("Panel/CloudSelector").FindChild ("Text").GetComponent<Text> ().text = "Clouds"; 
					noMoreClouds = false; 
				}
			}
			Vector2 mouse_position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

			Debug.Log("Temperature: "+grid.GetTemp(mouse_position.x,mouse_position.y));
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

	public List<GameObject> GetCloudList(){
		return cloud_list;
	}


}

public enum ClickOption
{
	Cloud, 
	Delete
};
