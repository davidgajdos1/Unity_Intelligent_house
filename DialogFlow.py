def detect_intent_audio(project_id, session_id, audio_file_path,
                        language_code):
    """Returns the result of detect intent with an audio file as input.

    Using the same `session_id` between requests allows continuation
    of the conversation."""
    import dialogflow_v2 as dialogflow
    import  simpleaudio as sa
    import os

    os.environ["GOOGLE_APPLICATION_CREDENTIALS"] = 'D:\\Downloads\\newagent-ckdyhs-6fd93af863f1.json'
    session_client = dialogflow.SessionsClient()

    # Note: hard coding audio_encoding and sample_rate_hertz for simplicity.
    audio_encoding = dialogflow.enums.AudioEncoding.AUDIO_ENCODING_LINEAR_16
    sample_rate_hertz = 16000

    session = session_client.session_path(project_id, session_id)
    #print('Session path: {}\n'.format(session))

    with open(audio_file_path, 'rb') as audio_file:
        input_audio = audio_file.read()

    audio_config = dialogflow.types.InputAudioConfig(
        audio_encoding=audio_encoding, language_code=language_code,
        sample_rate_hertz=sample_rate_hertz)

    query_input = dialogflow.types.QueryInput(audio_config=audio_config)

    output_audio_config = dialogflow.types.OutputAudioConfig(
            audio_encoding=dialogflow.enums.OutputAudioEncoding
            .OUTPUT_AUDIO_ENCODING_LINEAR_16)

    response = session_client.detect_intent(
        session=session, query_input=query_input,
        input_audio=input_audio, output_audio_config=output_audio_config)

    #print('=' * 20)
    #print('Query text: {}'.format(response.query_result.query_text))
    #print('Detected intent: {} (confidence: {})\n'.format(response.query_result.intent.display_name,response.query_result.intent_detection_confidence))
    #print('Fulfillment text: {}\n'.format(response.query_result.fulfillment_text))
        # The response's audio_content is binary.
    with open('output.wav', 'wb') as out:
        out.write(response.output_audio)
        #print('Audio content written to file "output.wav"')

    filename = 'output.wav'
    wave_obj = sa.WaveObject.from_wave_file(filename)
    play_obj = wave_obj.play()
    play_obj.wait_done()  # Wait until sound has finished playing
    #print(response.query_result.parameters)
