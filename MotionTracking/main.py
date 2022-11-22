# Created by MediaPipe
# Modified by Augmented Startups 2021
# Pose-Estimation in 5 Minutes
# Watch 5 Minute Tutorial at www.augmentedstartups.info/YouTube
import cv2
import mediapipe as mp
import time
import socket
import random
import json
from time import sleep

from VideoGet import VideoGet

mp_drawing = mp.solutions.drawing_utils
mp_pose = mp.solutions.pose


def sendDataToUnity(data):
    s = socket.socket()
    s.connect(("127.0.0.1", 1755))
    s.send((

        str(data).encode("UTF-8")))
    s.close()


# For Video input:
steps = 0
leftLegIsBackLeg = True

mp_drawing = mp.solutions.drawing_utils
mp_pose = mp.solutions.pose

cap = VideoGet().start()
# For Video input:
prevTime = 0
with mp_pose.Pose(
        static_image_mode=False,
        model_complexity=0,
        smooth_landmarks=True,
        smooth_segmentation=True,
        min_detection_confidence=0.3,
        min_tracking_confidence=0.3) as pose:
    while cap.stream.isOpened():
        # success,image = cap.read()
        success = cap.grabbed
        image = cap.frame
        if not success:
            print("Ignoring empty camera frame.")
            # If loading a video, use 'break' instead of 'continue'.
            continue

        # Convert the BGR image to RGB.
        # To improve performance, optionally mark the image as not writeable to
        # pass by reference.
        image.flags.writeable = False
        results = pose.process(cv2.cvtColor(image, cv2.COLOR_RGB2BGR))

        # Draw the pose annotation on the image.
        mp_drawing.draw_landmarks(
            image, results.pose_landmarks, mp_pose.POSE_CONNECTIONS)
        currTime = time.time()
        fps = 1 / (currTime - prevTime)
        prevTime = currTime
        cv2.putText(image, f'FPS: {int(fps)}', (20, 70), cv2.FONT_HERSHEY_PLAIN, 3, (0, 196, 255), 2)
        cv2.imshow('BlazePose', image)
        if cv2.waitKey(5) & 0xFF == 27:
            break
    try:
        landmarks = results.pose_landmarks.landmark
    except:
        pass
    data = "["
    for index in range(len(landmarks)):
        currentLandmark = landmarks[index]
        data += "{\"x\":" + str(currentLandmark.x) + ", " + "\"y\":" + str(currentLandmark.y) + ", " + "\"z\":" + str(
            currentLandmark.z) + ", " + "\"visibility\":" + str(currentLandmark.x) + "}"
        if (index < len(landmarks) - 1):
            data += ","
    data += "]"
    # data = [
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
# send json
cap.stop()

# Learn more AI in Computer Vision by Enrolling in our AI_CV Nano Degree:
# https://bit.ly/AugmentedAICVPRO
