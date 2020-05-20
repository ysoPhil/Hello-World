using CardGameUIEffect;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{

    [Tooltip("Text Box for the card name")]
    [SerializeField]
    private Text card_name;



    [Tooltip("Image for the card")]
    [SerializeField]
    private Image card_artwork;

    public void setDisplay(CardData data)
    {
        this.card_name.text = data.GetCardName();
        this.card_artwork.sprite = data.GetArtWork();
        
        //Makes the artwork right side up
        this.card_artwork.transform.localRotation = Quaternion.Euler(180,0, 0);
    }


}
