using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    // Start is called before the first frame update
    public Image levelSlider;
    public Image currentLevelImg;
    public Image nextlevelImg;

    public GameObject settingBTN;
    public GameObject allBTN;

    public GameObject soundONBTN;
    public GameObject soundOFFBTN;
    public bool soundOnOffBo;

    public bool buttonSettingBo;
    
    public Material playerMat;

    private PlayerController player;
    [SerializeField] private GameObject homeUI;
    [SerializeField] private GameObject gameUI;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private bool IgnoreUI()
    {
        return false;
        
    }

    public void LevelSliderFill(float fillAmount)
    {
        
    }

    public void settingShow()
    {
        
    }

  
    
    
}
