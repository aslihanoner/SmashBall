using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance;

    public GameObject FinishUI, GameOverUI;
    public Image LevelSlider;
    public Image CurrentLevelImage;
    public Image NextLevelImage;
    public Material PlayerMaterial;
    public PlayerController player;

    [Header("Finish")]
    public Text leveltxt;
    public Text completedtxt;
    public Text LevelText;

    [Header("GameOver")]
    public Text gameovertxt;
    public Text scoretxt;
    public Text scorenumbertxt;
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        PlayerMaterial = FindObjectOfType<PlayerController>().transform.GetChild(0).GetComponent<MeshRenderer>().material;
        player = FindObjectOfType<PlayerController>();

        LevelSlider.transform.GetComponent<Image>().color = PlayerMaterial.color + Color.red;
        LevelSlider.color = PlayerMaterial.color;
        CurrentLevelImage.color = PlayerMaterial.color;
        NextLevelImage.color = PlayerMaterial.color;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && player.playerState == PlayerController.PlayerState.Prepare)
        {
            player.playerState = PlayerController.PlayerState.Playing;

        }
    }

    private bool IgnoreUI()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        for(int i=0; i < raycastResults.Count; i++)
        {
            if (raycastResults[i].gameObject.GetComponent<IgnoreGameUI>() != null)
            {
                raycastResults.RemoveAt(i);
                i--;
            }
        }
        return raycastResults.Count > 0;
    }

    public void LevelSliderFill(float fillAmount)
    {
        LevelSlider.fillAmount = fillAmount;
    }

    public void ShowFinishUI(int levelNumber, int completedLevels)
    {
        FinishUI.SetActive(true);
        GameOverUI.SetActive(false);

        leveltxt.text = "Level " + levelNumber;
        completedtxt.text = "Completed Levels: " + completedLevels;
        LevelText.text = "Level " + levelNumber;
    }

    public void ShowGameOverUI(int score)
    {
        FinishUI.SetActive(false);
        GameOverUI.SetActive(true);

        scorenumbertxt.text = score.ToString();

    }

}
