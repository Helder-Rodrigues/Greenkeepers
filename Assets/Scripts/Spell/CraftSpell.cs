using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class CraftSpell : MonoBehaviour
{
    [SerializeField] private BarController bar;

    [SerializeField] private DetectTrigger spellTrigger;

    [SerializeField] private GameObject trail;
    private GameObject currTrail = null;

    public static Color originalColor = new Color(0, 1, 0, 146 / 255);
    private List<DrawingTrigger> detectedChildrens = new List<DrawingTrigger>();


    private void AddTrail(Collider collider)
    {
        if (collider != null)
        {
            GameObject colliderObject = collider.gameObject;

            if (trail != null)
            {
                if (currTrail != null)
                    RemoveTrail();
                currTrail = Instantiate(trail, colliderObject.transform);
            }
            else
                Debug.LogWarning("Trail GameObject reference is null!");
        }
        else
            Debug.LogWarning("Collider is null!");
    }
    private void RemoveTrail()
    {
        if (currTrail != null)
        {
            //currTrail.GetComponent<TrailRenderer>().Clear();
            Destroy(currTrail);
            currTrail = null;
        }
        else
            Debug.LogWarning("The Trail GameObject added is null or wasn't added!");
    }

    public void ChildDetected(DrawingTrigger child, Collider collider)
    {
        if (detectedChildrens.Count == 0)
        {
            AddTrail(collider);
            detectedChildrens.Add(child);
        }
        else if (detectedChildrens.Last() != child)
        {
            RemoveLastChildColor();

            detectedChildrens.Add(child);

            //spell finished
            if (AllChildrenWereDetected()) //if (detectedChildrens.First() == detectedChildrens.Last())
            {
                DetectSpell();

                RemoveLastChildColor();
                detectedChildrens = new List<DrawingTrigger>();

                RemoveTrail();

                this.gameObject.SetActive(false);
            }
        }
    }
    private bool AllChildrenWereDetected()
    {
        string allChilds = "udlr";

        for (int i = 0; i < allChilds.Length; i++)
            if (!detectedChildrens.Any(c => GameController.ObjectNameContains(c.name, allChilds[i].ToString())))
                return false;
        return true;
    }
    private void RemoveLastChildColor()
    {
        detectedChildrens.Last().gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", originalColor);
    }

    private void DetectSpell()
    {
        Dictionary<string, string> spells = new Dictionary<string, string>();
        spells.Add("TreeGrower", "dulur");
        spells.Add("CleanWater", "rlurud");
        spells.Add("DirtyWater", "rldrdu");

        foreach (var spell in spells)
        {
            if (IdentifySpell(spell.Value))
            {
                switch (spell.Key)
                {
                    case "TreeGrower":
                        TreeGrower();
                        break;
                    case "CleanWater":
                        ClearWater();
                        break;
                    case "DirtyWater":
                        DirtyWater();
                        break;
                }
                break;
            }
        }
    }

    private string GetChildrenObjectName(int index) => detectedChildrens[index].gameObject.name;
    private bool IdentifySpell(string spellName)
    {
        if (detectedChildrens.Count == spellName.Length)
        {
            for (int i = 0; i < spellName.Length; i++)
                if (!GameController.ObjectNameContains(GetChildrenObjectName(i), spellName[i].ToString()))
                    return false;

            return true;
        }
        return false;
    }


    #region Spells
    private void TreeGrower()
    {
        List<GameObject> trees = spellTrigger.GetDetectedObjectsWithName("dead");
        if (trees != null)
        {
            bar.Value += 15;
            trees.ForEach(obj => obj.GetComponent<Tree>().Grow());
        }
    }

    private void ClearWater()
    {

        GameObject river = spellTrigger.GetDetectedObjectWithName("river");
        if (river != null)
        {
            bar.Value += 15;
            river.GetComponent<RiverColorTransition>().CleanWater();
        }
    }

    private void DirtyWater()
    {
        GameObject river = spellTrigger.GetDetectedObjectWithName("river");
        if (river != null)
            river.GetComponent<RiverColorTransition>().DirtyWater();
    }

    #endregion
}
