#!/usr/bin/env python
# coding: utf-8

# In[1]:


# -*- coding: utf-8 -*-

PoemName = "杜甫《江畔獨步尋花·其六》"
PoemTest = """
黃四孃家花滿蹊，千朵萬朵壓枝低。
留連戲蝶時時舞，自在嬌鶯恰恰啼。
"""

print(PoemName)
print(PoemTest)

MottoEn = "A warm smile is the universal language of kindness."
MottoCh = "溫暖的微笑是表示善意的通用語言。"

print(MottoEn)
print(MottoCh)
print("AAA")

# -*- coding: utf-8 -*-
"""
Spyder Editor

This is a temporary script file.
"""

import cv2
import numpy as np

input_Path = "C:\\Users\\B20_PC3\\Desktop\\DAN\\Augmented-reality-in-Industrial-maintenance\\Assets\\Resources\\"
output_Path = "C:\\Users\\B20_PC3\\Desktop\\DAN\\Augmented-reality-in-Industrial-maintenance\\Assets\\Resources\\OutCamera.jpg"
src = cv2.imread(input_Path + "Camera.jpg", 1)
(h,w,c) = src.shape
RedPoint  = [] #放找到紅點的座標
print("AAA")

def filter_out_red(src_frame):
    if src_frame is not None:
        (h,w,c) = src_frame.shape
        hsv = cv2.cvtColor(src_frame, cv2.COLOR_BGR2HSV)
        lower_red = np.array([170, 150, 46])
        upper_red = np.array([180, 255, 255])
        # inRange()方法返回的矩阵只包含0,255 (CV_8U) 0表示不在区间内
        mask = cv2.inRange(hsv, lower_red, upper_red)  # 找符合顏色範圍的點  符合的點是255 不符合是0
        
        #底下把圖片平分成4等
        #找左上
        flag = 0  
        for i in range (int(h/2) - 1):
            for j in range (int(w/2) - 1):
                if mask[i][j] !=0:
                    print(i,j,mask[i][j])
                    RedPoint.append([i,j])
                    flag = 1
                    break
            if flag == 1:
                flag = 0
                break
        #找右上
        for i in range(int(h/2) - 1):
            for j in range (int(w/2) - 1, w):
                if mask[i][j] !=0:
                    print(i,j,mask[i][j])
                    RedPoint.append([i,j])
                    flag = 1
                    break
            if flag == 1:
                flag = 0
                break
        #找左下
        for i in range(int(h/2) - 1, h):
            for j in range (int(w/2) - 1):
                if mask[i][j] !=0:
                    print(i,j,mask[i][j])
                    RedPoint.append([i,j])
                    flag = 1
                    break
            if flag == 1:
                flag = 0
                break
        #找右下
        for i in range(int(h/2) - 1, h):
            for j in range (int(w/2) - 1, w):
                if mask[i][j] !=0:
                    print(i,j,mask[i][j])
                    RedPoint.append([i,j])
                    flag = 1
                    break
            if flag == 1:
                flag = 0
                break
        return cv2.bitwise_and(src_frame, src_frame, mask=mask)  #RETURN mask跟原圖相疊後得結果





Rframe = filter_out_red(src)
print(RedPoint)

if len(RedPoint) >= 3:
    srcPoint = np.float32([[0, 0],[0, 640],[480,0]])
    AffinePoint = np.float32([RedPoint[0], RedPoint[1], RedPoint[2]])
    
    
    M = cv2.getAffineTransform(AffinePoint ,srcPoint)
    Affinedst = cv2.warpAffine(src,M,(w, h))
    cv2.imwrite(output_Path, Affinedst)


cv2.imwrite(output_Path, src)





