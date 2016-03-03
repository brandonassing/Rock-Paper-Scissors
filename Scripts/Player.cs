using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Player functions
public class Player : MonoBehaviour {

    public int score = 0;
    static public int choice;
    public Main mainRef;

    public Button buttonRock, buttonPaper, buttonScissors;

    public GameObject p1Prefab;
    private GameObject p1IM;

    public Text scoreGT;

    void Start()
    {
        //Resets buttons and text

        ResetButtons();

        GameObject scoreGO = GameObject.Find("YourScore");
        scoreGT = scoreGO.GetComponent<Text>();
        scoreGT.text = "Your Score: " + score.ToString();
    }

    //Called from Main.cs when player wins comparison; updates score
    public void PlayerWin(Button b)
    {
        ChangeButtonColor(b, Color.green);
        score++;
        scoreGT.text = "Your Score: " + score.ToString();
    }

    //Called when rock is selected
    public void ChooseRock()
    {
        choice = 1;
        ChangeButtonColor(buttonRock, Color.yellow);
        //Instantiates player image
        p1IM = Instantiate(p1Prefab) as GameObject;
        mainRef.Compare(this);
    }

    //Called when paper is selected
    public void ChoosePaper()
    {
        choice = 2;
        ChangeButtonColor(buttonPaper, Color.yellow);
        //Instantiates player image
        p1IM = Instantiate(p1Prefab) as GameObject;
        mainRef.Compare(this);
    }

    //Called when scissors is selected
    public void ChooseScissors()
    {
        choice = 3;
        ChangeButtonColor(buttonScissors, Color.yellow);
        //Instantiates player image
        p1IM = Instantiate(p1Prefab) as GameObject;
        mainRef.Compare(this);
    }

    //Sets all buttons to initial state
    public void ResetButtons()
    {
        //Sets buttons active
        buttonRock.gameObject.SetActive(true);
        buttonPaper.gameObject.SetActive(true);
        buttonScissors.gameObject.SetActive(true);
        //Sets buttons to interactable
        buttonRock.interactable = true;
        buttonPaper.interactable = true;
        buttonScissors.interactable = true;
        //Changes button colors to white
        buttonRock.GetComponent<Image>().color = Color.white;
        buttonPaper.GetComponent<Image>().color = Color.white;
        buttonScissors.GetComponent<Image>().color = Color.white;
    }

    //Changes button colour depending on game state
    public void ChangeButtonColor(Button b, Color c)
    { 
            b.GetComponent<Image>().color = c;  
    }

    //Destroys the player's image
    public void DestroyP1IM()
    {
        Destroy(p1IM);
    }
    

    
}
