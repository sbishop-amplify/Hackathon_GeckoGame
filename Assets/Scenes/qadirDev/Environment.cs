using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Environment : MonoBehaviour {
	//TODO: Replace object with animal class later
	private Dictionary<Food, int> foodSupply = new Dictionary<Food, int>();

	public int grid_cell_size = 1;
	public float base_temp = 50;

	// Gets the position of the bottom left corner of the screen, used to get position in relation of to the grid
	private float bot_left_screen_x;
	private float bot_left_screen_y;

	public Space[,] temp_node;		// gets the temperature based on node position, Is seen from the top left screen

	// Use this for initialization
	void Start () {

		int grid_width = Screen.width / grid_cell_size;
		int grid_height = Screen.height / grid_cell_size;

		temp_node = new Space[grid_width, grid_height];

		// Initalizes each node with the base temp
		for (int x = 0; x < grid_width; x++) {
			for (int y = 0; y < grid_height; y++) {
				Space space = new Space ();
				space.Temperature = base_temp;
				temp_node [x, y] = space;
			}
		}

		bot_left_screen_x = Camera.main.transform.position.x - Screen.width / 2; 
		bot_left_screen_y = Camera.main.transform.position.y - Screen.height / 2;
	}

	public float CurrentTemp { get; set; }

	public int GetFoodAmount(Food food){
		return foodSupply[food];
	}

	public void AddFood(Food food, int n) {
		if (foodSupply.ContainsKey (food)) {
			n += foodSupply [food];
		}  else {
			foodSupply [food] = n;
		}
		foodSupply [food] = n;
		GameObject.Find (food.Name + "Count").GetComponent<Text> ().text = n.ToString ();
	}

	// Removes n food from the supply, or as much as possible, and returns how much removed
	public int RemoveFood(Food food, int n) {
		if (foodSupply.ContainsKey (food)) {
			int avail = foodSupply [food];
			if (avail < n) {
				foodSupply [food] = 0;
				return avail;
			}
			foodSupply [food] = avail - n;
			return n;
		}
		return 0;
	}

	/*
	 * Called when an object is added to the grid that affects the temperature
	 * Gets the bounds and update the grids in betweeen
	 */ 
	public void UpdateGridTemps(GameObject shade_object, float temp_change){

		Bounds shade_bounds = shade_object.GetComponent<BoxCollider2D> ().bounds;
		// Gets the position, width, and height
		float x_pos = shade_object.transform.position.x - bot_left_screen_x;
		float y_pos = shade_object.transform.position.y - bot_left_screen_y;
		float width = shade_bounds.size.x;
		float height = shade_bounds.size.y;


		// Converts the position into terms of grid size
		int node_width = (int)(width / grid_cell_size);
		int node_height = (int)(height / grid_cell_size);

		int start_pos_x = (int)((x_pos - width / 2) / grid_cell_size);
		int start_pos_y = (int)((y_pos - height / 2) / grid_cell_size);

		for (int node_x = start_pos_x; node_x < node_width + start_pos_x; node_x++) {
			for (int node_y = start_pos_y; node_y > 0; node_y--) {
				// Updates the node
				temp_node[node_x, node_y].Temperature += temp_change;
				Debug.Log("Was placed in "+node_x+", "+node_y);
			}
		}

	}

	/*
	 * Returns the temperature at the specified node
	 */
	public float GetTemp(float x, float y){
		int grid_x = (int)((x - bot_left_screen_x) / grid_cell_size);
		int grid_y = (int)((y - bot_left_screen_y) / grid_cell_size);

		Debug.Log ("Mouse Position: "+ x + ", "+ y +"Grid: " + grid_x + ", " + grid_y);

		return temp_node [grid_x, grid_y].Temperature;
	}

	/*
	 * Used to draw the grid for debuging the grid
	 */ 
	public void DisplayGrid(){

		int grid_width = Screen.width / grid_cell_size;
		int grid_height = Screen.height / grid_cell_size;
		for (int x = (int)bot_left_screen_x; x < grid_width; x += grid_cell_size) {
			for (int y = (int)bot_left_screen_y; y < grid_height; y += grid_cell_size) {
				Debug.DrawRay (new Vector3 (x, y), Vector2.up);		// Draws the ray
				Debug.DrawRay (new Vector3 (x, y), Vector2.right);		// Draws the ray
			}
		}
	}
}

