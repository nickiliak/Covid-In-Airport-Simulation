# Covid-In-Airport-Simulation

A Unity/C# agent-based simulation that reconstructs Fairbanks International Airport’s terminal to explore COVID-19 spread using an SIR-inspired model and configurable mitigation settings.

---

## Features

- **3D Airport Layout**  
  Virtual rebuild of Fairbanks International Airport terminal.  
- **Agent Movement**  
  Passengers navigate via weighted decision graphs (e.g., chance to shop, wait, board).  
- **SIR-Style Infection**  
  Susceptible → Infected → Recovered transitions based on proximity and timing.  
- **Mitigation Controls**  
  Tweak capacity caps, social-distancing radius, and mask-efficacy in real time.  
- **Inspector Tuning**  
  All key variables exposed in Unity’s Inspector for on-the-fly adjustments.

---

## Prerequisites

- Unity **2021.3 LTS** or newer  
- .NET **4.x** (built-in with Unity)  
- Git (to clone the repo)

---

## Quick Start

```bash
git clone https://github.com/nickiliak/Covid-In-Airport-Simulation.git
cd Covid-In-Airport-Simulation

# In Unity:
# 1. Open Unity Hub → Add project → select this folder.
# 2. Open Assets/Scenes/MainScene.unity.
# 3. Press Play.

# From command line (headless build):
make
./COVIDSim    # Linux/macOS
