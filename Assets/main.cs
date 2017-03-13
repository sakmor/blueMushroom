using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour
{

    GameObject player;              //我們操控的玩家
    GameObject mushroom;            //香菇之王，所有的香菇都是透過複製(Instantiate)他而來的
    GameObject blackScreen;         //問題介面
    public float mushroomSpeed;		//間隔多少時間產生新的香菇
    float lastTime;					//計時用
    int liveMushroom;				//現在存活的香菇數量
    public int maxLiveMushroom;		//場景內存活香菇的上限值，達到此數值不再新增香菇
    bool mouseClickbool;            //真假值，用來控制是否開啟滑鼠點擊判斷
    int state;                      //紀錄每次問答的狀態
    int questionDeep;               //紀錄現在是第幾層問題


    // Use this for initialization
    void Start()
    {
        questionDeep = 0;                   //最初始問題
        state = -1;                         //最初始狀態
        mouseClickbool = true;              //預設開啟滑鼠點擊判斷
        maxLiveMushroom = 30;               //預設最多30個香菇在畫面
        mushroomSpeed = 2;                  //預設兩秒生產一個香菇
        lastTime = Time.fixedTime;          //計時用
        player = GameObject.Find("player");
        mushroom = GameObject.Find("murshroom");
        blackScreen = GameObject.Find("blackScreen");
        blackScreen.SetActive(false);       //隱藏問題介面

    }

    // Update is called once per frame
    void Update()
    {
        mouseClick();                       //檢查使用者是否點擊滑鼠
        mushroomfactory();                  //檢查時間是否該生產香菇了
        scoreCheck();                       //檢查得分是否需要顯示問題介面
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
        if (Input.GetMouseButtonUp(0) && mouseClickbool)
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
    void scoreCheck()
    {

        int score = player.GetComponent<player>().getScore();
        GameObject.Find("scoreText").GetComponent<UnityEngine.UI.Text>().text = score.ToString("F0");
        switch (score)
        {
            case 5:
                if (questionDeep == 0)
                {
                    showQuestion();
                    questionDeep = 1;
                }
                break;
            case 10:
                if (questionDeep == 1)
                {
                    showQuestion();
                    questionDeep = 10;
                }
                break;
            case 15:
                endGame();
                break;
        }
    }

    public void pressButtonA()
    {
        if (state == -1)
            state = 0;

        state += 1 * questionDeep;
        closeQuestion();
    }
    public void pressButtonB()
    {
        if (state == -1)
            state = 0;

        closeQuestion();
    }
    void closeQuestion()
    {
        blackScreen.SetActive(false);   // 關閉問題介面
        mouseClickbool = true;        // 開啟滑鼠點香菇判斷
    }
    void endGame()
    {
        mouseClickbool = false;     //關閉遊戲操作

        //依據選項給稱號
        switch (state)
        {
            case 00:
                GameObject.Find("scoreText").GetComponent<UnityEngine.UI.Text>().text = "很樂天派的";
                break;
            case 01:
                GameObject.Find("scoreText").GetComponent<UnityEngine.UI.Text>().text = "課長等級的";
                break;
            case 10:
                GameObject.Find("scoreText").GetComponent<UnityEngine.UI.Text>().text = "吃飽太閒的";
                break;
            case 11:
                GameObject.Find("scoreText").GetComponent<UnityEngine.UI.Text>().text = "灰熊殺氣的";
                break;
        }

    }
    void showQuestion()
    {
        blackScreen.SetActive(true);   // 顯示問題介面
        mouseClickbool = false;        // 關閉滑鼠點香菇判斷

        //依據狀態出問題
        switch (state)
        {
            case -1:
                GameObject.Find("blackScreenText").GetComponent<UnityEngine.UI.Text>().text = "中午你想吃那一道？";
                GameObject.Find("ButtonAText").GetComponent<UnityEngine.UI.Text>().text = "日式";
                GameObject.Find("ButtonBText").GetComponent<UnityEngine.UI.Text>().text = "美式";
                break;
            case 0:
                GameObject.Find("blackScreenText").GetComponent<UnityEngine.UI.Text>().text = "天氣妳喜歡？";
                GameObject.Find("ButtonAText").GetComponent<UnityEngine.UI.Text>().text = "冬天";
                GameObject.Find("ButtonBText").GetComponent<UnityEngine.UI.Text>().text = "夏天";
                break;
            case 1:
                GameObject.Find("blackScreenText").GetComponent<UnityEngine.UI.Text>().text = "喜歡哪個飲料？";
                GameObject.Find("ButtonAText").GetComponent<UnityEngine.UI.Text>().text = "咖啡";
                GameObject.Find("ButtonBText").GetComponent<UnityEngine.UI.Text>().text = "牛奶";
                break;
        }
    }
}
