import requests
import pandas as pd
import matplotlib.pyplot as plt
from io import StringIO

# 你的 Google Sheets CSV 链接
sheet_url = "https://docs.google.com/spreadsheets/d/1lBKyh2hF4yTiWB3mJKe_25lsSgrpPnK5eVrXXvDRnyU/gviz/tq?tqx=out:csv&gid=0"

try:
    # 发送请求
    response = requests.get(sheet_url)
    response.raise_for_status()  # 确保请求成功
    
    # 解析 CSV 数据
    csv_data = StringIO(response.text)
    df = pd.read_csv(csv_data)

    # print(df.head())  # 打印前几行

except requests.exceptions.RequestException as e:
    print("请求失败:", e)

column_name = "Game_Over_Reason"
data = df[column_name].value_counts()
total_count = data.sum()

plt.figure(figsize=(6, 6))
print(data.index)
plt.pie(data, labels=data.index, autopct='%1.1f%%', startangle=140, colors=['#ff9999','#66b3ff','#99ff99','#ffcc99'])
plt.title(f"{column_name} Distribution (Total: {total_count})", fontsize=14)
plt.show()