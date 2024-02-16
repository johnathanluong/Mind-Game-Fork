INCLUDE ../../globals.ink

{Smile==1: ->choose}
~memories++
{ rock =="": ->choice1 | ->choice2 }

===choice1===
Never seen this picture frame here before...#speaker: L #portrait: L_Cutscene1  #audio: LAudio #layout: Cutscene
Or...oh
Guess you want to know what this is too, don't you?
+[Not really]
... #speaker:Hero #portrait: MC_neutral_cutscene #audio: default #layout: Cutscene
O-oh I'm sorry... #speaker: L #portrait: L_Cutscene1  #audio: LAudio #layout: Cutscene
I'll keep this brief then...
This is a picture... of a family friend
Well, <i>was</i> really.
He was so nice, but then he
And then...
...
Nevermind! I bet you don't want to hear me ramble so lets go...
<b\>!</b> #speaker:Hero #portrait: MC_neutral_cutscene #audio: default #layout: Cutscene
<u>*Hero has unlocked the memory Smile. you unlocked a new move*<u> #speaker: ??? #portrait: default #layout: Default
~Smile=1
->END
+[Please]
^-^ #speaker:Hero #portrait: MC_neutral_cutscene #audio: default #layout: Cutscene
Alright... this is the picture of a guy I used to know #speaker: L #portrait: L_Cutscene1  #audio: LAudio #layout: Cutscene
I was a pretty nervous kid
always so untrusting of everything
So when my parents introduced me to their friends kid
I was scared, seeing someone new
I hid behind a tree of all things
But that kid, biggest smile ever, made me smile too just looking at him
He had the brightest smile...
He just wanted to play and be my friend
I...
I miss those days
<b\>!</b> #speaker:Hero #portrait: MC_neutral_cutscene #audio: default #layout: Cutscene
<u>*Hero has unlocked the memory Smile. you unlocked a new move*<u> #speaker: ??? #portrait: default #layout: Default
~Smile=1
->END

===choice2===
Guess this is one of your memories then, huh?#speaker: L #portrait: L_Cutscene2  #audio: LAudio #layout: Cutscene
+[answer]
 .-#speaker:Hero #portrait: MC_neutral_cutscene #audio: default #layout: Cutscene
<b>don't</b> bother#speaker: L #portrait: L_Cutscene2  #audio: LAudio #layout: Cutscene
! #speaker:Hero #portrait: MC_cutscene
->main
+[don't]
->main


===main===
This is a picture from when we first met, back when you used to actually smile #speaker: L #portrait: L_Cutscene2  #audio: LAudio #layout: Cutscene
You were so nice to me then...
I was so nervous when our parents first introduced us
But your smile was so contagious
It's how we became friends
We were such good friends, I <i>trusted</i> you
...
But you changed
Day after day you just got worse
Even though I <i>never</i> did anything in return
What happened to you?
What happened to that smile?
<b\>!</b> #speaker:Hero #portrait: MC_neutral_cutscene #audio: default #layout: Cutscene
<u>*Hero has unlocked the memory Smile. you unlocked a new move*<u> #speaker: ??? #portrait: default #layout: Default
~Smile=1
->END

===choose===
which version would you like too see?
+[L nice]
->choice1
+[L angry]
->choice2