using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 5/9/2012</para>
/// <para>Last modified: 05/01/2013 18:30</para> 
/// <para>Author: Marcos Zalacain </para>
/// StorySceneGUI:   
///    -This script is to launch the right Level from StoryScene.
/// </summary>
public class StorySceneGUI : MonoBehaviour {
	
	public GUIStyle labelStyle;
	public Font labelFontSML;
	public Font labelFontSMH;
	public Font labelFontN7;
	public Font labelFontN10;
	
	private int longitud1, longitud2 = 0;
	
	private float screenWidth, screenHeight;
	private float unitW, unitH;
	
	private string text1;
	private string text2;

	void Start () {
		// Size related.
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		unitW = screenWidth/20;
		unitH = screenHeight/20;
		
		// We set the background and styles acording to deviceType here.
		if (Globals.deviceType == Globals.SmartPhoneL) {
			labelStyle.font = labelFontSML;
			labelStyle.fixedHeight = screenHeight/2;
		}else if (Globals.deviceType == Globals.SmartPhoneH) {
			labelStyle.font = labelFontSMH;
			labelStyle.fixedHeight = screenHeight/4;
		}else if (Globals.deviceType == Globals.N7) {
			labelStyle.font = labelFontN7;
			labelStyle.fixedHeight = screenHeight/3;
		}else{
			labelStyle.font = labelFontN10;
			labelStyle.fixedHeight = screenHeight/3;
		}
		
		text1 = setText1(Globals.levelToLaunch);
		text2 = setText2(Globals.levelToLaunch);
	}
	
	// Actualiza la longitud del texto a mostrar y la pone al máximo al tocar pantalla.
	void Update ( ) {
		if ( longitud1 < text1.Length ) {
			longitud1++;
		}
		if ( longitud2 < text2.Length && longitud1 >= text1.Length) {
			longitud2++;
		}
		
		if ( ( Input.touchCount > 0 ) || Input.GetKeyDown ( KeyCode.Space ) ) {
			longitud1 = text1.Length;
			longitud2 = text2.Length;
		}
		
		if (longitud2 >= text2.Length) {
			StartCoroutine(launchLevelJustInCase(4.0f));
		}
	}
	
	void OnGUI(){
		
		// LINE 1
		GUI.Label(new Rect(1.5f*unitW, unitH, 17*unitW, 2*unitH), text1.Substring ( 0 , longitud1 ), labelStyle );
		// LINE 2
		GUI.Label(new Rect(1.5f*unitW, 15.5f*unitH, 17*unitW, 2*unitH), text2.Substring ( 0 , longitud2 ), labelStyle );
		
	}
	
	// Launch level through animation.
	void launchLevel(){
		
		Application.LoadLevel("Level" + Globals.levelToLaunch);
	}
	
	// In case the animation does NOT launch level.
	IEnumerator launchLevelJustInCase(float waitingTime){
		
		yield return new WaitForSeconds(waitingTime);
		
		Application.LoadLevel("Level" + Globals.levelToLaunch);
	}
	
