﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Movement : MonoBehaviour {

    Scoring score;
    public float timestep = 0.2F;
    float time;

    CubeArray cA;

    //The actual group which can rotate and will move down
    public GameObject actualGroup;

    void Start()
    {
        cA = gameObject.GetComponent<CubeArray>();
        score = gameObject.GetComponent<Scoring>();
    }

    public void startGame()
    {
        actualGroup = this.gameObject.GetComponent<GroupSpawner>().spawnNext();
    }
    //Move down in interval of timestep
    void Update()
    {
        time += Time.deltaTime;
        if (time > timestep)
        {
            time = 0;
            move(Vector3.down);
        }

        
       // GetBoostKey();
        //Use this while building the application for Windows Platform (.exe)
        /*  checkForInput();    */
    }

    public void GetInputKey (int x)
    {
        if (x == 0)
        {
            Debug.Log("Rotated RIGHT");
            actualGroup.GetComponent<Rotation>().rotateRight(false);
            cA.updateArrayBool();
        }
        else if (x == 1)
        {
            Debug.Log("Rotated LEFT");
            actualGroup.GetComponent<Rotation>().rotateLeft(false);
            cA.updateArrayBool();
        }
        else if (x == 2)
        {
            move(Vector3.left);
        }
        else if (x == 3)
        {
            move(Vector3.right);
        }

     /*   else if (x == 4)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Speeding Downwards");
                timestep = 0.05F;
                cA.updateArrayBool()
            }

            else if (Input.GetMouseButtonUp(0))
            {
                gameObject.GetComponent<Scoring>().setNewSpeed();
                cA.updateArrayBool()
            }
        }   */
        
    }

 /*  public void GetBoostKey()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Speeding Downwards");
            timestep = 0.05F;
        }

        else if (Input.GetMouseButtonUp(0))
        {
            gameObject.GetComponent<Scoring>().setNewSpeed();
        }

        cA.updateArrayBool();
    }       */

/*    void checkForInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            actualGroup.GetComponent<Rotation>().rotateRight(false);
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            actualGroup.GetComponent<Rotation>().rotateLeft(false);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            move(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            move(Vector3.right);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            timestep = 0.05F;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            gameObject.GetComponent<Scoring>().setNewSpeed();
        }
        cA.updateArrayBool();
    }      */

    void move(Vector3 pos)
    {
        if (actualGroup != null)
        {
            actualGroup.transform.position += pos;
            if (!cA.updateArrayBool())
            {
                actualGroup.transform.position -= pos;
                gameObject.GetComponent<AudioController>().playCantMove();      
                if (pos == Vector3.down)
                {
                    spawnNew();
                }
            }
        }
    }

    //Handle spawning a new group and check if there is any intersection after spawning
    private void spawnNew()
    {
        actualGroup.GetComponent<Rotation>().isActive = false;
        actualGroup = gameObject.GetComponent<GroupSpawner>().spawnNext();
        actualGroup.GetComponent<Rotation>().isActive = true;
        if (!cA.updateArrayBool())
        {
            print("GAME OVER!!!");
            SceneManager.LoadScene("Tetris3DGame");
        }
        else
        {
            cA.checkForFullLine();
        }
    }
}
