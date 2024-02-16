INCLUDE ../globals.ink

{Jester==3: ->Normal}
{ Jester == 0: ->Intro | ->Normal } //intro

===Intro===
Welcome to your Mindscape! Press enter#speaker: ???  #portrait: Jester_neutral #layout: Default #audio: JesterAudio
Good, you can still follow instructions
I'm JJ, and I thank you for not being boring. #speaker: JJ
Feel free to explore the area with the <b>arrow keys</b> or <b>WASD</b>
though I don't believe you will find anything interesting here at the moment
If you wish to explore faster, press the <b>left [SHIFT] </b> while moving
Once you're bored of seeing nothing, sit on the seat in front of me and press <b>Space</b>~
->END

===Normal===
Welcome back~ #speaker: JJ  #portrait: Jester_neutral #layout: Default #audio: JesterAudio
{Y==1 && Sequence==0: ->BeforeWorld2 |->Normal2} //if 2 havent gone in yet the before world 2 else if they come back go to next divert
->END

===Normal2===
{Y==2 && Jester==2: ->Final |->END} //if they come back and Y==2 then goes to Final else just says welcome back. added Jester==2

===Final===
Congratulations on completing the worlds #speaker: JJ  #portrait: Jester_neutral #layout: Default #audio: JesterAudio
You defeated the nightmares, and made it here in one piece
Sit and talk to me again 
I shall give you a few words before you go
->END
===BeforeWorld2===
Seems you have gotten through the first world#speaker: JJ  #portrait: Jester_neutral #layout: Default #audio: JesterAudio
<i>Impressive!</i>
Sit and talk to me
I have a little message before I can allow you onto the next challenge
->END