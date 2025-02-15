import cv2
import numpy as np
import os
import sys

def remove_background_with_edges_fixed(image_path):
    # Đọc ảnh
    img = cv2.imread(image_path)
    original = img.copy()

    # Chuyển sang Grayscale và phát hiện biên
    gray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
    edges = cv2.Canny(gray, 50, 150)

    # Nối liền các biên bằng Morphological Closing
    kernel = cv2.getStructuringElement(cv2.MORPH_RECT, (15, 15))
    edges_closed = cv2.morphologyEx(edges, cv2.MORPH_CLOSE, kernel)

    # Tìm contour từ biên
    contours, _ = cv2.findContours(edges_closed, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)

    # Tạo mask
    mask = np.zeros_like(edges)
    if contours:
        # Chọn contour lớn nhất
        max_contour = max(contours, key=cv2.contourArea)
        cv2.drawContours(mask, [max_contour], -1, 255, thickness=cv2.FILLED)

        # Tạo bounding box từ contour lớn nhất
        x, y, w, h = cv2.boundingRect(max_contour)
        cropped_img = original[y:y+h, x:x+w]  # Cắt ảnh gốc
    else:
        cropped_img = original

    # Tạo ảnh kết quả với alpha channel
    b, g, r = cv2.split(cropped_img)
    alpha = mask[y:y+h, x:x+w] if contours else np.ones_like(gray) * 255
    result = cv2.merge((b, g, r, alpha))

    return result

def main_fixed():
    if len(sys.argv) < 3:
        return

    image_path = sys.argv[1]
    output_path = sys.argv[2]

    # Kiểm tra định dạng file đầu ra
    if not output_path.lower().endswith(".png"):
        output_path = os.path.splitext(output_path)[0] + ".png"

    # Gọi hàm xử lý ảnh và lưu kết quả vào numpy array
    result = remove_background_with_edges_fixed(image_path)
    
    # Đảm bảo result là numpy array trước khi lưu
    if isinstance(result, np.ndarray):
        cv2.imwrite(output_path, result)
    else:
        print("Error: The output result is not a valid numpy array.")

if __name__ == "__main__":
    main_fixed()
