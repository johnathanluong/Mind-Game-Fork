INCLUDE ../../globals.ink

It is you, {Name}
->Main


===Main===
Want to keep looking at yourself?
+[no]
->END
+[ABSOLUTELY]
you continue to look at yourself.
->Loop

===Loop===
... #speaker: Hero #portrait: MC_neutral #audio: default
..
.
Dude we should go...#speaker: Gary #portrait: Gary_neutral  #audio: GaryAudio
+[Keep looking at yourself]
.-. #speaker: Hero #portrait: MC_neutral #audio: default
->Loop
+[Leave]
->END
