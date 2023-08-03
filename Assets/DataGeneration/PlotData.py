import pandas as pd
from matplotlib import pyplot as plt

df = pd.read_csv('Datasets/dataset1.csv')

print(df)
S = df['Susceptible']
E = df['Exposed']
I = df['Infected']
T= df['Time']


# Plotting the lines
plt.plot(T, S, label='At time Susceptible', color='blue')
plt.plot(T, E, label='Total Exposed', color='yellow')
plt.plot(T, I, label='At time Infected', color='red')

# Adding labels and title
plt.xlabel('X-axis')
plt.ylabel('Y-axis')
plt.title('Multiple Lines Plot')

# Adding a legend to distinguish the lines
plt.legend()

plt.savefig("Plots/my_plot1.png")

