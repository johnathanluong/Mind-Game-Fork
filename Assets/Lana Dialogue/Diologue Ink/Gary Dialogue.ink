INCLUDE globals.ink

{Jester >=2: ->Final}
{CoreMemory1 ==1: ->Toilet}
{Dragon >=2: ->Cartridge}
{People >=1: ->DefeatTutorial}
{TownScene==1:->BeforeTutorial}
//this is default
If you get tired we can go to my house to heal up! #speaker: Gary #portrait: Gary_neutral  #audio: GaryAudio
Or if you're feeling more adventurous, let's go to town!
Just follow the road up and we can heal up and get items!
->END
===BeforeTutorial===
Lets stock up. I have a feeling that puzzle is going to be really annoying #speaker: Gary #portrait: Gary_neutral  #audio: GaryAudio
I bet you want some C4's, you never stopped drinking those!
->END
===DefeatTutorial===
Let's explore the town and hang out! #speaker: Gary #portrait: Gary_neutral  #audio: GaryAudio
<b>DO NOT</b> leave town and head towards the western mountains
There is a dragon... and we are to cool for dragons!
->END
===Cartridge===
That cartridge...reminds me of something #speaker: Gary #portrait: Gary_neutral  #audio: GaryAudio
->END
===Toilet===
Good luck in the other worlds! But... why is it a toilet? #speaker: Gary #portrait: Gary_neutral  #audio: GaryAudio
->END
===Final===
Welcome back dude! Lets grind a bit! #speaker: Gary #portrait: Gary_neutral  #audio: GaryAudio
->END