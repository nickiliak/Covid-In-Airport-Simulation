# Covid-In-Airport-Simulation

A Unity/C# agent-based simulation that recreates Fairbanks International Airport’s terminal to study COVID-19 spread using an SIR-inspired model and configurable mitigation strategies.

---

## Features

- **3D Airport Layout**: Virtual reconstruction of Fairbanks International Airport :contentReference[oaicite:0]{index=0}  
- **Agent-Based Movement**: Passengers follow a weighted decision graph (e.g., 20 % chance to shop) :contentReference[oaicite:1]{index=1}  
- **SIR-Inspired Infection**: Susceptible→Infected→Recovered transitions on proximity encounters :contentReference[oaicite:2]{index=2}  
- **Mitigation Controls**: Adjustable capacity limits, distancing radii, and mask efficacy via Inspector  
- **Inspector Tuning**: Change infection radius, transmission probability, and decision weights on the fly  

---

## Prerequisites

- Unity **2021.3 LTS** or later  
- .NET **4.x** (built into Unity)  
- Git (to clone)  

---

## Quick Start

```bash
git clone https://github.com/nickiliak/Covid-In-Airport-Simulation.git
cd Covid-In-Airport-Simulation

# Open in Unity:
# 1. Launch Unity Hub → Add → select this folder.
# 2. Open Assets/Scenes/MainScene.unity.
# 3. Press Play.

# Or build headlessly:
make
./COVIDSim    # on Linux/macOS