	// Setting the string 1 for each level scene.
	string setText1(int txt1){
		 
		string st = "";
		
		if(Globals.choosenLanguage == Globals.English){
			
			switch (txt1) {
				case 1:
                    st = " An evil uprising spreads through the south; and you, the toughest archer in the kingdom, have set to help Elric fighting it.";
					break;
				case 10:
                    st = " Elric believes the uprising is coming from Red Mountain; go with Elric to Wizard Hills to find out who is behind this terror.";
					break;
				case 19:
                    st = " Elric warned: we must be ready for the worst. Head to Willows Lake for more powerful magic mushrooms.";
					break;
				case 28:
                    st = " Go deep into Willows Land to find out what’s going on, rumours say there is an evil witch involved.";
					break;
				case 37:
                    st = " Sutherland has been destroyed. You should head down to seek for survivors. And why not,";
					break;
				case 46:
                    st = " It’s been confirmed, evil witch Maeva is behind all this mess. Since it is magic what we need to fight to...";
					break;
				case 55:
                    st = " Maeva’s evil army is heading towards Great Lake, run before they massacre that land too. Elric will be waiting,";
					break;
				case 64:
                    st = " Their army is weakening by the moment and they will soon be retreating to Red Mountain. Fight them before they get to their fortress.";
					break;
				case 73:
                    st = " Maeva’s fortress won’t be easy, but you must end her now or she could return in the future. This may be the final battle, so...";
					break;		
			}
		}else{					// Spanish
			switch (txt1) {
				case 1:
                    st = " Una invasión se expande por las tierras del sur; tú, la mejor arquera del reino, te diriges a ayudar a Elric para impedirla.";
					break;
				case 10:
                    st = " Elric cree que la invasión malvada sale desde Montaña Roja; acércate con Elric a su comarca para descubrir que produce este terror.";
					break;
				case 19:
                    st = " Elric dice que debemos estar prevenidos para lo peor. Acércate al lago de los sauces para hacerte con setas más potentes.";
					break;
				case 28:
                    st = " Adéntrate por la tierra de los sauces para ver si te enteras de que está ocurriendo, hay rumores de que se trata de una bruja malvada.";
					break;
				case 37:
                    st = " Las provincias del sur han sido destruidas. Dirígete a ellas para ver si encuentras algún superviviente. Y por qué no,";
					break;
				case 46:
                    st = " Elric lo ha confirmado, la bruja Maeva es la responsable de todo esto. Ya que es magia lo que habrá que luchar…";
					break;
				case 55:
                    st = " El ejército de Maeva se dirige a Lago Grande. Corre antes de que masacren también esa tierra. Elric te esperará,";
					break;
				case 64:
                    st = " Su ejército se está debilitando y pronto tendrán que retirarse a  Montaña Roja. Pelea contra ellos antes de que se pongan a salvo.";
					break;
				case 73:
                    st = " La fortaleza de Maeva no va a ser nada fácil, pero debes acabar con todos o algún día podrían reagruparse.";
					break;		
			}
		}
		
		return st;
	}
	
	// Setting the string 2 for each level scene.
	string setText2(int txt2){
		 
		string st = "";
		
		if(Globals.choosenLanguage == Globals.English){
			
			switch (txt2) {
				case 1:
                    st = "Don't forget to pick up some mushrooms for the wizard along the way!";
					break;
				case 10:
                    st = "Watch out for this ‘spiky’ land, and get more mushrooms for Elric.";
					break;
				case 19:
                    st = "But be aware, Elric believes there may be more of those evil soldiers.";
					break;
				case 28:
                    st = "And get more of those gorgeous mushrooms for Elric.";
					break;
				case 37:
                    st = "get some southern mushrooms while you’re there.";
					break;
				case 46:
                    st = "go to the Magic Woodlands for the best magic mushrooms.";
					break;
				case 55:
                    st = "as you know, he loves the mushrooms of that lake.";
					break;
				case 64:
                    st = "Yes, you’re right. Dark Water’s mushrooms have magic too.";
					break;
				case 73:
                    st = "Elric is going to need to power up with mushrooms.";
					break;		
			}
		}else{					// Spanish
			switch (txt2) {
				case 1:
                    st = "Y no te olvides de recoger algunas setas para el mago.";
					break;
				case 10:
                    st = "Cuidado con los pinchos de esta región, y recoge algunas setas para Elric.";
					break;
				case 19:
                    st = "Pero cuidado, Elric cree que puede haber más soldados enemigos.";
					break;
				case 28:
                    st = "Y recoge más de esas setas para Elric.";
					break;
				case 37:
                    st = "consigue alguna de esas setas del sur.";
					break;
				case 46:
                    st = "Vete a los bosques encantados a por las setas más mágicas del reino.";
					break;
				case 55:
                    st = "como ya sabes,  le encantan las setas del lago.";
					break;
				case 64:
                    st = "Sí, tienes razón; las setas de Aguas Oscuras también son mágicas.";
					break;
				case 73:
                    st = "Elric va a necesitar todas las setas que puedas conseguir.";
					break;		
			}
		}
		
		return st;
	}
}
