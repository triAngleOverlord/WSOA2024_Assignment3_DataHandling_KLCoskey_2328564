using TMPro;
using UnityEngine;

public class Actions : MonoBehaviour
{
    public AnimalStats animalStats;
    public string occupiedFarmer;
    public AnimalManager aManager;
    public TextMeshProUGUI taskText;
    public string task;
    public void cancel()
    {
        transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(170, -108, 0);
    }
    
    public void giveLove()
    {
        task = new string("");
        animalStats._love += animalStats._hug;
        animalStats.zLove = false;
        if(animalStats._love > animalStats.maxLove )
        {
            animalStats._love = animalStats.maxLove;
            animalStats._healthRateLoss -= animalStats.maxHealth * 0.01f;
        }
        if (aManager.night == true)
        {
            animalStats._energy -= animalStats.maxEnergy * 0.06f;
            task +=("You awoke them during the night so deducted "+ animalStats.maxEnergy * 0.06f);
        }
        task+=("\n"+"You gave " + animalStats._name + " some hugs. They now have " + animalStats._love);
        taskText.text = new string(task);
        cancel();
        StartCoroutine(GameManager.Instance.holdFarmer(GameObject.Find(occupiedFarmer)));
    }

    public void giveBath()
    {

        task = new string("");
        animalStats._cleanliness += animalStats._bath;
        animalStats.zCleanliness = false;
        if(animalStats._cleanliness> animalStats.maxCleanliness)
        {
            animalStats._cleanliness = animalStats.maxCleanliness;
            animalStats._healthRateLoss -= animalStats.maxHealth * 0.01f;
        }

        if(aManager.night==true)
        {
            animalStats._energy -= animalStats.maxEnergy * 0.06f;
            task += ("You awoke them during the nightso deducted " + animalStats.maxEnergy * 0.06f);
        }
        task += ("\n" + "You gave " + animalStats._name + " a bath. They now have " + animalStats._cleanliness);
        taskText.text = new string(task);
        cancel();
        StartCoroutine(GameManager.Instance.holdFarmer(GameObject.Find(occupiedFarmer)));
    }

    public void giveSnacks()
    {

        task = new string("");
        animalStats._hunger += animalStats._feed;
        animalStats.zHunger = false;
        animalStats._energy += animalStats.maxEnergy * 0.05f;
        animalStats.zEnergy = false;
        if(animalStats._hunger > animalStats.maxHunger )
        {
            animalStats._hunger = animalStats.maxHunger;
            animalStats._healthRateLoss -= animalStats.maxHealth * 0.02f;
        }
        if (aManager.night == true)
        {
            animalStats._energy -= animalStats.maxEnergy * 0.06f;
            task += ("You awoke them during the night so deducted " + animalStats.maxEnergy * 0.06f);
        }
        task += ("\n" + "You gave " + animalStats._name + " some snackos. They now have " + animalStats._hunger);
        taskText.text = new string(task);
        cancel();
        StartCoroutine(GameManager.Instance.holdFarmer(GameObject.Find(occupiedFarmer)));
    }

    public void occupyFarmer(TMP_Dropdown tmpdropdown)
    {
        //int num = transform.Find("Dropdown").transform.GetComponent<TMP_Dropdown>().value;
        occupiedFarmer = transform.Find("Dropdown").transform.GetComponent<TMP_Dropdown>().options[tmpdropdown.value].text;
        //Debug.Log(occupiedFarmer);
    }

    /*
    public IEnumerator holdFarmer()
    {
        GameObject.Find(occupiedFarmer).transform.GetComponent<MovingFarmer>().enabled = false;
        GameObject.Find(occupiedFarmer).transform.GetComponent<CanvasGroup>().alpha = 0.5f;
        Debug.Log(occupiedFarmer+ " is occupied");
        yield return new WaitForSeconds(3);
        GameObject.Find(occupiedFarmer).transform.GetComponent<MovingFarmer>().enabled = true;
        GameObject.Find(occupiedFarmer).transform.GetComponent<CanvasGroup>().alpha = 1f;
        Debug.Log(occupiedFarmer + " is now free");
    }*/

}
