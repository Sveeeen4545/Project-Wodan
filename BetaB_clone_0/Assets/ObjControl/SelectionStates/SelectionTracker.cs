using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionTracker : MonoBehaviour
{
    public GameObject[] HazardEffects = new GameObject[5];

    public GameObject dropperPrefab; 
    private SceneObjControl _selection;

    private Animator animator;

    public SceneObjControl Selection
    {
        get { return _selection; }
    }

    public void SetSelection(SceneObjControl newSelection)
    {
        _selection = newSelection;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetState(int state)
    {
        animator.SetInteger("SelectionState", state);
    }

    public void Delete()
    {
        Selection.RequestRemoval();
        animator.SetInteger("SelectionState", 0);
    }
}
