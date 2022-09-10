> Please see the Demo Scenes in the Package. Here's an In-Depth Guide

**How To Use:**

> To Get Started, You just need to make 2 Configuration Files.

> Right-Click anywhere in your Project Window and go to Create/Combo System/

> Now Create one 'Tweening Configuration' and one 'Level Configuration' file. 
> Now, Add the Reference of the level config in the Tweening Configs 'Levels' list.

> For Tweening Configuration: If you want to use just the ComboCounter, just adjust those properties.
> Similarly, if you want to use both the ComboCounter and TextPopups, adjust both properties.

> For Level Configuration: Set the LevelBoundary to 0, Fonts of your choice. 
> And a list of Line-Separated Words in a .txt file if you want Text Popups. 

> **In your Unity Scene, you need 3 things:**

> A way to trigger the Counter Increment/Text Popup/Both: 
> This totally Depends on your Project and how You want to use it.
> I've used Simple Buttons that Trigger them in the Demo Scenes.
> Call Increment() from ComboCounter and PopUpWord(int level) from Text Popup

> The ComboCounter/TextPopup/Both Components on a GameObject: 
> You will need to set the References here like GameObjects and Tweening Configuration. 
> If you want to use Both Components, just put the Reference of Text Popup in the ComboCounter Property for it.

> GameObjects containing the 'TextMeshPro - Text (UI)' Components:
> One for ComboCounter and one for TextPopup

**And You are Basically SetUp**

If you need help, just send an email at: razz2k01@gmail.com
I'll try my best to help you ASAP.

THANK YOU! <3