// Attach this to any object to make a frames/second indicator.
//
// It calculates frames/second over each updateInterval,
// so the display does not keep changing wildly.
//
// It is also fairly accurate at very low FPS counts (<10).
// We do this not by simply counting frames per interval, but
// by accumulating FPS for each frame. This way we end up with
// corstartRect overall FPS even if the interval renders something like
// 5.5 frames.
 
var startRect : Rect = Rect( 10, 10, 75, 50 ); // The rect the window is initially displayed at.
var updateColor : boolean = true; // Do you want the color to change if the FPS gets low
var allowDrag : boolean = true; // Do you want to allow the dragging of the FPS window
var frequency : float = 0.5; // The update frequency of the fps
var nbDecimal : int = 1; // How many decimal do you want to display
 
private var accum : float = 0; // FPS accumulated over the interval
private var frames : int = 0; // Frames drawn over the interval
private var color : Color = Color.white; // The color of the GUI, depending of the FPS ( R < 10, Y < 30, G >= 30 )
private var sFPS : String= ""; // The fps formatted into a string.
private var style : GUIStyle; // The style the text will be displayed at, based en defaultSkin.label.
 
function Start()
{
	accum = 0f;
	var frame = 1;
 
    // Infinite loop executed every "frenquency" secondes.
    while( Application.isPlaying )
    {
        // Update the FPS
        var fps : float = accum / (frames > 0 ? frame : 1);
        sFPS = fps.ToString( "f" + Mathf.Clamp( nbDecimal, 0, 10 ) );
 
        //Update the color
        color = (fps >= 30) ? Color.green : ((fps > 10) ? Color.red : Color.yellow);
 
        accum = 0;
        frames = 0;
 
        yield WaitForSeconds( frequency );
    }
}
 
function Update()
{
    accum += Time.timeScale/ Time.deltaTime;
    ++frames;
}
 
function OnGUI()
{
    // Copy the default label skin, change the color and the alignement
    if( style == null ){
        style = GUIStyle( GUI.skin.label );
        style.normal.textColor = Color.white;
        style.alignment = TextAnchor.MiddleCenter;
    }
 
    GUI.color = updateColor ? color : Color.white;
    startRect = GUI.Window(0, startRect, DoMyWindow, "");
}
 
function DoMyWindow( windowID : int )
{
    GUI.Label( Rect(0, 0, startRect.width, startRect.height), sFPS + " FPS", style );
    if( allowDrag ) GUI.DragWindow(Rect(0, 0, Screen.width, Screen.height));
}