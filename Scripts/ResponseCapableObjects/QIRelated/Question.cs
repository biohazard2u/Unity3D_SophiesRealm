using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 19/10/2012</para>
/// <para>Last modified: 19/10/2012 18:30</para> 
/// <para>Author: Marcos Zalacain </para>
/// Question:   
///    -This script is to store all questions and answers for each scroll. It goes as a scroll component.
///    -The info containing this component will be collected by the question GUI prefab.
/// </summary>
public class Question : MonoBehaviour {

	// Questions.
	public int questionNumber;
	public Texture questionBackground;
	
	public string theQuestionEngL1;
	public string theQuestionEngL2;
	public string theQuestionSpaL1;
	public string theQuestionSpaL2;
	public string[] theAnswersEng;
	public string[] theAnswersSpa;
	
	public int rightAnswer;
}
