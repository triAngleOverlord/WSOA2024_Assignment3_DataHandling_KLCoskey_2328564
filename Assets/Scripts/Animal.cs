using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Animal : MonoBehaviour 
{
    [SerializeField] public GameObject statDisplay;
    [SerializeField] public AnimalStats stats;
    public Transform parent;
    public GameObject popUp;
    public GameObject healthBar;
    public Actions _dropdown;
    public GameObject currentFarmer;

    public void animalSelected()
    {
        if (this.transform.parent.transform.GetComponent<PenStats>().farmerPresent == true)
        {
            //popUp.SetActive(true);
            popUp.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(-170, -170, 0);
            popUp.transform.Find("AnimalName").transform.GetComponent<TextMeshProUGUI>().text = new string(stats._name);

            PenStats pen = transform.parent.GetComponent<PenStats>();
            //Debug.Log( popUp.transform.Find("Dropdown").transform.name);
            popUp.transform.Find("Dropdown").transform.GetComponent<TMP_Dropdown>().ClearOptions();
            popUp.transform.Find("Dropdown").transform.GetComponent<TMP_Dropdown>().AddOptions(pen.farmersInPen);
            popUp.transform.GetComponent<Actions>().animalStats = stats;


            popUp.transform.GetComponent<Actions>().occupiedFarmer = popUp.transform.Find("Dropdown").transform.GetComponent<TMP_Dropdown>().options[0].text;

            currentFarmer = GameObject.Find(popUp.transform.GetComponent<Actions>().occupiedFarmer);
            

            currentFarmer.GetComponent<MovingFarmer>().currentAnimal = stats._name;


        }

    }

    public void Awake()
    {
        statDisplay = GameObject.Find("StatPanel").gameObject;
        popUp = GameObject.Find("ActionsPopUp").gameObject;
        healthBar = transform.Find("HealthBar").gameObject;
        healthBar.transform.GetComponent<Slider>().maxValue = stats.maxHealth;
        healthBar.SetActive(false);
        _dropdown = popUp.transform.GetComponent<Actions>();
        
    }


    void Update()
    {
        
        if (transform.parent.transform.GetComponent<PenStats>().farmerPresent == true)
        {
            GetComponent<Button>().interactable = true;
            healthBar.SetActive(true);
            healthBar.transform.GetComponent<Slider>().value = stats._health;
            //statDisplay.SetActive(true);
            currentFarmer = GameObject.Find(popUp.transform.GetComponent<Actions>().occupiedFarmer);
            if (currentFarmer != null)
            {
                if (currentFarmer.GetComponent<MovingFarmer>().currentAnimal == stats._name)
                {
                    //statDisplay.SetActive(true);

                    statDisplay.transform.Find("Animal_Name").GetComponent<TextMeshProUGUI>().text = new string("Name: " + stats._name.ToString());
                    statDisplay.transform.Find("Health").GetComponent<TextMeshProUGUI>().text = new string("Health: " + stats._health.ToString());
                    statDisplay.transform.Find("Hunger").transform.GetComponent<TextMeshProUGUI>().text = new string("Hunger: " + stats._hunger.ToString());
                    statDisplay.transform.Find("Attention").transform.GetComponent<TextMeshProUGUI>().text = new string("Attention: " + stats._love.ToString());
                    statDisplay.transform.Find("Energy").transform.GetComponent<TextMeshProUGUI>().text = new string("Energy: " + stats._energy.ToString());
                    statDisplay.transform.Find("Cleanliness").transform.GetComponent<TextMeshProUGUI>().text = new string("Cleanliness: " + stats._cleanliness.ToString());
                }

                else if (currentFarmer.GetComponent<MovingFarmer>().currentAnimal==null)
                {
                    statDisplay.transform.Find("Animal_Name").GetComponent<TextMeshProUGUI>().text = new string(" ");
                    statDisplay.transform.Find("Health").GetComponent<TextMeshProUGUI>().text = new string(" ");
                    statDisplay.transform.Find("Hunger").transform.GetComponent<TextMeshProUGUI>().text = new string(" ");
                    statDisplay.transform.Find("Attention").transform.GetComponent<TextMeshProUGUI>().text = new string(" ");
                    statDisplay.transform.Find("Energy").transform.GetComponent<TextMeshProUGUI>().text = new string(" ");
                    statDisplay.transform.Find("Cleanliness").transform.GetComponent<TextMeshProUGUI>().text = new string(" ");

                }
            }   
        }

        else if (transform.parent.transform.GetComponent<PenStats>().farmerPresent == false)
        {
            //statDisplay.SetActive(false);
            //popUp.transform.GetComponent<Actions>().cancel();
            healthBar.SetActive(false);
            GetComponent<Button>().interactable = false;

        }

        

    }
}
