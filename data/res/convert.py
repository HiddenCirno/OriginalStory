import sys
import os
from PIL import Image

def convert_jfif_to_png(input_path):
    # 检查文件是否存在
    if not os.path.exists(input_path):
        print(f"错误: 找不到文件 '{input_path}'")
        sys.exit(1)

    # 检查文件扩展名是否为 .jfif (忽略大小写)
    if not input_path.lower().endswith('.jfif'):
        print(f"警告: '{input_path}' 的扩展名不是 .jfif，但将尝试转换。")

    try:
        # 打开图片
        with Image.open(input_path) as img:
            # 转换为 RGB 模式 (以防原图是 CMYK 等模式，PNG 最好用 RGB 或 RGBA)
            img = img.convert('RGB')
            
            # 生成输出图片的文件名 (替换扩展名为 .png)
            base_name = os.path.splitext(input_path)[0]
            output_path = f"{base_name}.png"
            
            # 保存为 PNG 格式
            img.save(output_path, 'PNG')
            print(f"成功！图片已转换为: {output_path}")
            
    except Exception as e:
        print(f"转换失败: {e}")

if __name__ == "__main__":
    # 检查是否传入了参数
    if len(sys.argv) < 2:
        print("用法: python jfif_to_png.py <jfif文件路径>")
        sys.exit(1)
        
    input_file = sys.argv[1]
    convert_jfif_to_png(input_file)