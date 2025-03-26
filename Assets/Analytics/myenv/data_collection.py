import json
import time
import random

# Simulated Data Collection
def collect_test_data():
    return {
        "player_id": random.randint(1, 100),
        "pattern_id": random.randint(1, 10),
        "completion_time": round(random.uniform(5.0, 20.0), 2),  # Time to complete a pattern
        "collisions": random.randint(0, 5),  # Number of times the player hits obstacles
        "successful_shots": random.randint(0, 3),  # Correct shots at gate keys
        "crystals_collected": random.randint(0, 10),  # Number of crystals collected
        "timestamp": time.time()
    }

if __name__ == "__main__":
    test_data = collect_test_data()
    with open("collected_data.json", "w") as file:
        json.dump(test_data, file, indent=4)
    print("Test data collected and saved to `collected_data.json`")
