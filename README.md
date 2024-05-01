# Unity Genetic Algorithm Simulation

## Overview

This project simulates the movement of agents in a Unity environment using a genetic algorithm. Agents navigate towards a target, and their paths evolve over time to optimize the distance traveled using genetic principles like mutation and crossover.

## Features

- **Agent Simulation:** Each agent has a genome that dictates its movement towards a target. The fitness of each agent is calculated based on its proximity to the target.
- **Genetic Evolution:** The agents' genomes evolve through processes of mutation and crossover, enhancing their ability to reach the target efficiently across generations.

## Components

- `Agent.cs`: Defines the behavior of individual agents, including movement, genome initialization, mutation, and fitness calculation.
- `Population.cs`: Manages a population of agents, handling the lifecycle of genetic algorithms including selection, reproduction, and generation transitions.

## Setup

1. Clone the repository to your local machine.
2. Open the project in Unity.
3. Load the scene containing the `Population` script to initialize and observe the simulation.

## Usage

- Start the Unity scene to watch the agents as they attempt to reach the target.
- Adjust parameters such as genome length, mutation rate, and population size in the `Population` script to see how they affect the evolution of agents.

## Requirements

- Unity 2020.3 or later.

## Demo - Only first iteration shown

https://github.com/brendankariniemi/PathfindingAI/assets/138073658/5264b5c2-4e78-42f6-bfb5-95458fa300bb


