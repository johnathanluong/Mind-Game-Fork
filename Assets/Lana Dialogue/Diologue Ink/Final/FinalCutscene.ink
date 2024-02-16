INCLUDE ../globals.ink
~tem=0
//~Fin=1
//~Fin=2
{Fin==0:->WorstEnding }
{Fin==1:->GoodEnding }
{Fin==2: ->BadEnding}
===GoodEnding===
//black screen
~tem=0
No! #speaker: Jester #portrait: Jester_angry #layout: Cutscene #audio: JesterAudio
I...
I didn't think you would actually do it
...
Fine, a deals a deal
You <i>were</i> entertaining, after all
So much so that if you were to die again, I'd happily offer for you to play this game again!
Oh! Maybe I could send you into Olivia's world...
...
Nevermind, mayhaps another time
Goodbye, <i>for now</i>
<i>Theo</i>
~Name="Theo"
!!!#speaker:Hero #portrait: MC_cutsceneF #audio: default #layout: Cutscene
<u>*Theo has regained all his memories*<u> #speaker: ??? #portrait: default #layout: Default #audio: default
... #speaker:Hero #portrait: MC_cutsceneF #audio: default #layout: Cutscene
..
.
#layout: Fade
~tem=1
...
...#speaker:Theo #portrait: MC_cutsceneF #audio: default #layout: Cutscene
...
! #speaker:Theo #portrait: MC_cutsceneF #audio: default #layout: Cutscene
! #speaker:Lily #audio: LAudio #layout: Cutscene #portrait: L_IRL
! #speaker: Gary #portrait: Gary_IRL #audio: GaryAudio #layout: Cutscene
BEST BUD!!!!!
... #speaker:Lily #audio: LAudio #layout: Cutscene #portrait: L_IRL
Hey... #speaker:Theo #portrait: MC_cutsceneF #audio: default #layout: Cutscene
Theo! I'm so happy you're awake! You won't believe this but I had the craziest dream! #speaker: Gary #portrait: Gary_IRL #audio: GaryAudio #layout: Cutscene
Oh, was it of you actually beating MachoMan2? #speaker:Theo #portrait: MC_cutsceneF #audio: default #layout: Cutscene
HA! Better!  #speaker: Gary #portrait: Gary_IRL #audio: GaryAudio #layout: Cutscene
I looked like Gary from pocketmonz!
We beat up an annoying puzzle piece and a dragon together and...
I'm just so happy my best friend is alive!
Yeah, what a strange dream... #speaker:Theo #portrait: MC_cutsceneF #audio: default #layout: Cutscene
... #speaker:Lily #audio: LAudio #layout: Cutscene #portrait: L_IRL
!#speaker:Theo #portrait: MC_cutsceneF #audio: default #layout: Cutscene
Gary, could I have a second with Lily?
Oh, so you <i>do</i> know her? #speaker: Gary #portrait: Gary_IRL #audio: GaryAudio #layout: Cutscene
I thought she was just a weird stalker-
I WOULD NEVER STALK HIM! #speaker:Lily #audio: LAudio #layout: Cutscene #portrait: L_IRL
!!! Sheesh ok !! Fine !! #speaker: Gary #portrait: Gary_IRL #audio: GaryAudio #layout: Cutscene
<i>Smell ya later</i> Theo, I'm gonna go spread the good news!
//prob him running animation and sound effect
...So you're alive... #speaker:Lily #audio: LAudio #layout: Cutscene #portrait: L_IRL
I had a dream too, you know...
It was similar to Gary's. He loves to talk a lot
Yeah. Listen Lily I- #speaker:Theo #portrait: MC_cutsceneF #audio: default #layout: Cutscene
I'M SORRY THEO! #speaker:Lily #audio: LAudio #layout: Cutscene #portrait: L_IRL
! #speaker:Theo #portrait: MC_cutsceneF #audio: default #layout: Cutscene
This was all my fault... #speaker:Lily #audio: LAudio #layout: Cutscene #portrait: L_IRL
I'm sorry, really
So bully me or... I don't know, do what you usually do!
I can take it!
I deserve it!
...Lily... #speaker:Theo #portrait: MC_cutsceneF #audio: default #layout: Cutscene
I'm the one who should be saying sorry... 
W-what? #speaker:Lily #audio: LAudio #layout: Cutscene #portrait: L_IRL
Please don't joke with me I...
No Lily, I'm serious. #speaker:Theo #portrait: MC_cutsceneF #audio: default #layout: Cutscene
I was terrible to you
Listen, my family...
I... I've been in a bad place for awhile
Hell, I probably still am
And maybe I was jealous, or maybe I wanted someone to hurt like I was hurting
But whatever it may be, I should <i>never</i> have taken it out on you
You didn't deserve that.
I'm sorry.
... #speaker:Lily #audio: LAudio #layout: Cutscene #portrait: L_IRL
God...why...
Why am I crying?
I- #speaker:Theo #portrait: MC_cutsceneF #audio: default #layout: Cutscene
...Lily? 
...I think I'm going to go #speaker:Lily #audio: LAudio #layout: Cutscene #portrait: L_IRL
Thank you, for what you said
But I think we should go our seperate ways, for now at least
I believe you can be better, but I don't think I can be a part of it
...ok?
I understand, I promise #speaker:Theo #portrait: MC_cutsceneF #audio: default #layout: Cutscene
Take care of yourself Theo #speaker:Lily #audio: LAudio #layout: Cutscene #portrait: L_IRL
Bye.
//runs off
...#speaker:Theo #portrait: MC_cutsceneF #audio: default #layout: Cutscene
'Was that a dream?'
'...If it wasn't a dream... that means...'
Theo! #speaker: Gary #portrait: Gary_IRL #audio: GaryAudio #layout: Cutscene
?! #speaker:Theo #portrait: MC_cutsceneF #audio: default #layout: Cutscene
ergdserafgdfrsef THEO THEO THEO! YOU CAN BE DISCHARGED TONIGHT!! #speaker: Gary #portrait: Gary_IRL #audio: GaryAudio #layout: Cutscene
LET'S PLAY MACHOMAN2, BUT BLINDFOLDED!
PLEASE!
...fine. But if I beat it you have to dye your hair green #speaker:Theo #portrait: MC_cutsceneF #audio: default #layout: Cutscene
Deal! But for now, let me catch you up on what you missed! #speaker: Gary #portrait: Gary_IRL #audio: GaryAudio #layout: Cutscene
First...
...
..
.
#layout: FadeOut
...
~tem=0
Many more adventures await Theo, but that is a story for another day #speaker: Jester #portrait: Jester_angry #layout: Cutscene #audio: JesterAudio
For now, you have unlocked your true name and set yourself back on the right path
There will always be more secrets to find though
so we hope you will consider playing again
but most of all...
<i>thanks for playing, from all of us <3</i>
~Ending=1
->END

