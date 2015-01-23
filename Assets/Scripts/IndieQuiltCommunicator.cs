using UnityEngine;
using System.Collections;

public class IndieQuiltCommunicator : MonoBehaviour {

	//Please do not make any major modifications to this document, aside from perhaps switching around the
	// difficulty level for testing purposes. Any changes you make here will not be brought to IndieQuilt Main,
	// so just do so for testing purposes.
	
	//Please have your game modify the "success" and "finished" booleans with the proper states when the
	// game finishes. Also have it draw the game's difficulty from the Difficulty variable.
	
	//Base difficulty is 1: Most Players should be able to complete this game on their first try
	//Highest difficulty is 10: Most Players have serious trouble beating this game
	public int difficulty; //Feel free to temporarily change this variable's value as you need for standalone builds / testing.
	
	//Set to true if the player has succeeded and false if the player has failed
	public bool success;
	
	//Set to true when the game is finished (setting this to true will cause the scene to change)
	public bool finished = false;
	
	//Inclusivity
	public bool photosensitive = false; //This will be automatically set to true if the Player chooses to activate photosensitive-safe mode from IndieQuilt main options.
	public bool colorblind = false; //This will also be set to true under the same situation, but for colorblind mode.
	

	void Start () {}
	
	void Update () {}

	//If your game has any special needs, contact me at erichornby@pelagicgames.com, hornbyeric@gmail.com, or @Erichermit on Twitter
	// and we can discuss how I can accomodate you.	
}
