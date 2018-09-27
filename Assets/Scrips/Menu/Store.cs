using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

public class Store : MonoBehaviour, IPointerClickHandler
{
    private Touch initialTouch = new Touch();
    private float distance = 0;
    private bool swipedSideways;
    private float deltaX;
    private float deltaY;
    private Transform maxHeight;
    private bool navigation;                //if true then uses tahes user to menu or game else navigates store

    private GameObject[] allArrowTogles;    //gets all arrow toggles
    private GameObject[] allTrailTogles;    //gets all trail toggles

    private GameObject[] allArrows;         //gets all arrows
    private GameObject[] allTrails;         //gets all trails

    private GameObject comfirmation;        //comfirmation window for buying items in store
    private Button accept;                  //acctept button for buying items from store
    private Button cancel;                  //cancel button for canceling purchuse in store
    private Text buyText;                   //shows what user is about to buy and price
    private int wasAccepted;                //1 if suer agreed to buy arrow 2 if user agreed to buy trail
    private int selectedIndex;              //points to object that user wants to buy

    //arrow prices
    private int[] arrowPrices = { 0, 10, 50}; 

    //trail prices
    private int[] trailPrices = { 0, 100, 100, 100, 100, 100 };


    // Use this for initialization
    void Start () {
        //---------------comfirmation window------------------
        comfirmation = GameObject.Find("Confirmation");
        comfirmation.SetActive(false);
        wasAccepted = 0;
        //----------------------------------------------------
        
        allArrows = GameObject.FindGameObjectsWithTag("Arrow").OrderBy(go => go.name).ToArray();
        allTrails = GameObject.FindGameObjectsWithTag("Trail").OrderBy(go => go.name).ToArray();

        //---------------grays out items that user dosent own------------------
        for (int i = 0; i < allArrows.Length; i++)
            if (!SaveManager.Instance.IsArrowOwned(int.Parse(allArrows[i].name)))
                allArrows[i].GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 1);

