using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {
    private int turnCtr = -1;
    private int numTurns;
    public bool isEvenTurn
    {
        get { return turnCtr % 2 == 0; }
    }
    public int playerTurn
    {
        get { return turnCtr % 2; }
    }
    private Material playerMat;

    private bool isTurnStarted = false;
    private bool isTurnOver = false;
    private PlayerController playerCon;
    private List<Rigidbody> rocks = new List<Rigidbody>();
    private List<Rock>[] teamRocks = { new List<Rock>(), new List<Rock>() };

    [SerializeField]
    private Text[] scores;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Color[] colors;
    [SerializeField]
    private GameObject rock;
    [SerializeField]
    private Transform rockSpawn;
    [SerializeField]
    private Transform playerPlacement;
    [SerializeField]
    private Slider numTurnSlider;
    [SerializeField]
    private Text numTurnsNotifier;
    [SerializeField]
    private Slider playerMassSlider;
    [SerializeField]
    private Text playerMassNotifier;
    [SerializeField]
    private Slider playerAccelSlider;
    [SerializeField]
    private Text playerAccelNotifier;
    [SerializeField]
    private Slider rockMassSlider;
    [SerializeField]
    private Text rockMassNotifier;

    // Use this for initialization
    void Start () {
        player = Instantiate(player) as GameObject;
        playerMat = player.GetComponent<MeshRenderer>().material;
        playerCon = player.GetComponent<PlayerController>();
        initTurn();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug start next round
        if (Input.GetKeyDown(KeyCode.R))
        {
            initTurn();
        }

        if (isTurnStarted && isTurnOver)
        {
            initTurn();
        }
	}

    private void FixedUpdate()
    {
        if (isTurnStarted)
        {
            bool temp = true;

            foreach (Rigidbody rb in rocks)
            {
                temp = temp && rb.velocity.sqrMagnitude < 0.001;
            }

            isTurnOver = temp;
        }
    }

    private void initTurn()
    {
        Debug.Log("<<initTurn" + "@" + Time.frameCount);
        ++turnCtr;
        updateScore();
        isTurnOver = false; 
        isTurnStarted = false;
        //Change stats and stuff
        placePlayer();
        placeRock();
        Debug.Log(">>initTurn" + "@" + Time.frameCount);
    }

    private void placePlayer()
    {
        player.transform.position = playerPlacement.position;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        adjustPlayerMat();
    }

    private void adjustPlayerMat()
    {
        playerMat.color = colors[playerTurn];
    }

    private void placeRock()
    {
        GameObject thisRock  = Instantiate(rock, rockSpawn.position, rockSpawn.rotation) as GameObject;
        Material thisMaterial = thisRock.GetComponent<MeshRenderer>().material;
        thisMaterial.color = colors[playerTurn];

        rocks.Add(thisRock.GetComponent<Rigidbody>());
        teamRocks[playerTurn].Add(thisRock.GetComponent<Rock>());
    }

    private void updateScore()
    {
        for(int i = 0; i < 2; i++)
        {
            int score = 0;
            foreach(Rock rock in teamRocks[i])
            {
                score += rock.Value;
            }

            scores[i].text = "Score: " + score;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isTurnStarted && other.CompareTag("Rock"))
        {
            other.tag = "Untagged";
            isTurnStarted = true;
        }
    }

    public void UpdateNumTurns()
    {
        numTurns = (int)numTurnSlider.value;
        numTurnsNotifier.text = numTurns.ToString();
    }

    public void UpdatePlayerMass()
    {
        playerCon.rb.mass = playerMassSlider.value;
        playerMassNotifier.text = playerCon.rb.mass.ToString();
    }

    public void UpdatePlayerAccel()
    {
        playerCon.accel = playerAccelSlider.value;
        playerAccelNotifier.text = playerCon.accel.ToString();
    }

    public void UpdateRockMass()
    {
        rocks[rocks.Count - 1].mass = rockMassSlider.value;
        rockMassNotifier.text = rocks[rocks.Count - 1].mass.ToString();
    }

    public void Fault(GameObject faultingObject)
    {
        Debug.Log(faultingObject.name + turnCtr);
        Destroy(faultingObject);
        initTurn();
    }
}