===BadEnding===
//Black Screen
~tem=0
You have defeated me, congratulations! #speaker: Jester #portrait: Jester_angry #layout: Cutscene #audio: JesterAudio
However, you only found {memories} memories
You did not hold up your full end of the deal
...
Luckily for you, I am a generous man
You tried, and I can appreciate that
Have a good life, ****
... #speaker:Hero #portrait: MC_cutsceneF #audio: default #layout: Cutscene
..
.
//now new picture 
#layout: Fade
~tem=1
...
...#speaker:??? #portrait: MC_cutsceneF #audio: default #layout: Cutscene
...
!#speaker:??? #portrait: MC_cutsceneF #audio: default #layout: Cutscene
Hello? #speaker: Gary #portrait: Gary_IRL #audio: GaryAudio #layout: Cutscene
HELLO!!
LILY COME OVER HERE, I CAN'T BELIEVE IT! 
! #speaker:Lily #audio: LAudio #layout: Cutscene #portrait: L_IRL
H-hey. //Lily
???#speaker:??? #portrait: MC_cutsceneF #audio: default #layout: Cutscene
A-are you ok? How do you feel? I can't believe you got hit by a <i>car</i> dude! #speaker: Gary #portrait: Gary_IRL #audio: GaryAudio #layout: Cutscene
...#speaker:??? #portrait: MC_cutsceneF #audio: default #layout: Cutscene
Who...
+[speak]
Who are you? #speaker:??? #portrait: MC_cutsceneF #audio: default #layout: Cutscene
Best bud? #speaker: Gary #portrait: Gary_IRL #audio: GaryAudio #layout: Cutscene
It's me, Gary! 
Your cool, amazing, awesome and handsome best buddy!
... #speaker:??? #portrait: MC_cutsceneF #audio: default #layout: Cutscene
... #speaker: Gary #portrait: Gary_IRL #audio: GaryAudio #layout: Cutscene
Do you really not remember me?
No...#speaker:??? #portrait: MC_cutsceneF #audio: default #layout: Cutscene
! #speaker: Gary #portrait: Gary_IRL #audio: GaryAudio #layout: Cutscene
! #speaker:Lily #audio: LAudio #layout: Cutscene #portrait: L_IRL
Do you know...anything?
No... what... what am I doing here? What even is my name? #speaker:??? #portrait: MC_cutsceneF #audio: default #layout: Cutscene
! #speaker:Lily #audio: LAudio #layout: Cutscene #portrait: L_IRL
! #speaker: Gary #portrait: Gary_IRL #audio: GaryAudio #layout: Cutscene
I...I need to go #speaker: Gary #portrait: Gary_CutsceneRun #audio: GaryAudio #layout: Cutscene
Gary wait! #speaker:Lily #audio: LAudio #layout: Cutscene #portrait: L_IRL
I-
...
You really don't remember who you are? How you got here?
No, why am I in a hospital bed? #speaker:??? #portrait: MC_cutsceneF #audio: default #layout: Cutscene
Did I really get hit by a car?
...
Who am I?
... #speaker:Lily #audio: LAudio #layout: Cutscene #portrait: L_IRL
Please! #speaker:??? #portrait: MC_cutsceneF #audio: default #layout: Cutscene
... #speaker:Lily #audio: LAudio #layout: Cutscene #portrait: L_IRL
You have a second chance //Lily
Be better this time
Wait! #speaker:??? #portrait: MC_cutsceneF #audio: default #layout: Cutscene
W-what are you talking about?
... #speaker:Lily #audio: LAudio #layout: Cutscene #portrait: L_IRL
Please, just leave me alone. <b>Never Speak to me again</b>
<b>Understood?</b>
Yeah! #speaker:??? #portrait: MC_cutsceneF #audio: default #layout: Cutscene
Yeah
Understood
... #speaker:Lily #audio: LAudio #layout: Cutscene #portrait: L_IRL
Good. Goodbye...
//L leaves
I- #speaker:??? #portrait: MC_cutsceneF #audio: default #layout: Cutscene
..
'What happened? Who am I? Why am I here? What did I do to that girl?' #speaker:??? #portrait: MC_cutsceneF #audio: default #layout: Cutscene
'No one is telling me anything...'
'I'm scared...'
'...'
'I just want to remember...'
#layout: FadeOut
...
~tem=0
Perhaps, next time, you'll find the rest?  #speaker: Jester #portrait: Jester_angry #layout: Cutscene #audio: JesterAudio
Then, maybe you will remember
~Ending=2
->END

