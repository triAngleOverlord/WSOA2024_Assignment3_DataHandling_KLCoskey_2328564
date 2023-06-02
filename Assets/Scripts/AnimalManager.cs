using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnimalManager : MonoBehaviour 
{
    //[SerializeField] private AnimalStats animalStats;
    public AnimalStats[] animlStats;
    public bool night;


    public void Awake()
    {
        animlStats = Resources.LoadAll<AnimalStats>("AnimalInfo");
        for (int i = 0; i < animlStats.Length; i++)
        {
            animlStats[i].Start();
        }

        StartCoroutine(healthCheck());
    }

    public IEnumerator healthCheck()
    {
        animlStats = Resources.LoadAll<AnimalStats>("AnimalInfo");
        for (int i = 0; i < animlStats.Length; i++)
        {
            animlStats[i].allStatsDecrease();
            //animlStats[i].newHealthRate();
        }

        yield return new WaitForSeconds(2);

        StartCoroutine(healthCheck());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (GameManager.hour>22 || GameManager.hour<7)
        {
            night = true;
            animlStats = Resources.LoadAll<AnimalStats>("AnimalInfo");
            for (int i = 0; i < animlStats.Length; i++)
            {
                animlStats[i]._sleep = animlStats[i].maxEnergy * -0.05f;
                animlStats[i].zEnergy = false;
            }
            GameObject.Find("Clock").GetComponent<Image>().color = Color.blue;
        }

        else if (GameManager.hour<22 || GameManager.hour > 7)
        {
            night = false;
            animlStats = Resources.LoadAll<AnimalStats>("AnimalInfo");
            for (int i = 0; i < animlStats.Length; i++)
            {
                animlStats[i]._sleep = animlStats[i].maxEnergy * 0.05f;
            }
            GameObject.Find("Clock").GetComponent<Image>().color = Color.white;
        }
    }

}
