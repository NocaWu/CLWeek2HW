using UnityEngine;
using System.Collections;

public class MatchManagerScript : MonoBehaviour {

	protected GameManagerScript gameManager;    //"protected" means this field is public to child scripts
												//but not to unrelated scripts

	public virtual void Start () {
		gameManager = GetComponent<GameManagerScript>();
	}


	//++++++++++++++++++++++++++++++++ check if match +++++++++++++++++++++++++++++++++++++++++
	/// <summary>
	/// Checks the entire grid for matches.
	/// </summary>
	/// 
	/// <returns><c>true</c>, if there are any matches, <c>false</c> otherwise.</returns>
	public virtual bool GridHasHorMatch(){
		bool matchHor = false; //assume there is no match

		//check each square in the grid
		for(int x = 0; x < gameManager.gridWidth; x++){
			for(int y = 0; y < gameManager.gridHeight ; y++){
				if(x < gameManager.gridWidth - 2){	//GridHasHorizontalMatch checks 2 to the right
													//gameManager.gridWidth - 2 ensures you're never extending into
													//a space that doesn't exist
					matchHor = matchHor || GridHasHorizontalMatch(x, y); //if match was ever set to true, it stays true forever
	
				} 
			}
		}

		return matchHor;
	}

	public virtual bool GridHasVerMatch(){
		bool matchVer = false; //assume there is no match

		//check each square in the grid
		for(int x = 0; x < gameManager.gridWidth; x++){
			for(int y = 0; y < gameManager.gridHeight ; y++){
				if(y < gameManager.gridHeight - 2){	//GridHasHorizontalMatch checks 2 to the right
					//gameManager.gridWidth - 2 ensures you're never extending into
					//a space that doesn't exist
					matchVer = matchVer || GridHasVerticalMatch(x, y); //if match was ever set to true, it stays true forever

				} 
			}
		}

		return matchVer;
	}

	//++++++++++++++++++++++++++++++++ check Hor / Ver match +++++++++++++++++++++++++++++++++++++++++
	/// <summary>
	/// Check if there is a horizontal match, based on the leftmost token.
	/// </summary>
	/// <returns><c>true</c> there is a horizontal match originating at these coordinates, 
	/// <c>false</c> otherwise.</returns>
	/// <param name="x">The x coordinate of the token to check.</param>
	/// <param name="y">The y coordinate of the token to check.</param>
	public bool GridHasHorizontalMatch(int x, int y){
		//check the token at given coordinates, the token to the right of it, and the token 2 to the right
		GameObject tokenHor1 = gameManager.gridArray[x + 0, y];
		GameObject tokenHor2 = gameManager.gridArray[x + 1, y];
		GameObject tokenHor3 = gameManager.gridArray[x + 2, y];

		if(tokenHor1 != null && tokenHor2 != null && tokenHor3 != null){ //ensure all of the token exists
			SpriteRenderer srHor1 = tokenHor1.GetComponent<SpriteRenderer>();
			SpriteRenderer srHor2 = tokenHor2.GetComponent<SpriteRenderer>();
			SpriteRenderer srHor3 = tokenHor3.GetComponent<SpriteRenderer>();
			
			return (srHor1.sprite == srHor2.sprite && srHor2.sprite == srHor3.sprite);  //compare their sprites
																						//to see if they're the same
		} else {
			return false;
		}
	}

	public bool GridHasVerticalMatch(int x, int y){

		//check the token at given coordinates, the 1+2 token above it
		GameObject tokenVer1 = gameManager.gridArray[x, y + 0];
		GameObject tokenVer2 = gameManager.gridArray[x, y + 1];
		GameObject tokenVer3 = gameManager.gridArray[x, y + 2];

		if(tokenVer1 != null && tokenVer2 != null && tokenVer3 != null){ //ensure all of the token exists
			SpriteRenderer srVer1 = tokenVer1.GetComponent<SpriteRenderer>();
			SpriteRenderer srVer2 = tokenVer2.GetComponent<SpriteRenderer>();
			SpriteRenderer srVer3 = tokenVer3.GetComponent<SpriteRenderer>();

			return (srVer1.sprite == srVer2.sprite && srVer2.sprite == srVer3.sprite);  //compare their sprites
			//to see if they're the same

		} else {
			return false;
		}
	}



	//+++++++++++++++++++++++++ check up / down / left / right match ++++++++++++++++++++++++++++++++++

