#Created by MediaPipe
#Modified by Augmented Startups 2021
#Pose-Estimation in 5 Minutes
#Watch 5 Minute Tutorial at www.augmentedstartups.info/YouTube
import cv2
import mediapipe as mp
import time
import socket
import random
import json
from time import sleep

import os
mp_drawing = mp.solutions.drawing_utils
mp_pose = mp.solutions.pose
mp_holistic = mp.solutions.holistic

def sendDataToUnity(data):
    s = socket.socket()
    s.connect(("127.0.0.1", 1755))
    s.send((

                   str(data).encode("UTF-8")))
    s.close()

# For static images:
with mp_pose.Pose(
    static_image_mode=True,
    model_complexity=2,
    min_detection_confidence=0.5) as pose:
    image = cv2.imread('4.jpg')  #Insert your Image Here
    image_height, image_width, _  = image.shape
    # Convert the BGR image to RGB before processing.
    results = pose.process(cv2.cvtColor(image, cv2.COLOR_BGR2RGB))
    # Draw pose landmarks on the image.
    annotated_image = image.copy()
    mp_drawing.draw_landmarks(annotated_image, results.pose_landmarks, mp_pose.POSE_CONNECTIONS)
    cv2.imwrite(r'4.png', annotated_image)

# For webcam input:
cap = cv2.VideoCapture(0)
#cap = cv2.VideoCapture(2)
#For Video input:
prevTime = 0
steps = 0
leftLegIsBackLeg = True
with mp_pose.Pose(
    min_detection_confidence=0.5,
    min_tracking_confidence=0.5) as pose:
  while cap.isOpened():
    success, image = cap.read()
    if not success:
      print("Ignoring empty camera frame.")
      # If loading a video, use 'break' instead of 'continue'.
      continue

    # Convert the BGR image to RGB.
    image = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)
    # To improve performance, optionally mark the image as not writeable to
    # pass by reference.
    image.flags.writeable = False
    results = pose.process(image)


    #send json
    landmarks = results.pose_landmarks.landmark
    data = "["
    for index in range(len(landmarks)):
        currentLandmark = landmarks[index]
        data += "{\"x\":" + str(currentLandmark.x) + ", " + "\"y\":" + str(currentLandmark.y) + ", " + "\"z\":" + str(currentLandmark.z) + ", " + "\"visibility\":" + str(currentLandmark.x) + "}"
        if (index < len(landmarks) -1):
            data += ","
    data += "]"
    #data = [
    #        {
    #            'x': leftLeg.x,
    #            'y': leftLeg.y,
    #            'z': leftLeg.z,
    #            'visibility': leftLeg.visibility
    #        },
    #        {
    #            'x': rightLeg.x,
    #            'y': rightLeg.y,
    #            'z': rightLeg.z,
    #            'visibility': rightLeg.visibility
    #        }
    #    ]
    #

    json_object = json.loads(data)
    sendDataToUnity(json_object)


    # Draw the pose annotation on the image.
    image.flags.writeable = True
    image = cv2.cvtColor(image, cv2.COLOR_RGB2BGR)


    mp_drawing.draw_landmarks(
        image, results.pose_landmarks, mp_pose.POSE_CONNECTIONS)
    currTime = time.time()
    fps = 1 / (currTime - prevTime)
    prevTime = currTime
    cv2.putText(image, f'FPS: {int(fps)}', (20, 70), cv2.FONT_HERSHEY_PLAIN, 3, (0, 196, 255), 2)
    cv2.imshow('BlazePose', image)
    if cv2.waitKey(5) & 0xFF == 27:
      break
cap.release()


# Learn more AI in Computer Vision by Enrolling in our AI_CV Nano Degree:
# https://bit.ly/AugmentedAICVPRO