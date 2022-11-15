using UnityEngine;
using System.Collections.Generic;

public class Results : MonoBehaviour
{
    [SerializeField] private PlayerManager pM;

    [SerializeField] private GameObject template;
    private GameObject lastInstantiated;
    PlayerStats[] playersStats;
    private List<GameObject> instantiated = new List<GameObject>();

    void OnEnable()
    {
        playersStats = pM.GetPlayersStats();
        foreach (PlayerStats pS in playersStats)
            InstantiateResults(pS);
    }

    private void InstantiateResults(PlayerStats pS)
    {
        if (lastInstantiated != null)
        {
            Vector3 pos = lastInstantiated.transform.position;
            lastInstantiated = GameObject.Instantiate(template, new Vector3(pos.x, pos.y - lastInstantiated.GetComponent<RectTransform>().rect.height, pos.z), Quaternion.identity, this.transform);
        } else
            lastInstantiated = GameObject.Instantiate(template, template.transform.position, Quaternion.identity, this.transform);
        
        instantiated.Add(lastInstantiated);
        TemplateReferences references = lastInstantiated.GetComponent<TemplateReferences>();
        
        references.ID.text = pS.ID.ToString();
        references.Score.text = pS.Score.ToString();
        references.Wins.text = pS.Wins.ToString();
    }

    public void OnDisable()
    {
        foreach (GameObject go in instantiated)
            Destroy(go);
        instantiated.Clear();
        lastInstantiated = null;
    }
}