	/// <summary>
	/// Determine how far to the right a match extends.
	/// </summary>
	/// <returns>The horizontal match length.</returns>
	/// <param name="x">The x coordinate of the leftmost gameobject in the match.</param>
	/// <param name="y">The y coordinate of the leftmost gameobject in the match.</param>
	public int GetHorizontalMatchLength(int x, int y){

		int matchLength = 1;
		
		GameObject firstHor = gameManager.gridArray[x, y]; //get the gameobject at the provided coordinates

		//make sure the script found a gameobject, and--if so--get its sprite
		if(firstHor != null){
			SpriteRenderer srHor1 = firstHor.GetComponent<SpriteRenderer>();

			//compare the gameobject's sprite to the sprite one to the right, two to the right, etc.
			//each time the script finds a match, increment matchLength
			//stop when it's not a match, or if the matches extend to the edge of the play area
			for(int i = x + 1; i < gameManager.gridWidth; i++){
				GameObject other = gameManager.gridArray[i, y];

				if(other != null){
					SpriteRenderer srHor2 = other.GetComponent<SpriteRenderer>();

					if(srHor1.sprite == srHor2.sprite){
						matchLength++;
					} else {
						break;
					}
				} else {
					break;
				}
			}
		}
		
		return matchLength;
	}

	//check vertical
	public int GetVerticalMatchHeight(int x, int y){
		Debug.Log ("131");

		int matchHeight = 1;

		GameObject firstVer = gameManager.gridArray[x, y]; //get the gameobject at the provided coordinates

		//make sure the script found a gameobject, and--if so--get its sprite
		if(firstVer != null){
			SpriteRenderer srVer1 = firstVer.GetComponent<SpriteRenderer>();
			Debug.Log ("140");

			//compare the gameobject's sprite to the sprite one to the right, two to the right, etc.
			//each time the script finds a match, increment matchLength
			//stop when it's not a match, or if the matches extend to the edge of the play area
			for(int i = y + 1; i < gameManager.gridHeight; i++){
				GameObject other = gameManager.gridArray[x, i];

				Debug.Log ("146");

				if(other != null){
					SpriteRenderer srVer2 = other.GetComponent<SpriteRenderer>();

					if(srVer1.sprite == srVer2.sprite){
						matchHeight++;
					} else {
						break;
					}
				} else {
					break;
				}
			}
		}

		return matchHeight;

	}


	//TODO:Make a Vertical line manager?

	//++++++++++++++++++++++++++++++++ remove Hor / Ver match +++++++++++++++++++++++++++++++++++++++++

	/// <summary>
	/// Destroys all tokens in a match of three or more
	/// </summary>
	/// <returns>The number of tokens destroyed.</returns>
	public virtual int RemoveHorizontalMatches(){
		int numHorRemoved = 0;

		//iterate across entire grid, looking for matches
		//wherever a horizontal match of three or more tokens is found, destroy them
		for(int x = 0; x < gameManager.gridWidth; x++){
			for(int y = 0; y < gameManager.gridHeight ; y++){
				if(x < gameManager.gridWidth - 2){

					int horizonMatchLength = GetHorizontalMatchLength(x, y);

					if(horizonMatchLength > 2){

						for(int i = x; i < x + horizonMatchLength; i++){
							GameObject token = gameManager.gridArray[i, y]; 
							Destroy(token);

							gameManager.gridArray[i, y] = null;
							numHorRemoved++;
						}
					}		
				}
			}
		}
		
		return numHorRemoved;
	}

	public virtual int RemoveVerticalMatches(){
		int numVerRemoved = 0;

		//iterate across entire grid, looking for matches
		//wherever a horizontal match of three or more tokens is found, destroy them
		for(int x = 0; x < gameManager.gridWidth; x++){
			for(int y = 0; y < gameManager.gridHeight ; y++){
				if(x < gameManager.gridHeight - 2){

					int verticalMatchHeight = GetVerticalMatchHeight(x, y);

					if(verticalMatchHeight > 2){

						for(int i = y; i < y + verticalMatchHeight; i++){
							GameObject token = gameManager.gridArray[x, i]; 
							Destroy(token);

							gameManager.gridArray[x, i] = null;
							numVerRemoved++;
						}
					}		
				}
			}
		}

		return numVerRemoved;
	}
}
