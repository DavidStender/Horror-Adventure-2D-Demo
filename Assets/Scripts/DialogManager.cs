using UnityEngine;
using UnityEngine.UI;

/**
 * A class that attaches to the Game Manager. Manages the Dialog Window and displays the text from a
 * DialogObject
 */
public class DialogManager : MonoBehaviour
{
    /**The game object that contains the dialog UI**/
    public GameObject dialogWindow;
    /**The text to be displayed**/
    public Text dialogText;

    /**If the dialog window is active or not**/
    private bool showingDialog = false;

    /**
     * Sets up the dialogManager while the object is being loaded
     */
    private void Awake()
    {
        dialogWindow.SetActive(true);
        dialogWindow.SetActive(false);
    }

    /**
     * Activates the dialog window game object and sets it text component to be dialogText
     * 
     * dialog (string): the text to be displayed
     */
    public void DisplayDialogWindow(string dialog)
    {
        dialogText.text = dialog;
        dialogWindow.SetActive(true);
    }

    /**
     * Checks if there is more dialog to be displayed.
     */
    public bool ContinueDialog()
    {
        if (dialogText.text.Length > 96)
        {
            dialogText.text = dialogText.text.Substring(96);
            return true;
        }
        else
        {
            CloseDialogWindow();
            return false;
        }
    }

    /**
     * Deactivates the dialog window game object
     */
    public void CloseDialogWindow()
    {
        dialogWindow.SetActive(false);
    }

    /**
     * Returns showingDialog
     */
    public bool GetShowingDialog()
    {
        return showingDialog;
    }
}
