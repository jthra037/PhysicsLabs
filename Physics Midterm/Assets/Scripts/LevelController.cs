using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {
    private int turnCtr = -1;
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
    private List<Rigidbody> rocks;

    [SerializeField]
    private Text score1;
    [SerializeField]
    private Text score2;
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

    // Use this for initialization
    void Start () {
        player = Instantiate(player) as GameObject;
        playerMat = player.GetComponent<MeshRenderer>().material;
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
                temp = temp && rb.velocity.sqrMagnitude < Mathf.Epsilon;
            }

            isTurnOver = temp;
        }
    }

    private void initTurn()
    {
        ++turnCtr;
        isTurnOver = false;
        isTurnStarted = false;
        //Change stats and stuff
        placePlayer();
        adjustPlayerMat();
        placeRock();
    }

    private void placePlayer()
    {
        player.transform.position = playerPlacement.position;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isTurnStarted)
            isTurnStarted = true;
    }

    private void OnTriggerExit(Collider other)
    {
        initTurn();
    }
}
