import firebase_admin
from firebase_admin import credentials, firestore
import pandas as pd
import matplotlib.pyplot as plt

# Firebase Initialization
cred = credentials.Certificate("firebase_credentials.json")
firebase_admin.initialize_app(cred)
db = firestore.client()

def fetch_data():
    docs = db.collection("playtest_data").stream()
    data = [doc.to_dict() for doc in docs]
    return pd.DataFrame(data)

def visualize_data(df):
    plt.figure(figsize=(12, 6))

    # Plot Completion Time
    plt.subplot(2, 2, 1)
    plt.hist(df["completion_time"], bins=10, color="blue", alpha=0.7)
    plt.xlabel("Completion Time (seconds)")
    plt.ylabel("Frequency")
    plt.title("Pattern Completion Time Distribution")

    # Plot Collisions
    plt.subplot(2, 2, 2)
    plt.hist(df["collisions"], bins=6, color="red", alpha=0.7)
    plt.xlabel("Collisions")
    plt.ylabel("Frequency")
    plt.title("Number of Collisions")

    # Plot Successful Shots
    plt.subplot(2, 2, 3)
    plt.hist(df["successful_shots"], bins=4, color="green", alpha=0.7)
    plt.xlabel("Successful Shots")
    plt.ylabel("Frequency")
    plt.title("Correct Gate Key Shots")

    # Plot Crystals Collected
    plt.subplot(2, 2, 4)
    plt.hist(df["crystals_collected"], bins=10, color="orange", alpha=0.7)
    plt.xlabel("Crystals Collected")
    plt.ylabel("Frequency")
    plt.title("Crystals Collected by Players")

    plt.tight_layout()
    plt.show()

if __name__ == "__main__":
    df = fetch_data()
    print(df.head())  # Display first few rows
    visualize_data(df)
