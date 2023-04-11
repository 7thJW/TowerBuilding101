using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameSystem : MonoBehaviour
{
    //Spawning and patrol variable
    public List<GameObject> spawnPoint;
    public GameObject spawnObject;
    public GameObject currentObject;
    public GameObject cam;
    private Vector3 startPos;
    private Vector3 endPos;
    private bool returnPoint;
    private bool currentObjIsMoving;
    private Vector3 endPoint;
    float nextPointvalue = 100;
    float moveUpValue = 5; 
    private int spawnPointValue;
    private float elapsedTime;
    private float duration = 3f;

    //Skill varaiable
    public Image skillBar;
    public Image cooldownFill;
    private float setCooldownTimer = 5f;
    private float coolDownTimer;
    private float setSkillDuration = 2f;
    private float skillDuration;
    private float slowDown = 1f;
    private Vector2 skillBarDefaultSize;

    //Score variable
    public TextMeshProUGUI GameScoreText;
    bool triggerOnceScore;
    [SerializeField] private Score score;
    // Start is called before the first frame update
    void Start()
    {
        score = GameObject.FindGameObjectWithTag("ScoreSystem").GetComponent<Score>();
        score.ResetScore();
        spawnPointValue = 0;
        triggerOnceScore = false;
        returnPoint = false;
        skillBarDefaultSize = skillBar.rectTransform.sizeDelta;
    }

    // Update is called once per frame
    void Update()
    {
        //Game Control
        if(Input.GetMouseButtonDown(0))
        {
            if(currentObject == null)
            {

                currentObject = Instantiate(spawnObject, spawnPoint[spawnPointValue].transform.position, Quaternion.identity);
                currentObjIsMoving = true;
                if (spawnPointValue == 0)
                {
                    spawnPointValue = 1;
                    endPoint = new Vector3(nextPointvalue, 0,0);
                   
                }
                else
                {
                    spawnPointValue = 0;
                    endPoint = new Vector3(0, 0, nextPointvalue);
                }
                startPos = spawnPoint[spawnPointValue].transform.position;
                endPos = new Vector3(spawnPoint[spawnPointValue].transform.position.x, spawnPoint[spawnPointValue].transform.position.y, spawnPoint[spawnPointValue].transform.position.z) - endPoint;
            }
            else
            {
                this.gameObject.transform.position += new Vector3(0, moveUpValue, 0);
                cam.transform.position += new Vector3(0, moveUpValue, 0);
                currentObject.GetComponent<ObjectScript>().isGravity = true;
                currentObject = null;
                elapsedTime = 0;
                triggerOnceScore = true;
                if (currentObjIsMoving)
                    currentObjIsMoving = false;
            }
            
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (coolDownTimer <= 0)
            {
                //activate skill
                slowDown = 0.5f;
                skillDuration = setSkillDuration;
                coolDownTimer = setCooldownTimer;
                skillBar.rectTransform.sizeDelta = skillBarDefaultSize;
                skillBar.gameObject.SetActive(true);
            }
        }
        //skill counting
        Skill();

        //Cube Partol
        if (currentObject != null)
        {

            if (currentObjIsMoving)
            {
                float distance = (currentObject.transform.position - endPos).magnitude;
                if (distance > 0)
                {
                    elapsedTime += Time.deltaTime * slowDown;
                    float completeLerpTime = elapsedTime / duration;
                    currentObject.transform.position = Vector3.Lerp(startPos, endPos, completeLerpTime);
                }
                else
                {
                    elapsedTime = 0;
                    if (!returnPoint)
                    {
                        //Switch Start and End point
                        endPos = spawnPoint[spawnPointValue].transform.position;
                        startPos = new Vector3(spawnPoint[spawnPointValue].transform.position.x, spawnPoint[spawnPointValue].transform.position.y, spawnPoint[spawnPointValue].transform.position.z) - endPoint;
                        returnPoint = true;
                    }
                    else
                    {
                        //Revert Start and End point
                        startPos = spawnPoint[spawnPointValue].transform.position;
                        endPos = new Vector3(spawnPoint[spawnPointValue].transform.position.x, spawnPoint[spawnPointValue].transform.position.y, spawnPoint[spawnPointValue].transform.position.z) - endPoint;
                        returnPoint = false;
                    }

                }
            }
            
        }
    }
    public void AddScore()
    {
        if (triggerOnceScore)
        {
            GameScoreText.text = "Score : " + score.AddScoreToEnd(1);
            triggerOnceScore = false;
        }
    }
    public void GameOver()
    {
        SceneManager.LoadScene("Main Menu");
    }
    void Skill()
    {
        coolDownTimer -= Time.deltaTime;
        skillDuration -= Time.deltaTime;
        cooldownFill.fillAmount = coolDownTimer / setCooldownTimer;
        if (skillBar.gameObject.activeSelf)
        {
            skillBar.rectTransform.sizeDelta = new Vector2(skillDuration / setSkillDuration * 200, 10);
        }
        if (skillDuration <= 0)
        {
            skillBar.gameObject.SetActive(false);
            slowDown = 1f;
            skillDuration = 0;
        }
        if(coolDownTimer <= 0)
        {
            coolDownTimer = 0;
        }
    }
}
