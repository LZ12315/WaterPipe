using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using System;

[Serializable]
public class DicNode
{
    public WaterNodeType nodeType;
    public List<IWaterRelated> connectNodes = new List<IWaterRelated>();
}

public class WaterNodeManager : MonoBehaviour
{
    public static WaterNodeManager Instance;
    [SerializeField] private Dictionary<IWaterRelated,DicNode> pipeDictionary = new Dictionary<IWaterRelated, DicNode>();
    [SerializeField] private List<IWaterRelated> waterSources = new List<IWaterRelated>();
    public event Action<WaterNodeManager> SetWaterContainsOff;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
        {
            Destroy(Instance);
            Instance = this;
        }
    }

    public void AddNode(IWaterRelated addedNode, List<IWaterRelated> waterNodes)
    {
        if (pipeDictionary.ContainsKey(addedNode))
        {
            NodeChange(addedNode, waterNodes);
            return;
        }

        pipeDictionary[addedNode] = new DicNode();
        pipeDictionary[addedNode].connectNodes = waterNodes;
        pipeDictionary[addedNode].nodeType = addedNode.NodeType;

        if (pipeDictionary[addedNode].nodeType == WaterNodeType.Source)
            waterSources.Add(addedNode);
        else
            SetWaterContainsOff += (value) => addedNode.SetWaterBreak(value);

        foreach (var pipe in pipeDictionary[addedNode].connectNodes)
        {
            if (pipeDictionary.ContainsKey(pipe) && !pipeDictionary[pipe].connectNodes.Contains(addedNode))
                pipeDictionary[pipe].connectNodes.Add(addedNode);
        }
    }

    public void NodeChange(IWaterRelated addedNode, List<IWaterRelated> waterNodes)
    {
        if (!pipeDictionary.ContainsKey(addedNode))
            return;
        pipeDictionary[addedNode].connectNodes = waterNodes;
    }

    public void DeleteNode(IWaterRelated addedNode)
    {
        if (!pipeDictionary.ContainsKey(addedNode))
            return;

        foreach (var pipe in pipeDictionary[addedNode].connectNodes)
        {
            if (pipeDictionary.ContainsKey(pipe) && pipeDictionary[pipe].connectNodes.Contains(addedNode))
                pipeDictionary[pipe].connectNodes.Remove(addedNode);
        }

        pipeDictionary.Remove(addedNode);
        if (waterSources.Contains(addedNode))
            waterSources.Remove(addedNode);

        WaterDivertion();
    }

    public void WaterContainsCheck(IWaterRelated startNode)
    {
        WaterDivertion();
    }

    private void WaterDivertion()
    {
        SetWaterContainsOff.Invoke(this);
        foreach (var source in waterSources)
            DFS(source);
    }

    private void DFS(IWaterRelated startNode)
    {
        HashSet<IWaterRelated> visited = new HashSet<IWaterRelated>();
        DFSRecursive(startNode, visited);
    }

    private void DFSRecursive(IWaterRelated node, HashSet<IWaterRelated> visited)
    {
        if (visited.Contains(node))
            return;

        visited.Add(node);
        node.WaterDivertion();

        if (pipeDictionary.ContainsKey(node))
        {
            foreach (var neighbor in pipeDictionary[node].connectNodes)
                DFSRecursive(neighbor, visited);
        }
    }

}
