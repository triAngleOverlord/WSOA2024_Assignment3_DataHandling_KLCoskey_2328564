using System;
using UnityEngine;

[CreateAssetMenu]
public class AnimalStats : ScriptableObject
{
    [SerializeField] public string _name;
    [SerializeField] public animalType type;
    [SerializeField] public float maxHealth;
    [SerializeField] public float maxLove;
    [SerializeField] public float maxHunger;
    [SerializeField] public float maxEnergy;
    [SerializeField] public float maxCleanliness;
    [SerializeField] public float _startHealthRate;

    [Header("Satisfaction")]
    public float _feed;
    public float _hug;
    public float _bath;
    public float _sleep;

    [Header("Changing Stats")]
    public float _health;
    public float _love;
    public float _hunger;
    public float _energy;
    public float _cleanliness;
    public float _healthRateLoss;

    [Header("Stats Zero")]
    public bool zLove;
    public bool zHunger;
    public bool zEnergy;
    public bool zCleanliness;


    public void Start()
    {
        _health = maxHealth;
        _love = 1;
        _hunger = maxHunger;
        _energy = maxEnergy;
        _cleanliness = maxCleanliness;
        _healthRateLoss = 0;
        _sleep = maxEnergy * 0.05f;

        animalRequirements(type);
        
    }

    public void allStatsDecrease()
    {
        //_health--;
        _love-= maxLove*0.05f;
        checkCurrent(_love, maxLove, 0.01f, zLove);

        _hunger-= maxHunger * 0.05f;
        checkCurrent(_hunger, maxHunger, 0.02f, zHunger);

        _cleanliness -= maxCleanliness * 0.05f;
        checkCurrent(_cleanliness, maxCleanliness, 0.01f, zCleanliness);

        _energy -=_sleep;
        checkCurrent(_energy, maxEnergy, 0.03f, zEnergy);

        _health -= maxHealth * _healthRateLoss;
        if(_health<=0)
        {
            Debug.Log(_name + " has died");
        }
    }

    public void newHealthRate()
    {
        if (_hunger == maxHunger/2)
        {
            _healthRateLoss= _healthRateLoss/maxHunger;
        }

        if (_love == maxLove / 2)
        {
            _healthRateLoss = _startHealthRate / maxLove;
            
        }

        if (_energy == _energy / 2)
        {
            _healthRateLoss = _startHealthRate / maxEnergy;
        }

        if (_cleanliness == maxCleanliness / 2)
        {
            _healthRateLoss = _healthRateLoss / maxCleanliness;
        }
        
    }

    public void animalRequirements(animalType type)
    {
        this.type = type;

        switch(type)
        {
            case animalType.PolarBear: _feed = 30;_hug = 10;_bath = 15;
                break;
            case animalType.ArticFox: _feed = 15; _hug = 5; _bath = 30;
                break;
            case animalType.Seal: _feed = 20; _hug = 10; _bath = 35;
                break;
            default: throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

    public enum animalType
    {
        PolarBear, ArticFox, Seal
    }

    public float checkCurrent(float current, float max, float loss, bool zeroR)
    {
        if (current > max)
        {
            current = max;
            zeroR = false;
        }
        if (current<0)
        {
            current = 0;
            zeroR = true;
        }

        if (current == 0)
        {
            

            if (zeroR==false)
            {
                _healthRateLoss += maxHealth * loss;
                zeroR = true;
            }
            
        }
        Debug.Log(current);
        return current;



    }
}
