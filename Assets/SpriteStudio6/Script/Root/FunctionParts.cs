/**
	SpriteStudio6 Player for Unity

	Copyright(C) Web Technology Corp. 
	All rights reserved.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Script_SpriteStudio6_Root
{
	/* ----------------------------------------------- Functions */
	#region Functions
	/* ******************************************************** */
	//! Get number of parts in animation
	/*!
	@param	
		(none)
	@retval	Return-Value
		Number of parts in animation
		-1 == Error / "Name" is not-found.

	Get number of parts in animation.<br>
	The range of Parts-ID is "0" to "ReturnValue-1".<br>
	*/
	public int CountGetParts()
	{
		if(null == DataAnimation)
		{
			return(-1);
		}

		return(DataAnimation.CountGetParts());
	}

	/* ******************************************************** */
	//! Get part's index(Parts-ID) from the part's-name
	/*!
	@param	Name
		Part's name
	@retval	Return-Value
		Parts-ID
		-1 == Error / "Name" is not-found.

	Get the part's-Index(Parts-ID) from the name.<br>
	The Index is the serial-number (0 origins) in the Animation-data.
	*/
	public int IDGetParts(string name)
	{
		if(null == DataAnimation)
		{
			return(-1);
		}

		return(DataAnimation.IndexGetParts(name));
	}

	/* ******************************************************** */
	//! Force-Hide Set
	/*!
	@param	idParts
		Parts-ID<br>
		0 == Hide the entire animation<br>
		-1 == Set Hide to all parts
	@param	flagSwitch
		true == Set Hide (Hide, force)<br>
		false == Hide Reset (Show. State of animation is followed.)
	@param	flagInvolveChildren
		true == Children are set same state.<br>
		false == only oneself.<br>
		default: false
	@retval	Return-Value
		true == Success<br>
		false == Failure (Error)

	State of "Hide" is set to parts, ignore with state of animation.<br>
	This setting is ignored when set to parts that does not have display capability such as "NULL"-parts.<br>
	Moreover, when set to "Mask"-parts, affect of mask can be erased.<br>
	This setting also affects the "Instance"-parts and "Effect"-parts, but not set to each (subordinate) animation objects.<br>
	<br>
	If set 0 or -1 to "idParts", hide entire animation.<br>
	However, behaviors differ clearly between 0 and -1.<br>
	Do not confuse both.<br>
	<br>
	idParts == 0:<br>
	Set hide state to whole animation.<br>
	(It is same behavior as checking or unchecking "Hide" on inspector)<br>
	Recommend that use this when normally set hide state of the whole animation.<br>
	This setting is separately from setting to each parts, and has priority.<br>
	<br>
	idParts == -1:<br>
	Set hide state to each all parts.<br>
	*/
	public bool HideSet(int idParts, bool flagSwitch, bool flagInvolveChildren=false)
	{
		if((null == DataAnimation) || (null == TableControlParts))
		{
			return(false);
		}

		int countParts = TableControlParts.Length;
		if(0 > idParts)
		{	/* All parts */
			for(int i=1; i<countParts; i++)
			{
				HideSetMain(i, flagSwitch, false);
			}
			return(true);
		}

		if(0 == idParts)
		{	/* "Root"-Parts */
			FlagHideForce = flagSwitch;
			return(true);
		}

		if(countParts <= idParts)
		{	/* Invalid ID */
			return(false);
		}

		HideSetMain(idParts, flagSwitch, flagInvolveChildren);

		return(true);
	}
	private void HideSetMain(int idParts, bool flagSwitch, bool flagInvolveChildren=false)
	{
		if(true == flagSwitch)
		{
			TableControlParts[idParts].Status |= Library_SpriteStudio6.Control.Animation.Parts.FlagBitStatus.HIDE_FORCE;
		}
		else
		{
			TableControlParts[idParts].Status &= ~Library_SpriteStudio6.Control.Animation.Parts.FlagBitStatus.HIDE_FORCE;
		}

		if(true == flagInvolveChildren)
		{
			int[] tableIDPartsChild = DataAnimation.TableParts[idParts].TableIDChild;
			int countPartsChild = tableIDPartsChild.Length;
			for(int i=0; i<countPartsChild; i++)
			{
				HideSetMain(tableIDPartsChild[i], flagSwitch, true);
			}
		}
	}

	/* ******************************************************** */
	//! Get part's ColorLabel-form
	/*!
	@param	idParts
		Parts-ID
	@retval	Return-Value
		ColorLabel's form<br>
		-1 == Error

	Get part's ColorLabel-form.<br>
	When "ColorLabel" is set to "Custom Color" on "SpriteStudio6",
	 this function returns "Library_SpriteStudio6.Data.Parts.Animation.ColorLabel.KindForm.CUSTOM".(Irrespective of the actual color)<br>
	*/
	public Library_SpriteStudio6.Data.Parts.Animation.ColorLabel.KindForm FormGetColorLabel(int idParts)
	{
		if(null == DataAnimation)
		{
			return((Library_SpriteStudio6.Data.Parts.Animation.ColorLabel.KindForm)(-1));
		}

		if((0 > idParts) || (DataAnimation.CountGetParts() <= idParts))
		{
			return((Library_SpriteStudio6.Data.Parts.Animation.ColorLabel.KindForm)(-1));
		}

		return(DataAnimation.TableParts[idParts].LabelColor.Form);
	}

	/* ******************************************************** */
	//! Get part's ColorLabel-color
	/*!
	@param	idParts
		Parts-ID
	@retval	Return-Value
		ColorLabel's actual color<br>
		"A/R/G/B all 0" == Error

	Regardless (Color-Label's) form, this function returns actual color of the color label.<br>
	Use to get color when form is "Custom Color".<br>
	*/
	public Color ColorGetColorLabel(int idParts)
	{
		if(null == DataAnimation)
		{
			return(Library_SpriteStudio6.Data.Parts.Animation.ColorLabel.TableDefault[(int)Library_SpriteStudio6.Data.Parts.Animation.ColorLabel.KindForm.NON].Color);
		}

		if((0 > idParts) || (DataAnimation.CountGetParts() <= idParts))
		{
			return(Library_SpriteStudio6.Data.Parts.Animation.ColorLabel.TableDefault[(int)Library_SpriteStudio6.Data.Parts.Animation.ColorLabel.KindForm.NON].Color);
		}

		return(DataAnimation.TableParts[idParts].LabelColor.Color);
	}
	#endregion Functions

	/* ----------------------------------------------- Classes, Structs & Interfaces */
	#region Classes, Structs & Interfaces
	public static partial class Parts
	{
		/* ----------------------------------------------- Functions */
		#region Functions
		/* ******************************************************** */
		//! Get Root-Parts
		/*!
		@param	gameObject
			GameObject of starting search
		@param	flagApplySelf
			true == Include "gameObject" as check target<br>
			false == exclude "gameObject"<br>
			default: true
		@retval	Return-Value
			Instance of "Script_SpriteStudio6_Root"<br>
			null == Not-Found / Failure	

		Get component "Script_SpriteStudio6_Root" by examining "gameObject" and direct-children.<br>
		<br>
		This function returns "Script_SpriteStudio6_Root" first found.<br>
		However, it is not necessarily in shallowest GameObject-hierarchy.<br>
		(Although guarantee up to direct-children, can not guarantee if farther than direct-children)<br>
		*/
		public static Script_SpriteStudio6_Root RootGet(GameObject gameObject, bool flagApplySelf=true)
		{
			Script_SpriteStudio6_Root scriptRoot = null;

			/* Check Origin */
			if(true == flagApplySelf)
			{
				scriptRoot = RootGetMain(gameObject);
				if(null != scriptRoot)
				{
					return(scriptRoot);
				}
			}

			/* Check Direct-Children */
			/* MEMO: Processing is wastefull, but check direct-children first so that make to find in closely-relation as much as possible. */
			int countChild = gameObject.transform.childCount;
			Transform transformChild = null;

			for(int i=0; i<countChild; i++)
			{
				transformChild = gameObject.transform.GetChild(i);
				scriptRoot = RootGetMain(transformChild.gameObject);
				if(null != scriptRoot)
				{
					return(scriptRoot);
				}
			}

			/* Check Children */
			for(int i=0; i<countChild; i++)
			{
				transformChild = gameObject.transform.GetChild(i);
				scriptRoot = RootGet(transformChild.gameObject, false);
				if(null != scriptRoot)
				{	/* Has Root-Parts */
					return(scriptRoot);
				}
			}

			return(null);
		}
		private static Script_SpriteStudio6_Root RootGetMain(GameObject gameObject)
		{
			Script_SpriteStudio6_Root scriptRoot = null;
			scriptRoot = gameObject.GetComponent<Script_SpriteStudio6_Root>();
			if(null != scriptRoot)
			{	/* Has Root-Parts */
				if(null == scriptRoot.InstanceRootParent)
				{	/* has no Parent */
					return(scriptRoot);
				}
			}

			return(null);
		}
		#endregion Functions
	}
	#endregion Classes, Structs & Interfaces
}
