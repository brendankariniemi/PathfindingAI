using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Population : MonoBehaviour
{
    public GameObject agentPrefab;
    public int populationSize = 100;
    public int eliteCount = 10;
    public int genomeIncrement = 100;
    public Vector3 targetPosition = new Vector3(19.2f, 0, 20.3f);
    private List<Agent> population = new List<Agent>();
    private bool isEvolving = false;
    private float generationTime = 5f;
    private Agent currentChampion = null;

    void Start()
    {
        InitializePopulation();
    }

    void Update()
    {
        if (!isEvolving)
        {
            StartCoroutine(Evolve());
        }
        
        Agent newChampion = FindHighestFitnessAgent();
        if (newChampion != currentChampion)
        {
            // Reset highlight of the previous champion
            if (currentChampion != null)
            {
                currentChampion.ApplyDefaultColor();
            }
            
            // Highlight the new champion
            currentChampion = newChampion;
            currentChampion.HighlightChampion();
        }
    }

    IEnumerator Evolve()
    {
        isEvolving = true;
        yield return new WaitForSeconds(generationTime); // Wait for the generation to finish

        // Sort the population based on fitness
        population = population.OrderBy(agent => -agent.fitness).ToList();

        // Create a new population list to hold new offspring
        List<Agent> newPopulation = new List<Agent>();

        // Keep the elite agents as is (without mutation)
        for (int i = 0; i < eliteCount; i++)
        {
            Agent elite = Instantiate(agentPrefab, transform.position, Quaternion.identity).GetComponent<Agent>();
            elite.ApplyDefaultColor();
            elite.genome = new List<Vector3>(population[i].genome);
            elite.targetPosition = targetPosition;
            newPopulation.Add(elite);
        }
        
        // Crossover and mutation to form new population
        for (int i = eliteCount; i < populationSize; i++)
        {
            Agent parent1 = ChooseParent();
            Agent parent2 = ChooseParent();
            Agent offspring = Instantiate(agentPrefab, transform.position, Quaternion.identity).GetComponent<Agent>();
            offspring.ApplyDefaultColor();
            offspring.targetPosition = targetPosition;
            Crossover(parent1, parent2, offspring);
            offspring.Mutate();
            newPopulation.Add(offspring);
        }

        // Destroy all current agents
        foreach (Agent agent in population)
        {
            Destroy(agent.gameObject);
        }

        // Replace the old generation with the new one
        population = newPopulation;
        isEvolving = false;
    }

    void InitializePopulation()
    {
        for (int i = 0; i < populationSize; i++)
        {
            Agent agent = Instantiate(agentPrefab, transform.position, Quaternion.identity).GetComponent<Agent>();
            agent.ApplyDefaultColor();
            agent.targetPosition = targetPosition;
            agent.FillGenome();
            population.Add(agent);
        }
    }

    Agent ChooseParent()
    {
        // Simple roulette wheel selection
        float totalFitness = population.Sum(agent => agent.fitness);
        float randomPoint = Random.value * totalFitness;

        float currentSum = 0;
        foreach (Agent agent in population)
        {
            currentSum += agent.fitness;
            if (currentSum >= randomPoint)
            {
                return agent;
            }
        }

        return population.Last();
    }

    void Crossover(Agent parent1, Agent parent2, Agent offspring)
    {
        // Make sure parents have genomes of the same length
        int minLength = Mathf.Min(parent1.genome.Count, parent2.genome.Count);
        int crossoverPoint = Random.Range(0, minLength);

        offspring.genome.Clear();

        for (int i = 0; i < crossoverPoint; i++)
        {
            offspring.genome.Add(parent1.genome[i]);
        }
        for (int i = crossoverPoint; i < minLength; i++)
        {
            offspring.genome.Add(parent2.genome[i]);
        }
    }
    
    private Agent FindHighestFitnessAgent()
    {
        float highestFitness = float.MinValue;
        Agent championAgent = null;

        foreach (var agent in population)
        {
            if (agent.fitness > highestFitness)
            {
                highestFitness = agent.fitness;
                championAgent = agent;
            }
        }

        return championAgent;
    }
}
