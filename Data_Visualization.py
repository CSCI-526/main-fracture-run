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

# column_name = "Game_Over_Reason"
# data = df[column_name].value_counts()
# total_count = data.sum()

# plt.figure(figsize=(6, 6))
# print(data.index)
# plt.pie(data, labels=data.index, autopct='%1.1f%%', startangle=140, colors=['#ff9999','#66b3ff','#99ff99','#ffcc99'])
# plt.title(f"{column_name} Distribution (Total: {total_count})", fontsize=14)
# plt.show()

scene_column = "Scene"
category_column = "Game_Over_Reason"

# 获取所有的场景
scenes = df[scene_column].unique()

# 创建画布，根据场景数量动态生成子图
fig, axes = plt.subplots(1, len(scenes), figsize=(6 * len(scenes), 6))

if len(scenes) == 1:  # 只有一个场景时，axes 不是列表
    axes = [axes]

# 遍历每个场景，绘制对应的饼状图
for i, scene in enumerate(scenes):
    scene_data = df[df[scene_column] == scene]  # 筛选出该场景的数据
    category_counts = scene_data[category_column].value_counts()  # 统计每个类别的数量

    # 绘制饼状图
    axes[i].pie(category_counts, labels=category_counts.index, autopct='%1.1f%%', startangle=140)
    axes[i].set_title(f"{scene} - {category_column} - total count = {category_counts.sum()}")

# 显示图表
plt.tight_layout()
plt.show()