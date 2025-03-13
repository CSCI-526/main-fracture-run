import requests
import pandas as pd
import matplotlib.pyplot as plt
from io import StringIO

# Your Google Sheets CSV link
sheet_url = "https://docs.google.com/spreadsheets/d/1lBKyh2hF4yTiWB3mJKe_25lsSgrpPnK5eVrXXvDRnyU/gviz/tq?tqx=out:csv&gid=0"

try:
    # Send a request
    response = requests.get(sheet_url)
    response.raise_for_status()  # Ensure the request is successful
    
    # Parse CSV data
    csv_data = StringIO(response.text)
    df = pd.read_csv(csv_data)

    # print(df.head())  # Print the first few rows

except requests.exceptions.RequestException as e:
    print("Request failed:", e)

scene_column = "Scene"
category_column = "Game_Over_Reason"

# Get all unique scenes
scenes = df[scene_column].unique()

# Create a figure and dynamically generate subplots based on the number of scenes
fig, axes = plt.subplots(1, len(scenes), figsize=(6 * len(scenes), 6))

if len(scenes) == 1:  # If there is only one scene, axes is not a list
    axes = [axes]

# Iterate through each scene and plot the corresponding pie chart
for i, scene in enumerate(scenes):
    scene_data = df[df[scene_column] == scene]  # Filter data for the specific scene
    category_counts = scene_data[category_column].value_counts()  # Count occurrences of each category

    # Plot pie chart
    axes[i].pie(category_counts, labels=category_counts.index, autopct='%1.1f%%', startangle=140)
    axes[i].set_title(f"{scene} - {category_column} - total count = {category_counts.sum()}")

# Display the chart
plt.tight_layout()
plt.show()
