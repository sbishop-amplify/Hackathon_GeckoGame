using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class InterfaceManager : MonoBehaviour {

	public ClickOption makeOnClick = ClickOption.Cloud; 
	public int maxClouds = 6; 

	public GameObject cloud;
	private GameObject current_cloud;	// The cloud that is currently being placed
	//private int numClouds;  // This is not needed because number can be obtained by clouds list

	List<GameObject> cloud_list; // This was never used, so I made use of it to store cloud_list in :p

	// Use this for initialization
	void Start () {
		/*
		 * I'm not sure why but the currently commented code in Start() was
		 * not working so I took it out and made line 11 public.
		 * You can revert the changes if you don't want this current 
		 * implementation.
		 * 
		 * You can delete this comment once you find out what works
		 */
		//cloud = (GameObject)Resources.Load ("Prefabs/Cloud");	// Had to be taken out because it was not working
		cloud_list = new List<GameObject>();	// Was added because the above was not working

		this.transform.FindChild ("DeleteSelector").GetComponent<Button> ().onClick.AddListener (SetDelete);  
		this.transform.FindChild ("CloudSelector").GetComponent<Button> ().onClick.AddListener (SetCloud); 
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

		// Checks when the mouse is released
		if (Input.GetMouseButtonUp (0)) {
			if (makeOnClick == ClickOption.Cloud && current_cloud != null) {
				SpriteRenderer cloudRenderer = current_cloud.GetComponent<SpriteRenderer> ();

				if (cloudRenderer.color == new Color (0, 1, 0, .5f)) {
					// Adds the current cloud to the list and increments the counter
					cloudRenderer.color = new Color (1, 1, 1, 1f);
					cloud_list.Add (current_cloud);
					//numClouds++; 
					Debug.Log ("Cloud was placed");
				} else if (cloudRenderer.color == new Color (1, 0, 0, .5f)) {
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
					cloud_list.Remove (hit.collider.gameObject);						
					Destroy (hit.collider.gameObject); 
					Debug.Log ("DESTROOOOOOYED"); 
				} 
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