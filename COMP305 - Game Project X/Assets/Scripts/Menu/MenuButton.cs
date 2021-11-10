using System.Collections;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    [SerializeField] MenuButtonController menuButtonController;
    [SerializeField] Animator animator;
    [SerializeField] AnimatorFunctions animatorFunctions;
    [SerializeField] int thisIndex;

    // Update is called once per frame
    void Update()
    {
        if (menuButtonController.index == thisIndex)
        {
            if (!animator.GetBool("pressed"))
            {
                animator.SetBool("selected", true);
            }
            if (Input.GetAxis("Submit") == 1)
            {
                animator.SetBool("pressed", true);
                animator.SetBool("selected", false);
                StartCoroutine(SwitchMenu());
            }
            else if (animator.GetBool("pressed"))
            {
                animatorFunctions.disableOnce = true;
            }
        }
        else
        {
            animator.SetBool("selected", false);
        }
    }
    private IEnumerator SwitchMenu()
    {
        yield return new WaitForSeconds(0.26f);
        menuButtonController.GetComponent<MainMenuController>().ButtonPressed(thisIndex);
    }
}
