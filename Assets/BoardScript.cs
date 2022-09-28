using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardScript : InteractiveItem {

    public int boards, previousBoards;
    public Animator[] boardAnim;
    public GameObject[] board;
    private Renderer rend;

    [SerializeField] private string _infoText = null;


    // Use this for initialization
    void Start () {
        boardAnim = GetComponentsInChildren<Animator> ();
        for (int i=0; i< 3; i++)
        {
            boardAnim[i].Play("BoardAnimation" + (i+1).ToString()); return;
        }
        boards = 3;
    }
	
	// Update is called once per frame
	void Update () {

        /*if()
        {

        }*/
		
	}

    // ---------------------------------------------------------------------------
    // Name	:	GetText (Override)
    // Desc	:	Returns the text to display when player's crosshair is over this
    //			button.
    // ---------------------------------------------------------------------------
    public override string GetText()
    {
        return _infoText;
    }

    void AddBoard()
    {
        if(boards < 3)
        {
            board[boards].SetActive(true);
            boardAnim[boards].Play("BoardAnimation" + (boards + 1).ToString());
        }
  
    }
    void RemoveBoard()
    {
        if(boards > 0)
        {
            board[boards - 1].SendMessage("DisableBoard", SendMessageOptions.RequireReceiver);
            boards -= 1;

            if (boards == 0) rend.enabled = true;
        }
    }
}
