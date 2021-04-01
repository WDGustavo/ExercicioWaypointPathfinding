using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Faz as variaveis aparecerem no inspector
[System.Serializable]

public struct Link
{
    //Cria o tipo de direção dos links do waypoint UNI so tem uma direção e BI pode ir e voltar pelo link
    public enum direction {UNI, BI}
    //Seta os links entre os waypoints
    public GameObject node1;
    public GameObject node2;
    //Seta a direção dos links
    public direction dir;
}

public class WPManager : MonoBehaviour
{
    //Cria uma lista de Waypoints
    public GameObject[] waypoints;
    //Cria uma lista de Links
    public Link[] links;
    //Pega informações do codigo Graph
    public Graph graph = new Graph();

    void Start()
    {
        //Quando a lista estiver maior que 0 faça os loops
        if(waypoints.Length > 0)
        {
            //Faz um loop dos Waypoints
            foreach(GameObject wp in waypoints)
            {
                //Adiciona um novo Waypoint
                graph.AddNode(wp);
            }
            //Faz o loop de Links
            foreach(Link l in links)
            {
                graph.AddEdge(l.node1, l.node2);
                //Caso o link tenha a direção BI faz a condição dele ir e voltar
                if (l.dir == Link.direction.BI)
                    graph.AddEdge(l.node2, l.node1);
            }
        }
    }

    void Update()
    {
        //Mostra as linhas formadas pelos Links
        graph.debugDraw(); 
    }
}