        for (int i = 0; i < allTrails.Length; i++)
            if (!SaveManager.Instance.IsTrailOwned(int.Parse(allTrails[i].name)))
                allTrails[i].GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 1);
        //---------------------------------------------------------------------

        allArrowTogles = GameObject.FindGameObjectsWithTag("ArrowToggle").OrderBy(go => go.name).ToArray();
        allTrailTogles = GameObject.FindGameObjectsWithTag("TrailToggle").OrderBy(go => go.name).ToArray();

        //---------------togles on check mark by selected items and disables all othre toggles------------------
        for (int i = 0; i < allArrowTogles.Length; i++)
        {
            allArrowTogles[i].GetComponent<Toggle>().interactable = false;
            if (allArrowTogles[i].name == SaveManager.Instance.GetArrowSprite())
                allArrowTogles[i].GetComponent<Toggle>().isOn = true;
        }
        for (int i = 0; i < allTrailTogles.Length; i++)
        {
            allTrailTogles[i].GetComponent<Toggle>().interactable = false;
            if (allTrailTogles[i].name == SaveManager.Instance.GetTrail())
                allTrailTogles[i].GetComponent<Toggle>().isOn = true;
        }
        //------------------------------------------------------------------------------------------------------

        navigation = false;
        maxHeight = GameObject.Find("MaxNavigationHeight").GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {

        //buys arrow
        if (wasAccepted == 1 && SaveManager.Instance.GetCoins() >= arrowPrices[selectedIndex])
        {
            SaveManager.Instance.AddCoin(-arrowPrices[selectedIndex]);
            SaveManager.Instance.UnlockArrow(selectedIndex);

            for (int i = 0; i < allArrows.Length; i++)
                if (SaveManager.Instance.IsArrowOwned(int.Parse(allArrows[i].name)))
                    allArrows[i].GetComponent<Image>().color = Color.white;

            allArrowTogles[int.Parse(SaveManager.Instance.GetArrowSprite())].GetComponent<Toggle>().isOn = false;
            SaveManager.Instance.SetArrowSprite(selectedIndex.ToString());
            allArrowTogles[int.Parse(SaveManager.Instance.GetArrowSprite())].GetComponent<Toggle>().isOn = true;

            comfirmation.SetActive(false);
            wasAccepted = 0;
        }

        //buys trail
        if (wasAccepted == 2 && SaveManager.Instance.GetCoins() >= trailPrices[selectedIndex])
        {
            SaveManager.Instance.AddCoin(-trailPrices[selectedIndex]);
            SaveManager.Instance.UnlockTrail(selectedIndex);

            for (int i = 0; i < allTrails.Length; i++)
                if (SaveManager.Instance.IsTrailOwned(int.Parse(allTrails[i].name)))
                    allTrails[i].GetComponent<Image>().color = Color.white;

            allTrailTogles[int.Parse(SaveManager.Instance.GetTrail())].GetComponent<Toggle>().isOn = false;
            SaveManager.Instance.SetTrail(selectedIndex.ToString());
            allTrailTogles[int.Parse(SaveManager.Instance.GetTrail())].GetComponent<Toggle>().isOn = true;

            comfirmation.SetActive(false);
            wasAccepted = 0;
        }
    }

    private void FixedUpdate()
    {

        foreach (Touch t in Input.touches)
        {
            if (Camera.main.ScreenToWorldPoint(t.position).y <= maxHeight.position.y && !navigation)
                navigation = true;
            
            if (t.phase == TouchPhase.Began)
            {
                initialTouch = t;
            }
            else if (t.phase == TouchPhase.Moved)
            {
                deltaX = initialTouch.position.x - t.position.x;
                deltaY = initialTouch.position.y - t.position.y;
                distance = Mathf.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                swipedSideways = Mathf.Abs(deltaX) > Mathf.Abs(deltaY);
            }
            else if (t.phase == TouchPhase.Ended)
            {
                if (distance > 100f)
                {
                    if (swipedSideways && deltaX > 0) //swiped left
                    {
                        if (navigation)
                        {
                            SceneManager.LoadScene("MainMenu");
                            navigation = false;
                        }
                    }
                    else if (swipedSideways && deltaX <= 0) // swiped right
                    {
                        if (navigation)
                        {
                            SceneManager.LoadScene("Game");
                            navigation = false;
                        }

                    }
                    else if (!swipedSideways && deltaY > 0) //swiped down
                    {

                    }
                    else if (!swipedSideways && deltaY <= 0) //swiped up
                    {

                    }
                }
                initialTouch = new Touch();
            }
            navigation = false;

        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //---------------------------------------------Arrows---------------------------------------------
        //if arrow is owned change togle and sprite of arrow
        if (eventData.pointerCurrentRaycast.gameObject.tag == "Arrow" && SaveManager.Instance.IsArrowOwned(int.Parse(eventData.pointerCurrentRaycast.gameObject.name)))
        {
            allArrowTogles[int.Parse(SaveManager.Instance.GetArrowSprite())].GetComponent<Toggle>().isOn = false;
            SaveManager.Instance.SetArrowSprite(eventData.pointerCurrentRaycast.gameObject.name.ToString());
            allArrowTogles[int.Parse(SaveManager.Instance.GetArrowSprite())].GetComponent<Toggle>().isOn = true;
        }
        //if arrow is not owned display purchase window where is shown what user is buying and price
        else if(eventData.pointerCurrentRaycast.gameObject.tag == "Arrow")
        {
            selectedIndex = int.Parse(eventData.pointerCurrentRaycast.gameObject.name);
            comfirmation.SetActive(true);
            buyText = GameObject.Find("BuyText").GetComponent<Text>();
            buyText.text = "Get arrow for " + arrowPrices[selectedIndex] + " coins";
            accept = GameObject.Find("Accept").GetComponent<Button>();
            cancel = GameObject.Find("Cancel").GetComponent<Button>();
            accept.onClick.RemoveAllListeners();
            accept.onClick.AddListener(delegate { onAcceptClick("arrow" ); } );

            cancel.onClick.RemoveAllListeners();
            cancel.onClick.AddListener(onCancleClick);
        }
        //------------------------------------------------------------------------------------------------


        //---------------------------------------------Trails---------------------------------------------
        //if trail is owned change togle and sprite of trail
        if (eventData.pointerCurrentRaycast.gameObject.tag == "Trail" && SaveManager.Instance.IsTrailOwned(int.Parse(eventData.pointerCurrentRaycast.gameObject.name)))
        {
            allTrailTogles[int.Parse(SaveManager.Instance.GetTrail())].GetComponent<Toggle>().isOn = false;
            SaveManager.Instance.SetTrail(eventData.pointerCurrentRaycast.gameObject.name.ToString());
            allTrailTogles[int.Parse(SaveManager.Instance.GetTrail())].GetComponent<Toggle>().isOn = true;
        }
        //if trail is not owned display purchase window where is shown what user is buying and price
        else if (eventData.pointerCurrentRaycast.gameObject.tag == "Trail")
        {
            selectedIndex = int.Parse(eventData.pointerCurrentRaycast.gameObject.name);
            comfirmation.SetActive(true);
            buyText = GameObject.Find("BuyText").GetComponent<Text>();
            buyText.text = "Get trail for " + trailPrices[selectedIndex] + " coins";
            accept = GameObject.Find("Accept").GetComponent<Button>();
            cancel = GameObject.Find("Cancel").GetComponent<Button>();
            accept.onClick.RemoveAllListeners();
            accept.onClick.AddListener(delegate { onAcceptClick("trail"); });

            cancel.onClick.RemoveAllListeners();
            cancel.onClick.AddListener(onCancleClick);
        }
        //-------------------------------------------------------------------------------------------------

    }

    /// <summary>
    /// executes when accept is clicked
    /// </summary>
    /// <param name="arrowOrTrail"></param>
    private void onAcceptClick(string arrowOrTrail)
    {
        if (arrowOrTrail == "arrow")
            wasAccepted = 1;

        if (arrowOrTrail == "trail")
            wasAccepted = 2;
    }

    /// <summary>
    /// executes when cancle is clicked
    /// </summary>
    private void onCancleClick()
    {
        wasAccepted = 0;
        comfirmation.SetActive(false);
    }

}
