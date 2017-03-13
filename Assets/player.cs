using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    int score;
    public float speed;
    GameObject clickMushroom;
    Vector3 goPos;
    // Use this for initialization
    void Start()
    {
        score = 0;
        speed = 15f;
    }

    // Update is called once per frame
    void Update()
    {
        gotoPos(goPos);
    }
    public void setgoPos(Vector3 temp)
    {
        goPos = temp;
    }
    public void setTargetMushroom(GameObject temp)
    {
        clickMushroom = temp;
    }
    void gotoPos(Vector3 pos)
    {
        Vector3 thisPos = this.transform.position;
        float posDist = Vector3.Distance(thisPos, pos);
        if (posDist > 0.01f)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, pos, speed * Time.deltaTime);
            this.GetComponent<Animator>().SetInteger("state", 1);
        }
        else if (clickMushroom != null)
        {
            score++;
            Destroy(clickMushroom);
        }
        else
        {
            this.GetComponent<Animator>().SetInteger("state", 0);
        }
    }
    public int getScore()
    {
        return score;
    }


}
