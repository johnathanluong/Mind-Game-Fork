INCLUDE ../globals.ink

{ Jester == 0: ->Intro}
{Jester ==1: ->Seq}
{Jester ==2: ->Seq2}
{Jester ==3: ->FinalF}

VAR k= 8


===Intro===
As you can tell, your mindscape is rather empty #speaker:JJ #portrait: Jester_neutral #layout: Default #audio: JesterAudio
But thats exactly what I want!
Just want to check one thing though...
Do you remember your name?
+[yes]
...#speaker: ??? #portrait: MC_neutral #audio: default
oh really? #speaker:JJ #portrait: Jester_neutral #layout: Default #audio: JesterAudio
then please tell me...
...#speaker: ??? #portrait: MC_neutral #audio: default
Exactly#speaker:JJ #portrait: Jester_neutral #layout: Default #audio: JesterAudio
->Intro2
+[no]
?#speaker: ??? #portrait: MC_neutral #audio: default
<i>Perfect~</i> #speaker:JJ #portrait: Jester_neutral #layout: Default #audio: JesterAudio
->Intro2

===Seq===
{Y ==0: ->Normal |->World2}
===Normal===
Do you have any questions? #speaker:JJ #portrait: Jester_neutral #layout: Default #audio: JesterAudio
+[Menu? Controls?]
->MenuQuestion
+[Nightmares? Memories?]
->Nightmares
+[No]
Ok.#speaker:JJ #portrait: Jester_neutral #layout: Default #audio: JesterAudio
->END

===Seq2===
{Y==2: ->Final |->Normal2}
===Normal2===
What are you standing there for?#speaker:JJ #portrait: Jester_neutral #layout: Default #audio: JesterAudio
I suggest you get moving
->END

===Final===
Oh Congratultions, you've regained the color in your eyes! #speaker:JJ #portrait: Jester_neutral #layout: Default #audio: JesterAudio
Now you look just like your true self!
Those souless eyes suited you pretty well though...
Anyways!
Congratulations, I honestly didn't think you had it in you! #speaker:JJ #portrait: Jester_neutral #layout: Default #audio: JesterAudio
I have opened the final door for you
This leads to the final game you must play
I shall be waiting for you~
~Jester=3
->END
===World2===
Welcome back~ #speaker:JJ #portrait: Jester_neutral #layout: Default #audio: JesterAudio
Thank you for being <i>oh so</i> entertaining #speaker:JJ #portrait: Jester_neutral #layout: Default #audio: JesterAudio
You and that Gary guy are quite the pair
Anyways...Have you noticed your new look?
It seems you are coming back together
You look more like your true self now...
Moving on though~
I have unlocked the next world for you, this one is a little <i>different</i>
Feel free to continue on, or return to Garys mind to train or find memories
If you decide to continue though, <i>good luck</i>
<u>you will need it~</u>
~Jester=2
->END
===Intro2===
Can't remember a thing can you?
I have stripped you of everything that made you <i>you</i>
Your name, your appearance, your <i>memories</i>...
If you wish to get them back and return to the world of the living
You must explore the minds of two of your... let's say <i>friends</i>
I was going to do three but...
nah, im rather lazy, and two should be plenty for you~
I have scattered your memories throughout your friends minds
you must go and find them to regain yourself
Your journey will not be that simple though~
You will also have to defeat your friends nightmare... you will see what I mean
I have opened the first door on the left for you, so enter if you dare
and please, make sure not to be boring...
...
Oh, one more thing.
If you have any questions about how to access the <b>menu</b> or anything else I just said
then you should probably speak to me again.
~Jester=1
->END

===MenuQuestion===
These worlds can be dangerous, so you will have to learn to fight #speaker:JJ #portrait: Jester_neutral #layout: Default #audio: JesterAudio
If you want to get stronger, I suggest you learn the menu
To Access it, press <b>[TAB]</b> and to leave it press <b>[ESC]</b>
+[learn more about the menu]
->MenuQuestion2
+[You're Good]
->Normal

===MenuQuestion2===
Through the menu you can view your stats, heal with items, and equip weapons and armor #speaker:JJ #portrait: Jester_neutral #layout: Default #audio: JesterAudio
There is also an option to <b>save</b>, which I suggest you do now
You can also view the controls, since I know you can be rather... <i>forgetful~</i>
->Normal

===Nightmares===
Which would you like to learn more about?
+[Nightmares]
->Nightmares2
+[Memories]
->Memories
+[Nah jk]
->Normal

===Nightmares2===
I said you would find out but...#speaker:JJ #portrait: Jester_neutral #layout: Default #audio: JesterAudio
sigh, OK!
Your friends... have some <i>issues</i>
With some help from yours truly~ these issues have manifested as a powerful enemy
A <i>nightmare</i>
In order to continue with my game, you must help your friend and defeat this nightmare~
...
Yawn, next question
->Nightmares

===Memories===
You currently have the brain of a dumb potato#speaker:JJ #portrait: Jester_neutral #layout: Default #audio: JesterAudio
So, your moveset is...rather limited
And I'm sure you want to learn more about yourself
If you find anything in the world that looks out of place, it is probably a memory
Press space on it and it shall lead you to a 'scene'
Then you will gain a move (which you currently lack) or an item
Trust me when I say this
<b>find them all</b>
->Nightmares

===FinalF===
First off, I see you're looking like yourself now
Though those souless eyes suited you rather well...
You can still go through the worlds, but If you feel like 'yourself'. Then leave through the final portal. #speaker:JJ #portrait: Jester_neutral #layout: Default #audio: JesterAudio
Out of the 8 memories, you currenty have {memories}
{memories <8 : There are still have {k-memories} more. So if you wish to go back and entertain me, be my guest~ | ->FINALFF}
->END

===FINALFF===
You have found all your memories
I don't believe there is anything else to find.
<i>probably</i>
But if you wish to leave, the door is open.
It will just be you, and <i>Me~</i>
I suggest, you give your final words.
->END
