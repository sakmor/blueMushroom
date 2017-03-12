using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour
{

    GameObject player;              //我們操控的玩家
    GameObject mushroom;			//香菇之王，所有的香菇都是透過複製(Instantiate)他而來的
    public float mushroomSpeed;		//間隔多少時間產生新的香菇
    float lastTime;					//計時用
    int liveMushroom;				//現在存活的香菇數量
    public int maxLiveMushroom;		//場景內存活香菇的上限值，達到此數值不再新增香菇


    // Use this for initialization
    void Start()
    {
        maxLiveMushroom = 30;
        mushroomSpeed = 2;
        lastTime = Time.fixedTime;
        player = GameObject.Find("player");
        mushroom = GameObject.Find("murshroom");

    }

    // Update is called once per frame
    void Update()
    {
        mouseClick();
        mushroomfactory();

    }
    void mushroomfactory()
    {
        if (Time.fixedTime - lastTime > mushroomSpeed)
        {
            lastTime = Time.fixedTime;
            //如果香菇數量沒有達到上限值的話...
            if (maxLiveMushroom > liveMushroom)
            {
                makeNewMushroom();
            }
        }

    }
    void makeNewMushroom()
    {
        liveMushroom++;
        GameObject makeNewMushroom = Instantiate(mushroom);
        float randomX = UnityEngine.Random.Range(-7.5f, 7.5f);
        float randomZ = UnityEngine.Random.Range(-6, 8);
        makeNewMushroom.transform.position = new Vector3(randomX, 0, randomZ);

    }
    void mouseClick()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "mushroom")
                {
                    player.GetComponent<player>().setTargetMushroom(hit.transform.gameObject);
                    player.GetComponent<player>().setgoPos(new Vector3(hit.point.x, 0, hit.point.z));
                }

            }
        }
    }

}
