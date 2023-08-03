import pandas as pd
from matplotlib import pyplot as plt
import glob

def plot_csv_files(folder_path):
    # Use glob to get a list of all CSV files in the folder
    file_list = glob.glob(folder_path + '/*.csv')

    count = 0
    # Loop through each CSV file and plot its data
    for file_path in file_list:
        # Create a new figure for each plot
        plt.figure()

        # Read the CSV data into a pandas DataFrame
        df = pd.read_csv(file_path)

        S = df['Susceptible']
        E = df['Exposed']
        I = df['Infected']
        T = df['Time']

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

        # Save the plot to a file
        plt.savefig("Plots/plot" + str(count) + ".png")

        count = count + 1
    
    
plot_csv_files('Datasets')