==WorstEnding===
//Black Screen
~tem=0
Congratulations! You bested the worlds! #speaker: Jester #portrait: Jester_angry #layout: Cutscene #audio: JesterAudio
However... #speaker: Jester #portrait: Jester_angry #layout: Cutscene #audio: JesterAudio
I said for you to collect <b>ALL</b> your memories... #speaker: Jester #portrait: Jester_angry #layout: Cutscene #audio: JesterAudio
You only collected the core ones... you are still missing things... #speaker: Jester #portrait: Jester_angry #layout: Cutscene #audio: JesterAudio
Ha! You don't even remember your own name... #speaker: Jester #portrait: Jester_angry #layout: Cutscene #audio: JesterAudio
I may be a generous man #speaker: Jester #portrait: Jester_angry #layout: Cutscene #audio: JesterAudio
But I'm not <i> too </i> generous #speaker: Jester #portrait: Jester_angry #layout: Cutscene #audio: JesterAudio
... #speaker:Hero #portrait: MC_cutsceneF #audio: default #layout: Cutscene
..
.
//now new picture 
#layout: Fade
~tem=1
...
...#speaker:???  #portrait: default #layout: Default #audio: default
...
//MC appears in hospital room but unresponsive
Oh, Lily? You're here? #speaker: Gary #portrait: Gary_IRL #audio: GaryAudio #layout: Cutscene
You know him? #speaker: Gary #portrait: Gary_IRL #audio: GaryAudio #layout: Cutscene
I do...but... #speaker:Lily #audio: LAudio #layout: Cutscene #portrait: L_IRL
it's complicated.
Doesn't matter the reason, thanks for coming! I bet he would appreciate it #speaker: Gary #portrait: Gary_IRL #audio: GaryAudio #layout: Cutscene
Yeah... no problem #speaker:Lily #audio: LAudio #layout: Cutscene #portrait: L_IRL
...
Do you think he'll wake up?
The doctors don't think so... #speaker: Gary #portrait: Gary_IRL #audio: GaryAudio #layout: Cutscene
Oh... #speaker:Lily #audio: LAudio #layout: Cutscene #portrait: L_IRL
Yeah. He's my only friend you know? #speaker: Gary #portrait: Gary_IRL #audio: GaryAudio #layout: Cutscene
Oh, I didn't #speaker:Lily #audio: LAudio #layout: Cutscene #portrait: L_IRL
You don't know what he did to me, do you
No? I'm surprised you even knew him. He's never talked to me about you before #speaker: Gary #portrait: Gary_IRL #audio: GaryAudio #layout: Cutscene
Oh! Well don't worry about it #speaker:Lily #audio: LAudio #layout: Cutscene #portrait: L_IRL
I... just wanted to see him one more time
I... had to make sure
Make sure? #speaker: Gary #portrait: Gary_IRL #audio: GaryAudio #layout: Cutscene
Yeah...#speaker:Lily #audio: LAudio #layout: Cutscene #portrait: L_IRL
I...need to go
Oh! Well bye... #speaker: Gary #portrait: Gary_IRL #audio: GaryAudio #layout: Cutscene
...
...Dude
Please, wake up
I need you man...
Please, ****
//fades to black
#layout: FadeOut
...
~tem=0
If you had a second chance, would you make an effort to find the memories? #speaker: Jester #portrait: Jester_angry #layout: Cutscene #audio: JesterAudio
I hope you would
After all, that was the deal.
...
Goodbye, Hero.
~Ending=3 //3 as want to do if ending doesnt equal 0
->END