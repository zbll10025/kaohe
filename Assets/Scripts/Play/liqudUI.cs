using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class liqudUI : MonoBehaviour
{
   public Item liqud;
   public  TextMeshProUGUI Text;
   public string number;
    public Image background;
    public TextMeshProUGUI timeText;
    public PlyerController playercontroll;


    private void Awake()
    {
        Text.text = liqud.itemHeld.ToString();
    }
    private void Update()
    {
        Text.text = liqud.itemHeld.ToString();
        var par = playercontroll.usewait / playercontroll.usewaitstrat;
        background.fillAmount = par;
        if (playercontroll.usewait >1)
        {
            timeText.gameObject.SetActive(true);
            timeText.text =playercontroll.usewait.ToString();
        }
        else if (playercontroll.usewait < 1){
            timeText.gameObject.SetActive(false);
        }
        
    }
}
