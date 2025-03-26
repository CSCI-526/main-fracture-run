import firebase_admin
from firebase_admin import credentials, firestore
import json

cred = credentials.Certificate("firebase-credentials.json")
firebase_admin.initialize_app(cred)

db = firestore.client()
print("Firebase initialized successfully!")

def store_data():
    with open("collected_data.json", "r") as file:
        data = json.load(file)

    doc_ref = db.collection("playtest_data").add(data)
    print(f"Data stored in Firebase with ID: {doc_ref[1].id}")

if __name__ == "__main__":
    store_data()
