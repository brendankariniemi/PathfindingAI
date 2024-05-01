using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    internal Vector3 targetPosition;
    internal List<Vector3> genome = new List<Vector3>();
    internal float fitness;
    
    // Genome settings
    private int genomeLength = 3000;
    private float stepSize = 0.3f;
    private float mutationRate = 0.05f;

    // Internal variables
    private int currentStepIndex = 0;
    private bool hasReachedTarget = false;
    

    void Update()
    {
        if (!hasReachedTarget)
        {
            MoveAgent();
            CalculateFitness();
        }
    }

    public void FillGenome()
    {
        for (int i = genome.Count; i < genomeLength; i++)
        {
            // Random.Range with float parameters
            Vector3 randomDirection = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f)).normalized;
            genome.Add(randomDirection);
        }
    }

    void MoveAgent()
    {
        if (currentStepIndex < genome.Count)
        {
            transform.position += genome[currentStepIndex] * stepSize;
            currentStepIndex++;
        
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                hasReachedTarget = true;
                HighlightDone();
            }
        }
    }


    public void CalculateFitness()
    {
        // Use float for distance calculation
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        fitness = 1 / distanceToTarget;
    }

    public void Mutate()
    {
        for (int i = 0; i < genome.Count; i++)
        {
            if (Random.Range(0f, 1f) < mutationRate) // Use float parameters for Random.Range
            {
                Vector3 mutatedDirection = genome[i] + new Vector3(Random.Range(-0.1f, 0.1f), 0, Random.Range(-0.1f, 0.1f));
                genome[i] = mutatedDirection.normalized;
            }
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
            fitness = 0;
        }
    }
    
    public void HighlightChampion()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Color.blue; // Use a distinctive color for the champion
        }
    }
    
    public void HighlightDone()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Color.green;
        }
    }

    public void ApplyDefaultColor()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Color.gray; // Reset to the default color
        }
    }
}
