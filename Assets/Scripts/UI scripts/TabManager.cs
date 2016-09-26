using UnityEngine;
using System.Collections;

public class TabManager : MonoBehaviour {

    private int uiTab;
    public GameObject[] tabs = new GameObject[4]; //This is best modified in the Manager Panel instead of here. Easier and simpler.

	void Start () {  //For init
        uiTab = 0;
        switchTabs(0);
	}

    /// <summary>
    ///     Switches to the designated tab, disabling the panels except the designated tab.
    ///     This is expected to be used in the onClick events in the buttons and via Update KeyUp events.
    ///     The button must have a panel named "Contents" as a child. 
    /// </summary>
    /// <param name="tabNumber">
    ///     The INDEXED tab that should be switched to. Note that 
    ///     the index of the tabs are by the array's stored GameObjects.
    /// </param>
    public void switchTabs(int tabNumber)
    {
        uiTab = tabNumber;
        Debug.Log("Attempting to set indexed tab " + tabNumber + " to active tab");
        for (int i = 0; i < tabs.Length; i++)
        {
            var tabContents = tabs[i].transform.Find("Contents").gameObject;
            if (tabContents != null)
            {
                if (i == tabNumber)
                {
                    tabContents.SetActive(true);
                }
                else tabContents.SetActive(false);
            }
        }
        Debug.Log("Tab switch success!");
    }


    /// <summary>
    /// Gets the tab the manager is currently on.
    /// </summary>
    /// <returns></returns>
    public int getCurrentTab()
    {
        return uiTab;
    }
    /// <summary>
    /// Opens the mini-menu's large menu. 
    /// </summary>
    public void expandTab()
    {
        //TODO: Actually write this
        Debug.Log("Attempt to Expand Tab " + uiTab);
    }

    //Needs to poll keyboard input, since the game is intended for either keyboard or mouse.
    public void Update()
    {
        //TODO: Find a way to recode keys directly to the tabs, so that hard-coded keys and tab numbers aren't required.
        //TODO: Maybe find a way to simplify this? There has to be a more efficient way.
        if (Input.GetKeyUp(KeyCode.M))
        {
            if (uiTab != 0)
                switchTabs(0);
            else
                expandTab();
        }
        else if (Input.GetKeyUp(KeyCode.J))
        {
            if (uiTab != 1)
                switchTabs(1);
            else
                expandTab();
        }
        else if (Input.GetKeyUp(KeyCode.K))
        {
            if (uiTab != 2)
                switchTabs(2);
            else
                expandTab();
        }
        else if (Input.GetKeyUp(KeyCode.L))
        {
            if (uiTab != 3)
                switchTabs(3);
            else
                expandTab();
        }
    }
}
