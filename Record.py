def StartRecording():
    from pynput import keyboard
    import time
    import pyaudio
    import wave
    import sched
    import sys
    import DialogFlow
    import test

    global started, p, stream, frames

    CHUNK = 1024
    FORMAT = pyaudio.paInt16
    CHANNELS = 1
    RATE = 16000
    RECORD_SECONDS = 5
    WAVE_OUTPUT_FILENAME = "input.wav"

    p = pyaudio.PyAudio()
    frames = []

    def callback(in_data, frame_count, time_info, status):
        frames.append(in_data)
        return (in_data, pyaudio.paContinue)

    class MyListener(keyboard.Listener):
        def __init__(self):
            super(MyListener, self).__init__(self.on_press, self.on_release)
            self.key_pressed = None
            self.wf = wave.open(WAVE_OUTPUT_FILENAME, 'wb')
            self.wf.setnchannels(CHANNELS)
            self.wf.setsampwidth(p.get_sample_size(FORMAT))
            self.wf.setframerate(RATE)
        def on_press(self, key):
            if key.char == 'p':
                self.key_pressed = True
            return True

        def on_release(self, key):
            if key.char == 'p':
                self.key_pressed = False
            return True


    listener = MyListener()
    listener.start()
    started = False
    stream = None

    def recorder():
        global started, p, stream, frames

        if listener.key_pressed and not started:
            # Start the recording
            try:
                stream = p.open(format=FORMAT,
                                 channels=CHANNELS,
                                 rate=RATE,
                                 input=True,
                                 frames_per_buffer=CHUNK,
                                 stream_callback = callback)
                #print("Stream active:", stream.is_active())
                started = True
                #print("start Stream")
            except:
                raise

        elif not listener.key_pressed and started:
            #print("Stop recording")
            stream.stop_stream()
            stream.close()
            p.terminate()
            listener.wf.writeframes(b''.join(frames))
            listener.wf.close()
            #print("You should have a wav file in the current directory")
            project_id = 'newagent-ckdyhs'
            session_id = 'ya29.ImCzBwiG7tSKc1fySc1ry6JlnewaOQ6EA_RtWNBZe34vSuTpFhPQFa3AF98fZfjMmKrs3xrMXeIPlovyg7UnF_v63jgGq0fIUSI1QMndumjX9doDXsIfiCeDubxWtPEpJUg'
            audio_file_path = 'input.wav'
            language_code = 'sk'
            #Record.Record()
            #DialogFlow.detect_intent_audio(project_id, session_id, audio_file_path, language_code)
            test.detect_intent_stream(project_id, session_id, audio_file_path, language_code)
            sys.exit()
        # Reschedule the recorder function in 100 ms.
        task.enter(0.1, 1, recorder, ())


    #print("Press and hold the 'p' key to begin recording")
    #print("Release the 'p' key to end recording")
    task = sched.scheduler(time.time, time.sleep)
    task.enter(0.1, 1, recorder, ())
    task.run()
