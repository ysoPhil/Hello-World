using CardGameUIEffect;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UnitType { CHARACTER, MONSTER,NONE}
public enum UnitStatus { ACTIVE,UNACTIVE}
public class Unit : MonoBehaviour
{
    #region Parameters
    [SerializeField]
    private string unitName;

    private int unitLevel;

    [SerializeField]
    private int powerScore;

    [SerializeField]
    private int abilityScore;

    [SerializeField]
    private int skillScore;

    [SerializeField]
    private int maxHp;

    [SerializeField]
    private int currentHp;

    [SerializeField]
    private UnitType type;
    


    private int id;

    private UnitStatus status = UnitStatus.ACTIVE;

    #endregion

    #region private Methods


    private void onAttack(Unit target,CardType type)
    {
        int score = TypeSwitch(type);
        
        target.setUnitCurrentHP(target.getUnitCurrentHP() - score);
    }

    private int TypeSwitch(CardType type)
    {
        switch (type)
        {
            case CardType.ABILITY:
                return this.abilityScore;
            case CardType.POWER:
                return this.powerScore;
            case CardType.SKILL:
                return this.skillScore;
            default:
                Debug.LogError("onAttack::Invalid CARDTYPE");
                return 0;
        }

    }

    private void onHeal(Unit target, CardType type)
    {
        int score = TypeSwitch(type);
        target.setUnitCurrentHP(target.getUnitCurrentHP() + score);
        if(target.getUnitCurrentHP() > target.getUnitMaxHP())
        {
            target.setUnitCurrentHP(target.getUnitMaxHP());
        }
    }


    private void onCardActivation(Unit target, CardData data)
    {
        var cardFunction = data.GetCardFunction();
        var cardType = data.GetCardType();
        
        //first switch Checks what type of card is being activated, so appropriate skill can be used
        switch (cardFunction)
        {
            case CardFunction.ATTACK:
                this.onAttack(target, cardType);
                break;
            case CardFunction.HEAL:
                this.onHeal(target, cardType);
                break;
            default:
                Debug.LogError("onCardActivation::Invalid CardFunction");
                break;
        }
    }
    #endregion

    #region LifeCycle

    private void Start()
    {
        EventManager.current.onCardActivation += onCardActivation;
        unitLevel = powerScore + abilityScore + skillScore;
    }

    private void Update()
    {
        if (currentHp <= 0 & status != UnitStatus.UNACTIVE)
        {
            if (type.Equals(UnitType.MONSTER)) {
                Destroy(this.gameObject);
                Destroy(this);
            }
            else
            {
                currentHp = 0;
                this.status = UnitStatus.UNACTIVE;
            }
        }

    }

    #endregion

    #region Getter/Setters
    public string getUnitName() { return this.name; }
    public int getUnitCurrentHP() { return this.currentHp; }
    public void setUnitCurrentHP(int hp) { this.currentHp = hp; }
    public int getUnitMaxHP() { return maxHp; }
    public int getId() { return id; }
    public void setId(int id) { this.id = id; }

    public UnitType getUnitType(){ return this.type; }
    #endregion

}
