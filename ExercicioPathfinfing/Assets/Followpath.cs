﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Followpath : MonoBehaviour
{
    //Olha para o Waypoint
    Transform goal;
    //Seta a velocidade do tanque
    public float speed = 5.0f;
    //Seta a accuracy do tanque
    public float accuracy = 1.0f;
    //Seta a suavidade da rotação do tanque
    public float rotSpeed = 2.0f;
    //Setar o objeto que tem o codigo do WPManager
    public GameObject wpManager;
    //Pega a posição do waypoint a lista
    GameObject[] wps;
    //Links
    GameObject currentNode;
    //Waypoint atual
    int currentWP = 0;
    //Pega os componentes do codigo graph
    Graph g;

    void Start()
    {
        //Pega os componentes do WPManager
        wps = wpManager.GetComponent<WPManager>().waypoints;
        g = wpManager.GetComponent<WPManager>().graph;
        //Seta o waypoint atual no element 0
        currentNode = wps[0];
    }

    //Função para levar uma posição especifica no caso o heliporto
    public void GoToHeli()
    {
        //Seta o local da lista que o tanque deve parar
        g.AStar(currentNode, wps[1]);
        //Ao chegar no ponto a lista volta para 0
        currentWP = 0;
    }

    //Função para levar uma posição especifica no caso as ruinas
    public void GoToRuin()
    {
        //Seta o local da lista que o tanque deve parar
        g.AStar(currentNode, wps[5]);
        //Ao chegar no ponto a lista volta para 0
        currentWP = 0;
    }

    void LateUpdate()
    {
        //Se a posição do waypoint for 0 fique parado
        if (g.getPathLength() == 0 || currentWP == g.getPathLength())
            return;
        //Waypoint mais proximo
        currentNode = g.getPathPoint(currentWP);

        //Ao chegar em um waypoint vá para o proximo
        if (Vector3.Distance(g.getPathPoint(currentWP).transform.position, transform.position) < accuracy)
        {
            //Aumenta em 1 na contagem
            currentWP++;
        }

        //Movimentação para fazer a volta
        if (currentWP < g.getPathLength())
        {
            //Faz o tanque ir para um waypoint proximo
            goal = g.getPathPoint(currentWP).transform;
            Vector3 lookAtGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z);
            Vector3 direction = lookAtGoal - this.transform.position;
            //Ajuda a suavisar a rotação do tanque
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);
        }
        //Faz o tanque se mover
        transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
