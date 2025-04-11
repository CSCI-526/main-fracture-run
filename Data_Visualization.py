import requests
import pandas as pd
import matplotlib.pyplot as plt
from io import StringIO
import numpy as np

def plot_game_over_reason_distribution(df):
    scene_column = "Scene"
    category_column = "Game_Over_Reason"

    scenes = df[scene_column].unique()
    num_scenes = len(scenes)
    cols = 3
    rows = (num_scenes + cols - 1) // cols

    fig, axes = plt.subplots(rows, cols, figsize=(5 * cols, 5 * rows))
    axes = axes.flatten()

    fig.suptitle(f"{category_column} Distribution by Scene", fontsize=20)

    for i, scene in enumerate(scenes):
        scene_data = df[df[scene_column] == scene]
        category_counts = scene_data[category_column].value_counts()
        axes[i].pie(category_counts, labels=category_counts.index, autopct='%1.1f%%', startangle=140)
        axes[i].set_title(f"{scene}\nTotal: {category_counts.sum()}", fontsize=12)

    for j in range(len(scenes), len(axes)):
        axes[j].axis('off')

    plt.subplots_adjust(top=0.88)

    plt.savefig("./data_visulization/game_over_distribution.png", dpi=300)

def plot_distance_histogram(df):
    if 'Distance' in df.columns:
        df['Distance'] = pd.to_numeric(df['Distance'], errors='coerce')
        df = df.dropna(subset=['Distance'])  

        bin_width = 50
        max_distance = df['Distance'].max()
        bins = np.arange(0, max_distance + bin_width, bin_width)
        plt.figure(figsize=(10, 6))
        plt.hist(df['Distance'], bins=bins, edgecolor='black')
        plt.title("Player Survival Distance Distribution (per 50 units)")
        plt.xlabel("Distance Range")
        plt.ylabel("Count")
        plt.grid(axis='y', linestyle='--', alpha=0.7)
        plt.tight_layout()
        plt.savefig("./data_visulization/distance_histogram.png", dpi=300)
    else:
        print("No 'Distance' column found in the dataset.")

def plot_death_rate_distribution(df):
    scene_column = "Scene"

    scene_counts = df[scene_column].value_counts()

    plt.figure(figsize=(8, 8))
    plt.pie(scene_counts, labels=scene_counts.index, autopct='%1.1f%%', startangle=140)
    plt.title("Death Rate Distribution", fontsize=16, y=1.05)
    plt.axis('equal')  
    plt.savefig("./data_visulization/scene_distribution_single_pie.png", dpi=300)

def plot_net_gain(df):
    scene_columns = ["StartScene", "JYScene", "ElsaScene", "JiayuScene", "JingxuanScene", "SerenaScene", "ShujieScene"]

    cols = 3
    rows = (len(scene_columns) + cols - 1) // cols

    fig, axes = plt.subplots(rows, cols, figsize=(5 * cols, 4 * rows))
    axes = axes.flatten() 
    fig.suptitle(f"Net Score Gain Distribution by Scene", fontsize=20)

    for i, column in enumerate(scene_columns):
        scene_counts = df[column].value_counts().sort_index()
        x_values = range(len(scene_counts))

        axes[i].scatter(x_values, scene_counts, color='dodgerblue', marker='o')
        axes[i].set_xticks(x_values)
        axes[i].set_xticklabels(scene_counts.index, rotation=45, ha="right")
        axes[i].set_title(f"{column} Distribution")
        axes[i].set_ylabel("Frequency")

    for j in range(len(scene_columns), len(axes)):
        axes[j].axis('off')

    plt.tight_layout()
    # plt.show()
    plt.subplots_adjust(top=0.88)
    plt.savefig("./data_visulization/net_score_gain.png", dpi=300)


# Your Google Sheets CSV link
sheet_url = "https://docs.google.com/spreadsheets/d/1lBKyh2hF4yTiWB3mJKe_25lsSgrpPnK5eVrXXvDRnyU/gviz/tq?tqx=out:csv&gid=0"

try:
    response = requests.get(sheet_url)
    response.raise_for_status()
    csv_data = StringIO(response.text)
    df = pd.read_csv(csv_data)
except requests.exceptions.RequestException as e:
    print("Request failed:", e)



plot_game_over_reason_distribution(df)
plot_distance_histogram(df)
plot_death_rate_distribution(df)
plot_net_gain(df)