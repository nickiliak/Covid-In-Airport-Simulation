import pandas as pd
from matplotlib import pyplot as plt
import glob
import os

def create_plots_folders(folder_path):
    
    Plots = os.path.join(folder_path, "Plots")
    if os.path.exists(Plots) == True:
        return False
    os.makedirs(Plots)
        

    Graphs = os.path.join(Plots, "Graphs")
    if os.path.exists(Graphs) == True:
        return False
    os.makedirs(Graphs)
    
    Bars = os.path.join(Plots, "Bars")
    if os.path.exists(Bars) == True:
        return False
    os.makedirs(Bars)
    
    return True        

def plot_graph_csv_files(folder_path, PlotsPath):
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
        plt.xlabel('Time')
        plt.ylabel('Agents')
        plt.title('Total Exposed as Time Goes On')

        # Adding a legend to distinguish the lines
        plt.legend()

        # Set the y-axis tick interval to 5
        plt.yticks(range(0, int(max(max(S), max(E), max(I)) + 1), 10))
        
        # Save the plot to a file
        plt.savefig(PlotsPath + "Plots/Graphs/plot" + str(count) + ".png")
        count = count + 1

def plot_stack_line_csv_files(folder_path, PlotsPath):
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

        # Plotting the stacked lines
        plt.stackplot(T, S, E, I, labels=['Susceptible', 'Exposed', 'Infected'], colors=['blue', 'yellow', 'red'])

        # Adding labels and title
        plt.xlabel('Time')
        plt.ylabel('Agents')
        plt.title('Stacked Line Plot: Agent Dynamics Over Time')

        # Adding a legend to distinguish the lines
        plt.legend()
                
        # Save the plot to a file
        plt.savefig(PlotsPath + "Plots/Graphs/plot" + str(count) + ".png")
        count = count + 1
        
def gen_bar_color(bar_name):
    if(bar_name == 'CheckIn Plane'): return 'blue'
    if(bar_name == 'Bathroom (0) Plane'): return 'blue'
    if(bar_name == 'BaggageClaim Plane'): return 'yellow'
    if(bar_name == 'Car Rental Plane'): return 'purple'
    if(bar_name == 'Food Plane'): return 'orange'
    if(bar_name == 'Bathroom (1) Plane'): return 'blue'
    if(bar_name == 'Bathroom (2) Plane'): return 'blue'
    if(bar_name == 'Shop Plane'): return 'orange'
    if(bar_name == 'GatesDep Plane'): return 'red'
    if(bar_name == 'GatesArr Plane'): return 'red'
    if(bar_name == 'Entry Agents Plane'): return 'green'
    if(bar_name == 'Exit Agents Plane'): return 'red'
    
    return 'gray'

    
def plot_bar_csv_files(folder_path, PlotsPath):
    # Use glob to get a list of all CSV files in the folder
    file_list = glob.glob(folder_path + '/*.csv')


    count = 0
    # Loop through each CSV file and plot its data
    for file_path in file_list:
        # Create a new figure for each plot
        plt.figure()

        df = pd.read_csv(file_path)

        # Group the data by "Area" and sum the hits for each area
        area_hit_totals = df.groupby('Area')['Hit'].sum()
        print(area_hit_totals)
        # Print the total hits for each area
        for area, total_hits in area_hit_totals.items():
            #print(f"Area: {area}, Total Hits: {total_hits}")
                plt.bar(area, total_hits, color=gen_bar_color(area))
                plt.xlabel('Area')
                plt.ylabel('Infections')
                plt.title(f'Total Infections')
                plt.xticks(rotation=90)
                plt.tight_layout()

        # Save the plot to a file
        plt.savefig(PlotsPath + "Plots/Bars/bar" + str(count) + ".png")
        count = count + 1


# Get a list of all items (files and subdirectories) in the folder
items = os.listdir('Simulations')                                                                                                                                                                                   
# Filter out the subdirectories
subdirectories = [item for item in items if os.path.isdir(os.path.join('Simulations', item))]
# Get the count of subdirectories
subdirectory_count = len(subdirectories)

for i in range(subdirectory_count):
    if create_plots_folders("Simulations/" + "Simulation" + str(i)) == True:
        plot_graph_csv_files("Simulations/" + "Simulation" + str(i) + "/Datasets/Graphs", "Simulations/" + "Simulation" + str(i) + "/")
        #plot_stack_line_csv_files("Simulations/" + "Simulation" + str(i) + "/Datasets/Graphs", "Simulations/" + "Simulation" + str(i) + "/")
        plot_bar_csv_files("Simulations/" + "Simulation" + str(i) + "/Datasets/Bars", "Simulations/" + "Simulation" + str(i) + "/")


