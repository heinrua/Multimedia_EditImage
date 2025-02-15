import cv2
from skimage.metrics import structural_similarity as ssim
import sys
from PIL import Image
import numpy as np

def ssim(image1, image2, C1=6.5025, C2=58.5225):
   # Tính toán các đặc trưng cơ bản
    mu1 = np.mean(image1)
    mu2 = np.mean(image2)
    
    sigma1_sq = np.var(image1)
    sigma2_sq = np.var(image2)
    sigma12 = np.cov(image1.flatten(), image2.flatten())[0][1]

    # Tính toán các thành phần SSIM
    luminance = (2 * mu1 * mu2 + C1) / (mu1**2 + mu2**2 + C1)
    contrast = (2 * np.sqrt(sigma1_sq) * np.sqrt(sigma2_sq) + C2) / (sigma1_sq + sigma2_sq + C2)
    structure = (sigma12 + C2 / 2) / (np.sqrt(sigma1_sq) * np.sqrt(sigma2_sq) + C2 / 2)
    
    # Tính toán SSIM
    ssim_index = luminance * contrast * structure
    return ssim_index

def compare_images(image1_path, image2_path):
    image1 = cv2.imread(image1_path, cv2.IMREAD_GRAYSCALE)
    image2 = cv2.imread(image2_path, cv2.IMREAD_GRAYSCALE)

    edges1 = cv2.Canny(image1, 100, 200)
    edges2 = cv2.Canny(image2, 100, 200)

    edges2 = cv2.resize(edges2, (edges1.shape[1], edges1.shape[0]))

    score = ssim(edges1, edges2)
    return score

if __name__ == "__main__":
    image1_path = sys.argv[1]
    image2_path = sys.argv[2]
    similarity_score = compare_images(image1_path, image2_path)
    print(similarity_score)


