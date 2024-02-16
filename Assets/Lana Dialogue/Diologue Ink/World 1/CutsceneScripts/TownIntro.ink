INCLUDE ../../globals.ink
~TownScene=1
OMG our hero! #speaker:TownPerson #audio: TownAudio1 #portrait: default
We have been looking for you~
<i>Wait...</i>
<i>Who's that nobody? A pack mule?</i>
Hm-#speaker: Gary #portrait: Gary_cutscene #audio: GaryAudio #layout: Cutscene
->main
===main===
+[Interupt Gary]
->MeanMain
+[Don't interupt Gary]
->NiceMain

===NiceMain===
<b>Hey<i>!</i></b> He's my <i>disciple</i>, not a damn donkey! #speaker: Gary #portrait: Gary_cutscene #audio: GaryAudio #layout: Cutscene
Don't look down on him. After all my training, the only person who could take him is me of course!
Ohhh, I see I see. You truly are a hero for taking him under your wing! #speaker:TownPerson #audio: TownAudio1 #layout: Default #portrait: default
Now that you've arrived, here's the scoop...
->main2
===MeanMain===
! #speaker:Hero #portrait: MC_cutscene #audio: default #layout: Cutscene
Hmm? #speaker:TownPerson #portrait: default #audio: TownAudio1 #layout: Default 
+[Cut to the chase]
.-. #speaker:Hero #portrait: MC_cutscene #audio: default #layout: Cutscene
Hey-#speaker: Gary #portrait: Gary_cutscene #audio: GaryAudio #layout: Cutscene
O-Oh, I'm sorry. Okay well, Tutorial Guy is waiting for you guys at the end of town.#speaker:TownPerson #audio: TownAudio1 #layout: Default #portrait: default
There is also an inn and a shop... red and blue respectively
T-That's all! I'm sorry...
Dude, what was that... you should've let them thank me first. #speaker: Gary #portrait: Gary_cutscene #audio: GaryAudio #layout: Cutscene
Well, whatever.#speaker: Gary #portrait: Gary_cutscene #audio: GaryAudio #layout: Cutscene
Let's defeat that guy once and for all!
->END
+[Where is Tutorial Guy?]
? #speaker:Hero #portrait: MC_cutscene #audio: default #layout: Cutscene 
Oh, he's up top. I'm guessing you're not on his good side either? #speaker:TownPerson #audio: TownAudio1 #layout: Default #portrait: default
<i>Guess we have two heroes then, don't we?</i>
<i>He won't have a good side after you two are done with him!</i>
No. #speaker: Gary #portrait: Gary_cutscene #audio: GaryAudio #layout: Cutscene
He is the <i>sidekick</i>. I am the <i>main character</i>.
U-Understandable, legendary hero! #speaker:TownPerson #audio: TownAudio1 #layout: Default #portrait: default
Well, about that monster... 
->main2

===main2===
Tutorial Guy is waiting for you at the end of the village and he's looking pissed.
He's been screeching about a rematch. "Best 2 out of 3", he says...
You're asking me to defeat him? #speaker: Gary #portrait: Gary_cutscene #audio: GaryAudio #layout: Cutscene
Yes! Please! He's been really annoying and he won't stop forcing us into his... <b>Ultimate Tutorial</b>... #speaker:TownPerson #audio: TownAudio1 #layout: Default #portrait: default
<i>I still don't even know how to play...</i>
Got it. Consider him gone. I <i>am</i> the strongest, after all! #speaker: Gary #portrait: Gary_cutscene #audio: GaryAudio #layout: Cutscene
Thank you! If you need anything, the red house can heal you and the blue house sells items.#speaker:TownPerson #audio: TownAudio1 #layout: Default #portrait: default
See, that's how you thank your savior! Now, let's prepare...#speaker: Gary #portrait: Gary_cutscene #audio: GaryAudio #layout: Cutscene
We must defeat that monster once and for all! 
->END