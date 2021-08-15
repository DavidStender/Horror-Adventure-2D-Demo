using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A Class used as a middle man to help handle some systems of the game such as the dialog system
 */
public class GameManager : MonoBehaviour
{
    /**The main camera for the scene**/
    private Camera mainCamera;
    /**The player of the game**/
    private Player player;
    /**The system that handles the dialog interactions**/
    private DialogManager dialogManager;
    /**The system that handles the inventory menu**/
    private MenuController menuController;

    /**
     * Setups the components used by the game manager
     */
    private void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        dialogManager = GetComponent<DialogManager>();
        menuController = GetComponent<MenuController>();
    }

    /**
     * Calls the dialog manager to open the dialog window
     * 
     * dialog (string): The text to be displayed
     */
    public void DisplayDialog(string dialog)
    {
        dialogManager.DisplayDialogWindow(dialog);
    }

    /**
     * Calls the dialog manager to continue with the dialog
     */
    public bool ContinueDialog()
    {
         return dialogManager.ContinueDialog();
    }
}
