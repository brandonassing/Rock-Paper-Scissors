using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Main game controller
public class Main : MonoBehaviour
{
    //enum used for computer choice
    public enum p2ChoiceEnum
    {
        Rock = 1,
        Paper = 2,
        Scissors = 3
    }

    static public p2ChoiceEnum p2Choice;
    public GameObject p2Prefab;
    private GameObject p2IM;

    public int totalRounds = 10;
    private int roundNum = 1;
    private int p2Score = 0;

    public Text roundsGT, p2ScoreGT, resultGT, vsGT;
    public GameObject gameOverGO, p2TextGO, p1TextGO, vsGO;

    public Button playAgain, quitGame;

    void Start()
    {
        //All text initialized to starting state

        GameObject roundsGO = GameObject.Find("Rounds");
        roundsGT = roundsGO.GetComponent<Text>();
        roundsGT.text = "Round: " + roundNum.ToString();

        GameObject p2ScoreGO = GameObject.Find("ComputerScore");
        p2ScoreGT = p2ScoreGO.GetComponent<Text>();
        p2ScoreGT.text = "Computer Score: " + p2Score.ToString();

        GameObject resultGO = GameObject.Find("Result");
        resultGT = resultGO.GetComponent<Text>();
        resultGT.text = " ";

        p2TextGO = GameObject.Find("ComputerText");
        p2TextGO.SetActive(false);

        gameOverGO = GameObject.Find("GameOver");
        gameOverGO.SetActive(false);

        p1TextGO = GameObject.Find("PlayerText");
        p1TextGO.SetActive(false);

        vsGO = GameObject.Find("VS");
        vsGT = vsGO.GetComponent<Text>();
        vsGT.text = "Select Option";

        playAgain.gameObject.SetActive(false);
        quitGame.gameObject.SetActive(false);

        //Gets computers choice
        p2Choice = GetComputerChoice();
    }

    //Main game logic
    public void Compare(Player p1)
    {
        //Continues until game over (after round 10)
        if (roundNum <= totalRounds)
        {
            //Sets text based on game state
            p1TextGO.SetActive(true);
            p2TextGO.SetActive(true);
            vsGT.text = "VS";
            //Instantiate computer image
            p2IM = Instantiate(p2Prefab) as GameObject;

            //player and computer choice is the same
            if (Player.choice == (int)p2Choice)
            {
                resultGT.color = Color.yellow;
                resultGT.text = "Round Result: DRAW";
            }
            else
            {
                //Player chose Rock
                if (Player.choice == 1)
                {
                    //Computer chose Scissors (YOU WIN)
                    if ((int)p2Choice == 3)
                    {
                        resultGT.color = Color.green;
                        resultGT.text = "Round Result: YOU WIN";
                        p1.PlayerWin(p1.buttonRock);
                    }
                    //Computer chose Paper (YOU LOSE)
                    else
                    {
                        resultGT.color = Color.red;
                        resultGT.text = "Round Result: YOU LOSE";
                        p1.ChangeButtonColor(p1.buttonRock, Color.red);
                        ComputerWin();
                    }
                }

                //Player chose Paper
                if (Player.choice == 2)
                {
                    //Computer chose Rock (YOU WIN)
                    if ((int)p2Choice == 1)
                    {
                        resultGT.color = Color.green;
                        resultGT.text = "Round Result: YOU WIN";
                        p1.PlayerWin(p1.buttonPaper);
                    }
                    //Computer chose Scissors (YOU LOSE)
                    else
                    {
                        resultGT.color = Color.red;
                        resultGT.text = "Round Result: YOU LOSE";
                        p1.ChangeButtonColor(p1.buttonPaper, Color.red);
                        ComputerWin();
                    }
                }

                //Player chooses Scissors
                if (Player.choice == 3)
                {
                    //Computer chose Paper (YOU WIN)
                    if ((int)p2Choice == 2)
                    {
                        resultGT.color = Color.green;
                        resultGT.text = "Round Result: YOU WIN";
                        p1.PlayerWin(p1.buttonScissors);
                    }
                    //Computer chose Rock (YOU LOSE)
                    else
                    {
                        resultGT.color = Color.red;
                        resultGT.text = "Round Result: YOU LOSE";
                        p1.ChangeButtonColor(p1.buttonScissors, Color.red);
                        ComputerWin();
                    }
                }
            }
            roundNum++;
        }

        //Couroutine used for timer
        StartCoroutine(RoundFinished(p1));
    }

    //Called after each round; returns IEnumerator for timer use
    IEnumerator RoundFinished(Player p1)
    {
        //Disables buttons for set time to prevent player from continuing round
        p1.buttonRock.interactable = false;
        p1.buttonPaper.interactable = false;
        p1.buttonScissors.interactable = false;

        //Timer to between rounds to display results
        yield return new WaitForSeconds(2);

        //Resets buttons to working game state
        p1.ResetButtons();

        //Resets text to starting state
        resultGT.text = " ";
        p1TextGO.SetActive(false);
        p2TextGO.SetActive(false);
        vsGT.text = "Select Option";

        //Destroy images
        p1.DestroyP1IM();
        Destroy(p2IM);

        //Updates round number if game not finished
        if (roundNum <= totalRounds)
        {
            roundsGT.text = "Round: " + roundNum.ToString();
            p2Choice = GetComputerChoice();
        }
        else
        {
            GameFinished(p1);
        }
    }

    //Called when game finishes (after round 10)
    //Displays overall game result
    void GameFinished(Player p1)
    {
        vsGO.SetActive(false);
        gameOverGO.SetActive(true);

        //Player wins
        if (p1.score > p2Score)
        {
            resultGT.color = Color.green;
            resultGT.text = "YOU WIN";
        }

        //Computer wins
        else if (p2Score > p1.score)
        {
            resultGT.color = Color.red;
            resultGT.text = "YOU LOSE";
        }

        //Draw
        else
        {
            resultGT.color = Color.yellow;
            resultGT.text = "DRAW";
        }

        //Activates play again and quit buttons
        p1.buttonRock.gameObject.SetActive(false);
        p1.buttonPaper.gameObject.SetActive(false);
        p1.buttonScissors.gameObject.SetActive(false);
        playAgain.gameObject.SetActive(true);
        quitGame.gameObject.SetActive(true);
    }

    //Called when computer wins comparison; updates computer's score
    void ComputerWin()
    {
        p2Score++;
        p2ScoreGT.text = "Computer Score: " + p2Score.ToString();
    }

    //Randomizes a value from 1-3 and returns it as type p2ChoiceEnum
    p2ChoiceEnum GetComputerChoice()
    {
        int rand = Random.Range(1, 4);

        ///////////////////////////////USED FOR DEBUGGING////////////////////////////
        if (rand == 1)
        {
            Debug.Log("Computer: Rock");
        }
        else if (rand == 2)
        {
            Debug.Log("Computer: Paper");
        }
        else
        {
            Debug.Log("Computer: Scissors");
        }
        ///////////////////////////////USED FOR DEBUGGING////////////////////////////

        return (p2ChoiceEnum)rand;
    }
    
}
