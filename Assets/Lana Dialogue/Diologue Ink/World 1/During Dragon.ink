INCLUDE ../globals.ink

->AfterFirstFaze

===AfterFirstFaze===
('~') #speaker: Hero #portrait: MC_neutral #audio: default
Thee shouldst've known, thee cannot defeat me high-lone, young H'ro.#speaker:The Dragon #portrait: Dragon_neutral #audio: DragonAudio
?#speaker: Hero #portrait: MC_neutral #audio: default
+[Walk back to Gary]
->AfterFirstFaze2

===AfterFirstFaze2===
I TOLD YOU! I AM <b>NOT</b> FIGHTING THAT THING!#speaker: Gary #portrait: Gary_neutral  #audio: GaryAudio
I'm tired.
He's too hard.
Let's go back to town!
I'm sure they're still celebrating Tutorial Guy's defeat!
+[Reach out to Gary for help]
...#speaker: Hero #portrait: MC_neutral #audio: default
...#speaker: Gary #portrait: Gary_neutral  #audio: GaryAudio
Why are you looking me like that?
Why do you insist on fighting this guy?
You know that we can die right?
Isn't it better to just do what we've been doing?
We can turn back and fight the little bunnies, floating cats, and weird rocks.
So why...
WHY DO YOU WANT TO DO SOMETHING SO HARD!
WHY DO YOU ALWAYS TRY EVEN IF YOU CAN FAIL!
I'M NOT GOING TO FAIL HERO, NOT EVER!
I'M NOT A FAILURE!
Young sir, through mine own mythic eyes... I sense thou art in needeth of therapy. #speaker:The Dragon #portrait: Dragon_neutral #audio: DragonAudio 
->AfterFirstFaze3

===AfterFirstFaze3===
+[Slowly nod in agreement]
\-_- #speaker: Hero #portrait: MC_neutral #audio: default
Stop that. #speaker: Gary #portrait: Gary_neutral  #audio: GaryAudio
Please stop looking at me like that.
...
I wanted to be a stronger person.
I wanted to be the type of person nobody could look down on.
I wanted to be like you.
...#speaker: Hero #portrait: MC_neutral #audio: default
...#speaker:The Dragon #portrait: Dragon_neutral #audio: DragonAudio 
...#speaker: Hero #portrait: MC_neutral #audio: default
N-no way...#speaker: Gary #portrait: Gary_neutral  #audio: GaryAudio
You both can't look at me like that.
AGHHHH... FINE!
LET'S GET THIS OVER WITH!
//~Dragon=2
->END
