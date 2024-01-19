using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AnimationUI : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] bool isOpen;
    [SerializeField] GameObject contentToEnable;

    [SerializeField] GameObject openIcon, closeIcon;

    public void OpenPanel()
    {
        if (panel != null)
        {
            Animator anim = panel.GetComponent<Animator>();
            if (anim != null)
            {
                isOpen = anim.GetBool("open");

                anim.SetBool("open", !isOpen);
                Invoke("SetContentActive", .7f);            }
        }
    }

    private void SetContentActive()
    {
        if (!isOpen && contentToEnable != null)
        {
            contentToEnable.SetActive(true);
        }
    }

    private void Awake()
    {
        closeIcon.SetActive(true);
        openIcon.SetActive(false);
    }

    private void Update()
    {
        if (isOpen)
        {
            if (contentToEnable != null)
            {
                contentToEnable.SetActive(false);

            }

            closeIcon.SetActive(false);
            openIcon.SetActive(true);
        }
        else
        {
            closeIcon.SetActive(true);
            openIcon.SetActive(false);
        }
    }

}
